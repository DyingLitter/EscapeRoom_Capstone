using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Image PlayerImage;

    public GameObject dialoguePanel2;
    public TMP_Text dialogueText2;
    public Image portraitImage2;

    [Range(0f, 1f)]
    public float dimFactor = 0.5f;

    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;


    void Awake()
    {
        if (instance == null) instance = this;  
        else Destroy(gameObject);
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNPCInfo(string npcName, Sprite npcPortrait)
    {
        nameText.text = npcName;
        portraitImage.sprite = npcPortrait;
    }

    public void SetPlayerInfo(Sprite playerPortrait)
    {

        PlayerImage.sprite = playerPortrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void ShowDialogueUI2(bool show)
    {
        dialoguePanel2.SetActive(show);
    }

    public void SetNPCInfo2(Sprite npcPortrait)
    {
        portraitImage2.sprite = npcPortrait;
    }

    public void SetDialogueText2(string text)
    {
        dialogueText2.text = text;
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateChoiceButton(string choiceText, UnityAction onClickAction)
    {

        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        var label = choiceButton.GetComponentInChildren<TMP_Text>();
        if (label != null) label.text = choiceText;

        var btn = choiceButton.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(onClickAction);
        }

    }

    public void SetPortraitBrightness(bool bright)
    {
        if (portraitImage == null && PlayerImage == null) return;

        float dim = Mathf.Clamp01(dimFactor);

        if (bright)
        {
            if (portraitImage != null) portraitImage.color = new Color(1f, 1f, 1f, portraitImage.color.a);
            if (PlayerImage != null) PlayerImage.color = new Color(dim, dim, dim, PlayerImage.color.a);
        }
        else
        {
            if (portraitImage != null) portraitImage.color = new Color(dim, dim, dim, portraitImage.color.a);
            if (PlayerImage != null) PlayerImage.color = new Color(1f, 1f, 1f, PlayerImage.color.a);
        }
    }
}
