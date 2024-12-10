using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites; 
    [SerializeField] private Image leftSlot;
    [SerializeField] private Image middleSlot;
    [SerializeField] private Image rightSlot; 

    private ExtraEnemies extraEnemies;

    private void Start()
    {
        extraEnemies = GetComponent<ExtraEnemies>();
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
    }

    private Sprite GetSprite((int enemyType, int count) rollResult)
    {
        int maxCount = 5; // Adjust this to the maximum count for any enemy
        int index = rollResult.enemyType * maxCount + rollResult.count - 1;

        if (index < 0 || index >= sprites.Count)
        {
            Debug.LogError("Invalid sprite index.");
            return null;
        }

        return sprites[index];
    }

}
