using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnRotate(float mouseX, float mouseY);
    public static OnRotate OnRotateAction;
    public float MouseSensitivity;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float BulletCooldown;

    List<GameObject> BulletList;
    float BulletTimer;

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

        if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if(BulletTimer >= BulletCooldown)
            {
                GameObject go = Instantiate(BulletPrefab, BulletSpawn);
                go.transform.localPosition = Vector3.zero;
                Destroy(go, 1.5f);
                BulletTimer = 0;
            }
            BulletTimer += Time.deltaTime;
        }
        else
        {
            BulletTimer = BulletCooldown;
        }
    }

    void Rotate(float mouseX, float mouseY)
    {
        transform.Rotate(transform.up * mouseX);
    }
}
