using System;
using UnityEngine;

[Serializable]
public class GameProgressData
{
    [Header("Game Flow")]
    public GamePhase gamePhase;
    public TutorialStep tutorialStep;
    public string currentDialogId;

    [Header("Tutorial State")]
    public bool isTutorialCompleted;

    [Header("Pencil")]
    public int unSharpenedPencilCount;
    public int sharpeningPencilCount;
    public int sharpenedPencilCount;

    [Header("Graphite")]
    public int unPressedGraphiteCount;
    public int graphiteCount;

    [Header("Currency")]
    public int diamondCount;
    public int coinAmount;
}
