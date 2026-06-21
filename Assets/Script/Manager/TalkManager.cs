using System;
using System.Collections.Generic;
using UnityEngine;

// 대화의 데이터 및 Controller 역할을 하는 클래스
public class TalkManager : MonoBehaviour
{
    public TextAsset tutorialDialogTsv;
    public DialogManager dialogManager;

    Dictionary<string, DialogData> dialogDatabase = new Dictionary<string, DialogData>();
    DialogData currentData;

    [SerializeField] UIManager uiManager;

    void Awake()
    {
        LoadDialogData();
    }

    void OnEnable()
    {
        SharpeningPencil.OnPencilSharpeningCompleted += HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted += HandleCompletedPencilSharpening;
        PressMachine.OnMatchingTemperatureCompleted += HandleCompletedPencilSharpening;
    }

    void OnDisable()
    {
        SharpeningPencil.OnPencilSharpeningCompleted -= HandleCompletedPencilSharpening;
        PencilCollectedGraphite.OnGraphiteExtractionCompleted -= HandleCompletedPencilSharpening;
        PressMachine.OnMatchingTemperatureCompleted -= HandleCompletedPencilSharpening;
    }

    void HandleCompletedPencilSharpening(SharpeningPencil pencil)
    {
        ResumeAfterMinigame();
    }
    private void HandleCompletedPencilSharpening(PencilCollectedGraphite graphite)
    {
        ResumeAfterMinigame();
    }

    private void HandleCompletedPencilSharpening(bool isSuccess)
    {
        ResumeAfterMinigame_successOrFail(isSuccess);
    }

    public void ResumeAfterMinigame()
    {
        Debug.Log("미니게임 클리어! 대화 재개~");

        if(currentData != null && !string.IsNullOrEmpty(currentData.nextId))
        {
            StartDialog(currentData.nextId);
        }
        else
        {
            dialogManager.HideDialog();
            Debug.Log("대화 끝~!!");
            currentData = null;
        }
    }

    private void ResumeAfterMinigame_successOrFail(bool isSuccess)
    {
        Debug.Log($"미니게임 클리어 여부: {isSuccess}. 대화 재개~");
        string nextDialogId = isSuccess ? currentData.nextId : currentData.failId;

        if(!string.IsNullOrEmpty(nextDialogId))
        {
            StartDialog(nextDialogId);
        }
        else
        {
            dialogManager.HideDialog();
            Debug.Log("대화 끝~!!");
            currentData = null;
        }
    }

    public void OnClickForNextDialog()
    {
        if (currentData == null) return;

        // 현재 대사 진행중인 경우, 타이핑 스킵 및 전체 대사 표시
        if(dialogManager.isTyping)
        {
            dialogManager.SkipTyping(currentData.content);
            return;
        }
        else // 타이핑이 끝난 경우, 다음 대사로 이동
        {
            if(!string.IsNullOrEmpty(currentData.nextId)) StartDialog(currentData.nextId);
            else
            {
                dialogManager.HideDialog();
                Debug.Log("대화 끝~!!");
                currentData = null;
            }
        }
    }

    void LoadDialogData()
    {
        if (tutorialDialogTsv == null) return;

        dialogDatabase.Clear();
        string[] lines = tutorialDialogTsv.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // 첫 번째 줄은 헤더이므로 건너뛰기
        {
            string line = lines[i].Replace("\r", ""); // 줄바꿈 문자 제거
            
            if(string.IsNullOrWhiteSpace(line)) continue; // 빈 줄 건너뛰기

            string[] columns = line.Split('\t'); // TSV 파일이므로 탭으로 구분

            if(columns.Length >= 7)
            {
                DialogData data = new DialogData();

                data.id = columns[0];
                data.type = columns[1];
                data.speaker = columns[2];
                data.content = columns[3].Replace("\\n", "\n"); // 줄바꿈 처리

                if (Enum.TryParse(columns[4], out PlayerEmotion emotion))
                {
                    data.emotion = emotion;
                }
                else
                {
                    data.emotion = PlayerEmotion.Normal; // 기본값 설정
                }

                data.nextId = columns[5].Trim(); 
                data.failId = columns[6].Trim();

                if(!dialogDatabase.ContainsKey(data.id))
                {
                    dialogDatabase.Add(data.id, data);
                }
                else
                {
                    Debug.LogWarning($"중복된 Dialog ID: {data.id} (라인 {i + 1})");
                }
            }
        }
    }

    public void StartDialog(string startId)
    {
        if (dialogDatabase.TryGetValue(startId, out currentData))
        {
            if (string.IsNullOrEmpty(currentData.type))
            {
                uiManager.StartWorkingCanvas(currentData.id);
                return;
            }

            else 
            {
                uiManager.EndWorkCanvas(); 
            }

            dialogManager.ShowDialog(currentData.content, currentData.emotion);
        }
    }
}
