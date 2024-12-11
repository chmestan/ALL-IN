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
        // Ensure we only use active chips for display
        float chipHeight = 20f; // Set this to the height of your chip prefab
        float spacing = 5f;     // Spacing between chips

        for (int i = activeChips.Count; i < currentHealth; i++)
        {
            GameObject chip = HPChipPool.SharedInstance.GetPooledObject();
            if (chip != null)
            {
                chip.transform.SetParent(hpContainer, false);
                chip.SetActive(true);
                activeChips.Add(chip);

                // Position each chip relative to its index
                RectTransform rectTransform = chip.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, i * (chipHeight + spacing));
            }
        }

        // Hide excess chips
        while (activeChips.Count > currentHealth)
        {
            GameObject chip = activeChips[activeChips.Count - 1];
            activeChips.RemoveAt(activeChips.Count - 1);
            chip.SetActive(false);
        }
    }
    public void TriggerChipAnimation(int lostHealth)
    {
        for (int i = maxHealth - 1; i >= maxHealth - lostHealth; i--)
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
}
