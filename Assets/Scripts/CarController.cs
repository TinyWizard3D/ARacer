using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour, ICarControl
{   
    [SerializeField] Transform[] wheelTransforms; // Make sure the two first elements are the Front Wheels
    [SerializeField] WheelCollider[] wheelColliders; // Make sure the two first elements are the Front Wheels
    [SerializeField] float maxAcceleration = 15f;
    [SerializeField] float maxBrakeTorque = 15f;
    [SerializeField] float maxSteerAngle = 30f;
    private Slider accelerationSlider;
    private HoldButtonUI brakeBTN;
    private HoldButtonUI leftBTN;
    private HoldButtonUI rightBTN;
    private bool isBraking = false;
    private int steerDirection = 0;

    private void Start()
    {
        brakeBTN.onHoldStart += BrakeBTN_OnHoldStart;
        brakeBTN.onHoldEnd += BrakeBTN_OnHoldEnd;

        leftBTN.onHoldStart += leftBTN_onHoldStart;
        leftBTN.onHoldEnd += leftBTN_onHoldEnd;

        rightBTN.onHoldStart += rightBTN_onHoldStart;
        rightBTN.onHoldEnd += rightBTN_onHoldEnd;
    }

    private void rightBTN_onHoldEnd(object sender, EventArgs e)
    {
        steerDirection = 0;
    }

    private void rightBTN_onHoldStart(object sender, EventArgs e)
    {
        steerDirection = 1;
    }

    private void leftBTN_onHoldEnd(object sender, EventArgs e)
    {
        steerDirection = 0;
    }

    private void leftBTN_onHoldStart(object sender, EventArgs e)
    {
        steerDirection = -1;
    }

    private void BrakeBTN_OnHoldEnd(object sender, EventArgs e)
    {
        isBraking = false;
    }

    private void BrakeBTN_OnHoldStart(object sender, EventArgs e)
    {
        isBraking = true;
    }

    private void Update()
    {
        float torque = maxAcceleration * accelerationSlider.value;
        float brakeTorque = isBraking ? maxBrakeTorque : 0;
        Drive(torque, brakeTorque);
        SetSteerDirection(steerDirection);
        UpdateWheelTransforms();
    }

    private void Drive(float torque, float brakeTorque)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = torque;
            wheel.brakeTorque = brakeTorque;
        }
    }

    private void SetSteerDirection(int direction)
    {
        float steerAngle = maxSteerAngle * direction;
        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;
    }

    private void UpdateWheelTransforms()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            Vector3 position;
            Quaternion rotation;
            wheelColliders[i].GetWorldPose(out position, out rotation);

            Vector3 localEulerAngles = wheelTransforms[i].localEulerAngles;
            localEulerAngles.z = rotation.eulerAngles.z;
            wheelTransforms[i].localEulerAngles = localEulerAngles;

            if (i < 2) // Front wheels
            {
                localEulerAngles.y = wheelColliders[i].steerAngle;
                wheelTransforms[i].localEulerAngles = localEulerAngles;
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetControls(Slider slider, HoldButtonUI brakeBTN, HoldButtonUI leftBTN, HoldButtonUI rightBTN)
    {
        accelerationSlider = slider;
        this.brakeBTN = brakeBTN;
        this.leftBTN = leftBTN;
        this.rightBTN = rightBTN;
    }

}
