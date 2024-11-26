using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }
    public WaveManager _WaveConfig;
    public WaveManager WaveConfig
    { get => _WaveConfig; }

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

        _WaveConfig = GetComponent<WaveManager>();
        if (_WaveConfig == null)
        {
            Debug.LogError("(GlobalManager) WaveConfig component not found on the same GameObject.");
        }
    }
}
