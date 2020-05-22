using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemData
{
   public static Item CreateItem(int itemID)
    {
        string _name = "";
        string _description = "";
        int _value = 0;
        int _amount = 0;
        string _icon = "";
        string _mesh = "";
        ItemType _type = ItemType.Apparel;
        switch (itemID)
        {
            #region Food 0 - 99
            case 0:
                _name = "Apple";
                _description = "Munchies and Crunchies";
                _value = 1;
                _amount = 1;
                _icon = "Food/apple";
                _mesh = "Food/apple";
                _type = ItemType.Food;
                break;
            case 1:
                _name = "Meat";
                _description = "For all your plant negative needs";
                _value = 10;
                _amount = 1;
                _icon = "Food/Meat";
                _mesh = "Food/Meat";
                _type = ItemType.Food;
                break;
            #endregion
            #region Weapon 100 - 199
            case 100:
                _name = "Axe";
                _description = "Axe me a question, I DARE YOU!";
                _value = 150;
                _amount = 1;
                _icon = "Weapon/axe";
                _mesh = "Weapon/axe";
                _type = ItemType.Weapon;
                break;
            case 101:
                _name = "Bow";
                _description = "Unless you have arrows, you're screwed";
                _value = 75;
                _amount = 1;
                _icon = "Weapon/bow";
                _mesh = "Weapon/bow";
                _type = ItemType.Weapon;
                break;
            case 102:
                _name = "Sword";
                _description = "Point towards the person you want to die";
                _value = 200;
                _amount = 1;
                _icon = "Weapon/sword";
                _mesh = "Weapon/sword";
                _type = ItemType.Weapon;
                break;
            #endregion
            #region Apparel 200 - 299
            case 200:
                _name = "Armour";
                _description = "You'll need this";
                _value = 75;
                _amount = 1;
                _icon = "Apparel/Armour/armour";
                _mesh = "Apparel/Armour/armour";
                _type = ItemType.Apparel;
                break;
            case 201:
                _name = "Boots";
                _description = "Well you weren't gonna go bare foot, now were you?";
                _value = 20;
                _amount = 1;
                _icon = "Apparel/Armour/boots";
                _mesh = "Apparel/Armour/boots";
                _type = ItemType.Apparel;
                break;
            case 202:
                _name = "Bracers";
                _description = "Warning; Not for teeth";
                _value = 25;
                _amount = 1;
                _icon = "Apparel/Armour/bracers";
                _mesh = "Apparel/Armour/bracers";
                _type = ItemType.Apparel;
                break;
            case 203:
                _name = "Gloves";
                _description = "Well, aren't we fancy?";
                _value = 35;
                _amount = 1;
                _icon = "Apparel/Armour/gloves";
                _mesh = "Apparel/Armour/gloves";
                _type = ItemType.Apparel;
                break;
            case 204:
                _name = "Helmet";
                _description = "Get your mind out of the gutter!";
                _value = 95;
                _amount = 1;
                _icon = "Apparel/Armour/helmets";
                _mesh = "Apparel/Armour/helmets";
                _type = ItemType.Apparel;
                break;
            case 205:
                _name = "Shield";
                _description = "Place between you and enemy";
                _value = 100;
                _amount = 1;
                _icon = "Apparel/Armour/shield";
                _mesh = "Apparel/Armour/shield";
                _type = ItemType.Apparel;
                break;
            case 206:
                _name = "Pauldrins";
                _description = "Cause shoulder pads are for kids";
                _value = 50;
                _amount = 1;
                _icon = "Apparel/Armour/shoulders";
                _mesh = "Apparel/Armour/shoulders";
                _type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300 - 399

            #endregion
            #region Ingredients 400 - 499

            #endregion
            #region Potions 500 - 599

            #endregion
            #region Scrolls 600 - 699

            #endregion
            #region Quest 700 - 799

            #endregion
            default:
                itemID = 0;
                _name = "apple";
                _description = "Munchies and Crunchies";
                _value = 1;
                _amount = 1;
                _icon = "Food/apple";
                _mesh = "Food/apple";
                _type = ItemType.Food;
                break;
               
        }
        Item temp = new Item
        {
            ID = itemID,
            Name = _name,
            Description = _description,
            Value = _value,
            Amount = _amount,
            Type = _type,
            Icon = Resources.Load("Icons/"+_icon)as Texture2D,
            Mesh = Resources.Load("Mesh/"+_mesh) as GameObject
        };
        return temp;
    }
        
}
    
