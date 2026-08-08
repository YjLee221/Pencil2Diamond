using System;
using UnityEngine;
// ReSharper disable All

public class MainFlowController : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    void Start()
    {
        Debug.Log("Starting Main Flow Controller");
    }

    void OnEnable()
    {
        mainMenuUI.OnPressingButtonClickedEvent += HandlePressingButtonClickedEvent;
        mainMenuUI.OnSharpeningButtonClickedEvent += HandleSharpeningButtonClickedEvent;
    }

    void HandlePressingButtonClickedEvent()
    {
        Debug.Log("Pressing Button Clicked");
        mainMenuUI.ShowWorkAbleList(WorkingStep.Pressing);
    }

    void HandleSharpeningButtonClickedEvent()
    {
        Debug.Log("Sharpening Button Clicked");
        mainMenuUI.ShowWorkAbleList(WorkingStep.Sharpening);
    }

    void OnDisable()
    {
        mainMenuUI.OnPressingButtonClickedEvent -= HandlePressingButtonClickedEvent;
        mainMenuUI.OnSharpeningButtonClickedEvent -= HandleSharpeningButtonClickedEvent;
    }
}
