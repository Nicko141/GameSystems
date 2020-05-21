using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Game Systems/RPG/Player/Movement")]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    public float gravity = 20f;
    public CharacterController controller;
    [Header("Movement Variables")]
    public float speed = 5f;
    public float jumpspeed = 8f;
    public Vector3 moveDirection;
   
    // Start is called before the first frame update
    void Start()
    {
        //grabs character controller attatched to this object
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if(Input.GetKey(KeyBindManager.keys["Jump"]))
            {
                moveDirection.y = jumpspeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime); 
    }
}
