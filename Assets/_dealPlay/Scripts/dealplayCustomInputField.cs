using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dealplayCustomInputField : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private TextMeshPro m_PlaceHolder;
    [SerializeField]
    private TMP_InputField m_MainText;
   
    #endregion

    #region Private_Methods
   

    #endregion



    private TouchScreenKeyboard keyboard;
    public string text = "";
    private bool isSelected = false;

   // public Text displayText;  // Reference to a UI Text component to display the input text

    // Static reference to the currently active input field
    private static dealplayCustomInputField activeInputField;

    void Update()
    {
        if (isSelected && keyboard != null)
        {
            // Update the text based on the keyboard input
            if (keyboard.status == TouchScreenKeyboard.Status.Visible)
            {
               // m_PlaceHolder.gameObject.SetActive(false);


                text = keyboard.text;
             //   displayText.text = text;
                m_MainText.text = text;
            }
            else if (keyboard.status == TouchScreenKeyboard.Status.Done || keyboard.status == TouchScreenKeyboard.Status.Canceled)
            {
                // Handle completion or cancellation
                Deactivate();
            }
        }
    }

    public void OnSelect()
    {
        if (activeInputField != null && activeInputField != this)
        {
            // Deactivate the previously active input field
            activeInputField.Deactivate();
        }

        // Open the keyboard when the input field is selected
        keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default);
        isSelected = true;
        activeInputField = this;
    }

    public void Deactivate()
    {
        if (keyboard != null)
        {
            keyboard = null;
        }
    /*    if(String.IsNullOrEmpty(m_MainText.text))
        {
            m_PlaceHolder.gameObject.SetActive(true);
        }*/
        isSelected = false;
    }







}
