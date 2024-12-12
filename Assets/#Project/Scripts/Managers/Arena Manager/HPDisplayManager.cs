using System.Collections.Generic;
using UnityEngine;

public class HPDisplayManager : MonoBehaviour
{
    [SerializeField] private Transform hpContainer; 
    [SerializeField] private int maxHealth;
    private List<GameObject> activeChips = new List<GameObject>();

    public void Initialize(int health)
    {
        maxHealth = health;
        UpdateHPDisplay(maxHealth);
    }

    public void UpdateHPDisplay(int currentHealth)
    {
        float chipHeight = GetChipHeight();
        float spacing = 2; 

        for (int i = activeChips.Count; i < currentHealth; i++)
        {
            GameObject chip = HPChipPool.SharedInstance.GetPooledObject();
            if (chip != null)
            {
                chip.transform.SetParent(hpContainer, false);
                chip.SetActive(true);
                activeChips.Add(chip);

                RectTransform rectTransform = chip.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, i * (chipHeight + spacing));
            }
        }

        while (activeChips.Count > currentHealth)
        {
            GameObject chip = activeChips[activeChips.Count - 1];
            activeChips.RemoveAt(activeChips.Count - 1);
            chip.SetActive(false);
        }
    }

    public void TriggerChipAnimation(int lostHealth)
    {
        int startIndex = Mathf.Max(0, maxHealth - lostHealth);
        for (int i = maxHealth - 1; i >= startIndex; i--)
        {
            if (i < activeChips.Count && activeChips[i].activeSelf)
            {
                Animator animator = activeChips[i].GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Slide");
                }
            }
        }
    }

    private float GetChipHeight()
    {
        // Create a temporary chip to get dimensions if none exist
        if (activeChips.Count > 0)
        {
            RectTransform rect = activeChips[0].GetComponent<RectTransform>();
            return rect.rect.height;
        }
        else
        {
            GameObject tempChip = HPChipPool.SharedInstance.GetPooledObject();
            tempChip.SetActive(true);
            RectTransform rect = tempChip.GetComponent<RectTransform>();
            float height = rect.rect.height;
            tempChip.SetActive(false);
            return height;
        }
    }
}
