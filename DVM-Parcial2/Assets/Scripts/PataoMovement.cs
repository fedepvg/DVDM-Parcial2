using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PataoMovement : MonoBehaviour
{
    public delegate void OnEnemyLocked(bool isShooting);
    public static OnEnemyLocked OnEnemyLockedAction;
    public delegate void OnPunch();
    public static OnPunch OnPunchAction;
    public Transform PlayerTransform;
    public int Health = 100;
    public int triggers;

    const float Speed = 8;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry exit = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        exit.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { StartShootingDelegate((PointerEventData)data); });
        exit.callback.AddListener((data) => { StopShootingDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
        trigger.triggers.Add(exit);
    }

    public void StartShootingDelegate(PointerEventData data)
    {
        if(OnEnemyLockedAction!=null)
            OnEnemyLockedAction(true);
    }

    public void StopShootingDelegate(PointerEventData data)
    {
        if (OnEnemyLockedAction != null)
            OnEnemyLockedAction(false);
    }

    void OnEnable()
    {
        transform.LookAt(PlayerTransform);
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        Health = 100;
    }

    private void OnDisable()
    {
        if (OnEnemyLockedAction != null)
            OnEnemyLockedAction(false);
    }

    void CheckHealthsStatus()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Bullet")
        {
            Health -= 25;
            CheckHealthsStatus();
        }
        else if(other.tag=="Player")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Animator>().SetTrigger("Box");
        }
    }

    void Punch()
    {
        if (OnPunchAction != null)
            OnPunchAction();
    }
}
