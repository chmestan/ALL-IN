using UnityEngine;
using TMPro;

public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        if (descriptionText != null)
        {
            descriptionText.gameObject.SetActive(false); // Hide the description initially
        }
    }

    public void ShowDescription(string description)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
            descriptionText.gameObject.SetActive(true);
        }
    }

    public void HideDescription()
    {
        if (descriptionText != null)
        {
            descriptionText.text = "";
            descriptionText.gameObject.SetActive(false);
        }
    }
}
