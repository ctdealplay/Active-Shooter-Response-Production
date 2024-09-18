using Oculus.Interaction;
using Oculus.VoiceSDK.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DealplayRadioReciever : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private Text m_RadioText;
    [SerializeField]
    private string m_RecorderString;
    [SerializeField]
    private string m_CurrentString;
    [SerializeField]
    private GameObject m_SubmitButton;
    [SerializeField]
    private AudioSource m_BeepAudio;
    [SerializeField]
    private AudioSource m_MicOff;
    #endregion

    #region Unity_Callbacks

    private void OnEnable()
    {
        m_SubmitButton.SetActive(false);
        DealplayTranscriptionLabel.s_TextCallback += TextCallback;
        DealplayRadioHandler.s_NewStringCallback += NewRadioStringCallback;
        DealplayGrabbable.s_GrabBegin += OnRadioGrab;
        DealplayGrabbable.s_GrabRelease += OnRadioRelease;
    }

    private void OnDisable()
    {
        DealplayGrabbable.s_GrabBegin -= OnRadioGrab;
        DealplayGrabbable.s_GrabRelease -= OnRadioRelease;
        DealplayTranscriptionLabel.s_TextCallback -= TextCallback;
        DealplayRadioHandler.s_NewStringCallback -= NewRadioStringCallback;
    }

    #endregion

    #region Private_Methods

    private void OnRadioGrab(bool status)
    {
        if (status)
            return;
        ResetRadioStrings();
        m_BeepAudio.PlayOneShot(m_BeepAudio.clip);
        m_SubmitButton.SetActive(false);
    }
    public void ResetRadioStrings()
    {
        m_CurrentString = null;
        m_RecorderString = null;
    }

    private void OnRadioRelease(bool status)
    {
        if (status)
            return;
        m_MicOff.PlayOneShot(m_MicOff.clip);
        m_SubmitButton.SetActive(true);
    }

    private void NewRadioStringCallback()
    {
        if (m_RecorderString == "Recording....")
        {
            return;
        }
        if (m_CurrentString != null)
        {
            m_CurrentString += ". ";
        }
        m_CurrentString += m_RecorderString;
    }
    private void TextCallback(string message)
    {

        if (message == "Recording....")
        {
            return;
        }

        m_RecorderString = message;
        /* string temp = message[0].ToString();
         string temp2 = ;
         temp.ToUpper();
         message[0] = temp[0];*/

        m_RadioText.text = m_CurrentString + message;
    }

    #endregion


}
