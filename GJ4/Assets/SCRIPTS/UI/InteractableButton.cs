using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.1f);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
    }
}
