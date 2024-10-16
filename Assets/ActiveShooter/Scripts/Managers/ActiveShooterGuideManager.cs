using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class GuideAudio
{
    public string AudioKey;
    public AudioClip AudioClip;
}

public class ActiveShooterGuideManager : MonoBehaviour
{
    #region Private_Vars
    private AudioSource m_AudioSource;
    #endregion
    #region Public_Vars
    public static ActiveShooterGuideManager Instance;
    public static Action s_QuestionEnds;
    public static Action<bool> s_ActivateObserverOptions;
    public static Action s_OnObserverAnswerEnds;
    public static Action s_DeactiveObservationCallback;
    public static Action s_DeactiveConversation;

    public static Action s_InitialTeleportationCallback;
    public static Action<int> s_ConversationCallback;
    public static Action<PlayableAsset> s_UpdateTimeline;

    public static Action<Action> s_ObservationDotsCallbacks;
    public static Action<bool> s_MicOptionCallback;
    public static Action s_StopSpeakerCallback;

    public static Action s_TeleportCallback;
    public static Action s_TeleportObservationReset;
    public static Action s_TeleportResetCallback;
    [SerializeField]
    private bool m_TeleportationSequenecEnded = false;

    [SerializeField]
    private PlayableAsset[] m_WomenConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_ManConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_BystanderConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_ScaredBystanderConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_Bystander1ConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_Bystander2ConversationTimeline;
    [SerializeField]
    private PlayableAsset[] m_Victim1Timeline;
    [SerializeField]
    private PlayableAsset[] m_Victim2Timeline;
    [SerializeField]
    private PlayableAsset[] m_FleeTimeline;
    [SerializeField]
    private PlayableAsset[] m_InsisTimeline;






    [SerializeField]
    private List<GuideAudio> guideAudios = new List<GuideAudio>();
    [SerializeField]
    private bool m_IsOptionMatched;

    [SerializeField]
    private GameObject[] m_ObservationWayPoints;
    [SerializeField]
    private int m_ObservationPoints2_2;
    public static Action<bool> s_TeleportReset;

    #endregion

    #region Unity_Callbacks

