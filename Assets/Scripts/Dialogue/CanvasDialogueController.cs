using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public string[] currentDialogue;
    public string characterNPCName;
    public MouseLook playerMouseLook;
    public Text dialogueText;
    public Text buttonText;
    public int index, optionIndex;
    


    void Start()
    {
        playerMouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
        
    }

    public void Dialogue()
    {
        dialogueText.text = characterNPCName + ": " + currentDialogue[index];
        buttonText.text = "Next";
    }
    public void Buttons()
    {
        if (!(index >= currentDialogue.Length - 1))
        {
            index++;
            if (index >= currentDialogue.Length -1)
            {
                buttonText.text = "Bye";
            }
        }
        else
        {
            index = 0;
            Camera.main.GetComponent<MouseLook>().enabled = true;
            playerMouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            dialoguePanel.SetActive(false);
        }
        dialogueText.text = characterNPCName + ": " + currentDialogue[index];
    }
   
}
