using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PataoMovement : MonoBehaviour
{
    public Transform PlayerTransform;
    public int Health = 100;
    const float Speed = 8;
    public int triggers;

    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //EventTrigger.Entry exit = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        //exit.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
        //exit.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerEnterDelegate(PointerEventData data)
    {
        Debug.Log("Enter called.");
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        Debug.Log("Exit called.");
    }

    void OnEnable()
    {
        transform.LookAt(PlayerTransform);
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        Health = 100;
    }

    private void Update()
    {
        
    }

    void CheckHealthsStatus()
    {
        if (Health <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Bullet")
        {
            Health -= 25;
            CheckHealthsStatus();
            Debug.Log(Health);
        }
    }
}
