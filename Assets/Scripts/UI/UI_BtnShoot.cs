using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BtnShoot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance?.Player?.TryShoot();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance?.Player?.StopShoot();
    }
}
