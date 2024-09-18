using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DealplayProgressPanel : MonoBehaviour
{
    #region Private_Vars
    [SerializeReference]
    private GameObject m_NextPanel;
    [SerializeField]
    private bool m_IsCompletion;
    [SerializeField]
    private TextMeshProUGUI m_TimerString;
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        Utilities.ExecuteAfterDelay(Constants.PROGRESS_DELAY, () =>
        {
            this.gameObject.SetActive(false);
            m_NextPanel.SetActive(true);
        });

        if(m_IsCompletion)
        {
            if (m_TimerString != null) 
            {
                m_TimerString.text = "Time Taken: "+ActiveShooterManager.Instance.TimerString;
            }
        }
    }

    #endregion

}
