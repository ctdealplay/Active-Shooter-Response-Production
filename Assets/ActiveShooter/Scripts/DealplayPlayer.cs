using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealplayPlayer : MonoBehaviour
{

    #region Unity_callbacks
    private void OnEnable()
    {
        TeleportInteractable.s_PlayerPositionCallback += OnPlayerTeleported;
        TeleportInteractable.s_PlayerRotationCallback += OnPlayerRotated;
        ActiveShooterSequenceManager.s_PlayerPositionCallback += OnPlayerTeleported;
        ActiveShooterSequenceManager.s_PlayerRotationCallback += OnPlayerRotated;
    }
    private void OnDisable()
    {
        TeleportInteractable.s_PlayerPositionCallback -= OnPlayerTeleported;
        TeleportInteractable.s_PlayerRotationCallback -= OnPlayerRotated;
        ActiveShooterSequenceManager.s_PlayerPositionCallback -= OnPlayerTeleported;
        ActiveShooterSequenceManager.s_PlayerRotationCallback -= OnPlayerRotated;

    }


    #endregion



    #region Private_Methods
    private void OnPlayerTeleported(Vector3 targetPosition)
    {
        transform.position = targetPosition;
        
    }
    private void OnPlayerRotated(Quaternion targetRotation)
    {
        transform.rotation = targetRotation;
       
    }
    #endregion
}
