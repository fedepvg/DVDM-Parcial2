using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataoMovement : MonoBehaviour
{
    public Transform PlayerTransform;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(PlayerTransform);
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}