    private void Awake()
    {
        Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    #endregion


    #region Public_Methods
    public void SetObservationSounds(string animationState, bool isQuestion = false, bool isAnswer = false, bool isTeleport = false, bool isStance = false)
    {
        if (String.IsNullOrEmpty(animationState) || animationState == "")
            return;
        m_AudioSource.Stop();
        AudioClip currentAudioClip = guideAudios.Find(element => element.AudioKey == animationState).AudioClip;
        if (currentAudioClip != null)
        {
            m_AudioSource.clip = currentAudioClip;
            m_AudioSource.Play();
        }
        else
            return;
        if (isQuestion)
            OnObservationQuestionStarts();
        if (isAnswer)
        {
            Utilities.ExecuteAfterDelay(currentAudioClip.length + 1, OnObservationAnswerEnds);
        }
        if (isTeleport)
        {

        }

    }

    public void UpdateGuideAudio(AudioClip clip)
    {
        m_AudioSource.Stop();
        if (clip == null)
            return;
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
    public void DeactiveConversation(AudioSource source)
    {
        Utilities.ExecuteAfterDelay(source.clip.length, () =>
        {
            s_DeactiveConversation?.Invoke();
        });

    }

    public void OnObservationContinueButtonTap()
    {
        int dots = 0;
        Action dotsCallback = () =>
        {
            dots++;
            Debug.Log("The dots triggerd:");
        };

        s_ObservationDotsCallbacks?.Invoke(dotsCallback);
        Utilities.ExecuteAfterDelay(1f, () =>
        {
            if (dots <= 0)
            {
                s_DeactiveObservationCallback?.Invoke();

            }
        });
    }

    public void CheckFleeConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        Debug.Log("The Flee conversation triggered" + (conversationIndex + 1));
        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_FleeTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);
    }
    public void CheckInsisConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        Debug.Log("The Insis conversation triggered" + (conversationIndex + 1));
        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_InsisTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);
    }

    public void CheckWomenConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();
        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_WomenConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);
    }

    public void CheckBystanderConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        Debug.Log("The bystander conversation triggered" + (conversationIndex + 1));
        s_UpdateTimeline?.Invoke(m_BystanderConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckBystander1Conversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        Debug.Log("The bystander conversation triggered" + (conversationIndex + 1));
        s_UpdateTimeline?.Invoke(m_Bystander1ConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }
    public void CheckBystander2Conversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        Debug.Log("The bystander conversation triggered" + (conversationIndex + 1));
        s_UpdateTimeline?.Invoke(m_Bystander2ConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckVictim1Conversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        Debug.Log("The Victim1 conversation triggered" + (conversationIndex + 1));
        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_Victim1Timeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckVictim2Conversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        Debug.Log("The Victim1 conversation triggered" + (conversationIndex + 1));
        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_Victim2Timeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckScaredBystanderConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        Debug.Log("The bystander conversation triggered" + (conversationIndex + 1));
        s_UpdateTimeline?.Invoke(m_ScaredBystanderConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckManConversation(int conversationIndex)
    {
        s_StopSpeakerCallback?.Invoke();

        m_IsOptionMatched = true;
        CheckConversationOptionStatus();
        s_UpdateTimeline?.Invoke(m_ManConversationTimeline[conversationIndex]);
        s_ConversationCallback?.Invoke(conversationIndex);

    }

    public void CheckConversationOptionStatus()
    {
        if (m_IsOptionMatched)
        {
            m_IsOptionMatched = false;
            s_MicOptionCallback?.Invoke(true);
        }

    }

    public void OnObservationTeleportationContinueTap(int observationIndex)
    {
        int dots = 0;
        Action dotsCallback = () =>
        {
            dots++;
            Debug.Log("The dots triggerd:");
        };

        s_ObservationDotsCallbacks?.Invoke(dotsCallback);
        Utilities.ExecuteAfterDelay(1f, () =>
        {

            if (dots <= 0)
            {
                m_ObservationWayPoints[observationIndex].SetActive(false);
                m_ObservationPoints2_2++;
                if (m_ObservationPoints2_2 < 4)
                {
                    Debug.Log("Teleport Test 1");
                    //we can have any possible issue here, check if you got any bugs related to the observation points
                    s_TeleportReset?.Invoke(true);
                    Utilities.ExecuteAfterDelay(0.1f, () =>
                    {
                        s_TeleportCallback?.Invoke();
                    });

                }
                else
                {
                    Debug.Log("Teleport Test 2");
                    /* ActiveShooterSequenceManager.s_PlayerPositionCallback?.Invoke(m_CentralHub2_2.position);
                     ActiveShooterSequenceManager.s_PlayerRotationCallback?.Invoke(m_CentralHub2_2.rotation);*/
                    s_TeleportObservationReset?.Invoke();
                    s_DeactiveObservationCallback?.Invoke();
                }
            }
        });
    }


    public void OnTeleportContinueTap()
    {
        int dots = 0;
        Action dotsCallback = () =>
        {
            Debug.Log("The dots callback: " + dots);
            dots++;
        };

        s_ObservationDotsCallbacks?.Invoke(dotsCallback);

        Utilities.ExecuteAfterDelay(1f, () =>
        {
            if (dots <= 0 || m_TeleportationSequenecEnded)
            {
                Debug.Log("The deactivation called");
                //StartSequence6();
                s_DeactiveObservationCallback?.Invoke();
            }
        });

    }

    public void OnTeleportEnds()
    {
        s_InitialTeleportationCallback?.Invoke();

        s_TeleportCallback?.Invoke();

    }

    public void OnMainTeleportRecieved()
    {
        Debug.Log("Log Check 2");
        m_TeleportationSequenecEnded = true;
        s_TeleportResetCallback?.Invoke();
    }


    public void OnObservationAnswerEnds()
    {
        s_OnObserverAnswerEnds?.Invoke();
    }
    public void OnObservationQuestionStarts()
    {
        s_ActivateObserverOptions?.Invoke(true);
    }


    public void OnQuestionEnds()
    {
        s_QuestionEnds?.Invoke();
    }

    #endregion
}
