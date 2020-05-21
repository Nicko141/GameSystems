using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]
public class PlayerHandler : Character
{
    

    #region Variables
    [Header("Physics")]
    public float gravity = 20f;
    public CharacterController controller;
    public Vector3 moveDirection;
    [Header("Level Data")]
    public int level = 0;
    public float currentExp, neededExp, maxExp;
    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckPoint;
    //                                   R  B  G  A
    public Color flashColour = new Color(1, 0, 0, 0.2f);
    public float flashSpeed = 5f;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;
    #endregion
    #region Behaviour

   
    private void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        if (KeyBindManager.keys.Count <1)
        {
            KeyBindManager.keys.Add("Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
            KeyBindManager.keys.Add("Backward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S")));
            KeyBindManager.keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
            KeyBindManager.keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));

            KeyBindManager.keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
            KeyBindManager.keys.Add("Crouch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl")));
            KeyBindManager.keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
            KeyBindManager.keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
            KeyBindManager.keys.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "Tab")));
        }
        
    }
    public override void Movement()
    {
        base.Movement();
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey(KeyBindManager.keys["Forward"]))
        {
            vertical++;
        }

        if (Input.GetKey(KeyBindManager.keys["Backward"]))
        {
            vertical--;
        }

        if (Input.GetKey(KeyBindManager.keys["Left"]))
        {
            horizontal--;
        }

        if (Input.GetKey(KeyBindManager.keys["Right"]))
        {
            horizontal++;
        }

        if (controller.isGrounded)
        {
            moveDirection = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
            moveDirection *= speed;
            if (Input.GetKey(KeyBindManager.keys["Jump"]))
            {
                moveDirection.y = jumpspeed;
            }
            if (Input.GetKey(KeyBindManager.keys["Sprint"]))
            {
                moveDirection *= sprint;
            }
            if (Input.GetKey(KeyBindManager.keys["Crouch"]))
            {
                moveDirection *= crouch;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
    public override void Update()
    {
        base.Update();
        #region bar update
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[0].displayImage.fillAmount = Mathf.Clamp01(attributes[0].currentValue / attributes[0].maxValue);

        }
        #endregion
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.X))
        {
            damagePlayer(5);
        }
#endif
        #region Damage Flash
        if(isDamaged && !isDead)
        {
            damageImage.color = flashColour;
            isDamaged = false;
        }
        else if(damageImage.color.a >0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        #endregion
        if(!canHeal)
        {
            healDelayTimer += Time.deltaTime;
            if(healDelayTimer >= 5)
            {
                canHeal = true;
            }
            
        }
        if(canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue >0)
        {
            regenHealth();
        }
    }

    public void damagePlayer(float damage)
    {
        //turn on red flicker
        isDamaged = true;
        //take damage
        attributes[0].currentValue -= damage;
        //delay regen
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead)
        {
            Death();
        }
    }
    public void regenHealth()
    {
        attributes[0].currentValue += Time.deltaTime *(attributes[0].regenValue/*plus our vitality value*/);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7;
            }
            PlayerSaveAndLoad.Save();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7;
            }
        }
    }
    #endregion

    #region Death and Respawn
    
    void Death()
    {
        //set death flag to dead
        isDead = true;
        //clear existing text just in case
        deathText.text = "";
        //set and play audio clip
        playersAudio.clip = deathClip;
        playersAudio.Play();
        //trigger death screen
        deathImage.GetComponent<Animator>().SetTrigger("isDead");
        //in 2 secs set death text when we die
        Invoke("DeathText", 2f);
        //in 6 secs set respawn text when we respawn
        Invoke("RespawnText", 6f);
        //in 9 secs respawn us
        Invoke("Respawn", 9f);
    }
    void DeathText()
    {
        deathText.text = "Death calls to you... are you gonna answer him?";
    }
    void RespawnText()
    {
        deathText.text = "So you refused to answer him, But you can only refuse for so long... He'll be waiting";
    }
    void Respawn()
    {
        //reset everything
        deathText.text = "";
        isDead = false;
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;

        }
        
        //load position
        this.transform.position = currentCheckPoint.position;
        this.transform.rotation = currentCheckPoint.rotation;
        //respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }
    #endregion

}
