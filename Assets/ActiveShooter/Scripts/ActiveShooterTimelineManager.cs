using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveShooterTimelineManager : MonoBehaviour
{

    #region Private_Vars
    [SerializeField]
    PlayableDirector m_MainTimeline;

    #endregion

    #region Unity_Callbacks

    private void OnEnable()
    {
        ActiveShooterSequenceManager.s_UpdateTimelineCall += UpdateCoupleTimeline;
        ActiveShooterGuideManager.s_UpdateTimeline += UpdateCoupleTimeline;
        ActiveShooterSequenceManager.s_PauseTimelineCall += PauseTimeline;
        ActiveShooterSequenceManager.s_ResumeTimelineCall += ResumeTimeline;
        TeleportInteractable.s_UpdateTimeline += UpdateCoupleTimeline;
    }

    private void OnDisable()
    {
        ActiveShooterSequenceManager.s_UpdateTimelineCall -= UpdateCoupleTimeline;
        ActiveShooterGuideManager.s_UpdateTimeline -= UpdateCoupleTimeline;
        ActiveShooterSequenceManager.s_PauseTimelineCall -= PauseTimeline;
        ActiveShooterSequenceManager.s_ResumeTimelineCall -= ResumeTimeline;
        TeleportInteractable.s_UpdateTimeline -= UpdateCoupleTimeline;
    }

    #endregion

    #region Private_Methods

    private void PauseTimeline()
    {
        m_MainTimeline.Pause();
    }
    private void ResumeTimeline()
    {
        m_MainTimeline.Play();
    }

    public void UpdateCoupleTimeline(PlayableAsset currentTimeLine)
    {
        m_MainTimeline.time = 0;
        m_MainTimeline.playableAsset = currentTimeLine;
        m_MainTimeline.Play();
    }
    #endregion
}
