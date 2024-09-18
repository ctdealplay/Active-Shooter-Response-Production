using PA_DronePack;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class DC_GameManager : MonoBehaviour
{
    #region Variable
    [Header("Common")]
    [SerializeField] GameObject m_SequenceFeddingSpehere;
    [SerializeField] AudioSource m_Guide_Audio_Source;
    [SerializeField] GameObject m_DroneOVERCammeraRig;

    [SerializeField] PA_DroneController m_Controller;
    [Header("Sequence 1 ")]
    //[SerializeField] GameObject m_Sequence_1_Canvas;
    [SerializeField] GameObject m_Conformation_UI_1_2;
    [SerializeField] GameObject m_UI_Sequence_1_3;
    [SerializeField] GameObject m_UI_Sequence_1_3_description;
    [SerializeField] AudioClip m_Sequence_1_3_Audio_Clip;
    [Header("Sequence 2 ")]
    // [SerializeField] GameObject m_Sequence_2_Canvas;
    [SerializeField] GameObject m_UI_Sequence_2_1_1;
    [SerializeField] GameObject m_UI_Sequence_2_1_2_Panel_1;
    [SerializeField] GameObject m_UI_Sequence_2_1_2_Panel_2;
    [SerializeField] GameObject m_UI_Sequence_2_1_2_Panel_3;

    [SerializeField] GameObject m_Sequence_2_2_1_Marker;
    [SerializeField] GameObject m_Sequence_2_2_2_Marker;
    [SerializeField] GameObject m_Sequence_2_2_3_Marker;
    [SerializeField] GameObject m_Sequence_2_2_4_Marker;

    [SerializeField] AudioClip m_Sequence_2_1_2_Audio_Clip;
    [SerializeField] AudioClip m_Sequence_2_2_1_Audio_Clip;
    [SerializeField] AudioClip m_Sequence_2_2_2_Audio_Clip;
    [SerializeField] AudioClip m_Sequence_2_2_3_Audio_Clip;
    [SerializeField] AudioClip m_Sequence_2_2_4_Audio_Clip;
    [SerializeField] AudioClip m_Sequence_2_2_5_Audio_Clip;

    #endregion


    #region UnityCallback
    // Start is called before the first frame update
    void Start()
    {
        StartSequence_1();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Sqruence1
    private void StartSequence_1() {
        LeanTween.alpha(m_SequenceFeddingSpehere, Constants.NO_FADE, Constants.HALF).setOnComplete(() => {
            LeanTween.scale(m_Conformation_UI_1_2, Vector3.one, Constants.HALF).setEaseInOutElastic();
        });
    }

    public void Conformation_Accepted() {
        LeanTween.scale(m_Conformation_UI_1_2, Vector3.zero, Constants.HALF).setOnComplete(() => {
            LeanTween.scale(m_UI_Sequence_1_3, Vector3.one, Constants.HALF).setEaseInOutElastic().setOnComplete(() => {
                m_Guide_Audio_Source.clip = m_Sequence_1_3_Audio_Clip;
                m_Guide_Audio_Source.Play();
                StartCoroutine(nameof(NarattorAudioComplete));
            });
        });
    }

    IEnumerator NarattorAudioComplete() {
        yield return new WaitForSeconds(m_Sequence_1_3_Audio_Clip.length);
        SkipNerratorAudio();
    }

    public void SkipNerratorAudio() {
        m_Guide_Audio_Source.Pause();
        m_UI_Sequence_1_3_description.SetActive(true);
        m_UI_Sequence_1_3.GetComponent<DC_ButtonOptions>().OptionsActivator(acceptBtn: true);
    }

    public void Sequence_1_3_Accepted() {
        LeanTween.alpha(m_SequenceFeddingSpehere, Constants.CONPLETE_FADE, Constants.HALF).setOnComplete(() => {
            LeanTween.scale(m_UI_Sequence_1_3, Vector3.zero, Constants.HALF);
            StartSequence_2();
            // m_Sequence_1_Canvas.SetActive(false);
        });
    }
    #endregion

    #region Sequence2
    private void StartSequence_2() {
        //m_Sequence_2_Canvas.SetActive(true);
        LeanTween.alpha(m_SequenceFeddingSpehere, Constants.NO_FADE, Constants.HALF).setOnComplete(() => {
            LeanTween.scale(m_UI_Sequence_2_1_1, Vector3.one, Constants.HALF).setOnComplete(() => {
                StartCoroutine(nameof(StartStageIntroduction_2_1_2));
            });
        });
    }

    #region Sequence 2_1_2
    IEnumerator StartStageIntroduction_2_1_2() {

        //wait for 3 seconds before closing progrerss 
        yield return new WaitForSeconds(Constants.THREE);
        LeanTween.scale(m_UI_Sequence_2_1_1, Vector3.zero, Constants.HALF);
        m_UI_Sequence_2_1_1.SetActive(false);
        m_Guide_Audio_Source.clip = m_Sequence_2_1_2_Audio_Clip;
        m_Guide_Audio_Source.Play();

        //  one second after guide audio completes 
        yield return new WaitForSeconds(m_Sequence_2_1_2_Audio_Clip.length + Constants.ONE);
        //m_UI_Sequence_2_1_2_Panel_1.SetActive(true);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_1, Vector3.one, Constants.HALF);
    }

    public void Stage_Indtroduction_Panel_1_Next() {
        //m_UI_Sequence_2_1_2_Panel_2.SetActive(true);
        //m_UI_Sequence_2_1_2_Panel_1.SetActive(false);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_2, Vector3.one, Constants.QUICK_SCALE);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_1, Vector3.zero, Constants.QUICK_SCALE);

    }
    public void Stage_Indtroduction_Panel_2_Previous() {
        //m_UI_Sequence_2_1_2_Panel_1.SetActive(true);
        //m_UI_Sequence_2_1_2_Panel_2.SetActive(false);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_1, Vector3.one, Constants.QUICK_SCALE);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_2, Vector3.zero, Constants.QUICK_SCALE);
    }
    public void Stage_Indtroduction_Panel_2_Next() {
        //m_UI_Sequence_2_1_2_Panel_3.SetActive(true);
        //m_UI_Sequence_2_1_2_Panel_2.SetActive(false);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_3, Vector3.one, Constants.QUICK_SCALE);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_2, Vector3.zero, Constants.QUICK_SCALE);
    }
    public void Stage_Indtroduction_Panel_3_Previous() {
        //m_UI_Sequence_2_1_2_Panel_2.SetActive(true);
        //m_UI_Sequence_2_1_2_Panel_3.SetActive(false);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_2, Vector3.one, Constants.QUICK_SCALE);
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_3, Vector3.zero, Constants.QUICK_SCALE);
    }
    public void Stage_Indtroduction_Panel_3_Next() {
        LeanTween.scale(m_UI_Sequence_2_1_2_Panel_3, Vector3.zero, Constants.HALF).setOnComplete(() => {
            //m_UI_Sequence_2_1_2_Panel_3.SetActive(false);
            Start_Sequence_2_2_1();
        });
    }

    #endregion

    public void Start_Sequence_2_2_1() {
        m_Controller.motorOn = true;

        //m_Sequence_2_2_1_Marker.SetActive(true);
        //m_Guide_Audio_Source.clip = m_Sequence_2_2_1_Audio_Clip;
        //m_Guide_Audio_Source.Play();
    }

    public void Start_Sequence_2_2_2() {

        m_Sequence_2_2_2_Marker.SetActive(true);
        m_Guide_Audio_Source.clip = m_Sequence_2_2_2_Audio_Clip;
        m_Guide_Audio_Source.Play();

    }
    public void Start_Sequence_2_2_3()
    {

      //  m_Sequence_2_2_3_Marker.SetActive(true);
        m_Guide_Audio_Source.clip = m_Sequence_2_2_3_Audio_Clip;
        m_Guide_Audio_Source.Play();
        StartCoroutine(nameof(GuideAudioEnd));

    }

    IEnumerator GuideAudioEnd()
{   
        yield return new WaitForSeconds(m_Sequence_2_2_2_Audio_Clip.length+Constants.HALF);
        m_Guide_Audio_Source.clip = m_Sequence_2_2_4_Audio_Clip;
        m_Guide_Audio_Source.Play();
        m_Sequence_2_2_3_Marker.SetActive(true);
    }

    public void Start_Sequence_2_2_4()
    {

        m_Sequence_2_2_4_Marker.SetActive(true);
        m_Guide_Audio_Source.clip = m_Sequence_2_2_5_Audio_Clip;
        m_Guide_Audio_Source.Play();

    }

    public void Start_Sequence_2_2_5()
    {

    }


        #endregion

        #region Sequence3
        #endregion

        #region Sequence4
        #endregion


#if (UNITY_EDITOR)
        [CustomEditor(typeof(DC_GameManager))]
    public class CustomInspectorAnimationTester_1 : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DC_GameManager ATester = (DC_GameManager)target;
            if (GUILayout.Button("Cards Distribution"))
            {
                ATester.Stage_Indtroduction_Panel_1_Next();
            }
            //if (GUILayout.Button("all fold "))
            //{
            //    ATester.CollectAllcardsOntable();
            //}
            //if (GUILayout.Button("All Coins On table"))
            //{
            //    ATester.allcoinsontable();
            //}
            //if (GUILayout.Button("All Coins On to dealer"))
            //{
            //    ATester.CollectChipsOntable();
            //}
        }
    }
#endif
}
