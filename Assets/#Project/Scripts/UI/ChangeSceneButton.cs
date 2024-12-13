using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    private ChangeScene changeScene;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
    }

    public void OnClick(string sceneName)
    {
        changeScene.LoadSceneWithFixedTransition(sceneName);
        button.interactable = false;
    }

}
