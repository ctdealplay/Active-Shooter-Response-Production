using PA_DronePack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DC_NewDroneInpute : MonoBehaviour
{
    public PA_DroneCamera DCScript;
    public DronControllerOVR Controlls = null;

    private void Awake()
    {
        Controlls =new DronControllerOVR();
        DCScript = FindObjectOfType<PA_DroneCamera>();
    }


    private void OnEnable()
    {
        Controlls.Enable();
        Controlls.DroneControlle.PrimaryButton.performed += changeCameraMode;

    }

    private void changeCameraMode(InputAction.CallbackContext context)
    {
        print("changeCameraMode");
        DCScript.ChangeCameraMode();
    }

    private void OnDisable()
    {
        Controlls.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
