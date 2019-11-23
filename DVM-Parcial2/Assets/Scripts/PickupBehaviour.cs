using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupBehaviour : MonoBehaviour
{
    public delegate void OnPickUp(string tag);
    public static OnPickUp OnPickUpAction;

    bool Locked = false;
    float PickUpTimer = 0f;
    float TimeToPickUp = 0.6f;

    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { Locked = true; });
        trigger.triggers.Add(entry);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((data) => { Locked = false; });
        trigger.triggers.Add(exit);
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Locked = false;
    }

    private void Update()
    {
        if (Locked)
        {
            PickUpTimer += Time.deltaTime;
            if (PickUpTimer >= TimeToPickUp)
            {
                PickUpTimer = 0;
                if (OnPickUpAction != null)
                    OnPickUpAction(gameObject.tag);
                gameObject.SetActive(false);
            }
        }
        else
        {
            PickUpTimer = 0;
        }
    }
}
