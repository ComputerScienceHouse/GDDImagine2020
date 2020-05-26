using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllyPlayer : Player
{
    private static int AllyScore;
    
    public float fireRate;
    public float nextFire;
    public float fireRange;

    public Material LIVE_MATERIAL;
    public Material DEAD_MATERIAL;

    [SerializeField]
    private GameObject BulletPrefab;

    protected void Start()
    {
        AllyScore = 0;

        fireRate = 2.0f;
        nextFire = 0.0f;
        fireRange = 4.0f;

        base.Start();
    }
    protected void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                // Death method found in Player.cs
                InitDeath();
                break;
            case "Player":
            case "Bullet":
                //do nothing
                break;
            case "KillConfirm":
                AllyScore += KillConfirm(collider);
                Debug.Log("AllyScore: " + AllyScore);
                break;
            case "Dot":
                AllyScore += 1;
                base.OnTriggerEnter(collider);
                break;
            default:
                base.OnTriggerEnter(collider);
                break;
        }
    }

    protected override Material setMaterial(string id)
    {
        if (id.Equals("dead"))
        {
            return DEAD_MATERIAL;
        } else
        {
            return LIVE_MATERIAL;
        }
    }
    protected override void Shoot(string controllerNum)
    {
        float fire = Input.GetAxisRaw($"RightTrigger_P{controllerNum}");

        if (fire >= 0.3f && Time.time > nextFire && localScore > 0 && AllyScore > 0)
        {
            float startTime = Time.time;

            nextFire = Time.time + ( 1 / fireRate ); // 1/FireRate gives period of shots
            Debug.Log("FIRE!");
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().setOwner(this);
            AllyScore--;
            localScore--;
            //Transform bulletTransform = bullet.GetComponent<Transform>();
            //bulletTransform.position = Vector3.Lerp(transform.position, transform.forward * 10, 100);
        }
    }
}
