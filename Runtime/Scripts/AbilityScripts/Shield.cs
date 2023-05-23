using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : CharacterAbility
{
    public Component comp;
    public GameObject shield;
    public GameObject parent;
    public Camera playerCam;
    public Vector3 pos;
    public KeyCode keybind;
    public bool moveWithPlayer;
    bool isInitialised = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveWithPlayer)
        {

            parent.transform.localPosition = pos;
            parent.transform.localRotation = Quaternion.identity;
        }

    }

    public void Init(Camera cam, GameObject obj, Color colour, KeyCode key, bool isChild, Vector3 offset,
        int health, float duration, float minRecoveryHealth, float regenPerSecond, float regenDelay)
    {
        if(!isInitialised)
        {
            isInitialised = true;

            parent = new GameObject();
            parent.name = "Shield Node";
            parent.AddComponent<ShieldScript>();
            parent.GetComponent<ShieldScript>().shieldHealth = health;
            parent.GetComponent<ShieldScript>().shieldKey = key;
            parent.GetComponent<ShieldScript>().isChild = isChild;
            parent.GetComponent<ShieldScript>().minimumRecoveryHealth = minRecoveryHealth;
            parent.GetComponent<ShieldScript>().regenPerSecond = regenPerSecond;
            parent.GetComponent<ShieldScript>().regenDelay = regenDelay;
            parent.transform.position = gameObject.transform.position;
            parent.transform.rotation = gameObject.transform.rotation;
            //Debug.Log(gameObject.transform.position);
            //Debug.Log(parent.transform.position);


            //playerCam = cam;
            pos = offset;
            //keybind = key;
            moveWithPlayer = isChild;

            shield = Instantiate(obj);
            shield.GetComponent<MeshRenderer>().sharedMaterial.color = colour;

            if (isChild)
            {
                parent.transform.parent = gameObject.transform;
                shield.transform.parent = parent.transform;
            }
            
        }
        if(!isChild)
        {
            //shield.SetActive(true);
            
            parent.GetComponent<ShieldScript>().shieldHealth = health;
            parent.GetComponent<ShieldScript>().shieldKey = key;
            parent.GetComponent<ShieldScript>().isChild = isChild;
            parent.GetComponent<ShieldScript>().shieldBroken = false;
            parent.GetComponent<ShieldScript>().shieldDuration = duration;

            parent.transform.position = gameObject.transform.position;
            parent.transform.rotation = gameObject.transform.rotation;

            shield.transform.position = parent.transform.position;
            shield.transform.rotation = parent.transform.rotation;
            shield.transform.parent = parent.transform;

            parent.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    
}
