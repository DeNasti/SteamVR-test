using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Pull : MonoBehaviour
{
    public SteamVR_Action_Boolean grab; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;

    public Vector3 prevPosition;

    [SerializeField]
    public bool canGrip;
    public bool isGrabGripPressed;

    public Rigidbody playerBody;

    #region Initialization
    void OnEnable()
    {
        if (grab != null)
        {
            grab.AddOnChangeListener(GripButtonClick, inputSource);
        }
    }

    private void OnDisable()
    {
        if (grab != null)
        {
            grab.RemoveOnChangeListener(GripButtonClick, inputSource);
        }
    }

    void Start()
    {
         prevPosition = this.transform.localPosition;
    }


    private void LateUpdate()
    {
        prevPosition = this.transform.position;
    }
    #endregion

    //questa funzione, viene chiamata quando viene premuto o rilasciato il tasto di grip
    private void GripButtonClick(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool pressed)
    {
        isGrabGripPressed = pressed;

        if (!pressed)
        {
            AddReleaseVelocity();
        }
    }
    
    public void TryGrab()
    {
        if (canGrip)
        {
            //playerBody.MovePosition(playerBody.transform.position + (prevPosition - transform.position));

            playerBody.transform.position += prevPosition - transform.position;
        }
    }
    
    private void AddReleaseVelocity()
    {
        ////se rilascio il bottone, dopo essermi attaccato a qualcosa, aggiungo una spinta
        if (canGrip)
        {

            playerBody.velocity = (prevPosition - transform.position) / Time.deltaTime * 100;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canGrip = true;
    }


    private void OnTriggerExit(Collider other)
    {
        canGrip = false;
    }
}
