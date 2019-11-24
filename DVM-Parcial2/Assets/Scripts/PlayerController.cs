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
    public LayerMask RayCastLayer;
    public float RayDistance;
    public GameObject ReticlePointer;

    List<GameObject> BulletList;
    float BulletTimer;
    bool IsShooting = false;
    int Health = 100;
    int BulletsLeft = 100;
    float PickupTimer = 0;

    private void Start()
    {
#if UNITY_STANDALONE
        ReticlePointer.SetActive(false);
#endif
        Cursor.lockState = CursorLockMode.Locked;
        OnRotateAction = Rotate;
        PickupBehaviour.OnPickUpAction = UsePickup;
        PataoBehaviour.OnEnemyLockedAction = SetShootingState;
        PataoBehaviour.OnPunchAction = RecievePunch;
    }

    private void Update()
    {
#if UNITY_STANDALONE
        float horizontal = Input.GetAxis("Mouse X") * MouseSensitivity* Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        if(OnRotateAction!=null)
           OnRotateAction(horizontal, vertical);

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else
        {
            BulletTimer = BulletCooldown;
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, RayDistance, RayCastLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            if (hit.transform.gameObject.tag == "HealthKit" || hit.transform.gameObject.tag == "AmmoBox")
            {
                PickupTimer += Time.deltaTime;
                if(PickupTimer>=0.6f)
                {
                    UsePickup(hit.transform.gameObject.tag);
                    hit.transform.gameObject.SetActive(false);
                    PickupTimer = 0;
                }
            }
            else
            {
                PickupTimer = 0;
            }
        }
#endif

#if UNITY_ANDROID
        if (IsShooting)
        {
            Shoot();
        }
        else
        {
            BulletTimer = BulletCooldown;
        }

#endif
        }

    void Shoot()
    {
        if (BulletTimer >= BulletCooldown && BulletsLeft > 0)
        {
            GameObject go = ObjectPooler.Instance.GetPooledObject("Bullet");
            if (go)
            {
                go.GetComponent<Bullet>().ActivateBullet();
                go.GetComponent<Bullet>().DeactivateBullet(1.5f);
                BulletTimer = 0;
                BulletsLeft--;
            }
        }
        BulletTimer += Time.deltaTime;
    }

    void Rotate(float mouseX, float mouseY)
    {
        transform.Rotate(transform.up * mouseX);
    }

    void SetShootingState(bool isShooting)
    {
        IsShooting = isShooting;
    }

    void CheckHealthsStatus()
    {
        Health = Mathf.Clamp(Health, 0, 100);
        if (Health == 0)
        {
            GameManager.Instance.KillPlayer();
            Health = 100;
        }
    }

    void RecievePunch()
    {
        Health -= 20;
        CheckHealthsStatus();
    }

    public int GetHealth()
    {
        return Health;
    }
    
    public int GetBulletsLeft()
    {
        return BulletsLeft;
    }

    void UsePickup(string tag)
    {
        if(tag == "HealthKit")
        {
            Health += 25;
            CheckHealthsStatus();
        }
        else if(tag == "AmmoBox")
        {
            BulletsLeft += 20;
        }
    }
}
