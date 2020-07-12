using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerHandler player;
    public LinearInventory inventory;
    public GameObject questWindow;

    public Text titleText, descriptionText, experienceText, goldText;
    public Text activeTitleText, activeDescText;
   

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }
    public void AcceptQuest()
    {
        quest.goal.questState = QuestState.Active;
        questWindow.SetActive(false);
        player.quest = quest;
        activeTitleText.text = quest.title;
        activeDescText.text = quest.description;
    }
   
    public void Claimed()
    {
        player.currentExp += quest.experienceReward;
        LinearInventory.money += quest.goldReward;
        quest.goal.questState = QuestState.Claimed;
        activeTitleText.text = "";
        activeDescText.text = "";
        Debug.Log("You Got " + quest.experienceReward + " EXP and $" + quest.goldReward);
    }

}
