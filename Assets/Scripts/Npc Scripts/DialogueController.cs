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

    public UnityEvent onDialogueTextClicked = new UnityEvent();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        AddClickListener(dialogueText);
        AddClickListener(dialogueText2);
    }
    private void AddClickListener(TMP_Text textComponent)
    {
        if (textComponent == null) return;

        Button btn = textComponent.GetComponent<Button>();
        if (btn == null)
        {
            btn = textComponent.gameObject.AddComponent<Button>();
        }

        btn.onClick.AddListener(() => onDialogueTextClicked.Invoke());
    }

    public void ShowDialogueUI(bool show)
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(show);
            if (show && dialoguePanel2 != null) dialoguePanel2.SetActive(false);
        }
    }

    public void SetNPCInfo(string npcName, Sprite npcPortrait)
    {
        if (nameText != null) nameText.text = npcName;
        if (portraitImage != null) portraitImage.sprite = npcPortrait;
    }

    public void SetPlayerInfo(string playerName, Sprite playerPortrait)
    {
        if (nameText != null) nameText.text = playerName;
        if (PlayerImage != null) PlayerImage.sprite = playerPortrait;
    }

    public void SetDialogueText(string text)
    {
        if (dialogueText != null) dialogueText.text = text;
    }

    public void ShowDialogueUI2(bool show)
    {
        if (dialoguePanel2 != null)
        {
            dialoguePanel2.SetActive(show);
            if (show && dialoguePanel != null) dialoguePanel.SetActive(false);
        }
    }

    public void SetNPCInfo2(Sprite npcPortrait)
    {
        if (portraitImage2 != null) portraitImage2.sprite = npcPortrait;
    }

    public void SetDialogueText2(string text)
    {
        if (dialogueText2 != null) dialogueText2.text = text;
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

    public void SetPortraitBrightness(NPCDialogue.Speaker speaker)
    {
        if (portraitImage == null && PlayerImage == null) return;

        float dim = Mathf.Clamp01(dimFactor);

        if (speaker == NPCDialogue.Speaker.NPC)
        {
            if (portraitImage != null) portraitImage.color = new Color(1f, 1f, 1f, portraitImage.color.a);
            if (PlayerImage != null) PlayerImage.color = new Color(dim, dim, dim, PlayerImage.color.a);
        }
        else if (speaker == NPCDialogue.Speaker.You)
        {
            if (portraitImage != null) portraitImage.color = new Color(dim, dim, dim, portraitImage.color.a);
            if (PlayerImage != null) PlayerImage.color = new Color(1f, 1f, 1f, PlayerImage.color.a);
        }
    }
}