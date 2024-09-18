using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VRConversationOptionss : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private int m_SequenceNumber;
    [SerializeField]
    private Image m_TargetImage;
    [SerializeField]
    private bool m_IsRight;
    [SerializeField]
    private Sprite m_WrongSprite;
    [SerializeField]
    private Sprite m_RightSprite;
    [SerializeField]
    private int m_Id;


    #endregion

    public static Action<int, int> s_DataCallback;

    #region Unity_Callbacks
    private void OnEnable()
    {
        ActiveShooterGuideManager.s_ConversationCallback += CheckOptionStatus;
    }

    private void OnDisable()
    {
        ActiveShooterGuideManager.s_ConversationCallback -= CheckOptionStatus;
    }
    #endregion

    #region Private_Methods
    private void CheckOptionStatus(int id)
    {
        if (id == m_Id)
        {

            m_TargetImage.sprite = m_WrongSprite;
            if (m_IsRight)
            {
                s_DataCallback?.Invoke(1, m_SequenceNumber);
            }
            else
            {

                s_DataCallback?.Invoke(0, m_SequenceNumber);

            }
        }
        if (m_IsRight)
        {
            m_TargetImage.sprite = m_RightSprite;
        }
        /*else
        {
            m_TargetImage.sprite = m_WrongSprite;

        }*/
    }

    #endregion

}
