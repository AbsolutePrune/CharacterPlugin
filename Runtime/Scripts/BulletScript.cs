using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int impactDamage;

    public float splashDamage;

    public float splashRadius; 

    public float hasSplashDamage;

    public float bulletDuration;

    public float bulletRadius; 

    public bool isEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRadius = transform.lossyScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        bulletDuration -= Time.deltaTime;
        if (bulletDuration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.layer);

        if(isEnemy)
        {
            col.gameObject.TryGetComponent(out CharacterScript player);
            if(player != null)
            {
                player.TakeDamage(impactDamage);
            }
            if(col.gameObject.GetComponentInParent<ShieldScript>())
            {
                col.gameObject.GetComponentInParent<ShieldScript>().TakeDamage(impactDamage);
            }
        }

        // if it hit an enemy
        if (col.gameObject.layer == 7)
        {
            HitEnemy(col);
        }
        else 
        {
            Missed();
        }
        Destroy(gameObject);
    }

    public void HitEnemy(Collision col)
    {
        // check splash damage
        if (splashDamage > 0)
        {
            col.gameObject.GetComponent<Enemy>().health -= impactDamage + Mathf.RoundToInt(splashDamage);


            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, splashRadius);
            foreach (Collider obj in hitColliders)
            {                
                if (obj.gameObject.layer == 7 && obj.gameObject != col.gameObject)
                {
                    Vector3 dir = (obj.transform.position - gameObject.transform.position).normalized;

                    Physics.Raycast(gameObject.transform.position, dir, out RaycastHit hit, splashRadius);

                    float splashFallOff = 1 - (hit.distance / splashRadius);

                    float fallOffRounded = Mathf.Round(splashFallOff);

                    float damageToDeal = splashDamage * fallOffRounded;

                    obj.gameObject.GetComponent<Enemy>().health -= Mathf.RoundToInt(damageToDeal);
                }
            }
        }
        else
        {
            col.gameObject.GetComponent<Enemy>().health -= impactDamage;
        }
    }

    public void Missed()
    {
        if (splashDamage > 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, splashRadius);
            foreach (Collider obj in hitColliders)
            {
                if (obj.gameObject.layer == 7)
                {
                    Vector3 dir = (obj.transform.position - gameObject.transform.position).normalized;

                    Physics.Raycast(gameObject.transform.position, dir, out RaycastHit hit, splashRadius);

                    float splashFallOff = 1 - (hit.distance / splashRadius);
                    
                    float fallOffRounded = Mathf.Round(splashFallOff * 10);

                    float damageToDeal = splashDamage * fallOffRounded / 10;

                    obj.gameObject.GetComponent<Enemy>().health -= Mathf.RoundToInt(damageToDeal);

                    Debug.DrawLine(gameObject.transform.position, obj.transform.position, Color.Lerp(Color.red, Color.green, splashFallOff), Mathf.Infinity);
                }
            }
        }
    }
}
