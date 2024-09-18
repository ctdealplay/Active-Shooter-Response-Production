using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_ButtonOptions : MonoBehaviour
{
    [SerializeField] GameObject Previous;
    [SerializeField] GameObject Next;
    [SerializeField] GameObject Skip;
    [SerializeField] GameObject Accept;

    public void OptionsActivator(bool previousBtn = false,bool nextBtn=false, bool skipBtn=false,bool acceptBtn=false) {
        Previous.SetActive(previousBtn);
        Next.SetActive(nextBtn);
        Skip.SetActive(skipBtn);
        Accept.SetActive(acceptBtn);
    }
}
