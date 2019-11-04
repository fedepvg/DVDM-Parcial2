using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnRotate(float mouseX, float mouseY);
    public static OnRotate OnRotateAction;
    public float MouseSensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        OnRotateAction += Rotate;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * MouseSensitivity* Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        if(OnRotateAction!=null)
           OnRotateAction(horizontal, vertical);
    }

    void Rotate(float mouseX, float mouseY)
    {
        transform.Rotate(transform.up * mouseX);
    }
}
