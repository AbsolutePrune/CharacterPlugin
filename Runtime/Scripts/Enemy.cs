using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isShooter;
    public bool canShoot;
    public float cooldown;
    public float timeUntilCanShoot;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilCanShoot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(canShoot && isShooter)
        {
            GameObject bullet = Instantiate(enemyBullet, gameObject.transform.position + gameObject.transform.forward, Quaternion.identity);
            BulletScript script = bullet.GetComponent<BulletScript>();
            script.isEnemy = true;
            script.bulletDuration = 3;
            script.impactDamage = 300;
            bullet.GetComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100, ForceMode.Force);
            timeUntilCanShoot = cooldown;
            canShoot = false;
        }

        if(canShoot == false)
        {
            timeUntilCanShoot -= Time.deltaTime;
            if(timeUntilCanShoot < 0)
            {
                canShoot = true;
            }
        }
    }
}
