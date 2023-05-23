using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Types;

public class ModelPreviewWindow : EditorWindow
{
    static GameObject model;
    static List<GameObject> modelParts;
    static Editor gameObjectEditor;
    
    void OnDisable()
    {
        //Editor.
        Debug.Log("disaddble");
        DestroyImmediate(gameObjectEditor);
        gameObjectEditor = null;
        
    }

    public static void Open(GameObject gameObject, List<GameObject> partList)
    {
        Debug.Log("open");
        ModelPreviewWindow modelWindow = GetWindow<ModelPreviewWindow>("Model Viewer");
        Init(gameObject, partList);
    }
    
    void OnGUI() 
    {
        Debug.Log("gui");
        
        EditorGUILayout.BeginHorizontal();

        if(model != null) 
        {
            if (gameObjectEditor != null)
            {
                Rect main = (Rect)EditorGUILayout.BeginVertical(GUILayout.Width(500), GUILayout.Height(500));
                foreach (GameObject part in modelParts)
                {
                    Rect sub = (Rect)EditorGUILayout.BeginVertical("Button", GUILayout.Width(500));
                    part.gameObject.GetComponent<MeshRenderer>().sharedMaterial =
                                    (Material)EditorGUILayout.ObjectField("part " + part.name,
                                    part.gameObject.GetComponent<MeshRenderer>().sharedMaterial, 
                                    typeof(Material), true);
                    part.gameObject.transform.localPosition = EditorGUILayout.Vector3Field("Part Position", part.transform.localPosition);
                    EditorGUILayout.EndVertical(); 
                    EditorGUILayout.Space(); 
                }
                EditorGUILayout.EndVertical();
                gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(500, 500), EditorStyles.whiteLabel); 
            }
        }
        EditorGUILayout.EndHorizontal();
         
        EditorGUILayout.BeginHorizontal(); 

        if (GUILayout.Button("Reload", GUILayout.Height(50)))
        { 
            Reload(); 
        }
         
         
        if (GUILayout.Button("Exit", GUILayout.Height(50)))
        {
            gameObjectEditor = null;
            DestroyImmediate(gameObjectEditor);
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    void Reload()
    {
        Debug.Log("reload"); 
        Init(model, modelParts);

    }

    static void Init(GameObject gameObject, List<GameObject> partList)
    {
        Debug.Log("init");
        model = gameObject;
        modelParts = partList;
        gameObjectEditor = Editor.CreateEditor(model);
    }

}
