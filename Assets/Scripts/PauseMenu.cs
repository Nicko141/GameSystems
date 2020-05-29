using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    // Start is called before the first frame update
    void Paused()
    {
        //pause everything
        isPaused = true;
        //stop time
        Time.timeScale = 0;
        //open pause menu
        pauseMenu.SetActive(true);
        //release the Kraken!! (cursor)
        Cursor.lockState = CursorLockMode.None;
        //show cursor
        Cursor.visible = true;
    }

    // Update is called once per frame
    public void UnPaused()
    {
        //unpause everything
        isPaused = false;
        //start time
        if (!LinearInventory.showInv)
        {
            Time.timeScale = 1;
        }
        
        //close pause menu
        pauseMenu.SetActive(false);
        //lock the Kraken!! (cursor)
        Cursor.lockState = CursorLockMode.Locked;
        //hide cursor
        Cursor.visible = false;
    }
    private void Start()
    {
        UnPaused();
    }
    void TogglePause()
    {
        if(!isPaused)
        {
            Paused();
        }
        else
        {
            UnPaused();
        }
    }
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            //if options panel is not on
            if(!optionsMenu.activeSelf)
            {
                //toggle freely
                TogglePause();
            }
            else
            {
                //close options panel
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
        }
    }
}
