using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFire : CharacterAbility
{
    public PrimaryFireData primaryFireData;
    public string abilityPathToCall;



    public void HitscanFire(Camera cam, RaycastHit hitinfo, int Damage)
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitinfo, Mathf.Infinity))
        {
            Debug.DrawLine(cam.transform.position, hitinfo.point, Color.red, Mathf.Infinity, true);

            if (hitinfo.collider.gameObject.layer == 7)
            {
                hitinfo.collider.gameObject.GetComponent<Enemy>().health = hitinfo.collider.gameObject.GetComponent<Enemy>().health - Damage;
            }
        }
    }//cam, projBullet, Damage, useGravity, hasSplashDamage, projectileSpeed, splashRadius, splashDamage, upwardForce, bulletMass
    public void ProjectileFire(Camera cam, GameObject projBullet, int Damage, bool useGravity, bool hasSplashDamage, float projectileSpeed,
        float splashRadius, float splashDamage, float upWardForce, float bulletMass)
    {
        GameObject currentBullet = Instantiate(projBullet, cam.transform.position + cam.transform.forward, Quaternion.identity);
        currentBullet.transform.forward = cam.transform.TransformDirection(Vector3.forward);
        currentBullet.GetComponent<BulletScript>().impactDamage = Damage;

        if (useGravity)
        {
            currentBullet.GetComponent<Rigidbody>().useGravity = true;
            currentBullet.GetComponent<Rigidbody>().mass = bulletMass;
        }
        else
        {
            currentBullet.GetComponent<Rigidbody>().useGravity = false;
        }

        if (hasSplashDamage)
        {
            currentBullet.GetComponent<BulletScript>().splashDamage = splashDamage;
            currentBullet.GetComponent<BulletScript>().splashRadius = splashRadius;
        }

        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward.normalized * projectileSpeed, ForceMode.Force);
    }

    public void BeamFire()
    {

    }
}
