using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlightSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool debug = false;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (debug) Debug.Log($"{gameObject.name} hovered");
        EventSystem.current?.SetSelectedGameObject(gameObject);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (debug) Debug.Log($"{gameObject.name} pointer exited");
        if (EventSystem.current?.currentSelectedGameObject == gameObject)
        {
            if (debug) Debug.Log($"{gameObject.name} deselected");
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}


