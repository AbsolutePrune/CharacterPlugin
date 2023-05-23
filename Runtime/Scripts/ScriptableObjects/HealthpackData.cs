using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public class HealthpackData : CharacterAbilityData
{
    public int healValue;

    public bool useGravity;
    public float packMass;

    public bool hasDuration;
    public float packDuration;

    public bool giveOverHealth;
    public float overHealthDuration;
    public bool onlyOverHealth;

    public GameObject healthpack;
    public TypeReference type;

    public void Begin(Camera cam, KeyCode key)
    {
        object[] parameters = new object[] { healthpack, cam, healValue,
            useGravity, packMass,
            hasDuration, packDuration,
            giveOverHealth, overHealthDuration, onlyOverHealth};
        System.Type[] parameterTypes = new System.Type[] { typeof(GameObject), typeof(Camera), typeof(int),
            typeof(bool), typeof(float),
            typeof(bool), typeof(float),
            typeof(bool), typeof(float), typeof(bool)};
        System.Reflection.MethodInfo methodInfo = typeof(SpawnHealthPack).GetMethod("Spawn", parameterTypes);
        SpawnHealthPack begin = (SpawnHealthPack)System.Activator.CreateInstance(type.Type);
        methodInfo.Invoke(begin, parameters);
    }

}
