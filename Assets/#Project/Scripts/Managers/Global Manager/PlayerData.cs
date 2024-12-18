using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int startingGold = 0;
    private int startingDamage = 5;
    private int startingHealth = 30;
    private float startingRange = 7f;
    private float startingBulletSpeed = 17f;
    private float startingShootingFrequency = 0.7f;

    [Header("Player Stats"), Space (10f)]
    public int playerGold;
    public int playerDamage;
    public int playerMaxHealth;
    public float playerRange;
    public float playerBulletSpeed;
    public float playerShootingFrequency;

    public ArenaState arenaState;
    public ChangeScene changeScene;
    private UpgradeData upgradeData;

    public void Start()
    {
        ResetValues();
        changeScene = GetComponent<ChangeScene>();
        upgradeData = GetComponent<UpgradeData>();
    }

    public void ResetValues()
    {
        playerGold = startingGold;
        playerDamage = startingDamage;
        playerMaxHealth = startingHealth;
        playerRange = startingRange;
        playerBulletSpeed = startingBulletSpeed;
        playerShootingFrequency = startingShootingFrequency;
    }

    public void ResetGame()
    {
        ResetValues();
        upgradeData.ResetUpgradeLevels();
        GlobalManager.Instance.waveManager.WaveCount = 1;
        GlobalManager.Instance.waveManager.Prize = 100;
        GlobalManager.Instance.waveManager.FirstWave();
    }

}
