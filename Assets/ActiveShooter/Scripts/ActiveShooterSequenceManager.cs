
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SequenceDetails
{
    public AudioClip Clip;
    public GameObject Sequence;
}

public class ActiveShooterSequenceManager : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private Transform m_IntroWayPoint;
    [SerializeField]
    private SequenceDetails m_Intro0_1;
    [SerializeField]
    private SequenceDetails m_Intro0_2;
    [SerializeField]
    private SequenceDetails m_Intro0_3;
    [SerializeField]
    private Transform m_Sequence1WayPoint;
    [SerializeField]
    private PlayableAsset m_Sequence1;
    [SerializeField]
    private SequenceDetails m_Seq1_1_1;
    [SerializeField]
    private SequenceDetails m_Seq1_1_2;
    [SerializeField]
    private SequenceDetails m_Seq1_2_1;
    [SerializeField]
    private SequenceDetails m_Seq1_3_1;
    [SerializeField]
    private SequenceDetails m_Seq1_3_2;//audio only
    [SerializeField]
    private SequenceDetails m_Seq1_3_3;// panel only
    [SerializeField]
    private SequenceDetails m_Seq1_4_2;// observation
    [SerializeField]
    private SequenceDetails m_Seq1_5_1;// Teleportatiom
    [SerializeField]
    private SequenceDetails m_Seq1_6_1;// observation
    [SerializeField]
    private PlayableAsset m_Sequence7;
    [SerializeField]
    private SequenceDetails m_Seq1_7_1;// conversation
    [SerializeField]
    private SequenceDetails m_Seq1_7_2;// conversation
    [SerializeField]
    private SequenceDetails m_Seq1_7_3;// conversation
    [SerializeField]
    private SequenceDetails m_Seq1_8;// radio
    [SerializeField]
    private PlayableAsset m_Sequence1_9;
    [SerializeField]
    private SequenceDetails m_Seq1_9;// Progress
    [SerializeField]
    private SequenceDetails m_Seq1_10;// Options
    [SerializeField]
    private SequenceDetails m_Seq1_11_1;// Timeline
    [SerializeField]
    private SequenceDetails m_Seq1_11_2;//radio
    [SerializeField]
    private PlayableAsset m_Sequence11_1;
    [SerializeField]
    private SequenceDetails m_Seq1_12_1;//conversation
    [SerializeField]
    private SequenceDetails m_Seq1_12_2;//conversation
    [SerializeField]
    private SequenceDetails m_Seq1_12_3;//conversation
    [SerializeField]
    private PlayableAsset m_Sequence13_1;
    [SerializeField]
    private SequenceDetails m_Seq1_13_1;//TimelineChanges
    [SerializeField]
    private SequenceDetails m_Seq1_13_2;//Observation
    [SerializeField]
    private PlayableAsset m_BystanderLeave;
    [SerializeField]
    private SequenceDetails m_Seq1_13_4;//conversation

    [SerializeField]
    private SequenceDetails m_Seq1_13_5_1;//conversation
    [SerializeField]
    private SequenceDetails m_Seq1_13_5_2;//conversation

    [SerializeField]
    private SequenceDetails m_Seq1_14;//radio
    [SerializeField]
    private SequenceDetails m_Seq1_14_1;
    [SerializeField]
    private SequenceDetails m_Seq1_14_2;
    [SerializeField]
    private SequenceDetails m_Seq1_14_3;
    [SerializeField]
    private SequenceDetails m_Seq1_15;
    #endregion

    #region Public_Vars
    public static Action<AudioClip> s_GuideAudioClip;
    public static Action s_GuideAudiolessClip;
    public static Action s_ConversationCallback;
    public static Action<Vector3> s_PlayerPositionCallback;
    public static Action<Quaternion> s_PlayerRotationCallback;
    public static Action<PlayableAsset> s_UpdateTimelineCall;
    public static Action s_PauseTimelineCall;





    #endregion

    #region Unity_Callbacks

    private void Start()
    {
        StartInitialSequence();
    }
    private void OnEnable()
    {
        ActiveShooterGuideManager.s_InitialTeleportationCallback += InitialTeleportationCallback;
        GuidePanelHandler.s_AutomatedFunctionCallback += OnAutomatedCallback;
    }

    private void OnDisable()
    {
        ActiveShooterGuideManager.s_InitialTeleportationCallback -= InitialTeleportationCallback;
        GuidePanelHandler.s_AutomatedFunctionCallback -= OnAutomatedCallback;

    }


    #endregion

    #region Private_Methods
    private void OnAutomatedCallback(string functionName)
    {
        Invoke(functionName, 0f);
    }
    private void InitialTeleportationCallback()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence1WayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence1WayPoint.rotation);
    }
    private void StartInitialSequence()
    {
        ActiveShooterManager.Instance.StartTimer();
        s_PlayerPositionCallback?.Invoke(m_IntroWayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_IntroWayPoint.rotation);
        Utilities.ExecuteAfterDelay(Constants.INITIAL_DELAY, () =>
        {
            s_GuideAudioClip?.Invoke(m_Intro0_1.Clip);
            m_Intro0_1.Sequence.SetActive(true);
        });
    }

    #endregion

    #region Public_Methods

    public void ActivateConversation()
    {
        s_ConversationCallback?.Invoke();
    }

    public void StartIntroSequence0_2()
    {
        m_Intro0_1.Sequence.SetActive(false);
        m_Intro0_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Intro0_2.Clip);
    }

    public void StartIntroSequence0_3()
    {

        m_Intro0_2.Sequence.SetActive(false);
        m_Intro0_3.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Intro0_3.Clip);
    }

    public void StartSequence1_1()
    {
        s_UpdateTimelineCall?.Invoke(m_Sequence1);
        s_PlayerPositionCallback?.Invoke(m_Sequence1WayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence1WayPoint.rotation);
        m_Intro0_3.Sequence.SetActive(false);
        m_Seq1_1_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_1_1.Clip);
    }

    public void StartProgressUI1_2()
    {
        m_Seq1_1_1.Sequence.SetActive(false);
        m_Seq1_1_2.Sequence.SetActive(true);
        Utilities.ExecuteAfterDelay(3f, StartSequence2_1);
    }

    public void StartSequence2_1()
    {
        m_Seq1_1_2.Sequence.SetActive(false);
        m_Seq1_2_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_2_1.Clip);
    }

    public void StartSequence3_1()
    {
        m_Seq1_2_1.Sequence.SetActive(false);
        m_Seq1_3_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_3_1.Clip);
    }

    public void StartSequence3_2()
    {
        m_Seq1_3_1.Sequence.SetActive(false);
        s_GuideAudioClip?.Invoke(m_Seq1_3_2.Clip);
        Utilities.ExecuteAfterDelay(m_Seq1_3_2.Clip.length, StartSequnce3_3);
    }

    public void StartSequnce3_3()
    {
        m_Seq1_3_3.Sequence.SetActive(true);
        Utilities.ExecuteAfterDelay(Constants.PROGRESS_DELAY, StartSequnce4_1);
    }
    public void StartSequnce4_1()
    {
        s_PauseTimelineCall?.Invoke();
        m_Seq1_3_3.Sequence.SetActive(false);
        m_Seq1_4_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_4_2.Clip);

        // pause the timeline here
    }

    public void StartSequence5_1()
    {



        m_Seq1_4_2.Sequence.SetActive(false);
        m_Seq1_5_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_5_1.Clip);
    }

    public void StartSequence6_1()
    {
        m_Seq1_4_2.Sequence.SetActive(false);
        m_Seq1_5_1.Sequence.SetActive(false);
        m_Seq1_6_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_6_1.Clip);
    }

    public void StartSequence7_1()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(true);
        /* s_PlayerPositionCallback?.Invoke(m_Sequence1WayPoint.position);
         s_PlayerRotationCallback?.Invoke(m_Sequence1WayPoint.rotation);*/
        s_UpdateTimelineCall?.Invoke(m_Sequence7);
        m_Seq1_6_1.Sequence.SetActive(false);
        m_Seq1_7_1.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence7_2()
    {



        m_Seq1_7_1.Sequence.SetActive(false);
        m_Seq1_7_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence7_3()
    {



        m_Seq1_7_1.Sequence.SetActive(false);
        m_Seq1_7_2.Sequence.SetActive(false);
        m_Seq1_7_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence8()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(false);
        m_Seq1_7_1.Sequence.SetActive(false);
        m_Seq1_7_2.Sequence.SetActive(false);
        m_Seq1_7_3.Sequence.SetActive(false);
        m_Seq1_8.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_8.Clip);
    }

    public void StartSequence9()
    {

      /*  s_PlayerPositionCallback?.Invoke(m_Sequence1WayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence1WayPoint.rotation);*/

        s_UpdateTimelineCall?.Invoke(m_Sequence1_9);
        m_Seq1_8.Sequence.SetActive(false);
        m_Seq1_9.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_9.Clip);


    }

    public void StartSequence10()
    {
        s_PauseTimelineCall?.Invoke();
        m_Seq1_8.Sequence.SetActive(false);
        m_Seq1_9.Sequence.SetActive(false);
        m_Seq1_10.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_10.Clip);
    }

    public void StartSequence11_1()
    {
        s_UpdateTimelineCall?.Invoke(m_Sequence11_1);
    }

    public void StartSequence11_2()
    {
        m_Seq1_11_1.Sequence.SetActive(false);
        m_Seq1_11_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_11_2.Clip);

    }

    public void StartSequence12_1()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(true);
        m_Seq1_11_1.Sequence.SetActive(false);
        m_Seq1_11_2.Sequence.SetActive(false);
        m_Seq1_12_1.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }
    public void StartSequence12_2()
    {
        m_Seq1_12_1.Sequence.SetActive(false);
        m_Seq1_12_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence12_3()
    {
        m_Seq1_12_1.Sequence.SetActive(false);
        m_Seq1_12_2.Sequence.SetActive(false);
        m_Seq1_12_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }
    public void StartSequence13_1()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(false);
        m_Seq1_12_1.Sequence.SetActive(false);
        m_Seq1_12_2.Sequence.SetActive(false);
        m_Seq1_12_3.Sequence.SetActive(false);


        s_UpdateTimelineCall?.Invoke(m_Sequence13_1);
        m_Seq1_13_1.Sequence.SetActive(true);
    }

    public void StartSequence13_2()
    {
        m_Seq1_13_1.Sequence.SetActive(false);
        m_Seq1_13_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_13_2.Clip);

    }


    public void StartSequence13_2(AudioSource source)
    {
        Utilities.ExecuteAfterDelay(source.clip.length, () =>
        {
            m_Seq1_13_1.Sequence.SetActive(false);
            m_Seq1_13_2.Sequence.SetActive(true);
            s_GuideAudioClip?.Invoke(m_Seq1_13_2.Clip);
        });
    }

    public void StartSequence13_4()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(true);
        m_Seq1_13_2.Sequence.SetActive(false);
        m_Seq1_13_4.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void OnBystanderLeave()
    {
        m_Seq1_13_4.Sequence.SetActive(false);
      
        Utilities.ExecuteAfterDelay(6f, () =>
        {
            s_UpdateTimelineCall?.Invoke(m_BystanderLeave);
            
        });

    }
    public void OnBystanderLeaved(AudioSource source)
    {
        Utilities.ExecuteAfterDelay(source.clip.length, StartSequence13_5_1);
    }

    public void StartSequence13_5_1()
    {
        m_Seq1_13_2.Sequence.SetActive(false);
        m_Seq1_13_4.Sequence.SetActive(false);
        m_Seq1_13_5_1.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();
    }

   

    public void StartSequence13_5_2()
    {
        m_Seq1_13_5_1.Sequence.SetActive(false);
        m_Seq1_13_5_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence14()
    {
        ActiveShooterManager.Instance.ToggleResponseHandler(false);
        m_Seq1_13_5_1.Sequence.SetActive(false);
        m_Seq1_13_5_2.Sequence.SetActive(false);
        m_Seq1_14.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_14.Clip);
        
    }

    public void StartSequence14_1()
    {
       
        m_Seq1_14.Sequence.SetActive(false);
        m_Seq1_14_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_14_1.Clip);
    }

    public void StartSequence14_2()
    {
        ActiveShooterManager.Instance.StopTimer();
        m_Seq1_14.Sequence.SetActive(false);
        m_Seq1_14_1.Sequence.SetActive(false);
        m_Seq1_14_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_14_2.Clip);
    }
    public void StartSequence14_3()
    {
        m_Seq1_14.Sequence.SetActive(false);
        m_Seq1_14_1.Sequence.SetActive(false);
        m_Seq1_14_2.Sequence.SetActive(false);
        m_Seq1_14_3.Sequence.SetActive(true);
    }   


    public void StartSequence15()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence1WayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence1WayPoint.rotation);
        m_Seq1_14_3.Sequence.SetActive(false);
        m_Seq1_15.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_15.Clip);
    }


    #endregion

}
