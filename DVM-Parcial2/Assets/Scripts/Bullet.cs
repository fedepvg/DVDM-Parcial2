using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Parent;

    public void ActivateBullet()
    {
        Parent = transform.parent.gameObject;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.VelocityChange);
        transform.parent = null;
        GetComponent<TrailRenderer>().Clear();
    }

    public void DeactivateBullet(float timeToDeactivate)
    {
        StartCoroutine(DisableBullet(timeToDeactivate));
    }

    IEnumerator DisableBullet(float timeToDeactivate)
    {
        yield return new WaitForSeconds(timeToDeactivate);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.parent = Parent.transform;
        gameObject.SetActive(false);
    }
}
