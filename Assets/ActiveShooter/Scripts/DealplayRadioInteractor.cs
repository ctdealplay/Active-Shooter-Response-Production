using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RadioSequenceData
{

    public float SequenceTimer;
    public List<float> Timers = new List<float>();
    public List<string> RadioTranscript = new List<string>();
}

[Serializable]
public class RadioRemoteData
{
    public string m_PlayerId;
    public int m_SeqenceNumber;
    public float SequenceTimer;
    public string sessionId;
    public List<float> Timers = new List<float>();
    public List<string> RadioTranscript = new List<string>();

    public RadioRemoteData(string playerId, int sequenceNumber, float sequenceTimer, List<float> timers, List<string> radioTrans, string session)
    {
        m_PlayerId = playerId;
        m_SeqenceNumber = sequenceNumber;
        SequenceTimer = sequenceTimer;
        Timers = timers;
        RadioTranscript = radioTrans;
        sessionId = session;
    }
}


public class DealplayRadioInteractor : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private List<float> m_Timers = new List<float>();
    [SerializeField]
    private List<string> m_Transcript = new List<string>();
    [SerializeField]
    private bool m_RadioInteractionStatus = false;
    [SerializeField]
    private float m_RadioTimer;
    [SerializeField]
    private float m_SequenceTimer;
    [SerializeField]
    private bool m_IsSequenceEnded = false;
    [SerializeField]
    int m_SequenceId;
    [SerializeField]
    private RadioSequenceData m_CurrentData;
    [SerializeField]
    private Vector3 m_RadioSnapPosition;
    [SerializeField]
    private Quaternion m_RadioSnapRotation;
    [SerializeField]
    private Text m_RadioText;
    [SerializeField]
    private bool m_CanRecord;
    [SerializeField]
    private AudioClip m_RecoredClip;
    /* private AudioSource m_AudioSource;*/
    [SerializeField]
    private int m_CurrentSequence;
    [SerializeField]
    private string m_RadioTranscript;



    #endregion

    #region Public_Vars
    public static Action<int, RadioSequenceData> s_SubmitData;
    public static Action s_RecordInteractionCallback;
    public static Action<string> s_RemoteDataCallback;
    public static Action<RadioRemoteData> s_RadioSaveDataCallback;
    public static Action s_DeactivateSubmitButtons;
    #endregion

    #region Unity_Callbacks
    private void Start()
    {
        // m_AudioSource = GetComponent<AudioSource>();
        StartCoroutine(StartSequeneceTimer());
    }
    private void OnEnable()
    {
       
        DealplayGrabbable.s_GrabBegin += OnGrabBegin;
        DealplayGrabbable.s_GrabRelease += OnGrabRelease;
        //   MicrophoneManager.s_MicCallback += OnRecordedAudioCall;
        m_RadioText.text = Constants.RADIO_WELCOMETEXT;
        /*chrisrevisionif (m_RadioText != null)
        {
        }*/
    }
    private void OnDisable()
    {

        DealplayGrabbable.s_GrabBegin -= OnGrabBegin;
        DealplayGrabbable.s_GrabRelease -= OnGrabRelease;
        s_DeactivateSubmitButtons?.Invoke();

       
    }
    #endregion

    #region Public_Methods
    public void OnSequenceEnded()
    {
        m_RadioTranscript = m_RadioText.text;
        s_DeactivateSubmitButtons?.Invoke();
        m_IsSequenceEnded = true;
        string data = "The Total Time taken to Complete Sequence is: " + m_SequenceTimer;
        m_CurrentData.SequenceTimer = m_SequenceTimer;
        m_CurrentData.Timers = m_Timers;
        m_CurrentData.RadioTranscript = m_Transcript;
        data += "\n";
        int timerCount = 1;
        foreach (float timer in m_Timers)
        {
            data += "\n" + "Attempt " + (timerCount++) + ": " + timer;
        }

        // turn this on when need to send the data

      /*  RadioRemoteData remoteData = new RadioRemoteData(GameManager.Instance.PlayerId, m_CurrentSequence, m_CurrentData.SequenceTimer, m_CurrentData.Timers, m_CurrentData.RadioTranscript, GameManager.Instance.SessionId);
        string jsonString = JsonUtility.ToJson(remoteData);
        Debug.Log("The radio Json Data is 1:" + remoteData);
        Debug.Log("The radio Json Data is 2:" + m_RadioTranscript);*/


        // send the remote data
        //chrischanges 06/05 s_RemoteDataCallback?.Invoke(jsonString);
       // GuideManager.s_RadioSaveDataCallback?.Invoke(remoteData);
        // s_SubmitData?.Invoke(m_SequenceId, m_CurrentData);
    }
   

    #endregion

    #region Private_Methods
    private void OnRecordedAudioCall(AudioClip clip)
    {
        /* if (m_CanRecord)
         {
             m_RecoredClip = clip;
         }*/
    }
    private void OnGrabBegin(bool status)
    {
        if (status)
            return;
        m_RadioInteractionStatus = true;
        m_RadioTimer = 0f;
        StartCoroutine(StartTimer());
    }

    private void OnGrabRelease(bool status)
    {
        if (status)
            return;
        m_RadioTranscript = m_RadioText.text;
        m_RadioInteractionStatus = false;
      
        m_Timers.Add(m_RadioTimer);
        m_Transcript.Add(m_RadioTranscript);
      
    }

    #endregion

    #region Coroutines
    IEnumerator StartTimer()
    {
        while (m_RadioInteractionStatus)
        {
            m_RadioTimer++;
            yield return new WaitForSeconds(1f);

        }
    }
    IEnumerator StartSequeneceTimer()
    {
        while (!m_IsSequenceEnded)
        {
            m_SequenceTimer++;
            yield return new WaitForSeconds(1f);
        }

    }

    #endregion
}
