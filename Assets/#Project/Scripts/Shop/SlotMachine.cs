using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public UnityEvent OnAnimEnded = new UnityEvent();
    [Header("References"), Space(3f)]
        [SerializeField] SlotMachineArm arm;
        [SerializeField] SlotManager slotManager;
    
    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] private AudioClip dingAudioClip;

    [Header("Debug"), Space(3f)]
        [SerializeField] private bool debug = false;

    private Animator anim;
    // private Button armButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        arm.slotMachine = this;
    }

    private void Start()
    {
        OnAnimEnded.AddListener(arm.SlotMachineHasRolled);
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    public void TriggerRollAnimation()
    {
        if (debug) Debug.Log("(SlotMachine) Rolling");
        anim.SetTrigger("Roll");
    }

    public void RollIsOver()
    {
        if (debug) Debug.Log("(SlotMachine) Roll is over");
        slotManager.UpdateSlots();

        StartCoroutine(PlaySFXWithDelay(.1f));

        MoneyTextUI moneyTextUI = FindObjectOfType<MoneyTextUI>();
        if (moneyTextUI != null)  moneyTextUI.UpdateMoneyDisplay();

        EnemyCountUI enemyCountUI = FindObjectOfType<EnemyCountUI>();
        if (enemyCountUI != null) enemyCountUI.UpdateEnemyCounts();

        OnAnimEnded.Invoke();
    }

    private IEnumerator PlaySFXWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioManager.PlaySFX(dingAudioClip);
    }

}
