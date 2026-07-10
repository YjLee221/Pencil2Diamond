using System;
using System.Collections.Generic;
using UnityEngine;

// 대화 데이터와 진행 흐름을 제어하는 클래스
public class TalkManager : MonoBehaviour
{
    public TextAsset tutorialDialogTsv;
    public DialogManager dialogManager;

    readonly Dictionary<string, DialogData> dialogDatabase = new Dictionary<string, DialogData>();
    DialogData currentData;

    [SerializeField] UIManager uiManager;

    public event Action<string> OnDialogFinished;

    void Awake()
    {
        LoadDialogData();
    }

    public void ResumeAfterMinigame()
    {
        if(currentData != null && !string.IsNullOrEmpty(currentData.nextId))
        {
            StartDialog(currentData.nextId);
        }
        else
        {
            dialogManager.HideDialog();

            if (currentData != null) OnDialogFinished?.Invoke(currentData.speakerType);
            currentData = null;
        }
    }

    public void ResumeAfterMinigame_successOrFail(bool isSuccess)
    {
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

        // 현재 대사가 진행 중이면 타이핑을 건너뛰고 전체 대사를 표시
        if(dialogManager.isTyping)
        {
            dialogManager.SkipTyping(currentData.content);
            return;
        }
        else // 타이핑이 끝났으면 다음 대사로 이동
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

            if(columns.Length >= 8)
            {
                DialogData data = new DialogData();

                data.id = columns[0];
                data.type = columns[1];
                data.speaker = columns[2];
                data.speakerType = columns[3];
                data.content = columns[4].Replace("\\n", "\n"); // 줄바꿈 처리

                if (Enum.TryParse(columns[5], out PlayerEmotion emotion))
                {
                    data.emotion = emotion;
                }
                else
                {
                    data.emotion = PlayerEmotion.Normal; // 기본값 설정
                }

                data.nextId = columns[6].Trim();
                data.failId = columns[7].Trim();

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
            if (currentData.type == "miniGameStart")
            {
                OnDialogFinished?.Invoke(currentData.speakerType);
                return;
            }

            else
            {
                uiManager.EndWorkCanvas();
            }

            dialogManager.ShowDialog(currentData.content, currentData.emotion, currentData.speaker, currentData.speakerType);
        }
    }
}
