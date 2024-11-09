using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadScene : MonoBehaviour
{
    public void LoadShopScene(string scene)
    {
        if (GlobalManager.Instance != null)
        {
            ChangeScene changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
            if (changeScene != null)
            {
                changeScene.LoadScene(scene);
            }
            else
            {
                Debug.LogError("ChangeScene component not found on GlobalManager!");
            }
        }
        else
        {
            Debug.LogError("GlobalManager instance not found!");
        }
    }
}
