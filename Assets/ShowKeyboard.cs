using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine;
using TMPro;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;
    
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());

    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard();
    }

    
}
