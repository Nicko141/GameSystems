using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Intro RPG/Player/Interact
[AddComponentMenu("Intro PRG/RPG/Player/Interact")]
public class Interact : MonoBehaviour
{
    #region Variables
    //[Header("Player and Camera connection")]
    //create two gameobject variables one called player and the other mainCam

    #endregion
    #region Start
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
                    if (hitInfo.collider.GetComponent<Dialogue>())
                    {
                        hitInfo.collider.GetComponent<Dialogue>().showDlg = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<MouseLook>().enabled = false;
                        GetComponent<MouseLook>().enabled = false;

                    }

                    if (hitInfo.collider.GetComponent<OptionLinearDialogue>())
                    {
                        hitInfo.collider.GetComponent<OptionLinearDialogue>().showDlg = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<MouseLook>().enabled = false;
                        GetComponent<MouseLook>().enabled = false;

                    }

                    if (hitInfo.collider.GetComponent<ApprovalDialogue>())
                    {
                        hitInfo.collider.GetComponent<ApprovalDialogue>().showDlg = true;
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
                        handler.OnCollection();
                    }
                }
                             
                #endregion
            }
        }
        #endregion
    }
    #endregion

}






