using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "Dialogue System/Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public Speaker speaker;
        public string text;
        public string endAction; // Replaces endDialogueLines + endDialogueActions

        public bool IsEndLine => !string.IsNullOrEmpty(endAction);
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public int dialogueIndex;
        public string[] choices;
        public int[] nextDialogueIndexes;
        public string[] actions;
    }

    [System.Serializable]
    public class NPCInfo
    {
        public string npcName;
        public Sprite portrait;
    }

    public enum Speaker { NPC, You }

    // Consolidated container
    public NPCInfo npcInfo = new();
    public Sprite playerPortrait;

    public DialogueLine[] dialogueLines; 
    public DialogueChoice[] choices;

    public float autoProgressDelay = 2f;
    public float textSpeed = 0.05f;
    public bool OneTimeDialogue;
    public bool IsPassiveDialogue;
    public bool autoProgress;

    public void OnChoiceSelected(NPCDialogue.DialogueChoice choice, int selectedOptionIndex) 
    {
        Debug.Log($"Choice group {choice.dialogueIndex}, option {selectedOptionIndex} selected.");

        if (choice != null && choice.actions != null && selectedOptionIndex >= 0 && selectedOptionIndex < choice.actions.Length)
        {
            string action = choice.actions[selectedOptionIndex];
            if (!string.IsNullOrEmpty(action))
            {
                TriggerWorldChange(action);
                return;
            }
        }

       
    }

    public void OnDialogueEnd(int currentDialogueIndex)
    {
        if (currentDialogueIndex >= 0 && currentDialogueIndex < dialogueLines.Length && dialogueLines[currentDialogueIndex].IsEndLine)
        {

            if (currentDialogueIndex < dialogueLines.Length && !string.IsNullOrEmpty(dialogueLines[currentDialogueIndex].endAction))
            {
                TriggerWorldChange(dialogueLines[currentDialogueIndex].endAction);
            }
        }
    }

    public void TriggerWorldChange(string action)
    {
        NPC npc = GameObject.FindAnyObjectByType<NPC>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Animator camani = camera.GetComponent<Animator>();

        if (action == ("Fade"))
        {
            camani.SetTrigger("Fade");
        }
    }
}
