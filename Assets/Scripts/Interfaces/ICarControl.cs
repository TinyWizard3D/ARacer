using UnityEngine;
using UnityEngine.UI;

public interface ICarControl
{
    GameObject GetGameObject();

    void SetControls(Slider slider, HoldButtonUI brakeBTN, HoldButtonUI leftBTN, HoldButtonUI rightBTN);
}
