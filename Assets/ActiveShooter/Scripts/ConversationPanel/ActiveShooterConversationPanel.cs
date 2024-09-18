    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ConversationScoreData
{
    public int score;
    public int conversationIndex;
}


[System.Serializable]
public class ConversationRemoteData
{
    public List<int> Score;
    public int SequenceNumber;
    public float SequenceTimer;
    public string m_PlayerId;
    public string sessionId;
    public ConversationRemoteData(string playerId, int sequenceNumber, float sequenceTimer, List<int> score, string session)
    {
        m_PlayerId = playerId;
        SequenceNumber = sequenceNumber;
        SequenceTimer = sequenceTimer;
        Score = score;
        sessionId = session;
    }


}

[System.Serializable]
public class Sequence7_1Data
{
    public float SequenceTimer;
    public List<int> Score = new List<int>();
}

/*
  public List<int> Score;
    public int SequenceNumber;      
    public int SequenceTimer;
    public string m_PlayerId;
 */

public class ActiveShooterConversationPanel : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private Sequence7_1Data m_CurrentData;
    [SerializeField]
    private bool m_SequenceActivated = false;
    public int SequenceCurrent;
    #endregion

    #region Public_Vars
    public static Action<Sequence7_1Data> s_ConversationDataCallback;
    public static Action<string> s_ConversationRemoteDataCallback;
    public static Action<ConversationRemoteData> s_ConversationSaveDataCallback;
    #endregion



    #region Unity_Callbacks
    private void OnEnable()
    {
        m_CurrentData.Score = new List<int>();
       m_SequenceActivated = true;
        StartCoroutine(nameof(StartSequenceTimer));
        VRConversationOptionss.s_DataCallback += UpdateConversationData;

    }

    private void OnDisable()
    {
        VRConversationOptionss.s_DataCallback -= UpdateConversationData;

        m_SequenceActivated = false;
        s_ConversationDataCallback?.Invoke(m_CurrentData);
        ConversationRemoteData remoteData = new ConversationRemoteData(ActiveShooterManager.Instance.PlayerId, SequenceCurrent, m_CurrentData.SequenceTimer, m_CurrentData.Score, ActiveShooterManager.Instance.SessionId);
        string jsonString = JsonUtility.ToJson(remoteData);
        // chris changes 06/05  s_ConversationRemoteDataCallback?.Invoke(jsonString);

        // data reated things
/*        GuideManager.s_ConversationSaveDataCallback?.Invoke(remoteData);*/
        m_CurrentData.SequenceTimer = 0;
    }

    #endregion

    #region Public_Methods
    public void UpdateScore(string jsonScore)
    {
        ConversationScoreData data = JsonUtility.FromJson<ConversationScoreData>(jsonScore);
        if(m_CurrentData.Score.Count < data.conversationIndex)
        {
            m_CurrentData.Score.Add(data.score);
        }
      // m_CurrentData.Score.Add(score);

    }
       
    public void UpdateConversationData(int score, int sequence)
    {
        Debug.Log("The conversation data is updated");
        m_CurrentData.Score.Add(score);
        SequenceCurrent = sequence;
    }
    #endregion

    #region Private_Methods


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
