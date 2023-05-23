using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(PrimaryFireData))]
public class PrimaryFireDataEditor : Editor
{
    public string[] weaponType = new string[] { "Hitscan", "Projectile", "Beam" };

    [MenuItem("Window/My Window")]
    static void Init()
    {
        Debug.Log("hello");
    }

    public override void OnInspectorGUI()
    {
        PrimaryFireData myTarget = (PrimaryFireData)target;

        DrawDefaultInspector();

        myTarget.cam = EditorGUILayout.ObjectField("Camera" ,myTarget.cam, typeof(Camera), true) as Camera;

        myTarget.index = EditorGUILayout.Popup("Weapon type", myTarget.index, weaponType);

        if (myTarget.index == 0)
        {
            myTarget.Damage = EditorGUILayout.IntField("Damage ", myTarget.Damage);
        }
        else if (myTarget.index == 1)
        {
            myTarget.projectileSpeed = EditorGUILayout.FloatField("Projectile speed", myTarget.projectileSpeed);
            myTarget.Damage = EditorGUILayout.IntField("Damage ", myTarget.Damage);
            myTarget.upwardForce = EditorGUILayout.FloatField("upward force", myTarget.upwardForce);
            myTarget.projBullet = EditorGUILayout.ObjectField("bullet", myTarget.projBullet, typeof(GameObject), true) as GameObject;
            myTarget.useGravity = EditorGUILayout.Toggle("use gravity" , myTarget.useGravity);
            if(myTarget.useGravity)
            {
                myTarget.bulletMass = EditorGUILayout.FloatField("bullet mass - larger number -> more dropoff", myTarget.bulletMass);
            }
            myTarget.hasSplashDamage = EditorGUILayout.Toggle("Explode on impact", myTarget.hasSplashDamage);
            if(myTarget.hasSplashDamage)
            {
                myTarget.splashDamage = EditorGUILayout.FloatField("splash damage", myTarget.splashDamage);
                myTarget.splashRadius = EditorGUILayout.FloatField("splash radius", myTarget.splashRadius);
            }
        }
        else if (myTarget.index == 2)
        {
            myTarget.beamRange = EditorGUILayout.FloatField("Beam length", myTarget.beamRange);
            myTarget.Damage = EditorGUILayout.IntField("Damage ", myTarget.Damage);
        }
    }

    public void SetPath()
    {

    }
}
