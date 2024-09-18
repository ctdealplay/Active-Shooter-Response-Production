using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


[Serializable]
public class ObservationRemoteData
{
    public string m_PlayerId;
    public int m_SeqenceNumber;
    public float SequenceTimer;
    public List<DotData> DotData = new List<DotData>();
    public int Score;
    public string sessionId;
    public ObservationRemoteData(string playerId, int sequenceNumber, float sequenceTimer, List<DotData> dotData, int score, string session)
    {
        m_PlayerId = playerId;
        m_SeqenceNumber = sequenceNumber;
        SequenceTimer = sequenceTimer;
        DotData = dotData;
        Score = score;
        sessionId = session;
    }
}

[System.Serializable]
public class DotData
{
    public string LocationName;
    public int ReplayCount;
    public float InitialDotTime;
    public float TotalTime;
}

[System.Serializable]
public class Sequence2Data
{
    public float SequenceTimer;
    public List<DotData> DotData = new List<DotData>();
    public int Score;
}

public class ActiveShooter_ObservationPanel : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private ObservationOptions m_Option1;
    [SerializeField]
    private ObservationOptions m_Option2;
    [SerializeField]
    private ObservationOptions m_Option3;
    [SerializeField]
    private ObservationOptions[] m_ObservationOptions;
    [SerializeField]
    private GameObject m_OptionsToggle;

    [SerializeField]
    private int m_CorrectAnswer;
    [SerializeField]
    private string m_Question;
    [SerializeField]
    private string m_Answer;
    [SerializeField]
    private Sequence2Data m_SequenceData;
    [SerializeField]
    private string m_CurrentActivatedData = null;
    [SerializeField]
    private float m_DotTimer = 0;
    [SerializeField]
    private float m_TotalSequenceTimer = 0;
    [SerializeField]
    private bool m_IsDotActive = false;
    [SerializeField]
    private bool m_IsSequenceActive = false;
    [SerializeField]
    private GameObject m_QuestionReplay;
    [SerializeField]
    private GameObject m_AnswerReplay;
    [SerializeField]
    private GameObject m_Continue;
    [SerializeField]
    private int m_PanelId;
    [SerializeField]
    private int m_CurrentSequenceNumber;
    [SerializeField]
    private bool m_SequencelessOptions;
    [SerializeField]
    private GameObject m_toggler;
    [SerializeField]
    private AudioSource m_OptionQuestionAudio;
    #endregion

    #region Public_Vars
    public static Action s_ResetOptions;
    public static Action s_ReplayOptions;
    public static Action<Sequence2Data, int> s_SequenceDataCallback;
    public static Action<string> s_ObservationRemoteDataCallback;
    public static Action<ObservationRemoteData> s_ObservationSaveDataCallback;
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        if(m_SequencelessOptions)
        {
            Utilities.ExecuteAfterDelay(m_OptionQuestionAudio.clip.length, () =>
            {
                m_toggler.SetActive(true);
            });
        }
        m_IsSequenceActive = true;
        StartCoroutine(StartSequenceTimer());


        ActiveShooterGuideManager.s_ActivateObserverOptions += OnOptionToggle;
        ActiveShooterGuideManager.s_OnObserverAnswerEnds += OnObservationQuestionEnds;
        EyeInteractable.s_UserInteracted += OnUserVisionInteracted;
        ObservationOptions.m_ResultStatusCallback += OnResultCallback;
        ActiveShooterGuideManager.s_QuestionEnds += OnObservationQuestionEnds;
    }

    private void OnDisable()
    {

        // on it when works for the data
        /*  m_IsSequenceActive = false;
          s_SequenceDataCallback?.Invoke(m_SequenceData, m_PanelId);
          ObservationRemoteData remoteData = new ObservationRemoteData(GameManager.Instance.PlayerId, m_CurrentSequenceNumber, m_SequenceData.SequenceTimer, m_SequenceData.DotData, m_SequenceData.Score, GameManager.Instance.SessionId);
          string jsonString = JsonUtility.ToJson(remoteData);
          GuideManager.s_ObservationSaveDataCallback?.Invoke(remoteData);

         */

        //chrischnages 06/05 s_ObservationRemoteDataCallback?.Invoke(jsonString);



        ActiveShooterGuideManager.s_ActivateObserverOptions -= OnOptionToggle;
        ActiveShooterGuideManager.s_OnObserverAnswerEnds -= OnObservationQuestionEnds;
        EyeInteractable.s_UserInteracted -= OnUserVisionInteracted;
        ObservationOptions.m_ResultStatusCallback -= OnResultCallback;
        ActiveShooterGuideManager.s_QuestionEnds -= OnObservationQuestionEnds;
    }
    public void OnObservationQuestionEnds()
    {
        m_QuestionReplay.SetActive(false);
        m_AnswerReplay.SetActive(true);
        m_Continue.SetActive(true);
        /* m_QuestionReplay.gameObject.SetActive(true);
         m_Continue.gameObject.SetActive(true);*/
    }
    public void OnObservationQuestionStarts()
    {
        m_QuestionReplay.gameObject.SetActive(true);
        m_Continue.gameObject.SetActive(false);
        m_AnswerReplay.SetActive(false);
    }

    public void OnAnswerReplay()
    {
        ActiveShooterGuideManager.Instance.SetObservationSounds(m_Answer, false, true);
    }
    public void OnQuestionReplay()
    {
        ActiveShooterGuideManager.Instance.SetObservationSounds(m_Question, true , false);
    }

    public void OnReplay()
    {
        ActiveShooterGuideManager.Instance.SetObservationSounds(m_Question, true, false);
        int dotIndex = m_SequenceData.DotData.FindIndex(element => element.LocationName == m_CurrentActivatedData);
        if (dotIndex != -1)
        {
            m_SequenceData.DotData[dotIndex].ReplayCount++;
        }
        s_ReplayOptions?.Invoke();
    }
    public void OnContinue()
    {

    }
    public void OnOptionToggle(bool status)
    {
       if(status)
        {
            OnObservationQuestionStarts();
        }
        m_OptionsToggle.SetActive(status);
        if (!status)
        {
            m_IsDotActive = false;
        }
    }
    #endregion

    #region Private_Methods
    private void OnResultCallback(bool resultStatus)
    {
        int dotIndex = m_SequenceData.DotData.FindIndex(element => element.LocationName == m_CurrentActivatedData);
        if (dotIndex != -1 && resultStatus)
        {
            m_SequenceData.Score++;
        }
    }
    private void OnUserVisionInteracted(string interactedObjectName, string option1, string option2, string option3, int correctAnswer, AudioClip answers, bool activeStatus, string answerState, string questionState, float initialTimer)
    {
        s_ResetOptions?.Invoke();
        m_Question = questionState;
        m_Answer = answerState;
        int optionIndex = m_SequenceData.DotData.FindIndex(element => element.LocationName == interactedObjectName);
        if (optionIndex != -1)
        {
            m_SequenceData.DotData[optionIndex].InitialDotTime = initialTimer;
            m_CurrentActivatedData = m_SequenceData.DotData[optionIndex].LocationName;
            m_IsDotActive = true;
            m_DotTimer = 0f;
            StartCoroutine(StartDotTimer());
        }
        m_Option1.SetOptionData(option1, correctAnswer, answerState);
        m_Option2.SetOptionData(option2, correctAnswer, answerState);
        m_Option3.SetOptionData(option3, correctAnswer, answerState);

    }

    #endregion

    #region Coroutines
    IEnumerator StartDotTimer()
    {
        while (m_IsDotActive)
        {
            m_DotTimer++;
            yield return new WaitForSeconds(1f);
        }
        int dotIndex = m_SequenceData.DotData.FindIndex(element => element.LocationName == m_CurrentActivatedData);
        if (dotIndex != -1)
        {
            m_SequenceData.DotData[dotIndex].TotalTime = m_DotTimer;
        }
        m_DotTimer = 0f;
    }
    IEnumerator StartSequenceTimer()
    {
        while (m_IsSequenceActive)
        {
            m_SequenceData.SequenceTimer++;
            yield return new WaitForSeconds(1f);
        }
        s_SequenceDataCallback?.Invoke(m_SequenceData, m_PanelId);
    }

    #endregion



}
