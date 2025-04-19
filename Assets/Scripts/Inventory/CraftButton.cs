using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject potion;

    private PotionUI ptUI;

    void Start()
    { 
        if (potion != null) {
            ptUI = potion.GetComponent<PotionUI>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ptUI?.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ptUI?.OnPointerExit(eventData);
    }
}
