using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public PlayerController Player;
    public Slider HealthSlider;
    public GameObject Crosshair;

    int LastPlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        LastPlayerHealth = Player.GetHealth();
#if UNITY_ANDROID
        Crosshair.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        int currentPlayerHealth = Player.GetHealth();
        if(currentPlayerHealth != LastPlayerHealth)
        {
            LastPlayerHealth = currentPlayerHealth;
            HealthSlider.value = currentPlayerHealth;
        }
    }
}
