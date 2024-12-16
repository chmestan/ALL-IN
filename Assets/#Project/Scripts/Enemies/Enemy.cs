using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : EnemyDefaultStateLogic
{
    private EnemyStats stats;
    public EnemyStats Stats
    { 
        get => stats;
    }
    protected int currentHealth;
    
    #region NavMesh
        protected NavMeshAgent agent;
        public NavMeshAgent Agent
        { 
            get => agent;
        }
        #endregion
    public UnityEvent OnDeath = new UnityEvent();

    #region Sprite Renderer
        private Color originalColor;
        private SpriteRenderer spriteRenderer;
        private Coroutine flashCoroutine;
    #endregion
    
    [Header("Animator"), Space(3f)]
        private Animator anim;

    [Header("Particles"), Space(3f)]
        public ParticleSystem poofSpawn;
        public ParticleSystem poofDeath;
        public ParticleSystem attackWarning;

    [Header("Audio"), Space(3f)]
        public AudioManager audioManager;
        public List<AudioClip> enemyShootAudioClips;
        public float enemyShootVolModifierdB = 0f;
        [SerializeField] private AudioClip enemyHitAudioClip;
        [SerializeField] private float enemyHitVolModifierdB = 0f;
        [SerializeField] private AudioClip enemyDeathAudioClip;
        [SerializeField] private float enemyDeathVolModifierdB = 0f;


    [SerializeField] private bool debug = false;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GetScriptableObject();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) Debug.LogError($"(Enemy) No SpriteRenderer found on {gameObject.name}");
        else originalColor = spriteRenderer.color;

        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError($"(Enemy) No Animator found on {gameObject.name}");

        StateMachine = new EnemyStateMachine();
        SpawnState = new EnemySpawnState(this, StateMachine);
        RoamState = new EnemyRoamState(this, StateMachine);
        DeadState = new EnemyDeadState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }


    private void OnEnable()
    {
        currentHealth = stats.MaxHealth;
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;

        stats.AdjustStatsForWave();
        StateMachine.Initialize(SpawnState);    
    }

    private void OnDisable()
    {
        agent.isStopped = true; 
        agent.ResetPath();  
    }

    public virtual void Update()
    {
        StateMachine.Update();

        UpdateMovementAnimation();
        if (debug)
        { 
            Debug.Log($"(Enemy) Enemy Velocity: {agent.velocity}");
            Debug.Log($"(Enemy) Enemy Speed: {agent.speed}");
            Debug.Log($"(Enemy) MoveX: {anim.GetFloat("MoveX")}, MoveY: {anim.GetFloat("MoveY")}");
            Debug.Log($"(Enemy) LastMoveX: {anim.GetFloat("LastMoveX")}, LastMoveY: {anim.GetFloat("LastMoveY")}");
            Debug.Log($"(Enemy) MoveMagnitude: {anim.GetFloat("MoveMagnitude")}");
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("player")) 
        {
            PlayerHealth playerHealth = Player.Instance.GetComponent<PlayerHealth>(); 
            if (playerHealth != null && StateMachine.CurrentEnemyState != SpawnState && StateMachine.CurrentEnemyState != DeadState)
            {
                playerHealth.GetHit(stats.CollisionDamage);
            }
        }
    }

    private void UpdateMovementAnimation()
    {
        Vector2 currentVelocity = new Vector2(agent.velocity.x, agent.velocity.y);

        if (currentVelocity.magnitude > 0.1f) 
        {
            Vector2 movementDirection = currentVelocity.normalized;

            anim.SetFloat("MoveX", movementDirection.x);
            anim.SetFloat("MoveY", movementDirection.y);

            anim.SetFloat("LastMoveX", movementDirection.x);
            anim.SetFloat("LastMoveY", movementDirection.y);
        }
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }

        anim.SetFloat("MoveMagnitude", currentVelocity.magnitude);
    }

    #region GET HIT AND DIE METHODS
        public void GetHit(int damage)
        {
            if (StateMachine.CurrentEnemyState == SpawnState) return;
            currentHealth -= damage;
            // Debug.Log($"(Enemy) {gameObject.name} took {damage} damage! Current Health: {currentHealth}");

            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine); // only one flash active
                spriteRenderer.color = originalColor;
            }
            flashCoroutine = StartCoroutine(FlashOnHit());

            if (currentHealth <= 0)
            {
                Die();
            }
            else 
            audioManager.PlaySFX(enemyHitAudioClip, enemyHitVolModifierdB);
        }

        public void Die()
        {
            if (debug) Debug.Log($"(Enemy) {gameObject.name} died.");
            OnDeath.Invoke(); 
            audioManager.PlaySFX(enemyDeathAudioClip, enemyDeathVolModifierdB);

            StateMachine.ChangeState(DeadState);
            spriteRenderer.enabled = false;
            poofDeath.Play();

            StartCoroutine(DeactivateAfterPoof());
        }

        private IEnumerator DeactivateAfterPoof()
        {
            yield return new WaitForSeconds(.3f);
            gameObject.SetActive(false);
        }

        private IEnumerator FlashOnHit()
        {
            spriteRenderer.color = Color.red; 
            yield return new WaitForSeconds(0.05f); 
            spriteRenderer.color = originalColor; 
        }

    #endregion

    #region INIT METHODS
        private void GetScriptableObject()
        {
            string enemyName = gameObject.name.Replace("(Clone)", "").Trim();
            stats = Resources.Load<EnemyStats>($"ScriptableObjects/Enemy Types Stats/{enemyName}Stats");
            
            // Debug.Log($"(Enemy) Looking for EnemyStats: {enemyName}");

            if (stats == null)
            {
                Debug.LogError($"(Enemy) No EnemyStats found for {enemyName}. Ensure it exists in Resources/EnemyStats.");
            }
        }

        public virtual EnemyState GetStartingState()
        {
            return RoamState;

            // switch (stateEnum)
            // {
            //     case EnemyStateEnum.Spawn:
            //         return SpawnState;
            //     case EnemyStateEnum.Roam:
            //         return RoamState;
            //     case EnemyStateEnum.Retreat:
            //         return RetreatState;
            //     case EnemyStateEnum.Chase:
            //         return ChaseState;
            //     case EnemyStateEnum.Attack:
            //         return AttackState;
            //     default:
            //         Debug.LogError("Invalid state enum provided.");
            //         return RoamState; 
            // }
        }
    #endregion
    
    #region DEBUG
    //     private void OnDrawGizmos()
            // {
            //     Gizmos.color = Color.red;
            //     Gizmos.DrawWireSphere(transform.position, GetComponentInChildren<CircleCollider2D>().radius);
            // }
    #endregion
}

