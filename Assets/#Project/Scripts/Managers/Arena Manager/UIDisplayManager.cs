using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIDisplayManager : MonoBehaviour
{
    [Header ("HP Chips References"), Space (3f)]
        [SerializeField] private Transform hpContainer; 
        [SerializeField] private GameObject chipPrefab; 
        
    [Header ("Wave Count Text"), Space (3f)]
        [SerializeField] private TextMeshProUGUI waveCountText;    

    private int maxHealth;
    private List<GameObject> activeChips = new List<GameObject>();

    public void Initialize(int health)
    {
        maxHealth = health;
        UpdateHPDisplay(maxHealth);
        waveCountText.text = $"{GlobalManager.Instance.waveManager.WaveCount}"; 
    }

    public void UpdateHPDisplay(int currentHealth)
    {
        RectTransform rect = chipPrefab.GetComponent<RectTransform>();
        float chipHeight = rect.rect.height;

        float spacing = 2;

        // for initializing AND
        // in case of health increases mid-round (for future upgrades maybe?)
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

            Animator animator = chip.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Slide");
                StartCoroutine(HideChipAfterAnimation(animator, chip));
            }
        }
    }

    private IEnumerator HideChipAfterAnimation(Animator animator, GameObject chip)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        chip.SetActive(false);
    }

}
