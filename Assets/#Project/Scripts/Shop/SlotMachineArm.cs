using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineArm : MonoBehaviour
{
    private Animator anim;
    private Button button;
    public SlotMachine slotMachine;
    [SerializeField] private bool debug = false;
    [SerializeField] private SlotManager slotManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void PullAnimation()
    {
        button.interactable = false;
        anim.SetTrigger("Pull");
        slotManager.TriggerFadeOut();
    }

    public void TriggerAnimSlots()
    {
        if (debug) Debug.Log("(SlotMachineArm) Rolling");
        slotMachine.TriggerRollAnimation();
    }

    public void SlotMachineHasRolled()
    {
        if (debug) Debug.Log("(SlotMachineArm) Roll is over, button is active again");
        button.interactable = true;
    }
}
