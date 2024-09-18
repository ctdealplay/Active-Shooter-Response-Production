using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LoginSavedData
{
    public bool IsRembember;
    public string Email;
    public string Password;

    public LoginSavedData(bool isRemember, string email, string password)
    {
        IsRembember = isRemember;
        Email = email;
        Password = password;
        if(!IsRembember)
        {
            Email = "";
            Password = "";
        }
    }
}


public class DPLoginManager : MonoBehaviour
{
    #region Public_Vars
    public static Action<ModuleData, Action> s_ModuleDataCallback;

    #endregion
    #region Private_Vars
    [SerializeField]
    private GameObject m_IntroScene;
    [SerializeField]
    private TMP_InputField m_Email;
    [SerializeField]
    private TMP_InputField m_Password;
    [SerializeField]
    private TextMeshProUGUI m_WarningText;
    [SerializeField]
    private Button m_LoginButton;
    [SerializeField]
    private Sprite[] m_PasswordIcons;
    [SerializeField]
    private Button m_PasswordIconButton;
    [SerializeField]
    private Image m_MainPasswordIcon;
    [SerializeField]
    private bool m_CanShowPassword = false;
    [SerializeField]
    private LoginData m_CurrentLoginData;
    [SerializeField]
    private GameObject m_LoadingBar;

    [SerializeField]
    private TextMeshProUGUI m_FirstName;
    [SerializeField]
    private TextMeshProUGUI m_EmailAddress;
    [SerializeField]
    private TextMeshProUGUI m_BusinessUnit;
    [SerializeField]
    private TextMeshProUGUI m_CompanyName;
    [SerializeField]
    private bool m_IsRembember;
    [SerializeField]
    private LoginSavedData m_LoginSaveData;
    [SerializeField]
    private Image m_ToggleButton;
    #endregion
    #region Unity_Callbacks
    private void Awake()
    {

        m_LoginButton.onClick.AddListener(OnLoginButtonTapped);
        m_PasswordIconButton.onClick.AddListener(OnPasswordIconTap);
    }

    private void OnEnable()
    {
      

        InitializeRememberME();
        m_LoadingBar.SetActive(false);
        m_WarningText.gameObject.SetActive(false);
        ServerManager.s_OnLoginResultRecived += OnLoginDataRecived;
    }

    private void InitializeRememberME()
    {
        m_LoginSaveData = JsonUtility.FromJson<LoginSavedData>(PlayerPrefs.GetString("PlayerLoginData"));
        if (m_LoginSaveData != null)
        {
            m_IsRembember = m_LoginSaveData.IsRembember;
            m_Email.text = m_LoginSaveData.Email;
            m_Password.text = m_LoginSaveData.Password;
        }
        m_ToggleButton.gameObject.SetActive(m_IsRembember);
    }

    public void OnRememberButtonTap()
    {
        m_IsRembember = !m_IsRembember;
        m_ToggleButton.gameObject.SetActive(m_IsRembember);
    }

    private void OnDisable()
    {
        ServerManager.s_OnLoginResultRecived -= OnLoginDataRecived;


    }

    #endregion

    #region Private_Methods
    /* private void UpdatePasswordField()
     {
         if (m_CanShowPassword)
         {
             m_Password.contentType = TMP_InputField.ContentType.Standard;
         }
         else
         {
             m_Password.contentType = TMP_InputField.ContentType.Password;

         }
         int imageIndex;
         m_MainPasswordIcon.sprite = m_PasswordIcons[imageIndex = (m_CanShowPassword) ? 1 : 0];
     }*/
  
    private void OnPasswordIconTap()
    {
        m_CanShowPassword = !m_CanShowPassword;
    }


    private bool CheckValidations()
    {
        bool status = true;
        if (string.IsNullOrEmpty(m_Email.text) || string.IsNullOrEmpty(m_Password.text))
        {
            status = false;
        }

        return status;
    }

    private void OnLoginButtonTapped()
    {
        m_LoadingBar.SetActive(true);
       
        ServerManager.instance.OnLearnerLogin(m_Email.text, m_Password.text);
        /* if(CheckValidations())
          {
              ServerManager.instance.OnLearnerLogin(m_Email.text, m_Password.text);
          }
          m_WarningText.gameObject.SetActive(!CheckValidations());*/
    }

    private void OnLoginDataRecived(string JsonData)
    {

        Action moduleCallback = () =>
        {
            m_IntroScene.SetActive(true);
            m_LoginSaveData = new LoginSavedData(m_IsRembember, m_Email.text, m_Password.text);
            string jsonData = JsonUtility.ToJson(m_LoginSaveData);
            PlayerPrefs.SetString("PlayerLoginData", jsonData);
            m_LoadingBar.SetActive(false);
            gameObject.SetActive(false);

        };
        m_CurrentLoginData = JsonUtility.FromJson<LoginData>(JsonData);
        if (m_CurrentLoginData.status == Constants.SUCCESS)
        {
            m_WarningText.gameObject.SetActive(false);
            SetProfileData(m_CurrentLoginData.result.PlayerData);
            s_ModuleDataCallback?.Invoke(m_CurrentLoginData.result.ModuleData, moduleCallback);

        }
        else
        {
            m_LoadingBar.SetActive(false);
            m_WarningText.text = m_CurrentLoginData.message;
            m_WarningText.gameObject.SetActive(true);
        }
    }


    private void SetProfileData(PlayerData playerData)
    {
        m_FirstName.text = playerData.firstName;
        m_EmailAddress.text = playerData.email;
        m_BusinessUnit.text = playerData.businessUnit;
        m_CompanyName.text = playerData.company.companyName;
    }
    #endregion

}
