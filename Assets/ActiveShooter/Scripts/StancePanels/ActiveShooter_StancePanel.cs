using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StanceRemoteData
{
    public string m_PlayerId;
    public int m_SequenceNumber;
    public float SequenceTimer;
    public float ScorePercentage;
    public int ReplayCount;
    public int Choices;
    public float Score;
    public string sessionId;
    public StanceRemoteData(string playerId, int sequenceNumber, float sequenceTimer, float scorePercentage, int replayCount, int choices, float score, string session)
    {
        m_PlayerId = playerId;
        m_SequenceNumber = sequenceNumber;
        SequenceTimer = sequenceTimer;
        ScorePercentage = scorePercentage;
        ReplayCount = replayCount;
        Score = score;
        Choices = choices;
        sessionId = session;
    }
}

[System.Serializable]
public class Sequence6_2Data
{
    public float SequenceTimer;
    public float ScorePercentage;
    public int ReplayCount;
    public int Choices;
    public float Score;
}

public class ActiveShooter_StancePanel : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private Sequence6_2Data m_CurrentData;
    [SerializeField]
    private bool m_SequenceActivated = false;
    [SerializeField]
    private GameObject m_MenuPanel;
    [SerializeField]
    private string m_CorrectStance;
    [SerializeField]
    private string m_InCorrectStance;
    [SerializeField]
    private GameObject[] m_SelectionButtons;
    [SerializeField]
    private GameObject[] m_StatnceImages;
    [SerializeField]
    private string m_CurrentState;
    [SerializeField]
    private int m_CurrentSequenceNumber;
    #endregion

    #region Public_Vars
    public static Action<Sequence6_2Data> s_ConversationDataCallback;
    public static Action<string> s_StanceRemoteDataCallback;
    public static Action<StanceRemoteData> s_StanceSaveDataCallback;
    public static Action s_DeactiveCallback;
    #endregion



    #region Unity_Callbacks
    private void OnEnable()
    {
        m_SequenceActivated = true;
        StartCoroutine(nameof(StartSequenceTimer));

    }

    private void OnDisable()
    {
        CalculateScorePercentage();
        m_SequenceActivated = false;

        // on this for the data
      /*  s_ConversationDataCallback?.Invoke(m_CurrentData);
        StanceRemoteData remoteData = new StanceRemoteData(GameManager.Instance.PlayerId, m_CurrentSequenceNumber, m_CurrentData.SequenceTimer, m_CurrentData.ScorePercentage, m_CurrentData.ReplayCount, m_CurrentData.Choices, m_CurrentData.Score, GameManager.Instance.SessionId);
        string jsonString = JsonUtility.ToJson(remoteData);
      //chrischnages 06/05  s_StanceRemoteDataCallback?.Invoke(jsonString);
        GuideManager.s_StanceSaveDataCallback?.Invoke(remoteData);*/
        
    }

    #endregion

    #region Public_Methods

    public void OnIncorrectButtonTap()
    {
        m_CurrentState = m_InCorrectStance;
        ToggleSelectionButtons(false);
    }
    public void OnCorrectButtonTap()
    {
        m_CurrentState = m_CorrectStance;
        ToggleSelectionButtons(false);
        ToggleStanceImages(true);

    }
    public void OnCorrectButtonTap2(AudioClip clip)
    {
        m_CurrentState = m_CorrectStance;
        ToggleSelectionButtons(false);
        ToggleStanceImages(false);
        m_StatnceImages[1].SetActive(true);
        Utilities.ExecuteAfterDelay(clip.length, () =>
        {
            ToggleStanceImages(false);
        });
    }

    public void OnIncorrectButtonTap2(AudioClip clip)
    {
        m_CurrentState = m_InCorrectStance;
        ToggleSelectionButtons(false);
        ToggleStanceImages(false);
        m_StatnceImages[0].SetActive(true);
        Utilities.ExecuteAfterDelay(clip.length, () =>
        {
            ToggleStanceImages(true);
            ToggleSelectionButtons(true);
        });
    }


    public void ToggleStanceImages(bool status)
    {
        foreach (GameObject stanceImages in m_StatnceImages)
        {
            stanceImages.SetActive(status);
        }
    }
    public void ToggleSelectionButtons(bool status)
    {
        foreach (GameObject selectionButtons in m_SelectionButtons)
        {
            selectionButtons.SetActive(status);
        }
    }

    public void OnContinueButtonTap()
    {
        if (m_CurrentState == m_CorrectStance)
        {
            s_DeactiveCallback?.Invoke();
        }
        else
        {
            m_MenuPanel.SetActive(false);
            ToggleSelectionButtons(true);
        }
    }
 /*skchnages 14-09   public void OnContinueButtonTapSequence32()
    {
        if (m_CurrentState == m_CorrectStance)
        {
            GuideManager.Instance.StartSequence33();
        }
        else
        {
            m_MenuPanel.SetActive(false);
            ToggleSelectionButtons(true);
        }
    }
    public void OnContinueButtonTapSequence45()
    {
        if (m_CurrentState == m_CorrectStance)
        {
            GuideManager.Instance.StartSequence46();
        }
        else
        {
            m_MenuPanel.SetActive(false);
            ToggleSelectionButtons(true);
        }
    }
*/


    public void ActivateGuideVoice(string stateName)
    {
        ActiveShooterGuideManager.Instance.SetObservationSounds(stateName, false, false, false, true);

    }
    public void OnReplay()
    {
        ActiveShooterGuideManager.Instance.SetObservationSounds(m_CurrentState, false, false, false, true);
        m_CurrentData.ReplayCount++;
    }
    public void UpdateScore(int score)
    {
        m_CurrentData.Score += score;
        m_CurrentData.Choices++;
    }
    public void TransitionToNextSequence(AudioClip clip)
    {
        float audioClipLength = clip.length;
        Utilities.ExecuteAfterDelay(audioClipLength, () =>
        {
            m_MenuPanel.SetActive(true);
            /*GuideManager.Instance.StartSequence6();*/
        });
    }
    #endregion

    #region Private_Methods

    private void CalculateScorePercentage()
    {
        if (m_CurrentData.Score > 0)
        {
            m_CurrentData.ScorePercentage = (m_CurrentData.Score / (float)m_CurrentData.Choices) * 100f;
        }
        else
        {
            m_CurrentData.ScorePercentage = 0f;
        }
    }



    #endregion

    #region Coroutines
    private IEnumerator StartSequenceTimer()
    {
        while (m_SequenceActivated)
        {
            m_CurrentData.SequenceTimer++;
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
