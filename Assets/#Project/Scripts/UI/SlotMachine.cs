using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    private Animator anim;
    private Button button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void PullAnimation()
    {
        button.interactable = false;
        anim.SetTrigger("Pull");
        StartCoroutine(ReEnableButtonAfterAnimation());
    }

    private IEnumerator ReEnableButtonAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        button.interactable = true; 
    }
}
