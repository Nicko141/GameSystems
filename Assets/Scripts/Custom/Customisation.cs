
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customisation : Stats
{
    #region Variables
    [Header("Character Name")]
    //name of character
    public string characterName;
    [Header("Character Class")]
    public CharacterClass charClass = CharacterClass.Warrior;
    public string[] selectedClass = new string[5];
    public int selectedIndex = 0;
    public string classButton = "";

   
    public int statPoints = 10;
    [System.Serializable]
    public struct PointUI
    {
        public Stats.StatBlock stat;
        public Text nameDisplay;
        public GameObject plusButton;
        public GameObject minusButton;

    };
    public PointUI[] pointSystem;

    public Text pointsText;
    void TextUpdate()
    {
        pointsText.text = "Points: " + statPoints;
    }


    [Header("Texture List")]
    //texture 2D List for skin,hair,mouth,eys,clothes and armour
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    //index numbers for current textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    [Header("Renderer")]
    //renderer for character mesh
    public Renderer characterRenderer;
    [Header("Max Index")]
    //max amount of textures that the list is filling with
    public int skinMax;
    public int hairMax, mouthMax, eyesMax, clothesMax, armourMax;
    
    #endregion
    #region Start
    //runs on the first frame that both an object and a script are active
    private void Start()
    {
        selectedClass = new string[]
        {
            "Warrior","Defender","Archer","Sorcerer","Healer"
        };
        string[] tempName = new string[] 
        {
            "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma"
        };
        for (int i = 0; i < tempName.Length; i++)
        {
            characterStats[i].name = tempName[i];
        }
        TextUpdate();
        chooseClass(0);

        #region Stat setup
        for (int i = 0; i < pointSystem.Length; i++)
        {
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);

            pointSystem[i].minusButton.SetActive(false);
        }
        #endregion

        #region for loop to pull textures from file
        #region Skin
        for (int i = 0; i < skinMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            skin.Add(temp);
        }
        #endregion
        #region Hair
        for (int i = 0; i < hairMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            hair.Add(temp);
        }
        #endregion
        #region Mouth
        for (int i = 0; i < mouthMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            mouth.Add(temp);
        }
        #endregion
        #region Eyes
        for (int i = 0; i < eyesMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            eyes.Add(temp);
        }
        #endregion
        #region Clothes
        for (int i = 0; i < clothesMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            clothes.Add(temp);
        }
        #endregion
        #region Armour
        for (int i = 0; i < armourMax; i++)
        {
            //temp Texture2D that grabs our textures using resources.load from the character file
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the skin list
            armour.Add(temp);
        }
        #endregion
        #endregion
        //connect and find SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        characterRenderer = GameObject.FindGameObjectWithTag("CharacterMesh").GetComponent<SkinnedMeshRenderer>();
        #region Set Textures on Start
        //SetTexture skin, hair, mouth, eyes, clothes, armour to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
     
        #endregion
    }
    #endregion
    public void SetName(string charName)
    {
        characterName = charName;
    }

    #region SetTexture
    //create a function that is called SetTexture it should contain a string for type and int for direction
    public void SetTexture(string type, int direction)
    {
        //we need varriables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        #region Switch Material and Values
        //switch statement that is 
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as out skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number is 1
                matIndex = 1;
                //end case
            break;
            //case eyes
            case "Eyes":
                //index is the same as our eyes index
                index = eyesIndex;
                //max is the same as out eyes max
                max = eyesMax;
                //textures is our eyes list .ToArray()
                textures = eyes.ToArray();
                //material index element number is 1
                matIndex = 2;
                //end case
                break;
            //case Mouth
            case "Mouth":
                //index is the same as our Mouth index
                index = mouthIndex;
                //max is the same as out Mouth max
                max = mouthMax;
                //textures is our Mouth list .ToArray()
                textures = mouth.ToArray();
                //material index element number is 1
                matIndex = 3;
                //end case
                break;
            //case hair
            case "Hair":
                //index is the same as our hair index
                index = hairIndex;
                //max is the same as out hair max
                max = hairMax;
                //textures is our hair list .ToArray()
                textures = hair.ToArray();
                //material index element number is 1
                matIndex = 4;
                //end case
                break;
            //case clothes
            case "Clothes":
                //index is the same as our clothes index
                index = clothesIndex;
                //max is the same as out clothes max
                max = clothesMax;
                //textures is our clothes list .ToArray()
                textures = clothes.ToArray();
                //material index element number is 1
                matIndex = 5;
                //end case
                break;
            //case armour
            case "Armour":
                //index is the same as our armour index
                index = armourIndex;
                //max is the same as out armour max
                max = armourMax;
                //textures is our armour list .ToArray()
                textures = armour.ToArray();
                //material index element number is 1
                matIndex = 6;
                //end case
                break;
        }
        #endregion
        #region Assign Direction
        //index plus equals our direction
        index += direction;
        //cap our index to loop back around if it is below 0 or above the max
        if (index > max - 1)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = max - 1;
        }
        //material array is equal to our characters material list
        Material[] mat = characterRenderer.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material arrays
        characterRenderer.materials = mat;
        #endregion
        #region Set Material Switch
        switch (type)
        {

            case "Skin":
                skinIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
        }
        #endregion
    }
    #region Use for Canvas
    #region SetTexturePos
    public void SetTexturePos(string type)
    {
        SetTexture(type, 1);
    }
    #endregion
    #region SetTextureNeg
   public void SetTextureNeg(string type)
    {
        SetTexture(type, -1);
     
    }
    public void SetTextureReset(string type)
    {
        
    }
    #endregion
    #endregion
    #endregion


    public void chooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                pointSystem[0].stat.value = 18;
                pointSystem[1].stat.value = 12;
                pointSystem[2].stat.value = 13;
                pointSystem[3].stat.value = 6;
                pointSystem[4].stat.value = 6;
                pointSystem[5].stat.value = 5;
                charClass = CharacterClass.Warrior;
                break;
            case 1:
                pointSystem[0].stat.value = 15;
                pointSystem[1].stat.value = 5;
                pointSystem[2].stat.value = 15;
                pointSystem[3].stat.value = 9;
                pointSystem[4].stat.value = 8;
                pointSystem[5].stat.value = 8;
                charClass = CharacterClass.Defender;
                break;
            case 2:
                pointSystem[0].stat.value = 7;
                pointSystem[1].stat.value = 18;
                pointSystem[2].stat.value = 8;
                pointSystem[3].stat.value = 9;
                pointSystem[4].stat.value = 10;
                pointSystem[5].stat.value = 8;
                charClass = CharacterClass.Archer;
                break;
            case 3:
                pointSystem[0].stat.value = 7;
                pointSystem[1].stat.value = 10;
                pointSystem[2].stat.value = 9;
                pointSystem[3].stat.value = 12;
                pointSystem[4].stat.value = 14;
                pointSystem[5].stat.value = 8;
                charClass = CharacterClass.Sorcerer;
                break;
            case 4:
                pointSystem[0].stat.value = 5;
                pointSystem[1].stat.value = 7;
                pointSystem[2].stat.value = 15;
                pointSystem[3].stat.value = 12;
                pointSystem[4].stat.value = 12;
                pointSystem[5].stat.value = 9;
                charClass = CharacterClass.Healer;
                break;
        }
        for (int i = 0; i < pointSystem.Length; i++)
        {
            pointSystem[i].stat.tempValue = 0;
            statPoints = 10;
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);

            pointSystem[i].minusButton.SetActive(false);
            pointSystem[i].plusButton.SetActive(true);
            TextUpdate();
        }
    }

    public void SetPointsPos(int i)
    {
        statPoints--;
        pointSystem[i].stat.tempValue++;

        if (statPoints <= 0)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(false);
            }
        }
        if (pointSystem[i].minusButton.activeSelf == false)
        {
            pointSystem[i].minusButton.SetActive(true);
        }
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
        TextUpdate();
    }
    public void SetPointNeg(int i)
    {
        statPoints++;
        pointSystem[i].stat.tempValue--;

        if (pointSystem[i].stat.tempValue <= 0)
        {
            pointSystem[i].minusButton.SetActive(false);
        }
        if (pointSystem[i].plusButton.activeSelf == false)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(true);
            }
        }
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
        TextUpdate();

    }
    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);

        PlayerPrefs.SetString("CharacterName", characterName);

        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].name, (characterStats[i].value + characterStats[i].tempValue));
        }
        PlayerPrefs.SetString("CharacterClass", selectedClass[selectedIndex]);

        SceneManager.LoadScene(2);
    }
   /* private void OnGUI()
    {
        //create floats scrW and scrH that govern our 16:9 ratio
        Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);
        //create an int that will help with shuffling your GUI elements under eachother
        int i=0;
        #region Skin
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f*scr.x,0.5f*scr.y +(i * 0.5f * scr.y), 0.5f*scr.x,0.5f*scr.y),"<"))
        {
            //SetTexture and grab skin material and move the texture index in the direction -1
            SetTexture("Skin", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Skin");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab skin material and move the texture index in the direction +1
            SetTexture("Skin", +1);
        }
        i++;
        #endregion
        #region Hair
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //SetTexture and grab Hair material and move the texture index in the direction -1
            SetTexture("Hair", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Hair");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab Hair material and move the texture index in the direction +1
            SetTexture("Hair", +1);
        }
        i++;
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //SetTexture and grab Eyes material and move the texture index in the direction -1
            SetTexture("Eyes", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Eyes");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab Eyes material and move the texture index in the direction +1
            SetTexture("Eyes", +1);
        }
        i++;
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //SetTexture and grab Mouth material and move the texture index in the direction -1
            SetTexture("Mouth", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Mouth");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab Mouth material and move the texture index in the direction +1
            SetTexture("Mouth", +1);
        }
        i++;
        #endregion
        #region Clothes
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f*scr.x,0.5f*scr.y +(i * 0.5f * scr.y), 0.5f*scr.x,0.5f*scr.y),"<"))
        {
            //SetTexture and grab Clothes material and move the texture index in the direction -1
            SetTexture("Clothes", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Clothes");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab Clothes material and move the texture index in the direction +1
            SetTexture("Clothes", +1);
        }
        i++;
        #endregion
        #region Armour
        //GUI button on the left of the screen with the contents <
        if (GUI.Button(new Rect(0.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
        {
            //SetTexture and grab Armour material and move the texture index in the direction -1
            SetTexture("Armour", -1);
        }
        //              {          Position         }{           Size           }        
        GUI.Box(new Rect(0.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), "Armour");
        //GUI button on the left of the screen with the contents >
        if (GUI.Button(new Rect(2.25f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
        {
            //SetTexture and grab Armour material and move the texture index in the direction +1
            SetTexture("Armour", +1);
        }
        i++;
        #endregion
        #region Random Reset


        characterName = GUI.TextField(new Rect(0.25f * scr.x, 0.5f * scr.y + i * (0.5f * scr.y), 2.5f * scr.x, 0.5f * scr.y), characterName, 32);
        i++;
        if (GUI.Button(new Rect(0.25f * scr.x, 0.5f * scr.y + i * (0.5f * scr.y), 2.5f * scr.x, 0.5f * scr.y), "Save and Play"))
        {
            SaveCharacter();
            SceneManager.LoadScene(2);
        }

        #endregion


        #region Character Class
        i = 0;
        //,0.5f*scr.y +(i * 0.5f * scr.y)
        if (GUI.Button(new Rect(12.75f * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 2* scr.x,0.5f*scr.y),classButton))
        {
            showDropdown = !showDropdown;
        }
        i++;
        if (showDropdown)
        {
            scrollPos = GUI.BeginScrollView(new Rect(13 * scr.x, 0.5f * scr.y + (i * 0.5f * scr.y), 
                2 * scr.x, 2f * scr.y), scrollPos, new Rect(0,0,0,selectedClass.Length* 0.5f*scr.y), false, true);

            for (int c = 0; c < selectedClass.Length; c++)
            {
                if (GUI.Button(new Rect(0,(0.5f*scr.y*c),1.75f*scr.x,0.5f*scr.y), selectedClass[c]))
                {
                    selectedIndex = c;
                    chooseClass(c);
                    classButton = selectedClass[c];
                    showDropdown = false;
                    //add point reset and stat temp reset

                }
            }

            GUI.EndScrollView();
        }
        GUI.Box(new Rect(12.75f * scr.x, 3.25f * scr.y, 2 * scr.x, 0.5f * scr.y), "Points: " + statPoints);
        for (int s = 0; s < characterStats.Length; s++)
        {
            if (statPoints > 0)
            {
                //+
                if (GUI.Button(new Rect(12.25f * scr.x, 3.75f * scr.y + (s * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "+"))
                {
                    statPoints--;
                    characterStats[s].tempValue++;
                }
            }
            //type
            GUI.Box(new Rect(12.75f * scr.x, 3.75f * scr.y + (s * 0.5f * scr.y), 2f * scr.x, 0.5f * scr.y), characterStats[s].name + ": " + (characterStats[s].value + characterStats[s].tempValue));
            if (statPoints < 10 && characterStats[s].tempValue > 0)
            {
                //-
                if (GUI.Button(new Rect(14.75f * scr.x, 3.75f * scr.y + (s * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "-"))
                {
                    statPoints++;
                    characterStats[s].tempValue--;
                }
            }
        }
        if (statPoints < 10)
        {
            if (GUI.Button(new Rect(12.75f * scr.x, 6.75f * scr.y + i * 0.5f * scr.y, 2f * scr.x, 0.5f * scr.y), "Reset"))
            {
                statPoints = 10;
                for (int s = 0; s < characterStats.Length; s++)
                {
                   characterStats[s].tempValue = 0;
                }
                
            }
        }
        
        #endregion
    }
    */
}
public enum CharacterClass
{
    Warrior,
    Defender,
    Archer,
    Sorcerer,
    Healer
}