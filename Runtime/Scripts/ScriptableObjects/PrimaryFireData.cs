using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using System.Reflection;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PrimaryFireData : CharacterAbilityData
{
    public Camera cam;

    public PrimaryFire primaryFire;

    public TypeReference type;

    public int index;

    public int Damage;

    public float projectileSpeed;

    public float upwardForce;

    public bool useGravity;

    public float bulletMass;

    public bool hasSplashDamage;

    public float splashRadius;

    public float splashDamage;

    public GameObject projBullet;

    public float beamRange;

    RaycastHit hitinfo;

    public delegate void MyDelegate();
    public MyDelegate attack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void WeaponSwap()
    {
        if(index != 2)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    public void Begin(Camera playerCam, KeyCode key)
    {
        cam = playerCam;
        Fire();

    }

    public void Fire()
    {       
        object[] parameters;
        if (index == 0)
        {
            parameters = new object[] {cam, hitinfo, Damage};
            System.Type[] parameterTypes = new System.Type[] { typeof(Camera), typeof(RaycastHit), typeof(int) };
            //System.Reflection.MethodInfo methodInfo = type.Type.GetMethod("HitscanFire", parameterTypes);
            System.Reflection.MethodInfo methodInfo = typeof(PrimaryFire).GetMethod("HitscanFire", parameterTypes);
            PrimaryFire begin = (PrimaryFire)System.Activator.CreateInstance(type.Type);
            methodInfo.Invoke(begin, parameters);
        }
        else if (index == 1)
        {//Camera cam, GameObject projBullet, int Damage, bool useGravity, bool hasSplashDamage, float projectileSpeed, float splashRadius, float splashDamage, float upWardForce, float bulletMass
            parameters = new object[] { cam, projBullet, Damage, useGravity, hasSplashDamage, projectileSpeed, splashRadius, splashDamage, upwardForce, bulletMass };
            System.Type[] parameterTypes = new System.Type[] { typeof(Camera), typeof(GameObject), typeof(int), typeof(bool), typeof(bool), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float) };
            //System.Reflection.MethodInfo methodInfo = type.Type.GetMethod("ProjectileFire", parameterTypes);
            System.Reflection.MethodInfo methodInfo = typeof(PrimaryFire).GetMethod("ProjectileFire", parameterTypes);
            PrimaryFire begin = (PrimaryFire)System.Activator.CreateInstance(type.Type);
            methodInfo.Invoke(begin, parameters);
        }
        else if (index == 2)
        {
            System.Reflection.MethodInfo myMethod = type.Type.GetMethod("BeamFire");
            //parameters = new object[] { cam, hitinfo, Damage };
            PrimaryFire begin = (PrimaryFire)System.Activator.CreateInstance(type.Type);
            myMethod.Invoke(begin, null);
        }
        else
        {
            Debug.Log("invalid fire mode");
            return;
        }

        
    }

    


}
