using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ObservationOptions : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private TextMeshProUGUI m_Option;
    [SerializeField]
    private bool m_IsCorrect = false;
    [SerializeField]
    private int m_OptionIndex = 0;
    [SerializeField]
    private Sprite m_CorrectImage;
    [SerializeField]
    private Sprite m_InCorrectImage;
    [SerializeField]
    private Sprite m_NormalImage;
    [SerializeField]
    private Image m_ButtonImage;
    [SerializeField]
    private bool m_CanInteact = true;
    [SerializeField]
    private AudioClip m_GuideAudio;
    [SerializeField]
    private string m_AnswerState;
    [SerializeField]
    private GameObject m_QuestionReplay;
    #endregion

    #region Public_Vars
    public static Action m_ResultCallback;
    public static Action<bool> m_ResultStatusCallback;
   
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        m_ButtonImage.sprite = m_NormalImage;
        m_CanInteact = true;
       // m_IsCorrect = false;
        m_ResultCallback += OnResultCallback;
        ActiveShooter_ObservationPanel.s_ResetOptions += ResetOptions;
        ActiveShooter_ObservationPanel.s_ReplayOptions += ReplayOptions;
    }
    private void OnDisable()
    {
        m_ResultCallback -= OnResultCallback;
        ActiveShooter_ObservationPanel.s_ResetOptions -= ResetOptions;
        ActiveShooter_ObservationPanel.s_ReplayOptions -= ReplayOptions;


    }
    #endregion




    #region Public_methods
    
    public void SetOptionData(string data, int optionIndex, string answerState)
    {
        m_Option.text = data;
        m_IsCorrect = (m_OptionIndex == optionIndex) ? true : false;
        m_AnswerState = answerState;
    }

    public void OnObserverOptionSelected()
    {
        if(m_CanInteact)
        {
            /*  if (m_GuideAudio != null)
              {
                  GuideManager.Instance.UpdateGuideAudio(m_GuideAudio);
              }*/
            m_QuestionReplay?.SetActive(false);
            ActiveShooterGuideManager.Instance.SetObservationSounds(m_AnswerState, false, true);
            m_ResultCallback?.Invoke();
            m_ResultStatusCallback?.Invoke(m_IsCorrect);
            if (!m_IsCorrect)
            {
                m_ButtonImage.sprite = m_InCorrectImage;
            }
        }
    }
    #endregion

    #region Private_Methods
    private void ResetOptions()
    {
        m_ButtonImage.sprite = m_NormalImage;
        m_CanInteact = true;
        m_IsCorrect = false;
    }
    private void ReplayOptions()
    {
        m_ButtonImage.sprite = m_NormalImage;
        m_CanInteact = true;
    }
    private void OnResultCallback()
    {
        m_CanInteact = false;
        m_ButtonImage.sprite = (m_IsCorrect) ? m_CorrectImage : m_ButtonImage.sprite;
    }

    #endregion

}
