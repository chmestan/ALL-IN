using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;

    public void Start()
    {
        currentHealth = GlobalManager.Instance.playerData.PlayerMaxHealth;
    }

    public void GetHit()
    {

    }


}
