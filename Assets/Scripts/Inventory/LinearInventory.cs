
using System.Collections.Generic;
using UnityEngine;

public class LinearInventory : MonoBehaviour
{
    public PlayerHandler player;
    public static List<Item> inv = new List<Item>();
    public Item selectedItem;
    public static bool showInv;
    public GUIStyle style;
    public GUISkin skin;

    public Vector2 scr;
    public Vector2 scrollPos;
    public string sortType = "";
    public string[] enumTypesForItems;
    public static int money;

    public Transform dropLocation;
    [System.Serializable]
    public struct Equipment
    {
        public string slotName;
        public Transform equipLocation;
        public GameObject currentItem;
    };
    public Equipment[] equipmentSlots;

    public static Chest currentChest;
    public static Shop currentShop;
    void Start()
    {
        player = this.gameObject.GetComponent<PlayerHandler>();
        enumTypesForItems = new string[] { "All", "Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potion", "Scrolls", "Quest" };

        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(50));// broken test
        inv.Add(ItemData.CreateItem(100));
        inv.Add(ItemData.CreateItem(101));
        inv.Add(ItemData.CreateItem(102));
        inv.Add(ItemData.CreateItem(200));
        inv.Add(ItemData.CreateItem(201));
        inv.Add(ItemData.CreateItem(202));
    }

    // Update is called once per frame
    void Update()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        if (currentShop == null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;
                if (showInv)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                else
                {
                    if (!PauseMenu.isPaused)
                    {
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        if (currentChest != null)
                        {
                            currentChest.showChestInv = false;
                            currentChest = null;
                        }



                    }

                    return;
                }
            }
        }
        
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.I))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
            inv.Add(ItemData.CreateItem(Random.Range(100, 103)));
            inv.Add(ItemData.CreateItem(Random.Range(200, 203)));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            sortType = "Food";
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            sortType = "All";
        }
#endif
    }
    void Display()
    {
        //want to display everything in inventory
        if (sortType == "All" || sortType == "")
        {
            //34 or less
            if (inv.Count <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }


                }
            }
            //more than 34 items
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(0f, 0.25f*scr.y,3.75f * scr.x,8.5f*scr.y), scrollPos, new Rect(0,0,0,/*8.5f*scr.y+(inv.Count-34))or*/ inv.Count* 0.25f* scr.y), false, true);

                #region EVERYTHIGN DISPLAYED INSIDE SCROLL VIEW
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                #endregion

                GUI.EndScrollView();
            }
        }
        //display based on type
        else
        {
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
            //amount in type
            int a = 0;
            //slot position
            int s = 0;
            for (int i = 0; i < inv.Count; i++)
            {
                if (inv[i].Type == type)
                {
                    a++;
                    
                }
            }
            if (a<=34)//less than 34 of this type
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {   
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                        s++;
                    }
                    
                }
            }
            else//more than 34 of this type
            {
                scrollPos = GUI.BeginScrollView(new Rect(0f, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, a * 0.25f * scr.y), false, true);

                #region EVERYTHIGN DISPLAYED INSIDE SCROLL VIEW
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        if (GUI.Button(new Rect(0.5f * scr.x, s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                        s++;
                    }
                    
                }
                #endregion

                GUI.EndScrollView();
            }
        }
    }
    void UseItem()
    {
        
        GUI.Box(new Rect(5f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
        GUI.Box(new Rect(5f * scr.x, 3.5f * scr.y, 3 * scr.x, 0.5f * scr.y), selectedItem.Name, style);//style adds to one
        GUI.skin = skin; //skin adds to all within range
        GUI.Box(new Rect(5f * scr.x, 4f * scr.y, 3 * scr.x, 1f * scr.y), selectedItem.Description + "\nValue " + selectedItem.Value + "\nAmount " + selectedItem.Amount);
        //GUI.Box(new Rect(8f * scr.x, 2f * scr.y, 5 * scr.x, 0.5f * scr.y), "Value " + selectedItem.Value);
        //GUI.Box(new Rect(8f * scr.x, 2.5f * scr.y, 5 * scr.x, 0.5f * scr.y), "Amount " + selectedItem.Amount);

        
        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (player.attributes[0].currentValue < player.attributes[0].maxValue)
                {
                    if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                    {
                        player.attributes[0].currentValue = Mathf.Clamp(player.attributes[0].currentValue += selectedItem.Heal,0, player.attributes[0].maxValue);

                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            selectedItem = null;
                            return;
                        }
                    }
                }
                
                break;
            case ItemType.Weapon:
                if (equipmentSlots[2].currentItem == null || selectedItem.Name != equipmentSlots[2].currentItem.name)
                {
                    if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                    {
                        if (equipmentSlots[2].currentItem != null)
                        {
                            Destroy(equipmentSlots[2].currentItem);
                        }
                        GameObject curItem = Instantiate(selectedItem.Mesh, equipmentSlots[2].equipLocation);
                        equipmentSlots[2].currentItem = curItem;
                        curItem.name = selectedItem.Name;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Unequip"))
                    {
                        Destroy(equipmentSlots[2].currentItem);
                    }
                }
                break;
            case ItemType.Potion:
                if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Drink"))
                {

                }
                break;
            case ItemType.Apparel:
                if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Wear"))
                {

                }
                break;
            case ItemType.Crafting:
                if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Craft"))
                {

                }
                break;
            case ItemType.Ingredients:
                if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                {

                }
                break;
            case ItemType.Scrolls:
                if (GUI.Button(new Rect(6f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Read"))
                {

                }
                break;
            case ItemType.Quest:
                break;
            case ItemType.Money:
                break;
            default:
                break;
        }
        GUI.skin = null;//end of skin range

        if (GUI.Button(new Rect(5f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                {
                    Destroy(equipmentSlots[i].currentItem);
                }
            }
            //spawn in world
            GameObject droppedItem = Instantiate(selectedItem.Mesh, dropLocation.position, Quaternion.identity);
            droppedItem.name = selectedItem.Name;
            droppedItem.AddComponent<Rigidbody>().useGravity = true;
            droppedItem.GetComponent<ItemHandler>().enabled = true;
            if (selectedItem.Amount > 1)
            {
                selectedItem.Amount--;
            }
            else
            {
                inv.Remove(selectedItem);
                selectedItem = null;
                return;
            }
        }
        if (currentChest != null)
        {
            if (GUI.Button(new Rect(7f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Store"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                    {
                        Destroy(equipmentSlots[i].currentItem);
                    }
                }
                //spawn in world
                currentChest.chestInv.Add(selectedItem);

                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
        if (currentShop != null)
        {
            if (GUI.Button(new Rect(7f * scr.x, 3.25f * scr.y, scr.x, 0.25f * scr.y), "Sell"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                    {
                        Destroy(equipmentSlots[i].currentItem);
                    }
                }
                //spawn in world
                money += selectedItem.Value;
                currentShop.shopInv.Add(selectedItem);

                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }

    }
    private void OnGUI()
    {
        if (showInv)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            for (int i = 0; i < enumTypesForItems.Length; i++)
            {
                if (GUI.Button(new Rect(4f*scr.x + i * scr.x, 0, scr.x, 0.25f *scr.y),enumTypesForItems[i]))
                {
                    sortType = enumTypesForItems[i];
                }
            }
            Display();
            if (selectedItem !=null)
            {
                UseItem();
            }
        }
        
    }
}
