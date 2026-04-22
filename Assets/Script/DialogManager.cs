using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogData
{
    public string dialogId;
    public string dialogType;
    public string speakerName;
    public string contents;
}

public enum PlayerEmotion
{
    Normal,
    Happy,
    Sad,
    Surprised,
    Max_Emotion
}

// Enum과 Sprite를 매칭하고 Inspector에서 설정할 수 있도록 Serializable 클래스로 감쌉니다.
[Serializable]
public struct EmotionSprite
{
    public PlayerEmotion emotionType;
    public Sprite playerSprite;
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject scriptPanel;
    [SerializeField] Image playerImage;
    [SerializeField] TextMeshProUGUI dialogContents;
    [SerializeField] List<EmotionSprite> emotionSprites; // Emotion과 Sprite를 매칭하는 리스트

    void Start()
    {
        scriptPanel.SetActive(false);
    }

    public void ShowDialog(string message, PlayerEmotion currentEmotion = PlayerEmotion.Happy)
    {
        scriptPanel.SetActive(true);

        ChangePlayerEmotion(currentEmotion);

        StartCoroutine(TypeSentence(message));
    }

    void ChangePlayerEmotion(PlayerEmotion emotion)
    {
        foreach (var item in emotionSprites)
        {
            if(item.emotionType == emotion)
            {
                playerImage.sprite = item.playerSprite;
                return;
            }
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogContents.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogContents.text += letter;

            yield return new WaitForSeconds(0.05f); // 글자 간격 조절
        }
    }
}
