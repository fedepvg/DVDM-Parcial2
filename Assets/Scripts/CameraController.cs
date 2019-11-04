using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float UpMaxXRotation = 270f;
    float DownMaxXRotation = 90f;
    float XAxisRotation;

    private void Start()
    {
        PlayerController.OnRotateAction += Rotate;
    }

    void Rotate(float mouseX, float mouseY)
    {
        XAxisRotation += mouseY;

        if (XAxisRotation < -90f)
        {
            XAxisRotation = -90f;
            mouseY = 0f;
            ClampXRotation(UpMaxXRotation);
        }
        else if (XAxisRotation > 90f)
        {
            XAxisRotation = 90f;
            mouseY = 0f;
            ClampXRotation(DownMaxXRotation);
        }

        transform.Rotate(Vector3.right * mouseY);
    }

    void ClampXRotation(float newXRot)
    {
        Vector3 eulerRot = transform.eulerAngles;
        eulerRot.x = newXRot;
        transform.eulerAngles = eulerRot;
    }
}
