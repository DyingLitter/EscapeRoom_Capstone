using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;
    private int dialogueIndex;
    public bool isTyping, isDialogueActive;
    private PlayerController player;
    [SerializeField] Canvas canvas;
    private void Start()
    {
        dialogueUI = DialogueController.instance;
        player = FindAnyObjectByType<PlayerController>();
    }

    public bool CanInteract() => !isDialogueActive;

    public void InteractedWith(GameObject NPC)
    {
        if (dialogueData == null)
        {
            return;
        }

        if (!isDialogueActive)
        {
            StartDialogue();
            return;
        }

        if (isTyping)
        {
            StopAllCoroutines();
            DisplayLineText(dialogueData.dialogueLines[dialogueIndex].text);
            isTyping = false;

            isTyping = false;
        }
        else
        {
            NextLine();
        }

    }

    public void StartDialogue() 
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        if (dialogueData.IsPassiveDialogue == false && player != null)
        {
            player.speed = 0;
        }

        DisplayCurrentLine();
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            DisplayLineText(dialogueData.dialogueLines[dialogueIndex].text);
            isTyping = false;
            return;
        }

        dialogueUI.ClearChoices();

        var currentLine = dialogueData.dialogueLines[dialogueIndex];
        if (currentLine.IsEndLine)
        {
            dialogueData.TriggerWorldChange(currentLine.endAction);
            EndDialogue();
            return;
        }

        foreach (NPCDialogue.DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
              DisplayChoices(dialogueChoice);
              return;
            }
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public IEnumerator TypeLine()
    {
        isTyping = true;

        var currentDialogueLine = dialogueData.dialogueLines[dialogueIndex];
        var currentLine = "";

        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(currentDialogueLine.speaker);

        if (dialogueData.IsPassiveDialogue == true)
        {
            SetSpeakerInfo(currentDialogueLine.speaker, null);
            dialogueUI.ShowDialogueUI2(true);
            dialogueUI.SetDialogueText2("");
        }
        else
        {
            SetSpeakerInfo(currentDialogueLine.speaker, null);
            dialogueUI.ShowDialogueUI(true);
            dialogueUI.SetDialogueText("");
        }


        foreach (char letter in currentDialogueLine.text.ToCharArray())
        {
            currentLine += letter;
            DisplayLineText(currentLine);
            yield return new WaitForSeconds(dialogueData.textSpeed);
        }

        isTyping = false;


        if (dialogueData.autoProgress)
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    private void SetSpeakerInfo(NPCDialogue.Speaker speaker, Sprite portraitOverride)
    {
        if (speaker == NPCDialogue.Speaker.NPC)
        {
            var portrait = portraitOverride ?? dialogueData.npcInfo.portrait;
            dialogueUI.SetNPCInfo(dialogueData.npcInfo.npcName, portrait);
        }
        else if (speaker == NPCDialogue.Speaker.You)
        {
            var portrait = portraitOverride ?? dialogueData.playerPortrait;
            dialogueUI.SetPlayerInfo("You", portrait);
        }
    }

    private void DisplayLineText(string text)
    {
        if (dialogueData.IsPassiveDialogue)
            dialogueUI.SetDialogueText2(text);
        else
            dialogueUI.SetDialogueText(text);
    }

    void DisplayChoices(NPCDialogue.DialogueChoice choice) //Allows for displaying choices and handling the logic when a choice is selected
    {
        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(NPCDialogue.Speaker.You);

        for (int i = 0; i < choice.choices.Length; i++)
        {
            int localI = i; 
            int nextIndex = choice.nextDialogueIndexes[localI];

            dialogueUI.CreateChoiceButton(choice.choices[localI], () =>
            {
                dialogueData.OnChoiceSelected(choice, localI);
                ChooseOption(nextIndex);
            });
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();

        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;

        if (player != null)
        {
            player.speed = 4;
        }

        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        dialogueUI.SetDialogueText2("");
        dialogueUI.ShowDialogueUI2(false);

        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(NPCDialogue.Speaker.NPC);

        if (dialogueData.OneTimeDialogue)
        {
            enabled = false;
        }
    }

    public void EmergencyClear()
    {
        dialogueUI.SetDialogueText("");
        dialogueUI.SetDialogueText2("");

    }

    public void PlayerHide()
    {
        player.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
}