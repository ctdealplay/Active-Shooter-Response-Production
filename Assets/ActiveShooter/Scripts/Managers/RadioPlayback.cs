using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPlayback : MonoBehaviour
{
    #region Private_Vars
    private AudioSource m_AudioSource;
    [SerializeField]
    private GameObject m_SubmitButton;
    [SerializeField]
    private bool m_IsStaticAudio;
    [SerializeField]
    private AudioClip m_StaticClip;
    [SerializeField]
    private Vector3 m_RadioSnapPosition;
    [SerializeField]
    private Quaternion m_RadioSnapRotation;
    #endregion

    #region Unity_Callbacks
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        m_RadioSnapPosition = transform.position;
        m_RadioSnapRotation = transform.rotation;
        DealplayGrabbable.s_GrabBegin += OnGrabBegin;
        DealplayGrabbable.s_GrabRelease += OnGrabRelease;
    }

    private void OnDisable()
    {
        DealplayGrabbable.s_GrabBegin -= OnGrabBegin;
        DealplayGrabbable.s_GrabRelease -= OnGrabRelease;
    }
    #endregion

    #region Private_Methods
    private void OnGrabBegin(bool status)
    {
        float audioLength = 0f; 
        m_SubmitButton.SetActive(false);
        if(m_IsStaticAudio)
        {
            audioLength = m_StaticClip.length;
            m_AudioSource.clip = m_StaticClip;
        }
       
        m_AudioSource.Play();
        Utilities.ExecuteAfterDelay(audioLength, () =>
        {
            m_SubmitButton.SetActive(true);
        });
    }
    private void OnGrabRelease(bool status)
    {
        transform.position = m_RadioSnapPosition;
        transform.rotation = m_RadioSnapRotation;
        m_AudioSource.Stop();
    }


    #endregion
}
