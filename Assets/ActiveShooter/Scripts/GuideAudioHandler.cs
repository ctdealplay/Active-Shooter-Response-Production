using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideAudioHandler : MonoBehaviour
{
    #region Private_Vars
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip m_ShotClip;

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
        ActiveShooterSequenceManager.s_PlayGunShotSequence += PlayGunShots;
        GuidePanelHandler.s_OnGuideSkips += OnSkip;
    }

    private void OnDisable()
    {
        ActiveShooterSequenceManager.s_GuideAudioClip -= OnGuideAudioRecieved;
        ActiveShooterSequenceManager.s_GuideAudiolessClip -= OnGuideAudioRecieved;
        ActiveShooterSequenceManager.s_PlayGunShotSequence -= PlayGunShots;
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

    private void PlayGunShots()
    {
        StartCoroutine(nameof(PlayGunshotSequence));
    }

    private void OnSkip()
    {
        if (m_AudioSource != null)
            m_AudioSource.Stop();
    }

    #endregion


    IEnumerator PlayGunshotSequence()
    {
        //AudioSource target = m_Sequence18.GetComponent<AudioSource>();
        for (int gunShotIndex = 0; gunShotIndex < Constants.GUN_SHOTS; gunShotIndex++)
        {
            // Play the gunshot audio clip
            m_AudioSource.PlayOneShot(m_ShotClip);

            // Wait for the duration of the gunshot audio clip
            yield return new WaitForSeconds(m_ShotClip.length);
        }
    }
}
