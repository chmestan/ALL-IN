using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    private ChangeScene changeScene;
    private void Start()
    {
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
    }

    public void OnClick(string sceneName)
    {
        changeScene.LoadSceneWithFixedTransition(sceneName);
    }

}
