using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] private Slider accelerationSlider;
    [SerializeField] private HoldButtonUI brakeBTN;
    [SerializeField] private HoldButtonUI leftBTN;
    [SerializeField] private HoldButtonUI rightBTN;



    void Start()
    {
        GameManager.Instance.OnCarSpawned += GameManager_OnCarSpawned;
    }

    private void GameManager_OnCarSpawned(object sender, GameManager.OnCarSpawnedEventArgs e)
    {
        e.car.SetControls(accelerationSlider, brakeBTN, leftBTN, rightBTN);
    }
    
}
