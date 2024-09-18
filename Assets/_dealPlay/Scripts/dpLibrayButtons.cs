using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dpLibrayButtons : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private int m_Id;

    [SerializeField]
    private GameObject m_Line;
    private static Action<int> s_OnButtonPressed;
    [SerializeField]
    private Button m_LibBtn;
    [SerializeField]
    private GameObject m_ModuleList;
    #endregion
    #region Public_Vars

    #endregion



    #region Unity_Callbacks
    private void Awake()
    {
        m_LibBtn = GetComponent<Button>();
        m_Line = this.transform.GetChild(1).gameObject;
        m_LibBtn.onClick.AddListener(OnLibraryButtonPressed);
        
    }
    private void OnEnable()
    {
       
        s_OnButtonPressed += OnLibraryButtonPressed;

    }
    private void OnDisable()
    {
        s_OnButtonPressed -= OnLibraryButtonPressed;

    }

    #endregion

    #region Private_Methods
    private void OnLibraryButtonPressed(int id)
    {
        bool activeStatus;
        if (m_Id == id)
            activeStatus = true;
        else
            activeStatus = false;
        m_Line.SetActive(activeStatus);
        m_ModuleList.SetActive(activeStatus);
    }
    private void OnLibraryButtonPressed()
    {
        s_OnButtonPressed?.Invoke(m_Id);
    }

    #endregion

}
