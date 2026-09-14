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



    private void Start()
    {
        dialogueUI = DialogueController.instance;
        player = FindAnyObjectByType<PlayerController>();
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void InteractedWith(GameObject NPC)
    {

        if (dialogueData == null)
        {
            return;
        }
        else if (isDialogueActive)
        {
            
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
        else
        {
            StartDialogue();
        }
    }

    public void StartDialogue() //Initializes dialogue, sets up UI, and disables player movement and camera control
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        player.speed = 0;
       
        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(true);

        DisplayCurrentLine();
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();

            var current = dialogueData;

            {
                dialogueUI.SetDialogueText(current.dialogueLines[dialogueIndex]);
            }

            isTyping = false;
        }

        dialogueUI.ClearChoices();

        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(false);

        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            if (dialogueIndex < dialogueData.endDialogueActions.Length && !string.IsNullOrEmpty(dialogueData.endDialogueActions[dialogueIndex]))
            {
                dialogueData.TriggerWorldChange(dialogueData.endDialogueActions[dialogueIndex]);
            }
            EndDialogue();
            return;
        }

        foreach(NPCDialogue.DialogueChoice dialogueChoice in dialogueData.choices)
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

        var current = dialogueData;
        string currentLine = "";

        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(false);

        if (current.Italics)
        {
            dialogueUI.dialogueText.fontStyle = FontStyles.Italic;
        }
        else
        {
            dialogueUI.dialogueText.fontStyle = FontStyles.Normal;
        }

        {
            dialogueUI.SetNPCInfo(current.npcName, current.NPCPortrait);
            dialogueUI.SetPlayerInfo(current.playerPortrait);
            dialogueUI.ShowDialogueUI(true);
            dialogueUI.SetDialogueText("");
        }

       
        foreach (char letter in current.dialogueLines[dialogueIndex].ToCharArray())
        {
            currentLine += letter;

            {
                dialogueUI.SetDialogueText(currentLine);
            }

            yield return new WaitForSeconds(dialogueData.textSpeed);
        }

        isTyping = false;

        
        if (current.autoProgress)
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(NPCDialogue.DialogueChoice choice) //Allows for displaying choices and handling the logic when a choice is selected
    {
        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(true);

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

        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(true);


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

        player.speed = 4;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);


        if (dialogueUI != null) dialogueUI.SetPortraitBrightness(true);

        if (dialogueData.OneTimeDialogue)
        {
            gameObject.GetComponent<NPC>().enabled = false;
        }

    }

    public void EmergencyClear()
    {
        dialogueUI.SetDialogueText("");

    }
}
