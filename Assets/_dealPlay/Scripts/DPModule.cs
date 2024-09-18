using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;


public class DPModule : MonoBehaviour
{

    #region Private_Vars

    [SerializeField]
    private AssetReference m_AddressableSceneName;
    [SerializeField]
    private AsyncOperationHandle<SceneInstance> m_SceneHandle;
    [SerializeField]
    private AsyncOperationHandle m_DownloadHandle;
    [SerializeField]
    private Button m_DownloadButton;
    [SerializeField]
    private Button m_PlayButton;


    [SerializeField]
    private TextMeshProUGUI m_ModuleName;
    [SerializeField]
    private Image m_ModuleImage;
    [SerializeField]
    private string m_TargetUrl;
    [SerializeField]
    private GameObject m_LoadingBar;
    [SerializeField]
    private GameObject m_ProgressBar;
    [SerializeField]
    private TextMeshProUGUI m_ProgressText;
    private bool m_CanCheck = false;
    private static Action s_DisablePlayButtons;
    [SerializeField]
    private string m_SceneName;
    #endregion

    #region Public_Methods
    public void SetData(DealplayModule module)
    {
        m_ModuleName.text = module.ModuleName;
        m_TargetUrl = ServerManager.instance.BaseUrl + module.ImageUrl;
        m_SceneName = module.SceneName;

        m_AddressableSceneName = DealplayData.Instance.DealplayList.Find(element => element.SceneName == module.SceneName).SceneReference;
       
    }
    #endregion

    #region Unity_Callbacks

    private void Awake()
    {
        m_PlayButton.onClick.AddListener(OnSiginTap);
        m_DownloadButton.onClick.AddListener(OnSiginTap);
    }
    private void OnEnable()
    {
        m_DownloadButton.gameObject.SetActive(false);
        m_PlayButton.gameObject.SetActive(false);
        m_LoadingBar.SetActive(true);
        m_ProgressBar.SetActive(false);
        if (!String.IsNullOrEmpty(m_TargetUrl))
            StartCoroutine(DownloadImage(m_TargetUrl));
        s_DisablePlayButtons += DisablePlayButtons;
    }

    private void OnDisable()
    {
        s_DisablePlayButtons -= DisablePlayButtons;
    }


    #endregion



    #region Private_Methods

    private void Update()
    {
        if (m_CanCheck)
        {
            if (m_SceneHandle.IsValid())
            {
                float percent = m_SceneHandle.GetDownloadStatus().Percent;
                m_ProgressText.text = percent * 100 + "%";

                if (m_SceneHandle.PercentComplete == 1f)
                {
                    Addressables.Release(m_SceneHandle);
                }
            }

        }

    }
    private void DisablePlayButtons()
    {
        m_PlayButton.gameObject.SetActive(false);
    }


    public void OnSiginTap()
    {
        s_DisablePlayButtons?.Invoke();
        m_ProgressBar.SetActive(true);
        // SceneManager.LoadScene(m_SceneName, LoadSceneMode.Single);
        LoadAddressableScene(m_AddressableSceneName);
    }

    public async void CheckAssetState(AssetReference sceneReference)
    {
        // Check the download size for the scene and its dependencies
        AsyncOperationHandle<long> getSizeHandle = Addressables.GetDownloadSizeAsync(sceneReference);

        await getSizeHandle.Task;

        if (getSizeHandle.Status == AsyncOperationStatus.Succeeded)
        {
            long downloadSize = getSizeHandle.Result;

            if (downloadSize > 0)
            {
                Debug.Log("Assets for this scene need to be downloaded.");
                m_DownloadButton.gameObject.SetActive(true);
                // Enable the download button since assets are not present
                // EnableDownloadButton();
            }
            else
            {
                Debug.Log("Assets for this scene are already downloaded.");
                m_DownloadButton.gameObject.SetActive(false);
                m_PlayButton.gameObject.SetActive(true);
                // Enable the play button directly if assets are already present
                // EnablePlayButton();
            }
        }
        else
        {
            Debug.LogError("Failed to check download size for assets.");
        }

        Addressables.Release(getSizeHandle);
    }



    public async void LoadAddressableScene(AssetReference sceneName)
    {
        m_CanCheck = true;
        m_SceneHandle = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await m_SceneHandle.Task;
    }




    /*  private async void CheckForUpdates()
      {
          // Start the operation to check for catalog updates
          AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates();

          // Await the completion of the operation
          await checkHandle.Task;

          // Now get the result from the handle
          List<string> catalogs = checkHandle.Result;

          // Check if any catalog updates are available
          if (catalogs.Count > 0)
          {
              // If there are updates, we need to download new content
              m_DownloadButton.gameObject.SetActive(true);
          }
          else
          {

              m_PlayButton.gameObject.SetActive(true);
          }

          // Release the handle after use
          Addressables.Release(checkHandle);

      }

      private void OnPlayButtonTap()
      {
          LoadAddressableScene(m_AddressableSceneName);
      }

      private void OnDownloadButtonTap()
      {
          DownloadAddressableScene(m_AddressableSceneName);
      }

      public async void DownloadAddressableScene(AssetReference sceneName)
      {
          // Download the addressable scene and cache it locally if not already cached
          m_DownloadHandle = Addressables.DownloadDependenciesAsync(sceneName);
          m_CanCheck = true;
          await m_DownloadHandle.Task;

          if (m_DownloadHandle.IsDone)
          {
              m_CanCheck = false;
              m_PlayButton.gameObject.SetActive(true);
          }
      }

      public async void LoadAddressableScene(AssetReference sceneName)
      {
          // Load the scene after checking cache or downloading
          m_SceneHandle = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
          await m_SceneHandle.Task;
      }
  */

    #endregion




    #region Coroutines
    /* IEnumerator DownloadImage(string url)
     {
         using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
         {
             yield return webRequest.SendWebRequest();

             if (webRequest.result != UnityWebRequest.Result.Success)
             {
                 Debug.LogError("Error downloading image: " + webRequest.error);
             }
             else
             {
                 Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                 Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                 m_ModuleImage.sprite = sprite;  // Apply the downloaded image to the UI Image component
                 m_LoadingBar.SetActive(false);
             }
         }
     }*/


    IEnumerator DownloadImage(string url)
    {

        // string targetUrl = m_BaseUrl + line.image;//add line path here
        //   string targetUrl = ServerManager.instance.BaseUrl + game.theme_icon;//add line path here
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error downloading image: {webRequest.error}");
            }
            else
            {
                // Get the downloaded texture
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                m_ModuleImage.sprite = sprite;  // Apply the downloaded image to the UI Image component
                m_ModuleImage.gameObject.SetActive(true);  // Apply the downloaded image to the UI Image component
                CheckAssetState(m_AddressableSceneName);                                       //skchnages m_PlayButton.gameObject.SetActive(true);
                m_LoadingBar.SetActive(false);
            }
        }



    }

    #endregion

}
