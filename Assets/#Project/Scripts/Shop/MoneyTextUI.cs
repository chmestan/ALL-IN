using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFundsText; 
    [SerializeField] private TextMeshProUGUI nextPayoutText; 
    private PlayerData playerData;
    private WaveManager waveManager;

    private void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
        waveManager = GlobalManager.Instance.waveManager;

        UpdateMoneyDisplay(); 
    }

    public void UpdateMoneyDisplay()
    {
        if (currentFundsText != null)
        {
            currentFundsText.text = $"{playerData.playerGold}"; 
        }
        else Debug.LogWarning("(MoneyDisplay) Current funds text object ???");

        if (nextPayoutText != null)
        {
            nextPayoutText.text = $"{waveManager.Prize}"; 
        }
        else Debug.LogWarning("(MoneyDisplay) Next payout text object ???");
    }
}
