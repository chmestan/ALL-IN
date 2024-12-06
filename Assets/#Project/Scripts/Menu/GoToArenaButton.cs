using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToArenaButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float delay;
    private ChangeScene changeScene;

    private void Start()
    {
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
    }

    public void OnClick()
    {
        changeScene.LoadSceneWithDelay(sceneName, delay);
    }

}
