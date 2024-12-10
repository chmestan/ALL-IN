using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public UnityEvent OnAnimEnded = new UnityEvent();
    [SerializeField] SlotMachineArm arm;
    [SerializeField] private bool debug = false;
    [SerializeField] SlotManager slotManager;

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
        OnAnimEnded.Invoke();
    }
}
