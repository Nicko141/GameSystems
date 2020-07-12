using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public QuestState questState;

    public GoalType goalType;
    public string enemyType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;
    public CanvasDialogueController dlgCont;


    public void EnemyKilled(string type)
    {
        if (goalType == GoalType.Kill && type == enemyType)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                isReached = true;
                dlgCont.completePanel.SetActive(true);
                questState = QuestState.Complete;
                Debug.Log("Quest Complete");
            }
        }
        
    }
    public void ItemCollected(int type)
    {
        if (goalType == GoalType.Gather && type == itemId)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                isReached = true;
                dlgCont.completePanel.SetActive(true);
                questState = QuestState.Complete;

                Debug.Log("Quest Complete");
            }
        }

    }
}
public enum GoalType
{
    Kill,
    Gather

}

public enum QuestState
{
    Availiable,
    Active,
    Complete,
    Claimed
}
