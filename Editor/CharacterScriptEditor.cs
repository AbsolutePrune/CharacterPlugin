using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditorInternal;
using Types;
using TypeReferences;



[CustomEditor(typeof(CharacterScript))]
public class CharacterScriptEditor : Editor
{

    SerializedProperty abilityList;
    bool showAbilities;
    bool showWeapons;
    public MouseButtons mouseButtons;
    MonoScript abilityType;
    bool isInit;

    private GameObject tmpObj;

    private void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        CharacterScript myTarget = (CharacterScript)target;

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        Rect scale = GUILayoutUtility.GetLastRect();
        if (scale.width != 1)
        {
            myTarget.inspectorWidth = scale.width;
        }

        if (!isInit && myTarget.abilityName == null) 
        {            
            isInit = true;

            myTarget.pluginLogo.image = Resources.Load<Texture2D>("Icons/AbilityIcon");
            myTarget.abilityIcon.image = Resources.Load<Texture2D>("Icons/AbilityIcon");
            myTarget.weaponIcon.image = Resources.Load<Texture2D>("Icons/WeaponIcon");
            myTarget.movementIcon.image = Resources.Load<Texture2D>("Icons/MovementIcon");

            myTarget.maxHealth = 100;

            myTarget.abilityList = new();
            myTarget.abilityName = new();
            myTarget.keyList = new();
            myTarget.keyName = new();
            myTarget.isListening = new();
            myTarget.remainingCooldown = new();
            myTarget.showMouseButtons = new();
            myTarget.mouseButtonsList = new();
            myTarget.cooldownList = new();

            myTarget.gunList = new();
            myTarget.gunPartList = new();
            myTarget.gunRotationVectors = new(); 
            myTarget.partMats = new();
            myTarget.partMatsList = new();
        }
        
        EditorGUILayout.LabelField(myTarget.pluginLogo, GUILayout.MaxWidth(myTarget.inspectorWidth / 2), GUILayout.MaxHeight(myTarget.inspectorWidth / 2));

        myTarget.maxHealth = EditorGUILayout.IntField("Max health", myTarget.maxHealth);
        //EditorGUILayout.IntField("Current health", myTarget.currentHealth);
        //EditorGUILayout.IntField("Over health", myTarget.overHealth);
        //EditorGUILayout.FloatField("Over health timer", myTarget.overHealthTimer);

        abilityList = serializedObject.FindProperty("abilityList");


