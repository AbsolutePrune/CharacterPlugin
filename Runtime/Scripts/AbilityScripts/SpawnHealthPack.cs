using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthPack : CharacterAbility
{
    public void Spawn(GameObject healthpack, Camera cam, int healValue,
        bool useGravity, float packMass,
        bool hasDuration, float packDuration,
        bool giveOverHealth, float overHealthDuration, bool onlyOverhealth)
    {
        GameObject pack = Instantiate(healthpack, cam.transform.position + cam.transform.forward, Quaternion.identity);
        pack.GetComponent<HealthpackScript>().healValue = healValue;
        if (useGravity)
        {
            pack.GetComponent<Rigidbody>().useGravity = true;
            pack.GetComponent<Rigidbody>().mass = packMass;
        }
        else
        {
            pack.GetComponent<Rigidbody>().useGravity = false;
        }
        if (hasDuration)
        {
            pack.GetComponent<HealthpackScript>().packDuration = packDuration;
        }
        pack.GetComponent<HealthpackScript>().giveOverHealth = giveOverHealth;
        if (giveOverHealth)
        {
            pack.GetComponent<HealthpackScript>().overHealthDuration = overHealthDuration;
            pack.GetComponent<HealthpackScript>().onlyOverHealth = onlyOverhealth;
        }
    }
}
