using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Panels;
    

    [SerializeField]
    private int m_CurrentActiveIndex = 0;

    #region Unity_Callbacks

    private void OnEnable()
    {
        m_CurrentActiveIndex = 0;
        m_Panels[m_CurrentActiveIndex].SetActive(true);
    }

    #endregion

    #region Public_Methods
    public void UpdatePanels(int panelsDelta)
    {
        
        m_Panels[m_CurrentActiveIndex].SetActive(false);


        m_CurrentActiveIndex += panelsDelta;

        if (m_CurrentActiveIndex < 0)
        {
            m_CurrentActiveIndex = 0;
        }
        if (m_CurrentActiveIndex >= m_Panels.Length)
        {
            m_CurrentActiveIndex = m_Panels.Length - 1;
        }
            Utilities.ExecuteAfterDelay(0.6f, () =>
            {
                m_Panels[m_CurrentActiveIndex].SetActive(true);
            });

    }


    #endregion


}
