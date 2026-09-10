using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        //public bool useSecondBox;
        public bool autoProgress;
        public bool[] endDialogueLines;
    
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

    private void TriggerWorldChange(string action)
    {
        NPC npc = GameObject.FindAnyObjectByType<NPC>();
        if (action == "fail")
        {
            Debug.Log("Player has failed the dialogue choice.");
            npc.EndDialogue();
            GameObject player = GameObject.FindWithTag("Player");
            Animator animator = player.GetComponent<Animator>();

            if (animator != null)
            {
                animator.applyRootMotion = false;
                animator.SetTrigger("Launch");
            }

        }

        if (action == "pass")
        {
            Animator animator = npc.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Launch2");
                npc.EndDialogue();
            }

            GameObject gate = GameObject.Find("BridgePontoon3");
            gate.GetComponent<MeshRenderer>().enabled = true;
  
        }

            if (action == "Gate1Open") //Change this to the actual action you want to trigger
            {
                GameObject gate = GameObject.Find("BridgePontoon1");
                gate.GetComponent<MeshRenderer>().enabled = true;
        }

            if (action == "Gate2Open") //Change this to the actual action you want to trigger
            {
                GameObject gate = GameObject.Find("BridgePontoon2");
                gate.GetComponent<MeshRenderer>().enabled = true;
        }        
    }
}
