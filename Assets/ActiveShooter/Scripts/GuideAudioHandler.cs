using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideAudioHandler : MonoBehaviour
{
    #region Private_Vars
    private AudioSource m_AudioSource;

    #endregion
    #region Public_Vars
    public static Action s_PanelCallback;
    #endregion


    #region Unity_Callbacks
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        ActiveShooterSequenceManager.s_GuideAudioClip += OnGuideAudioRecieved;
        ActiveShooterSequenceManager.s_GuideAudiolessClip += OnGuideAudioRecieved;
        GuidePanelHandler.s_OnGuideSkips += OnSkip;
    }

    private void OnDisable()
    {
        ActiveShooterSequenceManager.s_GuideAudioClip -= OnGuideAudioRecieved;
        ActiveShooterSequenceManager.s_GuideAudiolessClip -= OnGuideAudioRecieved;
        GuidePanelHandler.s_OnGuideSkips -= OnSkip;

    }

    #endregion

    #region Private_Methods
    private void OnGuideAudioRecieved(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
        Utilities.ExecuteAfterDelay(clip.length, () =>
        {
            s_PanelCallback?.Invoke();
        });
    }

    private void OnGuideAudioRecieved()
    {
       
        Utilities.ExecuteAfterDelay(0f, () =>
        {
            s_PanelCallback?.Invoke();
        });
    }

    private void OnSkip()
    {
        if (m_AudioSource != null)
            m_AudioSource.Stop();
    }

    #endregion
}
