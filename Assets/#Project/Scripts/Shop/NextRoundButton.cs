using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRoundButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void NextRoundButtonClicked()
    {
        GlobalManager.Instance.GetComponent<ChangeScene>().LoadSceneWithFixedTransition("MainScene");
        button.interactable = false;
    }

}
