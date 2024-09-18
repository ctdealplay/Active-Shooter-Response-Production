using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec;
using PA_DronePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_Sequence_Two_Marker : MonoBehaviour
{
    [SerializeField] DC_GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DronePatrol") {
            print("DroneController !");
        
        }
    }

}
