using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class TeleportInteractable : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private int m_id;
    [SerializeField]
    private Transform m_TargetPoint;
    [SerializeField]
    private Material m_Blue;
    [SerializeField]
    private Material m_Green;
    private Renderer m_Renderer;
    [SerializeField]
    private bool m_ObserverTeleport;
    [SerializeField]
    private bool m_StaticTeleport;
    [SerializeField]
    private bool m_ObserverInteracted;
    [SerializeField]
    private string m_StateName;
    [SerializeField]
    private GameObject m_VRUI;
    [SerializeField]
    private bool m_IsCorrectTeleport;
    [SerializeField]
    private AudioClip m_TeleportClip;
    private Quaternion m_TargetRotation;
    [SerializeField]
    private bool m_CanChangeTimeline;
    [SerializeField]
    private PlayableAsset m_CurrentTimeline;
    [SerializeField]
    private string m_SequenceName;
    [SerializeField]
    private bool m_Destructable;
    [SerializeField]
    private GameObject[] m_ObservationPoints;
    [SerializeField]
    private bool m_CanObserve;
    [SerializeField]
    private bool m_CanCallStaticFunction;



    #endregion

    #region Public_Vars
    public int id => m_id;
    public static Action<Vector3> s_PlayerPositionCallback;
    public static Action<Quaternion> s_PlayerRotationCallback;
    public static Action s_ChoiceCallback;
    public static Action<string, bool, AudioClip> s_TeleportationChoiceCallback;
    public static Action<bool> s_TeleportReset;
    public static Action<PlayableAsset> s_UpdateTimeline;
    #endregion

    #region Unity_Callbacks
    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        m_TargetRotation = m_TargetPoint.rotation;
        m_Renderer.material = m_Blue;
        EyeTrackingRay.s_EnableTeleporter += OnTeleportActivated;
        EyeTrackingRay.s_InteractedTeleporter += OnTeleportInteracted;
        EyeTrackingRay.s_DisableTeleporter += OnTeleportDisabled;


        ActiveShooterGuideManager.s_ObservationDotsCallbacks += CheckInteractionState;
        ActiveShooterGuideManager.s_TeleportCallback += GuideCallback;
        ActiveShooterGuideManager.s_TeleportResetCallback += TeleportReset;
            

    }
    private void OnDisable()
    {
        EyeTrackingRay.s_EnableTeleporter -= OnTeleportActivated;
        EyeTrackingRay.s_InteractedTeleporter -= OnTeleportInteracted;
        EyeTrackingRay.s_DisableTeleporter -= OnTeleportDisabled;


        ActiveShooterGuideManager.s_ObservationDotsCallbacks -= CheckInteractionState;
        ActiveShooterGuideManager.s_TeleportCallback -= GuideCallback;
        ActiveShooterGuideManager.s_TeleportResetCallback -= TeleportReset;
    }

    #endregion

    #region Private_Methods 
    private void TeleportReset()
    {
        if (m_ObserverTeleport)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void CheckInteractionState(Action dotCallback)
    {
        if (!m_ObserverInteracted && m_ObserverTeleport)
        {
            this.gameObject.SetActive(true);
            dotCallback?.Invoke();
        }
    }
    private void GuideCallback()
    {
        if (m_ObserverInteracted && m_ObserverTeleport)
        {
            this.gameObject.SetActive(false);

        }
        else if (m_ObserverTeleport)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<BoxCollider>().enabled = true;

            this.gameObject.SetActive(true);
        }
            
    }
    private void OnTeleportActivated(int id)
    {
        if (m_id == id)
        {
            if(m_CanObserve)
            {
               /* if(m_CanCallStaticFunction)
                {
                    GuideManager.Instance.SetObservationSounds(m_SequenceName);
                }*/
                s_PlayerRotationCallback?.Invoke(m_TargetRotation);
                s_PlayerPositionCallback?.Invoke(m_TargetPoint.position);
                foreach (GameObject observe in m_ObservationPoints)
                {
                    observe.SetActive(true);
                }
                transform.parent.parent.gameObject.SetActive(false);
                ActiveShooterGuideManager.Instance.SetObservationSounds(m_SequenceName, false, false, true);
                Debug.Log("The destruction call 1");
                Destroy(this.gameObject);
                return;

            }
            if (m_StaticTeleport)
            {
                Debug.Log("The destruction call 2");

                s_PlayerRotationCallback?.Invoke(m_TargetRotation);
                s_PlayerPositionCallback?.Invoke(m_TargetPoint.position);
                if(m_Destructable)
                {
                    s_TeleportReset?.Invoke(false);
                    foreach(GameObject observe in m_ObservationPoints)
                    {
                        observe.SetActive(true);
                    }
                    Destroy(this.gameObject);
                    return;
                }

                ActiveShooterGuideManager.Instance.SetObservationSounds(m_SequenceName, false, false, true);
           
                
                return;
            }

            if (m_ObserverTeleport)
            {
                Debug.Log("The destruction call 3");
                this.GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<BoxCollider>().enabled = true;

                ActiveShooterGuideManager.Instance.SetObservationSounds(m_StateName, false, false, true);
            
                s_ChoiceCallback?.Invoke();
                // m_VRUI.gameObject.SetActive(true);
                m_ObserverInteracted = true;
                if (m_CanChangeTimeline)
                {
                    s_UpdateTimeline?.Invoke(m_CurrentTimeline);
                }
                s_TeleportationChoiceCallback?.Invoke(m_StateName, m_IsCorrectTeleport, m_TeleportClip);

            }
            s_PlayerRotationCallback?.Invoke(m_TargetRotation);
            s_PlayerPositionCallback?.Invoke(m_TargetPoint.position);
            Utilities.ExecuteAfterDelay(0.25f, () =>
            {
                Destroy(this.gameObject);
            });
            Debug.Log("The position changes to: " + m_TargetPoint.position);
          
        }
        else if (m_ObserverTeleport && !m_CanObserve)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            //  m_VRUI.gameObject.SetActive(false);      
        }
    }
    private void OnTeleportInteracted(int id)
    {
        if (m_id == id)
        {
            m_Renderer.material = m_Green;
        }
    }


    private void OnTeleportDisabled()   
    {
        m_Renderer.material = m_Blue;
    }
    #endregion

}
