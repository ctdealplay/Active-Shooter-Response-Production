using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(LineRenderer))]
public class EyeTrackingRay : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private float rayDistance = 1f;
    [SerializeField]
    private float rayWidth = 0.01f;
    [SerializeField]
    private LayerMask layersToInclude;
    [SerializeField]
    private LayerMask m_TeleportLayer;
    [SerializeField]
    private LayerMask m_TempLayer;
    [SerializeField]
    private Color rayColorDefaultState = Color.yellow;
    [SerializeField]
    private Color rayColorHoverState = Color.red;
    private LineRenderer lineRenederer;
    [SerializeField]
    private bool m_IsInteracting = false;
    [SerializeField]
    private bool m_IsTeleportInteracting = false;
    [SerializeField]
    private GameObject m_TrackingSphere;
    private List<EyeInteractable> eyeInteractables = new List<EyeInteractable>();
    [SerializeField]
    private int m_InteractedId = -1;
    [SerializeField]
    private int m_TeleportInteractedId = -1;
    [SerializeField]
    private float m_HoverTimer;
    [SerializeField]
    private float m_HoverStartTime;
    [SerializeField]
    private float m_HoverTeleportStartTime;
    [SerializeField]
    private float m_HoverEndTime;
    private Coroutine m_InteractedCoroutine;
    [SerializeField]
    private bool m_CanSnap;
    #endregion

    #region Public_Vars
    public static Action s_DisableInteractables;
    public static Action s_ResetInteractables;
    public static Action<int> s_EnableInteractables;
    public static Action<int> s_EnableTeleporter;
    public static Action<int> s_InteractedTeleporter;
    public static Action<int> s_EnableLoader;
    public static Action s_DisableLoader;
    public static Action s_DisableTeleporter;
    #endregion



    #region Unity_callbacks
    private void Start()
    {
        lineRenederer = GetComponent<LineRenderer>();
        SetUpRay();
    }
    private void OnEnable()
    {
        ActiveShooterManager.s_VisonUIActivatedCallback += OnVisionActivated;
        ActiveShooterGuideManager.s_ObservationDotsCallbacks += CheckInteractionState;
    }
    private void OnDisable()
    {
        ActiveShooterManager.s_VisonUIActivatedCallback -= OnVisionActivated;
        ActiveShooterGuideManager.s_ObservationDotsCallbacks -= CheckInteractionState;

    }

    private void Update()
    {
        if (ActiveShooterManager.Instance.IsTeleportationActivated)
        {
            Vector3 rayctDirection = transform.TransformDirection(Vector3.forward) * rayDistance;
            TrackSphereTeleportaion(rayctDirection);
            HandleTeleportInteraction(rayctDirection);
        }




        if (!ActiveShooterManager.Instance.IsVisionUIActivated)
        {
            if (!ActiveShooterManager.Instance.IsTeleportationActivated)
            {
                m_TrackingSphere.SetActive(false);
            }

            return;
        }

        Vector3 raycastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;
        HandleObjectInteraction(raycastDirection);
        TrackSphere(raycastDirection);
        HandleTeleportInteraction(raycastDirection);
    }

    private void CheckInteractionState(Action ac)
    {
        m_IsInteracting = false;
    }

    private void HandleObjectInteraction(Vector3 raycastDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, Mathf.Infinity, layersToInclude) && !m_IsInteracting && (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
        {
            lineRenederer.startColor = rayColorHoverState;
            lineRenederer.endColor = rayColorHoverState;
            var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
            if (eyeInteractable != null)
            {
                
                /*   s_DisableInteractables?.Invoke();
                   s_EnableInteractables?.Invoke(eyeInteractable.id);
                   m_IsInteracting = true;*/

                if (eyeInteractable.id != m_InteractedId)
                {
                    m_InteractedId = eyeInteractable.id;
                    s_EnableLoader?.Invoke(eyeInteractable.id);
                    m_HoverStartTime = Time.time;
                    m_HoverTimer = 0f;
                }




                m_HoverEndTime = Time.time;
                s_DisableLoader?.Invoke();
                s_DisableInteractables?.Invoke();
                s_EnableInteractables?.Invoke(eyeInteractable.id);
                m_IsInteracting = true;


                /* // Increment the hover timer
                 m_HoverTimer += Time.deltaTime;
                 float hoverDuration = Time.time - m_HoverStartTime;

                 // Disable the frame object after the specified duration
                 if (hoverDuration >= Constants.HOVERED_TIME)
                 {
                     m_HoverEndTime = Time.time;
                     s_DisableLoader?.Invoke();
                     s_DisableInteractables?.Invoke();
                     s_EnableInteractables?.Invoke(eyeInteractable.id);
                     m_IsInteracting = true;
                 }*/

            }

        }
        else
        {
            s_DisableLoader?.Invoke();
            m_InteractedId = -1;
            m_HoverTimer = 0f;
           
        }

    }

    private void HandleTeleportInteraction(Vector3 raycastDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, Mathf.Infinity, m_TeleportLayer) && (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)))
        {
            lineRenederer.startColor = rayColorHoverState;
            lineRenederer.endColor = rayColorHoverState;
            var eyeInteractable = hit.transform.GetComponent<TeleportInteractable>();
            if (eyeInteractable != null)
            {
                if (eyeInteractable.id != m_TeleportInteractedId)
                {
                    m_TeleportInteractedId = eyeInteractable.id;
                    m_HoverTeleportStartTime = Time.time;
                    s_DisableTeleporter?.Invoke();
                    s_InteractedTeleporter?.Invoke(eyeInteractable.id);
                }





                s_EnableTeleporter?.Invoke(eyeInteractable.id);

                /*  float hoverDuration = Time.time - m_HoverTeleportStartTime;
                  // Disable the frame object after the specified duration
                  if (hoverDuration >= Constants.TELEPORTED_TIME)
                  {
                      s_EnableTeleporter?.Invoke(eyeInteractable.id);
                  }*/
            }
        }
        else
        {
            s_DisableTeleporter?.Invoke();
            m_TeleportInteractedId = -1;
        }
    }

    private void TrackSphere(Vector3 raycastDirection)
    {

        RaycastHit hit;
       


        float sphereRadius = 0.1f; // Adjust this radius as needed

        if (Physics.Raycast(transform.position, raycastDirection, out hit, Mathf.Infinity, (layersToInclude | m_TempLayer)))
        {
            m_TrackingSphere.SetActive(true);

            // Offset the sphere above the hit point by the sphere's radius
            Vector3 spherePosition = hit.point + hit.normal * sphereRadius;

            m_TrackingSphere.transform.position = spherePosition;

            // Adjust the rotation of the sphere to match the surface normal
            m_TrackingSphere.transform.rotation = Quaternion.LookRotation(hit.normal);

            /*var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
            if (eyeInteractable != null)
            {
                // Do something with the EyeInteractable component if needed
            }*/
        }
        else
        {
            m_TrackingSphere.SetActive(false);
        }

        /* skchnages return hit;*/
    }




    private RaycastHit TrackSphereTeleportaion(Vector3 raycastDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, Mathf.Infinity, (m_TeleportLayer | m_TempLayer)))
        {
            m_TrackingSphere.SetActive(true);
            m_TrackingSphere.transform.position = hit.point;
        }
        else
        {
            m_TrackingSphere.SetActive(false);
        }

        return hit;
    }

    #endregion

    #region Private_Methods
    private void OnVisionActivated()
    {
        m_IsInteracting = false;
    }
    private void UnSelect(bool clear = false)
    {
        foreach (var interactable in eyeInteractables)
        {
            interactable.IsHovered = false;
        }
        if (clear)
        {
            eyeInteractables.Clear();
        }
    }
    private void SetUpRay()
    {
        lineRenederer.useWorldSpace = false;
        lineRenederer.positionCount = 2;
        lineRenederer.startWidth = rayWidth;
        lineRenederer.endWidth = rayWidth;
        lineRenederer.startColor = rayColorDefaultState;
        lineRenederer.endColor = rayColorDefaultState;
        lineRenederer.SetPosition(0, transform.localPosition);
        lineRenederer.SetPosition(1, new Vector3(transform.localPosition.x, transform.localPosition.y,
            transform.localPosition.z + rayDistance));

    }


    #endregion

    #region Public_Methods
    public void OnContinueButtonTap()
    {
        s_ResetInteractables?.Invoke();
        m_IsInteracting = false;
    }

    #endregion







}