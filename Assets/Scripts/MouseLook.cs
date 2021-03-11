using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Game Systems/RPG/Player/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    // enums are comma seperated lists of identifiers
    // identifiers - something that can be identified
    public enum RotationalAxis
    {
        MouseX,
        MouseY
    }
    [Header("Rotation Variables")]
    public RotationalAxis axis = RotationalAxis.MouseX;
    [Range(0, 2000)]
    public float sensitivity = 400f;
    public float minY = -60, maxY = 60;
    private float _rotY;
    void Start()
    {
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = false;
        }
        if(GetComponent<Camera>())
        {
            axis = RotationalAxis.MouseY;
        }
        sensitivity = 400f;
    }

    void Update()
    {
        if (!PlayerHandler.isDead)
        {
            if(axis == RotationalAxis.MouseX)
            {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
            }
            else
            {
            _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            _rotY = Mathf.Clamp(_rotY, minY, maxY);
            transform.localEulerAngles = new Vector3(-_rotY,0,0);
            }
        }

    }
}
