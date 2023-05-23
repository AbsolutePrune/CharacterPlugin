using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Types;
using TypeReferences;




public class ScriptableWindowEditor : EditorWindow
{

    static PrimaryFireData primaryFireData;
    static DashData dashData;
    static HealthpackData healthpackData;
    static ShieldData shieldData;

    public static MonoScript abilityType;
    public static TypeReference compType;
    public static TypeReference type;

    //[SerializeField] public TypeReference typeField;

    public string abilityName;
    public string abilityPath;

    static CharacterScriptEditor characterScriptEditor;

    static AbilityTypes characterAbility;
    static FireModes fireMode;

    public static PrimaryFireData primaryFireInfo { get { return primaryFireData; } }
    public static DashData dashInfo { get { return dashData; } }


    public static void Open(/*System.Type type*/)
    {

        ScriptableWindowEditor abilityWindow = EditorWindow.GetWindow<ScriptableWindowEditor>("ability window");

        //abilityType = type;
    }

    void OnGUI()
    {

        abilityType = (MonoScript)EditorGUILayout.ObjectField("Ability Type", abilityType, typeof(MonoScript), false);

        //type = (TypeReference)EditorGUILayout.ObjectField(type, typeof(TypeReference));


        //int ability = (int)characterAbility;

        //abilityType = (MonoBehaviour)EditorGUILayout.ObjectField(abilityType, typeof(MonoScript), false);



        abilityName = EditorGUILayout.TextField("Name", abilityName);
        if (abilityType != null)
        {
            type = abilityType.GetClass();
            DrawAbility();
        }

        if (GUILayout.Button("save and exit"))
        {
            SaveAbilityData();
            Close();
        }
    }

    public void SaveAbilityData() //Packages//Runtime/Resources/ScriptableAbilities
    {
        abilityPath = "Packages/com.chilean_miner.charactercreator/Runtime/Resources/ScriptableAbilities/" + abilityName + ".asset"; ;

        if (abilityType.name == "PrimaryFire")
        {
            AssetDatabase.CreateAsset(primaryFireData, abilityPath);
        }
        else if (abilityType.name == "Dash")
        {
            AssetDatabase.CreateAsset(dashData, abilityPath);
        }
        else if (abilityType.name == "SpawnHealthPack")
        {
            AssetDatabase.CreateAsset(healthpackData, abilityPath);
        }
        else if (abilityType.name == "Shield")
        {
            AssetDatabase.CreateAsset(shieldData, abilityPath);
        }



    }

    public static void InitData()
    {
        primaryFireData = (PrimaryFireData)CreateInstance(typeof(PrimaryFireData));
        dashData = (DashData)CreateInstance(typeof(DashData));
        healthpackData = (HealthpackData)CreateInstance(typeof(HealthpackData));
        shieldData = (ShieldData)CreateInstance(typeof(ShieldData));

    }

    void OnEnable()
    {
        InitData();
    }

    void DrawAbility()
    {
        if (abilityType.name == "PrimaryFire")
        {
            DrawPrimaryFireSettings();
        }
        else if (abilityType.name == "Dash")
        {
            DrawDashSettings();
        }
        else if (abilityType.name == "SpawnHealthPack")
        {
            DrawHealthpackSettings();
        }
        else if (abilityType.name == "Shield")
        {
            DrawShieldSettings();
        }
    }

    void DrawPrimaryFireSettings()
    {
        primaryFireData.type = type;

        //primaryFireData.cam = EditorGUILayout.ObjectField("Camera", primaryFireData.cam, typeof(Camera), true) as Camera;
        fireMode = (FireModes)EditorGUILayout.EnumPopup("Fire Mode ", fireMode);
        primaryFireData.index = (int)fireMode;
        //self.GetComponent<CharacterScript>().abilityList[i];

        if (primaryFireData.index == 0)
        {
            primaryFireData.Damage = EditorGUILayout.IntField("Damage ", primaryFireData.Damage);
        }
        else if (primaryFireData.index == 1)
        {
            primaryFireData.projectileSpeed = EditorGUILayout.FloatField("Projectile speed", primaryFireData.projectileSpeed);
            primaryFireData.Damage = EditorGUILayout.IntField("Damage ", primaryFireData.Damage);
            primaryFireData.upwardForce = EditorGUILayout.FloatField("upward force", primaryFireData.upwardForce);
            primaryFireData.projBullet = EditorGUILayout.ObjectField("bullet", primaryFireData.projBullet, typeof(GameObject), false) as GameObject;
            primaryFireData.useGravity = EditorGUILayout.Toggle("use gravity", primaryFireData.useGravity);
            if (primaryFireData.useGravity)
            {
                primaryFireData.bulletMass = EditorGUILayout.FloatField("bullet mass", primaryFireData.bulletMass);
            }
            primaryFireData.hasSplashDamage = EditorGUILayout.Toggle("Explode on impact", primaryFireData.hasSplashDamage);
            if (primaryFireData.hasSplashDamage)
            {
                primaryFireData.splashDamage = EditorGUILayout.FloatField("splash damage", primaryFireData.splashDamage);
                primaryFireData.splashRadius = EditorGUILayout.FloatField("splash radius", primaryFireData.splashRadius);
            }
        }
        else if (primaryFireData.index == 2)
        {
            primaryFireData.beamRange = EditorGUILayout.FloatField("Beam length", primaryFireData.beamRange);
            primaryFireData.Damage = EditorGUILayout.IntField("Damage ", primaryFireData.Damage);
        }
    }

