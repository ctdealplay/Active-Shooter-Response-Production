using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Customer
{
    public int otp;
    public string token;
    public DateTime updatedAt;
    public DateTime createdAt;
    public string email;
    public string userName;
    public long phoneNumber;
    public DateTime otpTime;
    public string id;
    public string sessionId;
}

[Serializable]
public class UserData
{
    public string status;
    public Customer customer;
}

[Serializable]
public class ReportData
{
    public string PlayerId;
    public string sessionId;
    public int PerspectiveNumber;
    public List<RadioRemoteData> RadioData = new List<RadioRemoteData>();
    public List<ObservationRemoteData> ObservationData = new List<ObservationRemoteData>();
    //  public List<StanceRemoteData> StanceRemoteData = new List<StanceRemoteData>();
    //  public List<ConversationRemoteData> ConversationData = new List<ConversationRemoteData>();


}



public class ActiveShooterManager : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private bool m_IsVisionUIActivated = false;
    [SerializeField]
    private bool m_IsTeleportationActivated = false;
    [SerializeField]
    private UserData m_UserData;
    [SerializeField]
    private string m_PlayerId;
    [SerializeField]
    private string m_SessionId;
    [SerializeField]
    private GameObject m_ObserveEye;
    [SerializeField]
    private GameObject m_TeleportEye;
    [SerializeField]
    private GameObject m_ConversationalHandler;
    [SerializeField]
    private GameObject m_CanvasReference;

    #endregion

    #region Public_Vars
    public static ActiveShooterManager Instance;

    public string PlayerId => m_PlayerId;
    public string SessionId => m_SessionId;

    public static Action s_VisonUIDeactivatedCallback;
    public static Action s_VisonUIActivatedCallback;

    public bool isTimerRunning = false;  // The boolean flag to start the timer

    private float elapsedTime = 0f;
    private Coroutine timerCoroutine;
    public string TimerString;
    public int Minutes;
    public int Seconds;
    [SerializeField]
    public List<string> m_TimerLists;


    public bool IsTeleportationActivated => m_IsTeleportationActivated;
    public bool IsVisionUIActivated => m_IsVisionUIActivated;
    [SerializeField]
    private GameObject m_ResponseHandler;

    #endregion

    #region Unity_Callbacks
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        /* Utilities.ExecuteAfterDelay(0.25f, () =>
         {
             m_CanvasReference.transform.rotation = Quaternion.Euler(45f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
         });*/
        m_ResponseHandler.SetActive(false);
        m_ObserveEye.SetActive(false);
        m_TeleportEye.SetActive(false);
        ActiveShooterGuideManager.s_DeactiveObservationCallback += DeactiveVision;
        ActiveShooter_ConversationPanel.s_ConversationToggle += OnConversationToggle;
    }

    private void OnDisable()
    {
        ActiveShooterGuideManager.s_DeactiveObservationCallback -= DeactiveVision;
        ActiveShooter_ConversationPanel.s_ConversationToggle -= OnConversationToggle;

    }
    #endregion


    #region Public_Methods
    public void AngleCallback(float currentangle)
    {
        Quaternion currentRotation = m_CanvasReference.transform.rotation;

        // Set the exact Y rotation, keeping X and Z the same
        //    m_CanvasReference.transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, angle, currentRotation.eulerAngles.z);

        m_CanvasReference.transform.Rotate(currentRotation.x, currentangle, currentRotation.z, Space.Self);

        /* Vector3 angles = m_CanvasReference.transform.eulerAngles;
         Debug.Log("Before, angles = " + angles);
         if(currentangle < 0)
         {   
             angles.y = 360f + currentangle;
         }
         else
         {
             angles.y = 360f -  currentangle;
         }

         Debug.Log("Modified, angles = " + angles);
         m_CanvasReference.transform.eulerAngles = angles;*/

    }
    public void ToggleVisionUI(bool status, bool isTeleportation = false)
    {
        m_IsVisionUIActivated = status;
        if (status)
            s_VisonUIActivatedCallback?.Invoke();
        else
            s_VisonUIDeactivatedCallback?.Invoke();
        if(!status)
        {
            m_ObserveEye.SetActive(status);
            m_TeleportEye.SetActive(status);
        }

        if (!isTeleportation)
            m_ObserveEye.SetActive(status);
        else
            m_TeleportEye.SetActive(status);
    }
    private void OnConversationToggle(bool status)
    {
        m_ConversationalHandler.SetActive(status);
    }
    public void AddTimerList(string timer)
    {
        m_TimerLists.Add(timer);
    }

    private void DeactiveVision()
    {
        s_VisonUIDeactivatedCallback?.Invoke();
        m_ObserveEye.SetActive(false);
        m_TeleportEye.SetActive(false);
    }

    #endregion

    public void ToggleResponseHandler(bool status)
    {
        m_ResponseHandler.SetActive(status);
    }



    // Call this method when the boolean becomes true
    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }

    // Call this method to stop the timer
    public void StopTimer()
    {
        if (isTimerRunning)
        {
            isTimerRunning = false;
            StopCoroutine(timerCoroutine);
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            Minutes = minutes;
            Seconds = seconds;
            TimerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }
    }

    // Optional: Reset the timer
    public void ResetTimer()
    {
        Minutes = Seconds = 0;
        elapsedTime = 0f;
    }


}
