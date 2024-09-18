using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoSpeak : MonoBehaviour
{
    #region Private_Vars
   
    [SerializeField]
    private bool m_IsActivated;
    [SerializeField]
    private AudioClip[] m_TurnSounds;
    [SerializeField]
    private Image m_SpeakerImage;
    [SerializeField]
    private Sprite m_Wrong;
    [SerializeField]
    private Sprite m_Correct;
    [SerializeField]
    private Sprite m_Normal;
    [SerializeField]
    private AudioClip m_IntroSound;
    #endregion

    #region Unity_Callbacks
    
    private void OnEnable()
    {
        ActiveShooterGuideManager.Instance.UpdateGuideAudio(m_IntroSound);
        ActiveShooter_ConversationPanel.s_NoResponseCall += OnNoResponseCall;
        ActiveShooterGuideManager.s_MicOptionCallback += OnMicCallback;
        ActiveShooterGuideManager.s_StopSpeakerCallback += StopSpeaker;
    }
    private void OnDisable()
    {
        ActiveShooter_ConversationPanel.s_NoResponseCall -= OnNoResponseCall;
        ActiveShooterGuideManager.s_MicOptionCallback -= OnMicCallback;
        ActiveShooterGuideManager.s_StopSpeakerCallback -= StopSpeaker;

    }
    #endregion

    #region Private_Methods
    private void StopSpeaker()
    {
        AudioClip clip = null;
        ActiveShooterGuideManager.Instance.UpdateGuideAudio(clip);
    }
    private void OnMicCallback(bool status)
    {
        m_IsActivated = true;
        m_SpeakerImage.sprite = m_Correct;

        Utilities.ExecuteAfterDelay(2f, () =>
        {
            m_SpeakerImage.sprite = m_Normal;
        });
    }
    private void OnNoResponseCall(int turn)
    {
        if (turn > m_TurnSounds.Length)
        {
            return;
        }
        Utilities.ExecuteAfterDelay(1f, () =>
        {
            if (!m_IsActivated)
            {
                AudioClip clip = m_TurnSounds[turn - Constants.TURN_1];
               ActiveShooterGuideManager.Instance.UpdateGuideAudio(clip);
                m_SpeakerImage.sprite = m_Wrong;

                Utilities.ExecuteAfterDelay(2f, () =>
                {
                    m_SpeakerImage.sprite = m_Normal;
                });
            }
        });



    }

    #endregion
}
