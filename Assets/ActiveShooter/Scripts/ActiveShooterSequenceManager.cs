
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

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
    private PlayableAsset m_ManLeaves;

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


    [SerializeField]
    private Transform m_Sequence2_1;

    [SerializeField]
    private SequenceDetails m_Seq2_15_1;
    [SerializeField]
    private SequenceDetails m_Seq2_15_2;
    [SerializeField]
    PlayableAsset m_RunningTimeline;
    [SerializeField]
    private SequenceDetails m_Seq2_15_3;
    [SerializeField]
    private SequenceDetails m_Seq2_16;

    [SerializeField]
    private SequenceDetails m_Seq2_25_1;
    [SerializeField]
    private SequenceDetails m_Seq2_25_2;
    [SerializeField]
    private SequenceDetails m_Seq2_25_3;

    [SerializeField]
    PlayableAsset m_ScaredBystanderLeave;

    [SerializeField]
    private SequenceDetails m_Seq2_26;
    [SerializeField]
    private SequenceDetails m_Seq2_27;
    [SerializeField]
    private SequenceDetails m_Seq2_27_2;

    [SerializeField]
    private SequenceDetails m_Seq2_28;// progress
    [SerializeField]
    private SequenceDetails m_Seq2_28_1;// progress
    [SerializeField]
    private SequenceDetails m_Seq2_28_2;// progress

    [SerializeField]
    private SequenceDetails m_Seq2_29;// progress


    [SerializeField]
    private Transform m_Sequence2_2;

    [SerializeField]
    private SequenceDetails m_Seq2_30;
    [SerializeField]
    private SequenceDetails m_Seq2_31;
    [SerializeField]
    private SequenceDetails m_Seq2_30_2;


    [SerializeField]
    private PlayableAsset m_Sequece28_1;
    [SerializeField]
    private PlayableAsset m_Sequece28_2;


    [SerializeField]
    private SequenceDetails m_Seq2_31_1;
    [SerializeField]
    private SequenceDetails m_Seq2_32;//stance


    [SerializeField]
    private SequenceDetails m_Seq2_33_1;
    [SerializeField]
    private SequenceDetails m_Seq2_33_2;
    [SerializeField]
    private SequenceDetails m_Seq2_33_3;

    [SerializeField]
    private SequenceDetails m_Seq2_38; // radio
    [SerializeField]
    private SequenceDetails m_Seq2_34; // radio

    public Transform m_CentralHub2_2;

    [SerializeField]
    private SequenceDetails m_SequenceTeleportedPoints;
    [SerializeField]
    private GameObject m_SequenceTeleportedWaypoints;

    [SerializeField]
    private SequenceDetails m_Seq2_34_3; // completion
    [SerializeField]
    private SequenceDetails m_Seq2_37; // completion



    [SerializeField]
    private SequenceDetails m_Seq2_35_3_1;
    [SerializeField]
    private SequenceDetails m_Seq2_35_3_2;
    [SerializeField]
    private SequenceDetails m_Seq2_35_3_3;
    [SerializeField]
    private SequenceDetails m_Seq2_36_3;

    [SerializeField]
    private PlayableAsset m_Bystander1Leave;
    [SerializeField]
    private PlayableAsset m_Bystander2Leave;

    [SerializeField]
    private Transform m_Sequence3_1Waypoint;


    [SerializeField]
    private SequenceDetails m_Seq3_38_2;

    [SerializeField]
    private SequenceDetails m_Seq3_38_3;
    [SerializeField]
    private PlayableAsset m_Sequence3Leaving;
    [SerializeField]
    private SequenceDetails m_Seq3_38_4;
    [SerializeField]
    private SequenceDetails m_Seq3_40;

    [SerializeField]
    private SequenceDetails m_Seq3_4Option3;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option3Observation;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option3LastOption;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option3Radio;


    [SerializeField]
    private SequenceDetails m_Seq3_4Option1;
    [SerializeField]
    private PlayableAsset m_Victim1Intro;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option1Conversation1;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option1Conversation2;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option1Conversation3;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option1LastOption;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option1Radio;



    [SerializeField]
    private PlayableAsset m_Victim2Intro;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2Conversation1;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2Conversation2;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2Conversation3;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2LastOption;
    [SerializeField]
    private SequenceDetails m_Seq3_4Option2Radio;

    [SerializeField]
    private Transform m_Sequence3_2Waypoint;

    [SerializeField]
    private SequenceDetails m_Seq3_43;
    [SerializeField]
    private PlayableAsset m_LadyLeaving;
    [SerializeField]
    private SequenceDetails m_Seq3_44_2;
    [SerializeField]
    private SequenceDetails m_Seq3_45_2;
    [SerializeField]
    private SequenceDetails m_Seq3_46;
    [SerializeField]
    private Transform m_Sequence3_2FleePoint;
    [SerializeField]
    private Transform m_Sequence3_2InsisPoint;

    [SerializeField]
    private SequenceDetails m_Seq3_46Conversation1;
    [SerializeField]
    private SequenceDetails m_Seq3_46Conversation2;
    [SerializeField]
    private SequenceDetails m_Seq3_46Conversation3;
    [SerializeField]
    PlayableAsset m_FleeReset;
    [SerializeField]
    private SequenceDetails m_Seq3_46Radio;

    [SerializeField]
    private SequenceDetails m_Seq3_47RadioPlaypack;
    [SerializeField]
    private PlayableAsset m_InsisiInitial;
    [SerializeField]
    private SequenceDetails m_Seq3_47Options;
    [SerializeField]
    private SequenceDetails m_Seq3_47Conversation1;
    [SerializeField]
    private SequenceDetails m_Seq3_47Conversation2;
    [SerializeField]
    private SequenceDetails m_Seq3_47Conversation3;
    [SerializeField]
    private SequenceDetails m_Seq3_47Radio;
    [SerializeField]
    private Transform m_CarPoint;
    [SerializeField]
    private SequenceDetails m_Seq3_48Tele;






    [SerializeField]
    private int m_3_2Radios;

    #endregion

    #region Public_Vars
    public static Action<AudioClip> s_GuideAudioClip;
    public static Action s_GuideAudiolessClip;
    public static Action s_ConversationCallback;
    public static Action<Vector3> s_PlayerPositionCallback;
    public static Action<Quaternion> s_PlayerRotationCallback;
    public static Action<PlayableAsset> s_UpdateTimelineCall;
    public static Action s_PauseTimelineCall;
    public static Action s_ResumeTimelineCall;
    public static Action s_PlayGunShotSequence;





    #endregion

    #region Unity_Callbacks

    private void Start()
    {

       /* Action currentAction = null;
        if(PlayerPrefs.GetInt("GameStatus", 0) == 0)
        {
            StartInitialSequence();
        }else if(PlayerPrefs.GetInt("GameStatus", 0) == 1)
        {
            StartSequence15();
        }
        else
        {
            StartSequence37_1();
        }*/
       
       
      
       // StartSequence31_1();
       // StartSequence28_1();      
       // StartSequence1_1(); 
    }
    private void OnEnable()
    {
        ActiveShooterGuideManager.s_InitialTeleportationCallback += InitialTeleportationCallback;
        GuidePanelHandler.s_AutomatedFunctionCallback += OnAutomatedCallback;
        TeleportInteractable.s_TeleportReset += Reset2_2Teleportation;
        ActiveShooterGuideManager.s_TeleportReset += Reset2_2Teleportation;
        ActiveShooterGuideManager.s_TeleportObservationReset += ObservationTeleportationCompleted;
        CarTeleportedPoints.s_CarResetCall += ResetCarPoint;
    }

    private void OnDisable()
    {
        ActiveShooterGuideManager.s_InitialTeleportationCallback -= InitialTeleportationCallback;
        GuidePanelHandler.s_AutomatedFunctionCallback -= OnAutomatedCallback;
        TeleportInteractable.s_TeleportReset -= Reset2_2Teleportation;
        ActiveShooterGuideManager.s_TeleportReset -= Reset2_2Teleportation;
        ActiveShooterGuideManager.s_TeleportObservationReset -= ObservationTeleportationCompleted;
        CarTeleportedPoints.s_CarResetCall -= ResetCarPoint;


    }


    private void Update()
    {
        /*if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Test1");
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Test2");
        }*/
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

    public void ReloadScene()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }

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
        // Utilities.ExecuteAfterDelay(3f, StartSequence2_1);
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
        //skchanges Utilities.ExecuteAfterDelay(Constants.PROGRESS_DELAY, StartSequnce4_1);
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


    }

    public void StartSequence7_1_1(AudioSource source)
    {
        Utilities.ExecuteAfterDelay(source.clip.length, () =>
        {
            m_Seq1_7_1.Sequence.SetActive(true);
            s_GuideAudiolessClip?.Invoke();
        });

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
        s_PauseTimelineCall?.Invoke();
        m_Seq1_11_1.Sequence.SetActive(false);
        m_Seq1_11_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_11_2.Clip);

    }

    public void StartSequence12_1()
    {
        s_ResumeTimelineCall?.Invoke();
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
        s_UpdateTimelineCall?.Invoke(m_ManLeaves);
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
        s_GuideAudiolessClip?.Invoke();
    }
    public void StartSequence14_3()
    {
        m_Seq1_14.Sequence.SetActive(false);
        m_Seq1_14_1.Sequence.SetActive(false);
        m_Seq1_14_2.Sequence.SetActive(false);
        m_Seq1_14_3.Sequence.SetActive(true);
        PlayerPrefs.SetInt("GameStatus", 1);
    }


    public void StartSequence15()
    {
        s_PlayerPositionCallback?.Invoke(m_IntroWayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_IntroWayPoint.rotation);

        m_Seq1_14_3.Sequence.SetActive(false);
        m_Seq1_15.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq1_15.Clip);
    }

    public void StartSequence15_1()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence2_1.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence2_1.rotation);

        m_Seq1_15.Sequence.SetActive(false);
        m_Seq2_15_1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_15_1.Clip);
    }


    public void StartSequence15_2()
    {
        m_Seq2_15_1.Sequence.SetActive(false);
        m_Seq2_15_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void ActivatePeopleTimeLine()
    {
        m_Seq2_15_2.Sequence.SetActive(false);
        s_UpdateTimelineCall?.Invoke(m_RunningTimeline);
    }

    public void StartSequence15_3()
    {
        s_PauseTimelineCall?.Invoke();
        m_Seq2_15_2.Sequence.SetActive(false);
        m_Seq2_15_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence16()
    {
        s_ResumeTimelineCall?.Invoke();
        m_Seq2_15_2.Sequence.SetActive(false);
        m_Seq2_15_3.Sequence.SetActive(false);
        m_Seq2_16.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_16.Clip);
    }

    public void StartSequence25_1()
    {
        m_Seq2_15_2.Sequence.SetActive(false);
        m_Seq2_15_3.Sequence.SetActive(false);
        m_Seq2_16.Sequence.SetActive(false);
        m_Seq2_25_1.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence25_2()
    {

        m_Seq2_25_1.Sequence.SetActive(false);
        m_Seq2_25_2.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence25_3()
    {

        m_Seq2_25_1.Sequence.SetActive(false);
        m_Seq2_25_2.Sequence.SetActive(false);
        m_Seq2_25_3.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence26()
    {
        s_UpdateTimelineCall?.Invoke(m_ScaredBystanderLeave);
        m_Seq2_25_1.Sequence.SetActive(false);
        m_Seq2_25_2.Sequence.SetActive(false);
        m_Seq2_25_3.Sequence.SetActive(false);
        m_Seq2_26.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_26.Clip);
    }


    public void StartSequence27()
    {

        m_Seq2_26.Sequence.SetActive(false);
        m_Seq2_27.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_27.Clip);
    }

    public void StartSequence27_2()
    {

        m_Seq2_26.Sequence.SetActive(false);
        m_Seq2_27.Sequence.SetActive(false);
        m_Seq2_27_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }


    public void StartSequence28()
    {


        m_Seq2_27.Sequence.SetActive(false);
        m_Seq2_27_2.Sequence.SetActive(false);
        m_Seq2_28.Sequence.SetActive(true);


        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence28_1()
    {
        m_Seq2_28.Sequence.SetActive(false);
        s_PlayerPositionCallback?.Invoke(m_Sequence2_2.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence2_2.rotation);
        s_UpdateTimelineCall?.Invoke(m_Sequece28_1);

        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence28_2()
    {
        s_PlayGunShotSequence?.Invoke();
        s_UpdateTimelineCall?.Invoke(m_Sequece28_2);


    }

    public void StartSequence29()
    {
        m_Seq2_28_1.Sequence.SetActive(false);
        m_Seq2_28_2.Sequence.SetActive(false);
        s_PauseTimelineCall?.Invoke();

        m_Seq2_29.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_29.Clip);

    }

    public void StartSequence30()
    {
        m_Seq2_29.Sequence.SetActive(false);
        m_Seq2_30.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_30.Clip);

    }

    public void StartSequence31()
    {

        m_Seq2_30.Sequence.SetActive(false);
        m_Seq2_31.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_31.Clip);

    }

    public void StartSequence30_2()
    {


        m_Seq2_31.Sequence.SetActive(false);
        m_Seq2_30_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq2_30_2.Clip);


    }

    public void StartSequence31_1()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence2_2.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence2_2.rotation);


        m_Seq2_31.Sequence.SetActive(false);
        m_Seq2_30_2.Sequence.SetActive(false);

        m_Seq2_31_1.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq2_31_1.Clip);


    }

    public void StartSequence32()
    {
        m_Seq2_31.Sequence.SetActive(false);
        m_Seq2_30_2.Sequence.SetActive(false);

        m_Seq2_31_1.Sequence.SetActive(false);
        m_Seq2_32.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq2_32.Clip);


    }

    public void StartSequence33_1()
    {
        m_Seq2_31.Sequence.SetActive(false);
        m_Seq2_30_2.Sequence.SetActive(false);
        m_Seq2_31_1.Sequence.SetActive(false);
        m_Seq2_32.Sequence.SetActive(false);
        m_Seq2_33_1.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();


    }

    public void StartSequence33_2()
    {
        m_Seq2_33_1.Sequence.SetActive(false);
        m_Seq2_33_2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }
    public void StartSequence33_3()
    {
        m_Seq2_33_1.Sequence.SetActive(false);
        m_Seq2_33_2.Sequence.SetActive(false);
        m_Seq2_33_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence38()
    {
        m_Seq2_33_1.Sequence.SetActive(false);
        m_Seq2_33_2.Sequence.SetActive(false);
        m_Seq2_33_3.Sequence.SetActive(false);
        m_Seq2_38.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq2_38.Clip);

    }

    public void StartSequence34()
    {
      
        m_Seq2_38.Sequence.SetActive(false);
        m_Seq2_34.Sequence.SetActive(true);
       
        s_GuideAudioClip?.Invoke(m_Seq2_34.Clip);

    }

    public void StartInitialSequence34CentralHub()
    {
        m_Seq2_34.Sequence.SetActive(false);
        m_SequenceTeleportedPoints.Sequence.SetActive(true);
        m_SequenceTeleportedWaypoints.SetActive(true);

        s_GuideAudiolessClip?.Invoke();
        
    }

    public void Reset2_2Teleportation(bool status)
    {
        m_SequenceTeleportedPoints.Sequence.SetActive(status);
        m_SequenceTeleportedWaypoints.SetActive(status);
       
        if (status)
        {
            s_PlayerPositionCallback?.Invoke(m_CentralHub2_2.position);
            s_PlayerRotationCallback?.Invoke(m_CentralHub2_2.rotation);
        }
    }

    public void ObservationTeleportationCompleted()
    {
        s_PlayerPositionCallback?.Invoke(m_CentralHub2_2.position);
        s_PlayerRotationCallback?.Invoke(m_CentralHub2_2.rotation);
        StartSequence34_3();
    }

    private void StartSequence34_3()
    {
        m_Seq2_34_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }




    public void StartSequence35_3_1()
    {
        m_Seq2_34_3.Sequence.SetActive(false);
      
        m_Seq2_35_3_1.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();


    }

    public void StartSequence35_3_2()
    {
        m_Seq2_35_3_1.Sequence.SetActive(false);
        m_Seq2_35_3_2.Sequence.SetActive(true);

        s_GuideAudiolessClip?.Invoke();

    }
    public void StartSequence35_3_3()
    {
        m_Seq2_35_3_1.Sequence.SetActive(false);
        m_Seq2_35_3_2.Sequence.SetActive(false);
        m_Seq2_35_3_3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence36_3()
    {
        s_UpdateTimelineCall?.Invoke(m_Bystander1Leave);
        Utilities.ExecuteAfterDelay(7f, () =>
        {
            m_Seq2_35_3_1.Sequence.SetActive(false);
            m_Seq2_35_3_2.Sequence.SetActive(false);
            m_Seq2_35_3_3.Sequence.SetActive(false);
            m_Seq2_36_3.Sequence.SetActive(true);
            s_GuideAudiolessClip?.Invoke();
        });
        

    }

    public void UpdateBystandert2LeavingTimeLine()
    {
        s_UpdateTimelineCall?.Invoke(m_Bystander2Leave);
    }

    public void StartSequence37_1()  // Module Selection
    {
        PlayerPrefs.SetInt("GameStatus", 2);
        s_PlayerPositionCallback?.Invoke(m_IntroWayPoint.position);
        s_PlayerRotationCallback?.Invoke(m_IntroWayPoint.rotation);

        m_Seq2_37.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq2_37.Clip);
    }

    // Perspective 3 started

    public void StartSequence38_2()
    {
        s_UpdateTimelineCall?.Invoke(m_Sequence3Leaving);
        s_PlayerPositionCallback?.Invoke(m_Sequence3_1Waypoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence3_1Waypoint.rotation);
        m_Seq3_38_2.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq3_38_2.Clip);
    }

    public void StartSequence38_3()
    {
        
        m_Seq3_38_2.Sequence.SetActive(false);
        m_Seq3_38_3.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq3_38_3.Clip);
    }

    public void StartSequence38_4()
    {
       
        m_Seq3_38_2.Sequence.SetActive(false);
        m_Seq3_38_3.Sequence.SetActive(false);
        m_Seq3_38_4.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
       
    }

    public void StartSequence40_InitialTeleportation()
    {
        m_Seq3_38_4.Sequence.SetActive(false);
        m_Seq3_40.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence44Option3()
    {
        m_Seq3_40.Sequence.SetActive(false);

        m_Seq3_4Option3.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option3.Clip);
    }

    public void StartSequence44Option3Observation()
    {
        m_Seq3_4Option3.Sequence.SetActive(false);
        m_Seq3_4Option3Observation.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence44Option3LastOption()
    {
        m_Seq3_4Option3.Sequence.SetActive(false);
        m_Seq3_4Option3Observation.Sequence.SetActive(false);
        m_Seq3_4Option3LastOption.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option3LastOption.Clip);
    }

    public void StartSequence44Option3Radio()
    {
        m_Seq3_4Option3.Sequence.SetActive(false);
        m_Seq3_4Option3Observation.Sequence.SetActive(false);
        m_Seq3_4Option3LastOption.Sequence.SetActive(false);
        m_Seq3_4Option3Radio.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }




    public void On3_1RadioTap()
    {
        m_3_2Radios++;
        s_PlayerPositionCallback?.Invoke(m_Sequence3_1Waypoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence3_1Waypoint.rotation);
        Debug.Log("The radio incremented: " + m_3_2Radios);
        if (m_3_2Radios >= Constants.RADIO_COUNT_3_2)
        {
            StartPequence44_2Radio();
        }
        else
        {
            m_Seq3_40.Sequence.SetActive(true);
        }
    }

    public void StartSequence44Option1()
    {
        m_Seq3_40.Sequence.SetActive(false);

        m_Seq3_4Option1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option1.Clip);
    }

    public void StartSequence44Option1Converation()
    {
        s_UpdateTimelineCall?.Invoke(m_Victim1Intro);
        Utilities.ExecuteAfterDelay(12f, () =>
        {
            m_Seq3_4Option1.Sequence.SetActive(false);
            m_Seq3_4Option1Conversation1.Sequence.SetActive(true);
            s_GuideAudiolessClip?.Invoke();
        });
        
    }

    public void StartSequence44Option1Converation2()
    {
        m_Seq3_4Option1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }
    public void StartSequence44Option1Converation3()
    {
        m_Seq3_4Option1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence44Option1LastOption()
    {
        m_Seq3_4Option1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation3.Sequence.SetActive(false);
        m_Seq3_4Option1LastOption.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option1LastOption.Clip);

    }

    public void StartSequence44Option1Radio()
    {
        m_Seq3_4Option1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option1Conversation3.Sequence.SetActive(false);
        m_Seq3_4Option1LastOption.Sequence.SetActive(false);
        m_Seq3_4Option1Radio.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }


    public void StartSequence44Option2()
    {
        m_Seq3_40.Sequence.SetActive(false);

        m_Seq3_4Option2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option2.Clip);
    }


    public void StartSequence44Option2Converation()
    {
        s_UpdateTimelineCall?.Invoke(m_Victim2Intro);
        Utilities.ExecuteAfterDelay(12f, () =>
        {
            m_Seq3_4Option2.Sequence.SetActive(false);
            m_Seq3_4Option2Conversation1.Sequence.SetActive(true);
            s_GuideAudiolessClip?.Invoke();
        });

    }

    public void StartSequence44Option2Converation2()
    {
        m_Seq3_4Option2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence44Option2Converation3()
    {
        m_Seq3_4Option2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence44Option2LastOption()
    {
        m_Seq3_4Option2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation3.Sequence.SetActive(false);
        m_Seq3_4Option2LastOption.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_4Option2LastOption.Clip);

    }
        
    public void StartSequence44Option2Radio()
    {
        m_Seq3_4Option2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation1.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation2.Sequence.SetActive(false);
        m_Seq3_4Option2Conversation3.Sequence.SetActive(false);
        m_Seq3_4Option2LastOption.Sequence.SetActive(false);
        m_Seq3_4Option2Radio.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }



    public void StartPequence44_2Radio()
    {
        m_Seq3_43.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();

    }

    public void StartSequence44_2()
    {

        s_PlayerPositionCallback?.Invoke(m_Sequence3_2Waypoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence3_2Waypoint.rotation);


        m_Seq3_43.Sequence.SetActive(false);
        s_UpdateTimelineCall?.Invoke(m_LadyLeaving);
        Utilities.ExecuteAfterDelay(0.15f, () =>
        {
            s_PauseTimelineCall?.Invoke();
        });
        m_Seq3_44_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_44_2.Clip);
    }

    public void  StartSequence45_2()
    {
        s_ResumeTimelineCall?.Invoke();
        m_Seq3_44_2.Sequence.SetActive(false);
        m_Seq3_45_2.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_45_2.Clip);
    }
    public void StartSequence46()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence3_2FleePoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence3_2FleePoint.rotation);
        m_Seq3_45_2.Sequence.SetActive(false);
        m_Seq3_46.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_46.Clip);
    }


    public void StartSequence46Conversation1()
    {
        m_Seq3_45_2.Sequence.SetActive(false);
        m_Seq3_46.Sequence.SetActive(false);
        m_Seq3_46Conversation1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_46Conversation1.Clip);
    }

    public void StartSequence46Conversation2()
    {
        m_Seq3_46Conversation1.Sequence.SetActive(false);
        m_Seq3_46Conversation2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }
    public void StartSequence46Conversation3()
    {
        m_Seq3_46Conversation1.Sequence.SetActive(false);
        m_Seq3_46Conversation2.Sequence.SetActive(false);
        m_Seq3_46Conversation3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }


    public void StartSequence46Radio()
    {
        s_UpdateTimelineCall?.Invoke(m_FleeReset);
        m_Seq3_46Conversation1.Sequence.SetActive(false);
        m_Seq3_46Conversation2.Sequence.SetActive(false);
        m_Seq3_46Conversation3.Sequence.SetActive(false);
        m_Seq3_46Radio.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_46Radio.Clip);
    }

    public void StartSequence47RadioPlayback()
    {
        s_UpdateTimelineCall?.Invoke(m_InsisiInitial);

        m_Seq3_46Radio.Sequence.SetActive(false);
        m_Seq3_47RadioPlaypack.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }

    public void StartSequence47Options()
    {
        s_PlayerPositionCallback?.Invoke(m_Sequence3_2InsisPoint.position);
        s_PlayerRotationCallback?.Invoke(m_Sequence3_2InsisPoint.rotation);
        m_Seq3_46Radio.Sequence.SetActive(false);
        m_Seq3_47RadioPlaypack.Sequence.SetActive(false);
        m_Seq3_47Options.Sequence.SetActive(true);
        
        s_GuideAudioClip?.Invoke(m_Seq3_47Options.Clip);    
    }

   

    public void StartSequence47Conversation1()
    {
        m_Seq3_47Options.Sequence.SetActive(false);
        m_Seq3_47Conversation1.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_47Conversation1.Clip);

    }

    public void StartSequence47Conversation2()
    {
        m_Seq3_47Conversation1.Sequence.SetActive(false);
        m_Seq3_47Conversation2.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }
    public void StartSequence47Conversation3()
    {
        m_Seq3_47Conversation1.Sequence.SetActive(false);
        m_Seq3_47Conversation2.Sequence.SetActive(false);
        m_Seq3_47Conversation3.Sequence.SetActive(true);
        s_GuideAudiolessClip?.Invoke();
    }
    public void StartSequence47Radio()
    {
       
        m_Seq3_47Conversation1.Sequence.SetActive(false);
        m_Seq3_47Conversation2.Sequence.SetActive(false);
        m_Seq3_47Conversation3.Sequence.SetActive(false);
        m_Seq3_47Radio.Sequence.SetActive(true);
        s_GuideAudioClip?.Invoke(m_Seq3_47Radio.Clip);
    }


    public void StartSequence48()
    {
        s_PlayerPositionCallback?.Invoke(m_CarPoint.position);
        s_PlayerRotationCallback?.Invoke(m_CarPoint.rotation);

        m_Seq3_48Tele.Sequence.SetActive(true);

        s_GuideAudioClip?.Invoke(m_Seq3_48Tele.Clip);
    }


    public void ResetCarPoint(bool isCorrect)
    {
     //   m_Sequence63Observation.SetActive(!isCorrect);
        if (!isCorrect)
        {
            m_Seq3_48Tele.Sequence.SetActive(false);
            m_Seq3_48Tele.Sequence.SetActive(true);
            s_PlayerPositionCallback?.Invoke(m_CarPoint.position);
            s_PlayerRotationCallback?.Invoke(m_CarPoint.rotation);
        }
        else
        {

            Debug.Log("The next sequence Triggered");
           // StartSequence63_2();
        }
    }


    









    #endregion

}
