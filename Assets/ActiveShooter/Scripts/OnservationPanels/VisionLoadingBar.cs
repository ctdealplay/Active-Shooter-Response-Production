using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionLoadingBar : MonoBehaviour
{
    #region Private_Vars
    private Renderer m_Renderer;
    [SerializeField]
    private Texture[] m_LoadingFrames;
    [SerializeField]
    private float m_CurrentTime;
    [SerializeField]
    private float m_FrameDelay;
    [SerializeField]
    private int m_CurrentActivatedFrame;
    #endregion

    #region Unity_Callbacks
    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        m_CurrentActivatedFrame = Constants.INT_ZERO;
    }
    private void Update()
    {
        if (m_CurrentTime >= m_FrameDelay)
        {
            m_Renderer.material.SetTexture(Constants.MAIN_TEXTURE, m_LoadingFrames[m_CurrentActivatedFrame]);
            if (m_CurrentActivatedFrame >= (m_LoadingFrames.Length - 1))
            {
                m_CurrentActivatedFrame = Constants.INT_ZERO;
            }
            else
            {
                m_CurrentActivatedFrame++;
            }
            m_CurrentTime = Constants.INT_ZERO;
        }
        else
        {
            m_CurrentTime += Time.deltaTime;
        }
    }
    #endregion


}

