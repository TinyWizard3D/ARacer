using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AccelerationSliderUI : MonoBehaviour, IPointerUpHandler
{
    
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        slider.value = 0f;
    }

}