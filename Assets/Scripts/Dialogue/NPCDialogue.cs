using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string characterName;
    public string[] dialogueText;
    public string[] negText, neuText, posText;
    public int approval;
    public string response1, response2;

    //public Shop myShop;
    public QuestGiver myQuest;
    void Start()
    {
        approval = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
