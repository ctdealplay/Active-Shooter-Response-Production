using System;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;
using BestHTTP.JSON.LitJson;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using System.Collections;
using UnityEngine.Networking;

[Serializable]

public class ModuleData
{
    public List<DealplayModule> FeaturedModules;
    public List<DealplayModule> ActionModules;
    public List <DealplayModule> StoredModules;
    public List <DealplayModule> AllModules;
}

[Serializable]
public class DealplayModule
{
    public string ModuleName;
    public string BundleUrl;
    public string ImageUrl;
    public string SceneName;
}
[Serializable]
public class LoginData
{
    public string status;
    public LoginResult result;
    public string message;
    public int statusCode;
}

[Serializable]
public class PlayerData
{
    public string status;
    public string businessUnit;
    public string phoneNumber;
    public DateTime updatedAt;
    public DateTime createdAt;
    public string _id;
    public string userId;
    public string firstName;
    public string lastName;
    public string password;
    public string visiblePassword;
    public string email;
    public int pin;
    public Company company;
    public string role;
}

[Serializable]
public class Company
{
    public string companyName;
    public string companyAddress;
}

[Serializable]
public class LoginResult
{
    public ModuleData ModuleData;
    public PlayerData PlayerData;
}




public class ServerManager : MonoBehaviour
{



    #region Public_Vars
    public static ServerManager instance;
    public string BaseUrl;
    public static Action<string> s_OnLoginResultRecived;
    [SerializeField]
    private ModuleData m_ModuleData;
    #endregion

    #region Private_Vars
    [SerializeField]
    private string serverUrl;
    [SerializeField]
    private bool m_IsCustomServer;
    [SerializeField]
    string targetJson;
    #endregion

    #region Unity_Callbacks
    private void Awake()
    {
        targetJson = JsonUtility.ToJson(m_ModuleData);

        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // You may want to initialize or do something else here if needed
    }
    #endregion

    #region Private_methods


    private IEnumerator PostLoginData(string email, string password, Action<string> callback)
    {
        if (String.IsNullOrEmpty(email))
            email = "email";
        if (String.IsNullOrEmpty(password))
            password = "password";
        string url = BaseUrl + Constants.LOGIN_ROUTE;
        UnityWebRequest request = UnityWebRequest.Post(url, new List<IMultipartFormSection>
    {
        new MultipartFormDataSection(Constants.EMAIL, email),
        new MultipartFormDataSection(Constants.PASSWORD, password),
    });
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonString = request.downloadHandler.text;
            Debug.Log("Login Response Recieved: " + jsonString);
            callback?.Invoke(jsonString);
        }
        else
        {
            Debug.Log("Login Response Error " +request.downloadHandler.text);
            Debug.LogError(request.error);
        }

    }

    public void OnLearnerLogin(string email, string password)
    {
       
        StartCoroutine(PostLoginData(email, password, OnLoginResultReceived));
    }

    private void OnLoginResultReceived(string responseJson)
    {
        Debug.Log("The Login received data: " + responseJson);
        s_OnLoginResultRecived?.Invoke(responseJson); // Use this event to process the JSON data as per your requirement
    }
    #endregion









    /* #region Public_Vars
     public static ServerManager instance;
     public string BaseUrl;
     public static Action<string> s_OnLoginResultRecived;
     [SerializeField]
     private ModuleData m_ModuleData;

     #endregion
     #region Private_Vars
     private SocketManager socketManager;
     [SerializeField]
     private string serverUrl;
     [SerializeField]
     private bool m_IsCustomServer;
     [SerializeField]
     string targetJson;

     #endregion

     #region Unity_Callbacks
     private void Awake()
     {

         targetJson =  JsonUtility.ToJson(m_ModuleData);

         // Singleton pattern implementation
         if (instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject); // Persist across scenes
         }
         else if (instance != this)
         {
             Destroy(gameObject); // Destroy duplicate instances
         }
     }
     private void Start()
     {
         InitializeSocket();

     }
     #endregion

     #region Private_methods
     private void InitializeSocket()
     {
         socketManager = new SocketManager(new Uri(BaseUrl + serverUrl));
         socketManager.Socket.On(SocketIOEventTypes.Connect, OnServerConnection);


         socketManager.Socket.On(SocketIOEventTypes.Disconnect, (s, p, a) =>
         {
             Debug.Log("The server is disconnected");

         });
     }

     private void OnServerConnection(Socket socket, Packet packet, object[] args)
     {
         Debug.Log("The server is connected");
     }

     private void LoginCallback(Socket socket, Packet packet, params object[] args)
     {
         if (args != null && args.Length > 0)
         {
             Debug.Log(packet.GetType());
             Dictionary<string, object> responseData = args[0] as Dictionary<string, object>;
             string responseJson = JsonMapper.ToJson(responseData);
             Debug.Log("The Login sent data: "+responseJson);

             s_OnLoginResultRecived?.Invoke(responseJson);// use this event and process the json data as per your requirement
         }

     }

     public void OnLearnerLogin(string email, string password)
     {
         Dictionary<string, string> data = new Dictionary<string, string>
         {
             // pass the function parameter as sson as you set the things 
             { Constants.EMAIL, email },
             { Constants.PASSWORD, password }
         };
         Debug.Log("The Login sent data: " + JsonMapper.ToJson(data));
         socketManager.Socket.Emit(Constants.LOGIN_ROUTE, LoginCallback, JsonMapper.ToJson(data));
     }



     #endregion*/

}
