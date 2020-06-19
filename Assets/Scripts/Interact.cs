using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Intro RPG/Player/Interact
[AddComponentMenu("Intro PRG/RPG/Player/Interact")]
public class Interact : MonoBehaviour
{
    #region Variables
    //[Header("Player and Camera connection")]
    //create two gameobject variables one called player and the other mainCam
    public PlayerHandler player;
    public CanvasDialogueController dlgCont;

    #endregion
    #region Start
    private void Start()
    {
        player = GetComponent<PlayerHandler>();
    }
    private void Update()
    {
        #region Update   
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                #region NPC tag
                //and that hits info is tagged NPC
                if (hitInfo.collider.CompareTag("NPC"))
                {
                    
                    //Debug that we hit a NPC   
                    Debug.Log("Talk to the NPC");
                    if (hitInfo.collider.GetComponent<NPCDialogue>())
                    {
                        NPCDialogue character = hitInfo.collider.GetComponent<NPCDialogue>();
                        CanvasDialogueController.currentNPC = character;
                        dlgCont.Dialogue();
                        dlgCont.dialoguePanel.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<MouseLook>().enabled = false;
                        GetComponent<MouseLook>().enabled = false;

                    }

                    
                }
                #endregion
                #region Item
                //and that hits info is tagged Item
                if (hitInfo.collider.CompareTag("item"))
                {

                    //Debug that we hit an Item  
                    Debug.Log("Pick Up Item");
                    ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                    if (handler != null)
                    {
                        player.quest.goal.ItemCollected(handler.itemId);

                        handler.OnCollection();
                       
                    }
                }

                #endregion
                #region Chest
                if (hitInfo.collider.CompareTag("Chest"))
                {
                    Chest chest = hitInfo.transform.GetComponent<Chest>();
                    if (chest != null)
                    {
                        
                        chest.showChestInv = true;
                        LinearInventory.showInv = true;
                        
                        Time.timeScale = 0;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        LinearInventory.currentChest = chest;
                        
                    }
                }
                #endregion
            }
        }
        #endregion
    }
    #endregion

}






