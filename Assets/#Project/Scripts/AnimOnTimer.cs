using System.Collections;
using UnityEngine;

public abstract class AnimOnTimer : MonoBehaviour
{
    protected Animator anim;

    [SerializeField] protected float initialDelay = 1f; 
    [SerializeField] protected float animInterval = 5f;
    [SerializeField] protected string triggerName; 

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null) Debug.Log($"(AnimOnTimer) No Animator found on {gameObject.name}");

        StartCoroutine(AnimRoutine());
    }

    protected IEnumerator AnimRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            anim.SetTrigger(triggerName);
            yield return new WaitForSeconds(animInterval);
        }
    }
}
