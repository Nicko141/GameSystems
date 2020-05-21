using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprovalDialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //index for our current line of dialogu and an index for a set question marker of the dialogue 
    public int index, optionIndex;
    public Vector2 scr;
    public MouseLook playerMouseLook;
    //object reference to the player
    //mouselook script reference for the maincamera
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public new string name;
    //array for text for our dialogue
    public string[] dialogueText;

    //approval variable
    public string[] negText, neuText, posText;
    public int approval;
    public string response1, response2;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        dialogueText = neuText;
    }
    private void OnGUI()
    {
        if (showDlg)
        {
            //set up our ratio messurements for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;


            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, 3 * scr.y), name + ":" + dialogueText[index]);
            if (approval <= -1)
            {
                dialogueText = negText;
            }
            if (approval == 0)
            {
                dialogueText = neuText;
            }

            if (approval >= 1)
            {
                dialogueText = posText;
            }

            if (!(index >= dialogueText.Length - 1 || index == optionIndex))
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Next"))
                {
                    index++;
                    
                }
            }
            //else if we are at the options
            else if (index == optionIndex)
            {
                //accept
                if (GUI.Button(new Rect(5 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), response1))
                {
                    index++;
                    if (approval<1)
                    {
                        approval++;

                    }
                    
                }
                //refuse
                if (GUI.Button(new Rect(10 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), response2))
                {
                    index = dialogueText.Length - 1;
                    if (approval>-1)
                    {
                        approval--;

                    }
                    
                }
            }

            else
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                {
                    //close the dialogue box
                    showDlg = false;
                    //set index back to 0 
                    index = 0;
                    //allow cameras mouselook to be turned back on
                    //get the component mouselook on the character and turn that back on
                    Camera.main.GetComponent<MouseLook>().enabled = true;
                    //get the component movement on the character and turn that back on
                    playerMouseLook.enabled = true;
                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible
                    Cursor.visible = false;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
