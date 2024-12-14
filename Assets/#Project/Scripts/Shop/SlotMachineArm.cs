using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineArm : MonoBehaviour
{
    private Animator anim;
    private Button button;
    public SlotMachine slotMachine;

    [Header ("References"), Space (3f)]
        [SerializeField] private SlotManager slotManager;
        [SerializeField] Button nextRoundButton;

    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] private AudioClip leverAudioClip;

    [Header ("Debug"), Space (3f)]
        [SerializeField] private bool debug = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }
    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    public void PullAnimation()
    {
        button.interactable = false;
        StartCoroutine(PlaySFXWithDelay(.2f));
        nextRoundButton.interactable = false;
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
        nextRoundButton.interactable = true;
    }

    private IEnumerator PlaySFXWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioManager.PlaySFX(leverAudioClip);
    }


}
