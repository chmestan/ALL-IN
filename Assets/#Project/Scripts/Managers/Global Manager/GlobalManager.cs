using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    public WaveManager waveManager;
    public PlayerData playerData;

    private void Awake()
    {
        // Singleton design pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        playerData = GetComponent<PlayerData>();
        if (playerData == null)
        {
            Debug.LogError("(GlobalManager) PlayerData component not found on the same GameObject.");
        }
        waveManager = GetComponent<WaveManager>();
        if (waveManager == null)
        {
            Debug.LogError("(GlobalManager) WaveManager component not found on the same GameObject.");
        }
    }
}
