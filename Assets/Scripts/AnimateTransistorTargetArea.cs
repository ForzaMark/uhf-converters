using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EmissionStates
{
    Switching,
    ShutDown,
    PermanentEnabled
}

public class AnimateTransistorTargetArea : MonoBehaviour
{
    private EmissionStates emissionState = EmissionStates.ShutDown;
    private bool enableEmission = false;
    [SerializeField] GameObject transistorTarget;
    public void OnTransistorSelected() 
    {
        this.emissionState = EmissionStates.Switching;
    }

    public void OnTransistorDropped()
    {
        this.emissionState = EmissionStates.ShutDown;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TransistorTargetArea")
        {
            this.emissionState = EmissionStates.PermanentEnabled;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "TransistorTargetArea")
        {
            this.emissionState = this.emissionState == EmissionStates.ShutDown ? EmissionStates.ShutDown : EmissionStates.Switching;
        }
    }

    public void Awake()
    {
        float duration = 0.2F;
        InvokeRepeating("HandleEmissionState", 0, duration);
    }

    private void HandleEmissionState()
    {
        if (this.emissionState == EmissionStates.Switching)
        {
            if (this.enableEmission)
            {
                transistorTarget.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            } else
            {
                transistorTarget.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }
            this.enableEmission = !this.enableEmission;
        }


        if (this.emissionState == EmissionStates.PermanentEnabled)
        {
            transistorTarget.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

        if (this.emissionState == EmissionStates.ShutDown)
        {
            transistorTarget.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
