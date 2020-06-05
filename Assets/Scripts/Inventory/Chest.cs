
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> chestInv = new List<Item>();
    public Item selectedItem;
    public bool showChestInv;
    public Vector2 scr;

    private void Start()
    {
        chestInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        chestInv.Add(ItemData.CreateItem(Random.Range(100, 102)));

    }

    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        if (showChestInv)
        {
            //display of chest items
            for (int i = 0; i < chestInv.Count; i++)
            {
                if (GUI.Button(new Rect(12.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y),chestInv[i].Name))
                {

                }
            }
            //display items in player inventory

        }
    }
}
