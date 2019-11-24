using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
{
    bool Locked = false;
    float PressButtonTimer = 0f;
    float TimeToPressButton = 0.6f;

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
        Locked = false;
    }

    private void Update()
    {
        if (Locked)
        {
            PressButtonTimer += Time.unscaledDeltaTime;
            if (PressButtonTimer >= TimeToPressButton)
            {
                PressButtonTimer = 0;
                ActivateButton();
                transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            PressButtonTimer = 0;
        }
    }

    void ActivateButton()
    {
        switch(gameObject.tag)
        {
            case "ContinueButton":
                GameManager.Instance.WatchAd();
                break;
            case "RestartButton":
                GameManager.Instance.RestartGame();
                break;
            case "ExitButton":
                GameManager.Instance.ExitGame();
                break;
        }
    }
}
