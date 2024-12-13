using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Upgrade : MonoBehaviour
{
    public string upgradeName ;
    public int maxLevel;     
    public int baseCost;   
    public int increment = 50; 
    public string description;    
    public PlayerData playerData;
    public UpgradeData upgradeData;
    
    [Header ("Texts"), Space (3f)]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI priceText;

    [Header("Audio"),Space(3f)]
        public AudioClip purchasedAudioClip;
        public AudioClip cantBuyAudioClip;


    public int CurrentLevel
    {
        get
        {
            if (upgradeData != null && upgradeData.upgradeLevels.ContainsKey(upgradeName))
            {
                return upgradeData.upgradeLevels[upgradeName];
            }
            return 0; 
        }
    }

    protected virtual void Awake()
    {
        upgradeName = GetType().Name;        
    }

    protected virtual void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
        upgradeData = GlobalManager.Instance.GetComponent<UpgradeData>();
        upgradeData.InitializeUpgrades();

        UpdateTexts(); 
    }

    public int GetCurrentCost()
    {
        return baseCost + (CurrentLevel * increment);
    }


    public bool CanPurchase()
    {
        return CurrentLevel < maxLevel;
    }

    public virtual void Purchase()
    {
        upgradeData.IncrementUpgradeLevel(upgradeName);
        Debug.Log($"{upgradeName} at level {CurrentLevel}/{maxLevel}");
        ApplyEffect();
        UpdateTexts();
    }

    public abstract void ApplyEffect();

    private void UpdateTexts()
    {
        if (levelText != null) levelText.text = $"{CurrentLevel}";
        else Debug.LogWarning($"{upgradeName}: Level text is not assigned!");
        
        if (priceText != null) priceText.text = $"{GetCurrentCost()}";
        else Debug.LogWarning($"{upgradeName}: Price text is not assigned!");
    }    

}

