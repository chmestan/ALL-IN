using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites; 
    [SerializeField] private Image leftSlot;
    [SerializeField] private Image middleSlot;
    [SerializeField] private Image rightSlot; 
    private Animator leftSlotAnimator;
    private Animator middleSlotAnimator;
    private Animator rightSlotAnimator;

    private ExtraEnemies extraEnemies;

    private void Start()
    {
        extraEnemies = GetComponent<ExtraEnemies>();
        leftSlotAnimator = leftSlot.gameObject.GetComponent<Animator>();
        middleSlotAnimator = middleSlot.gameObject.GetComponent<Animator>();
        rightSlotAnimator = rightSlot.gameObject.GetComponent<Animator>();
    }

    public void UpdateSlots()
    {
        if (extraEnemies.rolledResults.Count != 3)
        {
            Debug.LogError("Rolled results do not contain exactly 3 entries.");
            return;
        }

        // Assign sprites based on rolled results
        leftSlot.sprite = GetSprite(extraEnemies.rolledResults[0]);
        middleSlot.sprite = GetSprite(extraEnemies.rolledResults[1]);
        rightSlot.sprite = GetSprite(extraEnemies.rolledResults[2]);

        // Enable the slots
        leftSlot.gameObject.SetActive(true);
        middleSlot.gameObject.SetActive(true);
        rightSlot.gameObject.SetActive(true);
        leftSlotAnimator.SetTrigger("FadeIn");
        middleSlotAnimator.SetTrigger("FadeIn");
        rightSlotAnimator.SetTrigger("FadeIn");

    }

        public void TriggerFadeOut()
        {
            // Set the FadeOut trigger for each slot
            leftSlotAnimator.SetTrigger("FadeOut");
            middleSlotAnimator.SetTrigger("FadeOut");
            rightSlotAnimator.SetTrigger("FadeOut");
        }

    private Sprite GetSprite((int enemyType, int count) rollResult)
    {
        if (rollResult.count == 0)
        {
            return sprites[15];
        }

        int maxCount = 5; 
        int index = rollResult.enemyType * maxCount + rollResult.count - 1;

        if (index < 0 || index >= sprites.Count)
        {
            Debug.LogError("Invalid sprite index.");
            return null;
        }

        return sprites[index];
    }
}
