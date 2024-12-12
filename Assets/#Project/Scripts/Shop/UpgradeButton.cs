using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UpgradeButton : ButtonHighlightSelection
{
    private UpgradeDescription descriptionManager;
    private Upgrade upgrade;

    private void Awake()
    {
        upgrade = GetComponent<Upgrade>();
    }

    private void Start()
    {
        descriptionManager = FindObjectOfType<UpgradeDescription>();
        if (descriptionManager == null) Debug.LogError("(UpgradeButton) Couldn't find description manager");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (descriptionManager != null)
        {
            descriptionManager.ShowDescription(upgrade.description);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (descriptionManager != null)
        {
            descriptionManager.HideDescription();
        }
    }
}
