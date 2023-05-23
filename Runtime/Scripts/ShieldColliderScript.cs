using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldColliderScript : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent(out BulletScript script);

        if(script != null)
        {
            
        }
        else
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
    }

}
