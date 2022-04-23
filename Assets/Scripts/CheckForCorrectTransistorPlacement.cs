using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CheckForCorrectTransistorPlacement : MonoBehaviour
{
    private bool isTransistorInTargetArea = false;
    private bool turnCameraSmooth = false;
    private int cameraTransistionStart = -31;
    private Rigidbody transistorRigidBody;

    [SerializeField] GameObject transistorTargetArea;
    [SerializeField] GameObject videoPlayer;
    [SerializeField] VideoClip successVideo;
    [SerializeField] GameObject xrOrigin;


    private void Start()
    {
        transistorRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (turnCameraSmooth)
        {
            if (cameraTransistionStart < 29)
            {
                cameraTransistionStart++;
                var successCameraRotation = new Vector3(0, cameraTransistionStart, 0);
                xrOrigin.transform.eulerAngles = successCameraRotation;
            }
        }

        if (cameraTransistionStart > 29)
        {
            turnCameraSmooth = false;
        }
    }



    public void OnTransistorPlaced()
    {
        if (isTransistorInTargetArea)
        {
            transistorRigidBody.constraints = RigidbodyConstraints.FreezeAll;
            var videoComponent = videoPlayer.GetComponent<VideoPlayer>();
            videoComponent.clip = successVideo;

            turnCameraSmooth = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TransistorTargetArea")
        {
            this.isTransistorInTargetArea = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "TransistorTargetArea")
        {
            this.isTransistorInTargetArea = false;    
        }
    }
}
