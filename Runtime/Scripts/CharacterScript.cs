using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Types;

public class CharacterScript : MonoBehaviour
{
    public int size;
    public float inspectorWidth;
    [SerializeField] public List<UnityEvent<Camera, KeyCode>> abilityList;
    public List<string> abilityPaths;
    public List<KeyCode> keyList;
    public List<bool> keyName;
    public List<bool> isListening;
    public List<int> cooldownList;
    public List<float> remainingCooldown;
    public List<bool> abilityName;
    public List<bool> showMouseButtons;
    public List<GameObject> gunList;
    public List<GameObject> gunParts = new List<GameObject>();
    public List<List<GameObject>> gunPartList;// = new List<List<GameObject>>();
    public string modelName;
    public string gunName;
    public List<Vector3> gunPositions;
    public List<Vector3> gunRotationVectors;
    public Vector3 defaultGunPos = new Vector3(0.1f, -0.1f, 0.05f);
    public Vector3 defaultGunRot = new Vector3(0f, 90f, 0f);
    public Camera cam;
    public List<MouseButtons> mouseButtonsList;
    //public GameObject _self;

    public Texture2D gunTex;

    //[Header("Movement")]
    public bool showMovement;
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    public Vector3 moveDir;
    Rigidbody rb;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool jumpReady = true;


    //[Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    //[Header("Keybinds")]
    public bool showKeyBinds;
    public KeyCode jumpKey = KeyCode.Space;

    public Material gunMat;
    public List<Material> partMats;
    public List<List<Material>> partMatsList;
    public Mesh gunMesh;

    public GUIContent abilityIcon;
    public GUIContent weaponIcon;
    public GUIContent movementIcon;
    public GUIContent pluginLogo;

    public Animator animator;

    public List<MonoBehaviour> abilityTypes;
    public MonoBehaviour abilityType;

    public PrimaryFireData primaryFireData;
    public PrimaryFire primaryFire;
    public DashData dash;

    public int maxHealth;
    public int currentHealth;
    public int overHealth;
    public int combinedHealth;
    public float overHealthTimer;


    public void Start()
    {
        //GetComponentInParent<CharacterScript>().GetComponent<MeshRenderer>().materials[0] = 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        currentHealth = maxHealth;
    }

    public void SwitchHere()
    {
        Debug.Log("switch");
    }

    private void MovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //Debug.DrawRay(transform.position, Vector3.down, Color.magenta, Mathf.Infinity);
        if (grounded)
        {

            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void AbilityInput()
    {

        for (int i = 0; i < keyList.Count; i++)
        {
            if (CheckCooldown(i))
            {
                if (Input.GetKeyDown(keyList[i]))
                {
                    remainingCooldown[i] = cooldownList[i];
                    abilityList[i].Invoke(cam, keyList[i]);

                }
            }
        }
    }

    private bool CheckCooldown(int i)
    {
        if (remainingCooldown[i] <= 0)
        {
            return true;
        }
        else
        {
            remainingCooldown[i] -= Time.deltaTime;
            return false;
        }
    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(moveDir != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else 
        {
            animator.SetBool("isMoving", false);
        }

        if (grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limit = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limit.x, limit.y, limit.z);
        }
    }

    public void TakeDamage(int damage)
    {
        if(overHealth > 0)
        {
            if(overHealth - damage < 0)
            {
                damage -= overHealth;
                overHealth = 0;
                overHealthTimer = 0;
                currentHealth -= damage;
            }
            else if(overHealth - damage > 0)
            {
                overHealth -= damage;
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }

    void FixedUpdate()
    {
        MovementInput();
        SpeedControl();
        MovePlayer();
    }

    private void Update()
    {
        AbilityInput();
        
        if(currentHealth <= 0)
        {
            //Destroy(gameObject);
            Debug.Log(gameObject.name + " Died");
        }
        else if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        if(overHealth > 0)
        {
            overHealthTimer -= Time.deltaTime;
            if(overHealthTimer < 0)
            {
                overHealth = 0;
                overHealthTimer = 0;
            }
        }

    }
}
