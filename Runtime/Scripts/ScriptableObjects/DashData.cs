using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using System.Security.Cryptography;
using UnityEditor.PackageManager;

public class DashData : CharacterAbilityData
{
    public float dashLength;
    public float dashDuration;
    public bool forwardDash;
    public bool doesDamage;
    public int damage;
    public Vector3 dashDir;
    public Vector3 startPos;
    public TypeReference type;
    public Camera cam;
    


    public void Begin(Camera playerCam, KeyCode key)
    {
        cam = playerCam;

        cam.GetComponentInParent<CharacterScript>().gameObject.TryGetComponent(out Dash component);

        if (component == null)
        {
            component = cam.GetComponentInParent<CharacterScript>().gameObject.AddComponent<Dash>();
        }





        startPos = cam.GetComponentInParent<CharacterScript>().transform.position;
        if(!forwardDash && cam.gameObject.GetComponentInParent<CharacterScript>().moveDir != Vector3.zero)
        {
            dashDir = cam.gameObject.GetComponentInParent<CharacterScript>().moveDir;
        }
        else
        {
            dashDir = cam.transform.forward;
        }


        if (component != null)
        {
            component.BeginDash(cam, startPos, dashDir, dashLength, dashDuration, damage, doesDamage);
        }
        //object[] parameters = new object[] { cam, startPos, dashDir, dashLength, dashDuration};
        //System.Type[] parameterTypes = new System.Type[] { typeof(Camera), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float)};

        //System.Reflection.MethodInfo methodInfo = typeof(Dash).GetMethod("BeginDash", parameterTypes);
        //Dash begin = (Dash)System.Activator.CreateInstance(type.Type);
        //methodInfo.Invoke(begin, parameters);


    } 
}
