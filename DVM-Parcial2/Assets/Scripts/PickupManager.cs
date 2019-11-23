using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public float DropChance;

    private void Start()
    {
        PataoBehaviour.OnKilledAction = DropPickup;
    }

    public void DropPickup(Vector3 spawnPos)
    {
        float rand = Random.Range(0f,100f);
        if(rand<DropChance)
        {
            int randomPickup = Random.Range(0, 2);
            GameObject pickup; 
            if (randomPickup == 0)
                pickup = ObjectPooler.Instance.GetPooledObject("HealthKit");
            else
                pickup = ObjectPooler.Instance.GetPooledObject("AmmoBox");
            if(pickup)
                pickup.transform.position = spawnPos;
        }
    }
}
