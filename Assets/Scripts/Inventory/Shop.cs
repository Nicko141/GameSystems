using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Item> shopInv = new List<Item>();
    public Item selectedItem;
    public bool showShopInv;
    public Vector2 scr;
    public ApprovalDialogue dlg;

    private void Start()
    {
        shopInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        shopInv.Add(ItemData.CreateItem(Random.Range(100, 102)));

    }

    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        if (showShopInv)
        {
            //display of chest items
            for (int i = 0; i < shopInv.Count; i++)
            {
                if (GUI.Button(new Rect(12.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), shopInv[i].Name))
                {
                    selectedItem = shopInv[i];
                }
            }
            if (selectedItem != null)
            {
                //GUI.Box(new Rect(9f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
                GUI.Box(new Rect(9f * scr.x, 3.5f * scr.y, 3 * scr.x, 0.5f * scr.y), selectedItem.Name);
                GUI.Box(new Rect(9f * scr.x, 4f * scr.y, 3 * scr.x, 1f * scr.y), selectedItem.Description + "\nValue " + selectedItem.Value + "\nAmount " + selectedItem.Amount);
                if (LinearInventory.money >= selectedItem.Value)
                {
                    if (GUI.Button(new Rect(11f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Buy Item"))
                    {
                        LinearInventory.money -= selectedItem.Value;
                        //add to player
                        LinearInventory.inv.Add(ItemData.CreateItem(selectedItem.ID));

                        //remove from chest
                        shopInv.Remove(selectedItem);
                        selectedItem = null;

                        return;
                    }
                }
                
            }
            if (GUI.Button(new Rect(12 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Exit"))
            {
                showShopInv = false;
                LinearInventory.showInv = false;
                LinearInventory.currentShop = null;
                dlg.showDlg = true;
            }
            GUI.Box(new Rect(10 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Money $" + LinearInventory.money);

        }
    }
}
