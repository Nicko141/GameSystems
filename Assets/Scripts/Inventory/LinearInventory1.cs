using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LinearInventory : MonoBehaviour
{
    #region Variables
    public static List<Item> inv = new List<Item>();
    public static bool showInv;
    public GameObject invShow;
    public Item item;
    public PlayerHandler player;
    public Vector2 scr;
    public static Item selectedItem;
    public static int money;
    public Vector2 scrollPos;
    public string sortType = "All";
    public Transform dropLocation;
    [System.Serializable]
    public struct EquippedItems
    {
        public string slotName;
        public Transform location;
        public GameObject equippedItem;
    };
    //shows what you have and where it is located
    public EquippedItems[] equippedItems;
    // allows you to placed the prefab to spawn in these array
    public GameObject[] weapons;
    public GameObject[] armours;
    public GameObject[] potions;
    public GameObject[] foods;
    public GameObject[] ingredients;
    public GameObject[] craftables;
    public GameObject[] quests;
    public GameObject[] miscs;
    //public Text[] ItemOptions;

    public DisplayItem display;
    public bool invFilterOptions;
    public GUISkin invSkin;
    public GUIStyle titleStyle;

    #endregion
    [System.Serializable]
    //displays the stats of the selected item
    public struct DisplayItem
    {
        public Text itemName;
        public RawImage itemIcon;
        public Text itemDescription;
        public Text itemDamage;
        public Text UseItem;
        public Text RemoveItem;

    };
    void Start()
    {
        //adds on a bunch of different items in your inventory
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(100));
        inv.Add(ItemData.CreateItem(101));
        inv.Add(ItemData.CreateItem(102));
        inv.Add(ItemData.CreateItem(200));
        inv.Add(ItemData.CreateItem(201));
        inv.Add(ItemData.CreateItem(202));
        inv.Add(ItemData.CreateItem(300));
        inv.Add(ItemData.CreateItem(301));
        inv.Add(ItemData.CreateItem(302));
        inv.Add(ItemData.CreateItem(400));
        inv.Add(ItemData.CreateItem(401));
        inv.Add(ItemData.CreateItem(402));
        inv.Add(ItemData.CreateItem(500));
        inv.Add(ItemData.CreateItem(501));
        inv.Add(ItemData.CreateItem(502));
        inv.Add(ItemData.CreateItem(600));
        inv.Add(ItemData.CreateItem(601));
        inv.Add(ItemData.CreateItem(602));
        inv.Add(ItemData.CreateItem(700));
        inv.Add(ItemData.CreateItem(701));
        inv.Add(ItemData.CreateItem(702));
        money = 1000;
        showInv = false;
        invShow.SetActive(false);
    }
    private void Update()
    {
        // makes the inventory open and close based on whther you press tab.
        if (Input.GetButtonDown("Inventory") && !PauseMenu.isPaused)
        {

            showInv = !showInv;
            if (showInv)
            {
                invShow.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                invShow.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                selectedItem = null;

            }
        }
        if (Input.GetKey(KeyCode.I))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            inv[10].Amount++;
        }
    }
    private void HideItems()
    {
        //makes all the items disappear
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        armours[0].SetActive(false);
        armours[1].SetActive(false);
        armours[2].SetActive(false);
        potions[0].SetActive(false);
        potions[1].SetActive(false);
        potions[2].SetActive(false);
        foods[0].SetActive(false);
        foods[1].SetActive(false);
        foods[2].SetActive(false);
        ingredients[0].SetActive(false);
        ingredients[1].SetActive(false);
        ingredients[2].SetActive(false);
        craftables[0].SetActive(false);
        craftables[1].SetActive(false);
        craftables[2].SetActive(false);
        quests[0].SetActive(false);
        quests[1].SetActive(false);
        quests[2].SetActive(false);
        miscs[0].SetActive(false);
        miscs[1].SetActive(false);
        miscs[2].SetActive(false);
    }
    public void ShowWeapon()
    {
        //disappears the items then makes the weopon appear
        HideItems();
        weapons[0].SetActive(true);
        weapons[1].SetActive(true);
        weapons[2].SetActive(true);

    }
    public void ShowArmour()
    {
        //disappears the items then makes the armour appear
        HideItems();
        armours[0].SetActive(true);
        armours[1].SetActive(true);
        armours[2].SetActive(true);
    }
    public void ShowPotion()
    {
        //disappears the items then makes the potion appear
        HideItems();
        potions[0].SetActive(true);
        potions[1].SetActive(true);
        potions[2].SetActive(true);
    }
    public void ShowFood()
    {
        //disappears the items then makes the food appear
        HideItems();
        foods[0].SetActive(true);
        foods[1].SetActive(true);
        foods[2].SetActive(true);
    }
    public void ShowIngredient()
    {
        //disappears the items then makes the ingredient appear
        HideItems();
        ingredients[0].SetActive(true);
        ingredients[1].SetActive(true);
        ingredients[2].SetActive(true);
    }
    public void ShowCraftable()
    {
        //disappears the items then makes the craftable appear
        HideItems();
        craftables[0].SetActive(true);
        craftables[1].SetActive(true);
        craftables[2].SetActive(true);
    }
    public void ShowQuest()
    {
        //disappears the items then makes the quest appear
        HideItems();
        quests[0].SetActive(true);
        quests[1].SetActive(true);
        quests[2].SetActive(true);
    }
    public void ShowMisc()
    {
        //disappears the items then makes the misc appear
        HideItems();
        miscs[0].SetActive(true);
        miscs[1].SetActive(true);
        miscs[2].SetActive(true);
    }
    public void ShowAll()
    {
        //makes all the items to select appear
        weapons[0].SetActive(true);
        weapons[1].SetActive(true);
        weapons[2].SetActive(true);
        armours[0].SetActive(true);
        armours[1].SetActive(true);
        armours[2].SetActive(true);
        potions[0].SetActive(true);
        potions[1].SetActive(true);
        potions[2].SetActive(true);
        foods[0].SetActive(true);
        foods[1].SetActive(true);
        foods[2].SetActive(true);
        ingredients[0].SetActive(true);
        ingredients[1].SetActive(true);
        ingredients[2].SetActive(true);
        craftables[0].SetActive(true);
        craftables[1].SetActive(true);
        craftables[2].SetActive(true);
        quests[0].SetActive(true);
        quests[1].SetActive(true);
        quests[2].SetActive(true);
        miscs[0].SetActive(true);
        miscs[1].SetActive(true);
        miscs[2].SetActive(true);
    }
   

    public void SelectItem(int j)
    {
        // inv.Add(ItemData.CreateItem(j));
        selectedItem = inv[j];

        print("The item name is " + selectedItem.Name);
        ShowItemOptions();
       //shows the stats, options and info of an selected item.


    }
    public void ShowItemOptions()
    {
        //ITEM DATA IS ALWAYS EMPTY!
        //shows the details based on the item data
        display.UseItem.text = selectedItem.UseItem;
        display.RemoveItem.text = selectedItem.RemoveItem;
        display.itemName.text = selectedItem.Name;
        //display.itemIcon.sprite = selectedItem.IconName;
        display.itemDescription.text = selectedItem.Description + "\nAmount: " + selectedItem.Amount + "\nPrice: $" + selectedItem.Value;
        display.itemDamage.text = "Damage: " + selectedItem.Damage;
        display.itemIcon.texture = selectedItem.IconName.texture;
        display.UseItem.text = "Use";

        Debug.Log("ShowItem");
    }
    public void DiscardItem()
    {
        //check if the item is equipped
                    for (int i = 0; i < equippedItems.Length; i++)
                    {
                       if (equippedItems[i].equippedItem != null && selectedItem.Name == equippedItems[i].equippedItem.name)
                        {
                            //if so destroy from scene
                            Destroy(equippedItems[i].equippedItem);
                        }
                    }

                    //spawn item at droplocation
                    GameObject itemToDrop = Instantiate(selectedItem.MeshName, dropLocation.position, Quaternion.identity);
                    //apply gravity and make sure its named correctly 
                    itemToDrop.name = selectedItem.Name;
                    itemToDrop.AddComponent<Rigidbody>().useGravity = true;

                    //is the amount > 1 if so reduce from list
                    if (selectedItem.Amount > 1)
                    {
                        selectedItem.Amount--;
                    }
                    else//else remove from list 
                    {
                        inv.Remove(selectedItem);
                        selectedItem = null;
                        return;
                    }
    }
    public void RemoveItem()
    {
        switch (selectedItem.ItemType)
            {
                case ItemTypes.Armour:
                    //this allows take off your armour from your inventory
                    if (!(equippedItems[1].equippedItem == null || selectedItem.Name != equippedItems[1].equippedItem.name))
                    {
                   
                        Destroy(equippedItems[1].equippedItem);
                        equippedItems[1].equippedItem = null;
                    
                }
                    
                    break;
                case ItemTypes.Weapon:
                    //when a weapon is selected this allows you to de equip it
                    
                    if (!(equippedItems[0].equippedItem == null || selectedItem.Name != equippedItems[0].equippedItem.name))
                    {
                   
                        Destroy(equippedItems[0].equippedItem);
                        equippedItems[0].equippedItem = null;
                    
                    }
                    

                        break;
                    //otherwise no function as the rest don't need it 
                    default:
                        break;
                }
    }
    public void UseItem()
    {
        switch (selectedItem.ItemType)
            {
                case ItemTypes.Armour:
                    //this allows to wear your armour from your inventory
                    if (equippedItems[1].equippedItem == null || selectedItem.Name != equippedItems[1].equippedItem.name)
                    {
                        
                            if (equippedItems[1].equippedItem != null)
                            {
                                Destroy(equippedItems[1].equippedItem);
                            }
                            equippedItems[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItems[1].location);
                            equippedItems[1].equippedItem.name = selectedItem.Name;
                        
                    }
                   
                    break;
                case ItemTypes.Weapon:
                    //when a weapon is selected this allows you to equip
                    
                    if (equippedItems[0].equippedItem == null || selectedItem.Name != equippedItems[0].equippedItem.name)
                    {
                        
                            if (equippedItems[0].equippedItem != null)
                            {
                                Destroy(equippedItems[0].equippedItem);
                            }
                            equippedItems[0].equippedItem = Instantiate(selectedItem.MeshName, equippedItems[0].location);
                            equippedItems[0].equippedItem.name = selectedItem.Name;
                        
                    }
                   

                        break;
                    case ItemTypes.Potion:
                        
                            // heals the player when they drink a potion
                            player.curHealth += selectedItem.Heal;
                            selectedItem.Amount -= 1;

                        break;
                    case ItemTypes.Food:
                        
                            //heals the player when they eat something
                            player.curHealth += selectedItem.Heal;
                           selectedItem.Amount -= 1;
                        
                        break;
                   case ItemTypes.Ingredient:
                
                            selectedItem.Amount -= 1;
                        
                        break;
                    case ItemTypes.Craftable:
                       
                            selectedItem.Amount -= 1;
                        
                        break;
                    default:
                        break;
                }
        }


       
    }