    void DrawDashSettings()
    {
        dashData.type = type;
        dashData.dashLength = EditorGUILayout.FloatField("dash length", dashData.dashLength);
        dashData.dashDuration = EditorGUILayout.FloatField("dash duration", dashData.dashDuration);
        dashData.doesDamage = EditorGUILayout.Toggle("Does it deal damage", dashData.doesDamage);
        if (dashData.doesDamage)
        {
            dashData.damage = EditorGUILayout.IntField("Damage ", dashData.damage);
        }
        dashData.forwardDash = EditorGUILayout.Toggle("Toggle forward or directional dash", dashData.forwardDash);
    }

    void DrawHealthpackSettings()
    {
        healthpackData.type = type;

        healthpackData.healValue = EditorGUILayout.IntField("Heal ", healthpackData.healValue);
        healthpackData.useGravity = EditorGUILayout.Toggle("Use Gravity", healthpackData.useGravity);
        if (healthpackData.useGravity)
        {
            healthpackData.packMass = EditorGUILayout.FloatField("Healthpack mass", healthpackData.packMass);
        }
        healthpackData.hasDuration = EditorGUILayout.Toggle("Expires", healthpackData.hasDuration);
        if (healthpackData.hasDuration)
        {
            healthpackData.packDuration = EditorGUILayout.FloatField("Healthpack lifetime", healthpackData.packDuration);
        }
        healthpackData.giveOverHealth = EditorGUILayout.Toggle("Can give overhealth", healthpackData.giveOverHealth);
        if (healthpackData.giveOverHealth)
        {
            healthpackData.overHealthDuration = EditorGUILayout.FloatField("Overhealth duration", healthpackData.overHealthDuration);
            healthpackData.onlyOverHealth = EditorGUILayout.Toggle("Only give overhealth", healthpackData.onlyOverHealth);
        }
        healthpackData.healthpack = EditorGUILayout.ObjectField("health pack", healthpackData.healthpack, typeof(GameObject), false) as GameObject;
    }

    void DrawShieldSettings()
    {
        shieldData.type = type;
        shieldData.shield = EditorGUILayout.ObjectField("Shield shape", shieldData.shield, typeof(GameObject), false) as GameObject;
        shieldData.shieldHealth = EditorGUILayout.IntField("Shield health ", shieldData.shieldHealth);
        shieldData.regenPerSecond = EditorGUILayout.IntField("Health regen /s ", shieldData.regenPerSecond);
        shieldData.regenDelay = EditorGUILayout.FloatField("Regen delay ", shieldData.regenDelay);

        shieldData.minRecoveryHealth = EditorGUILayout.IntField("Min health the shield is usable after breaking ", shieldData.minRecoveryHealth);
        shieldData.shieldDuration = EditorGUILayout.FloatField("Shield duration ", shieldData.shieldDuration);
        shieldData.colour = EditorGUILayout.ColorField(shieldData.colour);
        //shieldData.shield.GetComponent<MeshRenderer>().sharedMaterial = (Material)EditorGUILayout.ObjectField("part ", shieldData.shield.gameObject.GetComponent<MeshRenderer>().sharedMaterial, typeof(Material), true);
        shieldData.isPlayerChild = EditorGUILayout.Toggle("Shield moves with player", shieldData.isPlayerChild);
        shieldData.offset = EditorGUILayout.Vector3Field("Offset", shieldData.offset);

    }

    static void GetPaths(string folder)
    {
        Debug.Log(folder);
        string[] folders = AssetDatabase.GetSubFolders(folder);
        foreach (string fld in folders)
        {
            GetPaths(fld);

            //scriptPaths = AssetDatabase.FindAssets("ScriptableObject", folders);
        }

    }
}
