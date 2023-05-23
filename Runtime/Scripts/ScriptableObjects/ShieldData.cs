using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public class ShieldData : CharacterAbilityData
{
    public GameObject shield;
    public Color colour;
    public bool isPlayerChild;
    public Vector3 offset; 
    public int shieldHealth;
    public int minRecoveryHealth;
    public int regenPerSecond;
    public float regenDelay;
    public float shieldDuration;
    public TypeReference type;
    

    public void Begin(Camera cam, KeyCode key)
    {
        cam.GetComponentInParent<CharacterScript>().gameObject.TryGetComponent(out Shield component);

        if (component == null)
        {
            component = cam.GetComponentInParent<CharacterScript>().gameObject.AddComponent<Shield>();
        }

        component?.Init(cam, shield, colour, key, isPlayerChild, offset, shieldHealth, shieldDuration, minRecoveryHealth, regenPerSecond, regenDelay);
    }
}
