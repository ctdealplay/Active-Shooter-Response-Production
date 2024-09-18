using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class DealplayAddresableManager : MonoBehaviour
{




    [SerializeField]
    private AssetReference m_AddressableSceneName;
    [SerializeField]
    private AsyncOperationHandle<SceneInstance> m_Handle;
    [SerializeField]
    private TextMeshProUGUI m_ProgressText;
    [SerializeField]
    private GameObject m_ProgressPanel;
    [SerializeField]
    private bool m_CanCklick = false;


    private void Update()
    {
        if(!m_CanCklick && Input.GetKeyDown(KeyCode.Space))
        {
            OnSiginTap();
            m_CanCklick = true;
        }
        if(m_Handle.IsValid())
        {
            float percent = m_Handle.GetDownloadStatus().Percent;
            m_ProgressText.text = percent * 100 + "%";

            if(m_Handle.PercentComplete == 1f)
            {
                Addressables.Release(m_Handle);
            }
        }
    }



    public void OnSiginTap()
    {
        m_ProgressPanel.SetActive(true);
        LoadAddressableScene(m_AddressableSceneName);
    }

    public async void LoadAddressableScene(AssetReference sceneName)
    {
        m_Handle = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await m_Handle.Task;
    }
}
