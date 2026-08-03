using System;
using UnityEngine;
// ReSharper disable All

public class MainFlowController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Starting Main Flow Controller");
    }

    void OnEnable()
    {
        MainMenuUI.OnPressingButtonClickedEvent += HandlePressingButtonClickedEvent;
    }

    void HandlePressingButtonClickedEvent()
    {
        Debug.Log("Pressing Button Clicked");
        
    }

    void OnDisable()
    {
        MainMenuUI.OnPressingButtonClickedEvent -= HandlePressingButtonClickedEvent;
    }
}
