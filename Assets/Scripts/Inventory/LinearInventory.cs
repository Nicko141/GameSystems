
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearInventory : MonoBehaviour
{
    #region variables
    public static LinearInventory playerInventory;
    public PlayerHandler player;
    public static List<Item> inv = new List<Item>();
    public int selectedItemIndex;
    public Item selectedItem;
    public static bool showInv;
    public GameObject invPanel;
    public GUIStyle style;
    public GUISkin skin;

    public Vector2 scr;
    public Vector2 scrollPos;
    public string sortType = "Food";
    public string[] enumTypesForItems;
    [SerializeField] Transform inventoryParent;
    public InventorySlotDisplay[] inventoryDisplays;
    public static int money;
    public Button useButton;
    public Button discardButton;
    public Image selectedItemImageDisplay;
    public Text selectedItemNameDisplay;
    public Text selectedItemDescDisplay;
    public Text selectedItemAmountDisplay;
    public Text selectedItemValueDisplay;
    public Text useText;
    public Transform dropLocation;
    [System.Serializable]
    public struct Equipment
    {
        public string slotName;
        public Transform equipLocation;
        public GameObject currentItem, inventoryMenu;
    };
    public Equipment[] equipmentSlots;

    public static Chest currentChest;
    public static Shop currentShop;
    #endregion
    private void Awake()
    {
        playerInventory = this;
    }
    void Start()
    {
        player = this.gameObject.GetComponent<PlayerHandler>();
        enumTypesForItems = new string[] { "All", "Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potion", "Scrolls", "Quest" };//sets up filter

        inventoryDisplays = inventoryParent.GetComponentsInChildren<InventorySlotDisplay>();//displays the items in the inventory

        //inventoryDisplays = FindObjectsOfType<InventorySlotDisplay>();

        AddItemToInventory(ItemData.CreateItem(0));
        AddItemToInventory(ItemData.CreateItem(1));
        AddItemToInventory(ItemData.CreateItem(50));// broken test
        AddItemToInventory(ItemData.CreateItem(100));
        AddItemToInventory(ItemData.CreateItem(101));
        AddItemToInventory(ItemData.CreateItem(102));
        AddItemToInventory(ItemData.CreateItem(200));
        AddItemToInventory(ItemData.CreateItem(201));
        AddItemToInventory(ItemData.CreateItem(202));
    }


   public void AddItemToInventory(Item item)//adds items to the inventory
    {
        if (inv.Count < inventoryDisplays.Length)
        {
            Debug.Log("Added " + item.Name);
            inv.Add(item);
            UpdateInventory();
        }
    }

    void UpdateInventory()//updates the inventory when changes are made
    {
        ItemType? type = sortType == "All" || sortType == "" ? null : (ItemType?)System.Enum.Parse(typeof(ItemType), sortType);

        int filterCount = 0;
        for (int i = 0; i < inv.Count; i++)
        {
            var item = inv[i];
            if (type == null || item.Type == (ItemType)type)
            {
                int index = i;
                var displaySlot = inventoryDisplays[filterCount];
                displaySlot.itemImage.color = Color.white;
                displaySlot.itemImage.sprite = item.Icon;
                displaySlot.button.onClick.RemoveAllListeners();
                displaySlot.button.onClick.AddListener(() => SelectItem(index));
                filterCount++;
            }
        }
        for (int i = filterCount; i < inventoryDisplays.Length; i++)
        {
            var displaySlot = inventoryDisplays[i];
            displaySlot.itemImage.color = new Color(0,0,0,0);
            displaySlot.button.onClick.RemoveAllListeners();
        }
    }

    void SelectItem(int itemIndex)//selects the item in the inventory and shows the bio
    {
        selectedItemIndex = itemIndex;
        selectedItem = inv[selectedItemIndex];
        selectedItemImageDisplay.sprite = selectedItem.Icon;
        selectedItemNameDisplay.text = selectedItem.Name;
        selectedItemDescDisplay.text = selectedItem.Description;
        selectedItemAmountDisplay.text = selectedItem.Amount.ToString();
        selectedItemValueDisplay.text = selectedItem.Value.ToString();
        switch (selectedItem.Type)//sets up the "use" button and sets the functionality of the button
        {
            #region case ItemType.Food:
            case ItemType.Food:
                useText.text = "Eat";
                if (player.attributes[0].currentValue < player.attributes[0].maxValue)
                { useButton.interactable = true; }
                else
                { useButton.interactable = false; }
                break;
            #endregion
            #region case ItemType.Weapon:
            case ItemType.Weapon:
                useButton.interactable = true;
                if (equipmentSlots[2].currentItem == null || selectedItem.Name != equipmentSlots[2].currentItem.name)
                { useText.text = "Equip"; }
                else
                { useText.text = "Unequipt"; }
                break;
            #endregion
            #region case ItemType.Apparel:
            case ItemType.Apparel:
                // "Wear"
                break;
            #endregion
            #region case ItemType.Crafting:
            case ItemType.Crafting:
                useButton.interactable = false;
                break;
            #endregion
            #region case ItemType.Ingredients:
            case ItemType.Ingredients:
                useButton.interactable = false;
                break;
            #endregion
            #region case ItemType.Potions:
            case ItemType.Potion:
                useText.text = "Drink";
                if (player.attributes[2].currentValue < player.attributes[2].maxValue)
                { useButton.interactable = true; }
                else
                { useButton.interactable = false; }
                break;
            #endregion
            #region case ItemType.Scrolls:
            case ItemType.Scrolls:
                // "Read"
                break;
            #endregion
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        if (currentShop == null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))//self explanitory
            {
                showInv = !showInv;
                
                if (showInv)
                {
                    Time.timeScale = 0;//time stops
                    Cursor.lockState = CursorLockMode.None;//cursor can be seen and is not locked in place
                    Cursor.visible = true;
                    UpdateInventory();
                    invPanel.SetActive(true);//activate the inventory
                   

                    return;
                }
                else
                {
                    if (!PauseMenu.isPaused)// reverses the functions above
                    {
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
                        invPanel.SetActive(false);
                        Cursor.visible = false;
                        if (currentChest != null)
                        {
                            currentChest.showChestInv = false;
                            currentChest = null;
                        }
                        UpdateInventory();



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
            UpdateInventory();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            sortType = "All";
            UpdateInventory();
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
    }//former IMGUI function
    #region Sorting
    public void sortAll()
    {
        sortType = "All";
        UpdateInventory();
    }
    public void sortFood()
    {
        sortType = "Food";
        UpdateInventory();
    }
    public void sortWeapons()
    {
        sortType = "Weapons";
        UpdateInventory();
    }
    public void sortApparel()
    {
        sortType = "Apparel";
        UpdateInventory();
    }
    public void sortCrafting()
    {
        sortType = "Crafting";
        UpdateInventory();
    }
    public void sortIngredients()
    {
        sortType = "Ingredients";
        UpdateInventory();
    }
    public void sortPotions()
    {
        sortType = "Potions";
        UpdateInventory();
    }
    public void sortScrolls()
    {
        sortType = "Scrolls";
        UpdateInventory();
    }
    public void sortQuest()
    {
        sortType = "Quest";
        UpdateInventory();
    }
    #endregion
    public void UseItem()//use or equip the item
    {
        
        

        
        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (player.attributes[0].currentValue < player.attributes[0].maxValue)
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
                
                break;
            case ItemType.Weapon:
                if (equipmentSlots[2].currentItem == null || selectedItem.Name != equipmentSlots[2].currentItem.name)
                {
                    
                        if (equipmentSlots[2].currentItem != null)
                        {
                            Destroy(equipmentSlots[2].currentItem);
                        }
                        GameObject curItem = Instantiate(selectedItem.Mesh, equipmentSlots[2].equipLocation);
                        equipmentSlots[2].currentItem = curItem;
                        curItem.name = selectedItem.Name;
                    useText.text = "Equipt";


                }
                else
                {
                    useText.text = "Equipt";
                    Destroy(equipmentSlots[2].currentItem);
                    
                }
                break;
            case ItemType.Potion:
                
                break;
            case ItemType.Apparel:
               
                break;
            case ItemType.Crafting:
                
                break;
            case ItemType.Ingredients:
                
                break;
            case ItemType.Scrolls:
                
                break;
            case ItemType.Quest:
                break;
            case ItemType.Money:
                break;
            default:
                break;
        }
        #region IMGUI stuff
        //GUI.skin = null;//end of skin range
        //GUI.Box(new Rect(5f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
        //GUI.Box(new Rect(5f * scr.x, 3.5f * scr.y, 3 * scr.x, 0.5f * scr.y), selectedItem.Name, style);//style adds to one
        //GUI.skin = skin; //skin adds to all within range
        //GUI.Box(new Rect(5f * scr.x, 4f * scr.y, 3 * scr.x, 1f * scr.y), selectedItem.Description + "\nValue " + selectedItem.Value + "\nAmount " + selectedItem.Amount);
        //GUI.Box(new Rect(8f * scr.x, 2f * scr.y, 5 * scr.x, 0.5f * scr.y), "Value " + selectedItem.Value);
        //GUI.Box(new Rect(8f * scr.x, 2.5f * scr.y, 5 * scr.x, 0.5f * scr.y), "Amount " + selectedItem.Amount);



        /*if (currentChest != null)
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
        }*/
        #endregion

    }
    public void discardItem()//removes item from inventory
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
            UpdateInventory();
        }
        else
        {
            inv.Remove(selectedItem);
            selectedItem = null;
            UpdateInventory();
            return;
        }
    }
    private void OnGUI()
    {
        /*if (showInv)
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
        }*/
        
    }
}
