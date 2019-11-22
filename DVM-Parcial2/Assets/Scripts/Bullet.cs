using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 75, ForceMode.VelocityChange);
    }
}
