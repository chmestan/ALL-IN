using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadScene : MonoBehaviour
{
    public void LoadShopScene(string scene)
    {
        if (ChangeScene.Instance != null)
        {
            ChangeScene.Instance.LoadScene(scene);
        }
        else
        {
            Debug.LogError("ChangeScene instance not found!");
        }
    }
}
