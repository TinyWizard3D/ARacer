using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event EventHandler onHoldStart;
    public event EventHandler onHoldEnd;

    public void OnPointerDown(PointerEventData eventData)
    {
        onHoldStart?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onHoldEnd?.Invoke(this, EventArgs.Empty);
    }
}