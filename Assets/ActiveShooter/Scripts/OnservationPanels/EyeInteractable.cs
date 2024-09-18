using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Meta.XR.MRUtilityKit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    #region Public_Vars
    public bool IsHovered { get; set; }
    #endregion
    #region Private_Vars
    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;
    [SerializeField]
    private Material OnHoverActiveMaterial;
    [SerializeField]
    private Material OnHoverInActiveMaterial;
    private MeshRenderer meshRenderer;
    [SerializeField]
    private GameObject m_GreenDots;
    [SerializeField]
    private GameObject m_GreenBorder;

    [SerializeField]
    private int m_id;
    [SerializeField]
    private GameObject m_Loader;
    [SerializeField]
    private string m_Title;
    [SerializeField]
    private string m_Description;
    [SerializeField]
    private string m_Option1;
    [SerializeField]
    private string m_Option2;
    [SerializeField]
    private string m_Option3;
    [SerializeField]
    private int m_CorrectAnswer;
    [SerializeField]
    private bool m_HadInteracted = false;
    [SerializeField]
    private AudioClip m_GuideQuestionAudio;
    [SerializeField]
    private AudioClip m_GuideAnswerAudio;
    [SerializeField]
    private string m_QuestionState;
    [SerializeField]
    private string m_AnswerState;
    [SerializeField]
    private bool m_IsObservationDot = false;
    [SerializeField]
    private float m_InittialInteractionTimer = 0;

    #endregion

    #region Public_Vars
    public int id => m_id;
    public static Action s_IsObjectInteracted;
    public static Action<int> s_IsObjectDisableCallback;
    public static Action<string, string, string, string, int, AudioClip, bool, string, string, float> s_UserInteracted;


    #endregion


    #region Unity_Callbacks
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        //  m_GreenDots.SetActive(true);
        // s_IsObjectDisableCallback += OnObjectInteracted;
        EyeTrackingRay.s_DisableInteractables += DisableInteractables;
        EyeTrackingRay.s_ResetInteractables += ResetInteractables;
        EyeTrackingRay.s_EnableInteractables += OnObjectInteracted;
        EyeTrackingRay.s_EnableLoader += OnLoaderInteracted;
        EyeTrackingRay.s_DisableLoader += OnLoaderDisabled;

        ActiveShooterManager.s_VisonUIActivatedCallback += ResetInteractables;
        ActiveShooterManager.s_VisonUIDeactivatedCallback += DisableInteractables;
        ActiveShooterGuideManager.s_ObservationDotsCallbacks += OnObservationCallbacks;
    }
    private void OnDisable()
    {
        //   s_IsObjectDisableCallback -= OnObjectInteracted;
        EyeTrackingRay.s_DisableInteractables -= DisableInteractables;
        EyeTrackingRay.s_ResetInteractables -= ResetInteractables;
        EyeTrackingRay.s_EnableInteractables -= OnObjectInteracted;
        EyeTrackingRay.s_EnableLoader -= OnLoaderInteracted;
        EyeTrackingRay.s_DisableLoader -= OnLoaderDisabled;


        ActiveShooterManager.s_VisonUIActivatedCallback -= ResetInteractables;
        ActiveShooterManager.s_VisonUIDeactivatedCallback -= DisableInteractables;
        ActiveShooterGuideManager.s_ObservationDotsCallbacks -= OnObservationCallbacks;


    }


    #endregion

    #region Private_Methods
    private void OnObjectInteracted(int id)
    {
        if (m_id == id)
        {
            Debug.Log("The object is interacted");
            m_GreenDots.SetActive(false);
            m_Loader.SetActive(false);
            //m_GreenBorder.SetActive(true);
            m_HadInteracted = true;
            m_IsObservationDot = false;

            // Create a question Answer mechaism

            /*  GuideManager.Instance.SetObservationSounds(m_QuestionState);*/
            ActiveShooterGuideManager.Instance.SetObservationSounds(m_QuestionState, true, false);


            s_UserInteracted?.Invoke(m_Title, m_Option1, m_Option2, m_Option3, m_CorrectAnswer, m_GuideAnswerAudio, true, m_AnswerState, m_QuestionState, m_InittialInteractionTimer);

            Utilities.ExecuteAfterDelay(0.5f, () =>
            {
                Destroy(this.gameObject);
            });
            /*if(m_GuideQuestionAudio != null)
            {
                GuideManager.Instance.UpdateGuideAudio(m_GuideQuestionAudio);
            }*/
            // m_UIPanel.SetActive(false);
        }
    }
    public void OnObservationCallbacks(Action dotcallback)
    {
        if (m_IsObservationDot)
        {
            dotcallback?.Invoke();
        }
    }
    private void OnLoaderInteracted(int id)
    {
        if (m_id == id)
        {
            m_Loader.SetActive(true);
        }
    }
    private void OnLoaderDisabled()
    {
        m_Loader.SetActive(false);
    }

    private void ResetInteractables()
    {
        if (m_HadInteracted) // used for the sequence
        {
            m_GreenBorder.SetActive(false);
            return;
        }
        StartCoroutine(nameof(StartInitialDotTimer));
        m_GreenDots.SetActive(true);
        m_GreenBorder.SetActive(false);
        m_Loader.SetActive(false);
        s_UserInteracted?.Invoke(this.gameObject.name, m_Option1, m_Option2, m_Option3, m_CorrectAnswer, m_GuideAnswerAudio, false, m_AnswerState, m_QuestionState, m_InittialInteractionTimer);

        // m_UIPanel.SetActive(false);
    }
    private void DisableInteractables()
    {
        StopCoroutine(nameof(StartInitialDotTimer));
        m_GreenDots.SetActive(false);
        m_Loader.SetActive(false);
        m_GreenBorder.SetActive(false);
        s_UserInteracted?.Invoke(this.gameObject.name, m_Option1, m_Option2, m_Option3, m_CorrectAnswer, m_GuideAnswerAudio, false, m_AnswerState, m_QuestionState, m_InittialInteractionTimer);
        // m_UIPanel.SetActive(false);
    }
    #endregion

    #region Coroutines
    IEnumerator StartInitialDotTimer()
    {
        while (!m_HadInteracted)
        {
            m_InittialInteractionTimer++;
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
