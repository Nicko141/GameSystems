using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject completePanel;
    public string[] currentDialogue;
    public static NPCDialogue currentNPC;
    
    public MouseLook playerMouseLook;
    public Text dialogueText;
    public Text buttonText;
    public GameObject button;
    public GameObject acceptButton;
    public GameObject refuseButton;
    public int index, optionIndex;
    



    void Start()
    {
        playerMouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
        

    }
    public void DlgApproval()
    {
        if (currentNPC.approval <= -1)
        {
            currentDialogue = currentNPC.negText;
        }
        if (currentNPC.approval == 0)
        {
            currentDialogue = currentNPC.neuText;
        }

        if (currentNPC.approval >= 1)
        {
            currentDialogue = currentNPC.posText;
        }
        dialogueText.text = currentNPC.characterName + ": " + currentDialogue[index];
    }

    public void Dialogue()
    {
        DlgApproval();
        
        buttonText.text = "Next";
        acceptButton.SetActive(false);
        refuseButton.SetActive(false);
    }
    public void Buttons()
    {
        if (!(index >= currentDialogue.Length - 1 || index == optionIndex))
        {
            button.SetActive(true);
            acceptButton.SetActive(false);
            refuseButton.SetActive(false);
            index++;
            if (index >= currentDialogue.Length -1)
            {
                acceptButton.SetActive(false);
                refuseButton.SetActive(false);
                buttonText.text = "Bye";
                
            }
        }
        else if (index == optionIndex)
        {
            index++;
            button.SetActive(false);
            acceptButton.SetActive(true);
            refuseButton.SetActive(true);
        }
        else
        {
            index = 0;
            Camera.main.GetComponent<MouseLook>().enabled = true;
            playerMouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            dialoguePanel.SetActive(false);
            acceptButton.SetActive(false);
            refuseButton.SetActive(false);
            currentNPC = null;
        }
        if (currentNPC != null)
        {
            DlgApproval();
        }
        
      
    }
    public void Accept()
    {
        if (currentNPC.approval < 1)
        {
            currentNPC.approval++;

        }
    }
    public void Refuse()
    {
        
        if (currentNPC.approval > -1)
        {
            currentNPC.approval--;

        }
    }


}
