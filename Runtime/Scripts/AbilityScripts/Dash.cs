using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : CharacterAbility
{
    //public DashData dashData;

    GameObject player;
    Vector3 playerPosition;
    Vector3 startPosition;
    Vector3 dashDirection;
    float length;
    float duration;
    bool isDashing;
    int damage;
    bool doesDamage;

    public void BeginDash(Camera cam, Vector3 startPos, Vector3 dashDir, float dashLength, float dashDuration, int dmg, bool doesDmg)
    {
        player = cam.GetComponentInParent<CharacterScript>().gameObject;
        
        startPosition = startPos;
        dashDirection = dashDir;
        length = dashLength;
        duration = dashDuration;
        damage = dmg;
        doesDamage = doesDmg;
        isDashing = true;
        //cam.GetComponentInParent<CharacterScript>().transform.position += length * dashDirection;
    }

    public void Update()
    {
        playerPosition = GetComponent<Transform>().position;
        if (isDashing)
        {
            player.GetComponent<Rigidbody>().AddForce(dashDirection * (length/duration), ForceMode.Impulse);
            if(Vector3.Distance(playerPosition, startPosition) > length)
            {
                isDashing=false;
            }

            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing)
        {

            isDashing = false;

            if (doesDamage)
            {
                collision.gameObject.TryGetComponent(out Enemy enemy);
                if (enemy != null)
                {
                    collision.gameObject.GetComponent<Enemy>().health -= damage;
                }
            }
        }
    }
}
//player.GetComponentInParent<CharacterScript>().transform.position += ((length * dashDirection) * (duration * Time.deltaTime));