        EditorGUILayout.LabelField(myTarget.abilityIcon, GUILayout.Width(30), GUILayout.Height(30));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(10));
        showAbilities = EditorGUILayout.BeginFoldoutHeaderGroup(showAbilities, "Abilities");
        EditorGUILayout.EndHorizontal();
        if (showAbilities)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Abilities", GUILayout.Width(myTarget.inspectorWidth / 8));
            myTarget.size = Mathf.Max(1, EditorGUILayout.IntField(myTarget.size, GUILayout.Width(myTarget.inspectorWidth / 15)));


            while (myTarget.abilityList.Count > myTarget.size)
            {
                myTarget.abilityList.RemoveAt(myTarget.abilityList.Count - 1);
            }
            while (myTarget.abilityList.Count < myTarget.size)
            {
                myTarget.abilityList.Add(null);
            }

            while (myTarget.abilityName.Count > myTarget.size)
            {
                myTarget.abilityName.RemoveAt(myTarget.abilityName.Count - 1);
            }
            while (myTarget.abilityName.Count < myTarget.size)
            {
                myTarget.abilityName.Add(false);
            }

            while (myTarget.keyList.Count > myTarget.size)
            {
                myTarget.keyList.RemoveAt(myTarget.keyList.Count - 1);
            }
            while (myTarget.keyList.Count < myTarget.size)
            {
                myTarget.keyList.Add(KeyCode.None);
            }

            while (myTarget.keyName.Count > myTarget.size)
            {
                myTarget.keyName.RemoveAt(myTarget.keyName.Count - 1);
            }
            while (myTarget.keyName.Count < myTarget.size)
            {
                myTarget.keyName.Add(false);
            }

            while (myTarget.isListening.Count > myTarget.size)
            {
                myTarget.isListening.RemoveAt(myTarget.isListening.Count - 1);
            }
            while (myTarget.isListening.Count < myTarget.size)
            {
                myTarget.isListening.Add(false);
            }

            while (myTarget.cooldownList.Count > myTarget.size)
            {
                myTarget.cooldownList.RemoveAt(myTarget.cooldownList.Count - 1);
            }
            while (myTarget.cooldownList.Count < myTarget.size)
            {
                myTarget.cooldownList.Add(3);
            }

            while (myTarget.remainingCooldown.Count > myTarget.size)
            {
                myTarget.remainingCooldown.RemoveAt(myTarget.remainingCooldown.Count - 1);
            }
            while (myTarget.remainingCooldown.Count < myTarget.size)
            {
                myTarget.remainingCooldown.Add(0);
            }

            while (myTarget.showMouseButtons.Count > myTarget.size)
            {
                myTarget.showMouseButtons.RemoveAt(myTarget.showMouseButtons.Count - 1);
            }
            while (myTarget.showMouseButtons.Count < myTarget.size)
            {
                myTarget.showMouseButtons.Add(false);
            }

            while (myTarget.mouseButtonsList.Count > myTarget.size)
            {
                myTarget.mouseButtonsList.RemoveAt(myTarget.mouseButtonsList.Count - 1);
            }
            while (myTarget.mouseButtonsList.Count < myTarget.size)
            {
                myTarget.mouseButtonsList.Add(mouseButtons);
            }

            //myTarget.abilityPaths = Resources.LoadAll<ScriptableObject>("ScriptableAbilities").;
            EditorGUILayout.EndHorizontal();
            //abilityType = (MonoScript)EditorGUILayout.ObjectField("Ability Type",abilityType, typeof(MonoScript), false);
            //TypeReference type = abilityType.GetClass();
            //Debug.Log(type);

            

            if (myTarget.abilityName[0] = GUILayout.Button("Create Ability", GUILayout.Width(myTarget.inspectorWidth / 4)))
            {
                ScriptableWindowEditor.Open();
            }

            for (int i = 0; i < myTarget.size; i++)
            {
                EditorGUILayout.LabelField("Ability " + (i + 1), GUILayout.Width(myTarget.inspectorWidth / 4));
                EditorGUILayout.BeginHorizontal();

                if (myTarget.showMouseButtons[i])
                {
                    myTarget.mouseButtonsList[i] = (MouseButtons)EditorGUILayout.EnumPopup(myTarget.mouseButtonsList[i], GUILayout.Width(myTarget.inspectorWidth / 4));
                    //string buttonName = (string)mouseButtons.ToString();
                    int index = (int)myTarget.mouseButtonsList[i];

                    if (index == 0)
                    {
                        myTarget.keyList[i] = KeyCode.Mouse0;
                    }
                    else if (index == 1)
                    {
                        myTarget.keyList[i] = KeyCode.Mouse1;
                    }
                    else if (index == 2)
                    {
                        myTarget.keyList[i] = KeyCode.Mouse2;
                    }
                    else if (index == 3)
                    {
                        myTarget.keyList[i] = KeyCode.Mouse3;
                    }
                    else if (index == 4)
                    {
                        myTarget.keyList[i] = KeyCode.Mouse4;
                    }
                }
                else
                {
                    myTarget.keyName[i] = GUILayout.Button("Key: " + myTarget.keyList[i].ToString(), GUILayout.Width(myTarget.inspectorWidth / 4));
                }
                myTarget.showMouseButtons[i] = EditorGUILayout.Toggle(myTarget.showMouseButtons[i], GUILayout.Width(myTarget.inspectorWidth / 45));
                EditorGUILayout.LabelField("", GUILayout.Width(myTarget.inspectorWidth / 15));

                if (myTarget.keyName[i])
                {

                    if (!myTarget.isListening[i])
                    {
                        for (int j = 0; j < myTarget.size; j++)
                        {
                            myTarget.isListening[j] = false;
                        }
                        myTarget.isListening[i] = true;
                    }
                    else
                    {
                        myTarget.isListening[i] = false;
                    }
                }

                if (myTarget.isListening[i])
                {
                    KeyCode key = Event.current.keyCode;
                    if (key != KeyCode.None)
                    {
                        myTarget.keyList[i] = KeyCodeFieldLayout(key);
                        myTarget.isListening[i] = false;
                    }


                }

                EditorGUILayout.LabelField("Cooldown: ", GUILayout.Width(myTarget.inspectorWidth / 6));
                myTarget.cooldownList[i] = EditorGUILayout.IntField(myTarget.cooldownList[i], GUILayout.Width(myTarget.inspectorWidth / 8));
                EditorGUILayout.LabelField("", GUILayout.Width(myTarget.inspectorWidth / 45));




                EditorGUILayout.EndHorizontal();



                if (myTarget.size == abilityList.arraySize)
                {
                    EditorGUILayout.PropertyField(abilityList.GetArrayElementAtIndex(i));
                }
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        Camera cam = myTarget.GetComponentInChildren<Camera>();
        GunPos[] gunCount = cam.GetComponentsInChildren<GunPos>();

        if (myTarget.gunPartList == null)
        {
            //Debug.Log("was null");
            myTarget.gunPartList = new List<List<GameObject>>();
            myTarget.partMatsList = new List<List<Material>>();

            foreach (GunPos gun in gunCount)
            {
                myTarget.gunParts = new List<GameObject>();
                myTarget.partMats = new List<Material>();
                MeshRenderer[] partRend = gun.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer pr in partRend)
                {
                    GameObject parts = pr.gameObject;
                    myTarget.gunParts.Add(parts);
                    myTarget.partMats.Add(pr.sharedMaterial);
                }
                myTarget.gunPartList.Add(myTarget.gunParts);

                myTarget.partMatsList.Add(myTarget.partMats);
            }
        }

        if (myTarget.weaponIcon != null)
        {

        }
        EditorGUILayout.LabelField(myTarget.weaponIcon, GUILayout.Width(30), GUILayout.Height(30));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(10));
        showWeapons = EditorGUILayout.BeginFoldoutHeaderGroup(showWeapons, "Weapons");

        EditorGUILayout.EndHorizontal();
        if (showWeapons)
        {

            myTarget.defaultGunPos = EditorGUILayout.Vector3Field("Position Offset", myTarget.defaultGunPos);
            myTarget.defaultGunRot = EditorGUILayout.Vector3Field("Rotation Offset", myTarget.defaultGunRot);



            myTarget.modelName = EditorGUILayout.TextField("Model name", myTarget.modelName);
            myTarget.gunName = EditorGUILayout.TextField("Gun name", myTarget.gunName);


            if (GUILayout.Button("Create Gun", GUILayout.Width(myTarget.inspectorWidth / 4)))
            {
                if (myTarget.modelName == "" || myTarget.gunName == "")
                {
                    if (myTarget.modelName == "")
                    {
                        Debug.Log("Enter a model name");
                    }
                    if (myTarget.gunName == "")
                    {
                        Debug.Log("Give the gun a name");
                    }
                }
                else
                {


                    while (myTarget.gunList.Count > gunCount.Length)
                    {
                        myTarget.gunList.RemoveAt(myTarget.gunList.Count - 1);
                        myTarget.gunRotationVectors.RemoveAt(myTarget.gunRotationVectors.Count - 1);
                        myTarget.gunPartList.RemoveAt(myTarget.gunPartList.Count - 1);
                    }
                    GameObject gun = new GameObject();

                    gun.AddComponent<GunPos>();
                    gun.transform.parent = cam.transform;
                    gun.transform.position = cam.transform.position + myTarget.defaultGunPos;
                    gun.transform.rotation = cam.transform.rotation;
                    gun.name = myTarget.gunName;

                    myTarget.gunList.Add(gun);
                    myTarget.gunRotationVectors.Add(myTarget.defaultGunRot);

                    myTarget.gunParts = LoadMeshs(gun, myTarget.modelName, myTarget.gunMat);
                    myTarget.gunPartList.Add(myTarget.gunParts);

                    foreach (GameObject part in myTarget.gunParts)
                    {
                        myTarget.partMats.Add(part.GetComponent<MeshRenderer>().sharedMaterial);
                    }
                    myTarget.partMatsList.Add(myTarget.partMats);

                }

            }
           

            EditorGUILayout.Space();

            if (myTarget.gunList.Count > 0 || myTarget.gunList == null)
            {
                for (int i = 0; i < myTarget.gunList.Count; i++)
                {
                    EditorGUILayout.BeginVertical("button");
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(myTarget.gunList[i].name, EditorStyles.boldLabel, GUILayout.Width(myTarget.inspectorWidth / 2));
                    bool inspectGun = GUILayout.Button("Inspect Gun", GUILayout.Width(myTarget.inspectorWidth / 4));
                    bool destroyGun = GUILayout.Button("Destroy Gun", GUILayout.Width(myTarget.inspectorWidth / 4));
                    EditorGUILayout.EndHorizontal();

                    myTarget.gunList[i].transform.position = EditorGUILayout.Vector3Field("Gun Position", myTarget.gunList[i].transform.position);

                    myTarget.gunRotationVectors[i] = EditorGUILayout.Vector3Field("Gun Rotation", myTarget.gunRotationVectors[i]);
                    myTarget.gunList[i].transform.rotation = cam.transform.rotation * Quaternion.Euler(myTarget.gunRotationVectors[i]);


                    if (inspectGun)
                    {
                        Debug.Log(myTarget.gunList[i]);
                        ModelPreviewWindow.Open(myTarget.gunList[i], myTarget.gunPartList[i]);
                    }

                    //if (myTarget.gunPartList != null)
                    //{
                    //    for (int j = 0; j < myTarget.gunPartList[i].Count; j++)
                    //    {
                    //        myTarget.gunPartList[i][j].gameObject.GetComponent<MeshRenderer>().sharedMaterial =
                    //                (Material)EditorGUILayout.ObjectField("Gun " + i + " part " + j,
                    //                myTarget.gunPartList[i][j].gameObject.GetComponent<MeshRenderer>().sharedMaterial,
                    //                typeof(Material));
                    //    }
                    //}

                    if (destroyGun)
                    {
                        //myTarget.gunList[i].AddComponent<Destroyer>();
                        DestroyImmediate(myTarget.gunList[i]);
                        myTarget.gunRotationVectors.RemoveAt(i);
                        myTarget.gunList.RemoveAt(i);
                        if (myTarget.gunPartList != null)
                        {
                            myTarget.gunPartList.RemoveAt(i);
                            myTarget.partMatsList.RemoveAt(i);
                        }
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                }
            }
            else
            {
                while (myTarget.gunList.Count > 0 || myTarget.gunRotationVectors.Count > 0)
                {
                    if (myTarget.gunRotationVectors.Count > 0)
                    {
                        myTarget.gunRotationVectors.RemoveAt(myTarget.gunRotationVectors.Count - 1);
                    }
                    if (myTarget.gunList.Count > 0)
                    {
                        myTarget.gunList.RemoveAt(myTarget.gunList.Count - 1);
                    }
                    if (myTarget.gunPartList.Count > 0)
                    {
                        myTarget.gunList.RemoveAt(myTarget.gunPartList.Count - 1);
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        EditorGUILayout.LabelField(myTarget.movementIcon, GUILayout.Width(30), GUILayout.Height(50));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(10));
        myTarget.showMovement = EditorGUILayout.BeginFoldoutHeaderGroup(myTarget.showMovement, "Movement");
        EditorGUILayout.EndHorizontal();
        if (myTarget.showMovement)
        {
            myTarget.moveSpeed = EditorGUILayout.FloatField("Move speed", myTarget.moveSpeed);
            myTarget.orientation = EditorGUILayout.ObjectField("Orientation", myTarget.orientation, typeof(Transform), true) as Transform;
            myTarget.groundDrag = EditorGUILayout.FloatField("Ground drag", myTarget.groundDrag);
            myTarget.jumpCooldown = EditorGUILayout.FloatField("Jump cooldown", myTarget.jumpCooldown);
            myTarget.jumpForce = EditorGUILayout.FloatField("Jump force", myTarget.jumpForce);
            myTarget.airMultiplier = EditorGUILayout.FloatField("Air speed multiplier", myTarget.airMultiplier);
            myTarget.playerHeight = EditorGUILayout.FloatField("Player height", myTarget.playerHeight);
            LayerMask tempMask = EditorGUILayout.MaskField("Ground mask", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(myTarget.whatIsGround), InternalEditorUtility.layers);
            myTarget.whatIsGround = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();




        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

    }

    public List<GameObject> LoadMeshs(GameObject gunParent, string str, Material mat)
    {
        Mesh[] meshes = Resources.LoadAll<Mesh>("Models/" + str);
        //GameObject[] partArray = new GameObject[meshes.Length];
        List<GameObject> partArray = new List<GameObject>();
        foreach (Mesh mesh in meshes)
        {
            string partName = mesh.name;

            GameObject gunPart = new GameObject();

            gunPart.transform.parent = gunParent.transform;
            gunPart.transform.position = gunParent.transform.position;
            gunPart.transform.rotation = gunParent.transform.rotation;
            gunPart.transform.localScale = gunParent.transform.localScale;
            gunPart.name = partName;

            gunPart.AddComponent<MeshFilter>();
            gunPart.GetComponent<MeshFilter>().sharedMesh = mesh;

            gunPart.AddComponent<MeshRenderer>();
            gunPart.GetComponent<MeshRenderer>().sharedMaterial = mat;

            partArray.Add(gunPart);
        }
        return partArray;
    }

    void OnDisable()
    {
        
    }

    //[ContextMenu("ShowScriptables")]
    //public static MonoBehaviour ShowScriptableObjects(MonoScript type)
    //{
    //    ScriptableWindowEditor abilityWindow = EditorWindow.GetWindow<ScriptableWindowEditor>("ability window");

    //    return abilityWindow.abilityType;
    //}



    public static KeyCode KeyCodeField(Rect controlRect, KeyCode keyCode)
    {
        int controlID = GUIUtility.GetControlID(FocusType.Keyboard);

        KeyCode retVal = keyCode;

        Event evt = Event.current;

        switch (evt.GetTypeForControl(controlID))
        {
            case EventType.MouseDown:
                {
                    if (controlRect.Contains(Event.current.mousePosition) && Event.current.button == 0 && GUIUtility.hotControl == 0)
                    {
                        retVal = Event.current.keyCode;
                        GUIUtility.hotControl = controlID;
                        GUIUtility.keyboardControl = controlID;
                        evt.Use();
                    }
                    break;
                }
            case EventType.KeyDown:
                {
                    if (GUIUtility.keyboardControl == controlID)
                    {
                        retVal = Event.current.keyCode;
                        GUIUtility.hotControl = 0;
                        GUIUtility.keyboardControl = 0;
                        evt.Use();
                    }
                    break;
                }
            case EventType.KeyUp:
                {

                    break;
                }
        }
        return retVal;
    }


    public static KeyCode KeyCodeFieldLayout(KeyCode keyCode)
    {
        return KeyCodeField(EditorGUILayout.GetControlRect(), keyCode);
    }
}


//DrawPropertiesExcluding(serializedObject, "keyName", "inspectorWidth", "keyList", "isListening", "remainingCooldown", "size", "showKeyBinds", "showMovement", "moveSpeed", "orientation", "groundDrag", "jumpCooldown", "jumpForce", "airMultiplier", "playerHeight", "whatIsGround");
