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
  
        public string npcName;
        public Sprite NPCPortrait;
        public Sprite playerPortrait; 
        
        [TextArea(2, 5)]
   
        public string[] dialogueLines;

        public float autoProgressDelay = 2f;
        public float textSpeed = 0.05f;

        public bool OneTimeDialogue;
        
        public bool autoProgress;
        public bool[] endDialogueLines;
        public string[] endDialogueActions;

        public bool Italics;

        public DialogueChoice[] choices;

    

    [System.Serializable]
    public class DialogueChoice
    {
        public int dialogueIndex; //Current dialogue line index where the choice is presented
        public string[] choices; //Selectable Choices
        public int[] nextDialogueIndexes; //Where the choices lead
        public string[] actions;//If anything is to happen
    }



    public void OnChoiceSelected(NPCDialogue.DialogueChoice choice, int selectedOptionIndex) //Allows changes in the game based on choice
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
        // Check if this is an end dialogue line
        if (currentDialogueIndex >= 0 && currentDialogueIndex < endDialogueLines.Length && endDialogueLines[currentDialogueIndex])
        {
            // Check if there's a corresponding action
            if (currentDialogueIndex < endDialogueActions.Length && !string.IsNullOrEmpty(endDialogueActions[currentDialogueIndex]))
            {
                TriggerWorldChange(endDialogueActions[currentDialogueIndex]);
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
