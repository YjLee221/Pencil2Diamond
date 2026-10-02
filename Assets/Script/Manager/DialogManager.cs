using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogData
{
    public string id;
    public string type;
    public string speaker;
    public string speakerType;
    public string command;
    public string content;
    public PlayerEmotion emotion;
    public string nextId;
    public string failId;
}

public enum PlayerEmotion
{
    None,
    Normal,
    Happy,
    Sad,
    Surprised,
    Max
}

// Enum과 Sprite를 매칭하고 Inspector에서 설정할 수 있도록 Serializable 클래스로 감쌉니다.
[Serializable]
public struct PlayerEmotionSprite
{
    public PlayerEmotion emotionType;
    public string currentSpeaker;
    public Sprite playerSprite;
}

[Serializable]
public struct  NpcSprite
{
    public string currentSpeaker;
    public Sprite npcSprite;
}

// 실제 화면에 대화창을 띄우고, 대사와 감정에 따라 플레이어의 표정을 바꿔주는 역할을 하는 클래스
public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject scriptPanel;
    [SerializeField] Image playerImage;
    [SerializeField] Button nextDialogBtn;

    [SerializeField] TextMeshProUGUI dialogContents;
    [SerializeField] List<PlayerEmotionSprite> emotionSprites; // Emotion과 Sprite를 매칭하는 리스트
    [SerializeField] List<NpcSprite> npcSprites; // NPC와 Sprite를 매칭하는 리스트

    Coroutine _typingCoroutine; // 현재 진행 중인 타이핑 코루틴을 저장하는 변수
    public bool IsTyping { get; private set; } // 타이핑 중인지 여부를 나타내는 프로퍼티
    [SerializeField] private float typingTime;

    void Start()
    {
        scriptPanel.SetActive(false);
    }

    public void ShowDialog(string message, PlayerEmotion currentEmotion, string currentSpeaker, string speakerType)
    {
        if(currentSpeaker == "NPC") ShowNpcImage(speakerType);
        else ChangePlayerEmotion(currentEmotion);

        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeSentence(message));
    }

    void ChangePlayerEmotion(PlayerEmotion emotion)
    {
        foreach (var item in emotionSprites)
        {
            if(item.emotionType == emotion)
            {
                if (item.emotionType == PlayerEmotion.None)
                {
                    playerImage.gameObject.SetActive(false);
                    return;
                }
                else 
                {
                    playerImage.gameObject.SetActive(true);
                    if(item.playerSprite != null)
                    {
                        playerImage.sprite = item.playerSprite;
                    }
                }
            }
        }
    }

    void ShowNpcImage(string speakerType)
    {
        foreach (var item in npcSprites)
        {
            if(item.currentSpeaker == speakerType)
            {
                playerImage.gameObject.SetActive(true);
                if (item.npcSprite != null)
                {
                    playerImage.sprite = item.npcSprite;
                }
                break;
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        IsTyping = true;
        dialogContents.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogContents.text += letter;

            yield return new WaitForSeconds(typingTime); // 글자 간격 조절
        }

        IsTyping = false;
        _typingCoroutine = null;
    }

    public void SkipTyping(string fullSentence)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);

        dialogContents.text = fullSentence;
        IsTyping = false;
    }

    public void HideDialog()
    {
        scriptPanel.SetActive(false);
    }
}
