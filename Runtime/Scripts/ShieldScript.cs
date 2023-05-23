using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public KeyCode shieldKey;
    public float shieldHealth;
    public float minimumRecoveryHealth;
    public float regenPerSecond;
    public float regenDelay;
    public float shieldDuration;
    public bool isChild;
    public bool shieldBroken;

    float maxHealth;
    float timeUntilRegen;
    bool isRegenerating;
    bool isShielding;

    void Start()
    {
        maxHealth = shieldHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChild)
        {

            if (Input.GetKey(shieldKey))
            {
                if (!shieldBroken)
                {
                    isShielding = true;
                    isRegenerating = false;

                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    gameObject.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.identity;
                }
            }
            else if (Input.GetKeyUp(shieldKey))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                isShielding = false;
                timeUntilRegen = regenDelay;
            }

            if (shieldBroken)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                isShielding = false;
            }

            if (!isShielding && shieldHealth < maxHealth && !isRegenerating)
            {
                timeUntilRegen -= Time.deltaTime;
                if (timeUntilRegen < 0)
                {
                    isRegenerating = true;
                    timeUntilRegen = regenDelay;
                }
            }

            if (isRegenerating)
            {
                shieldHealth += regenPerSecond * Time.deltaTime;
                if (shieldHealth > minimumRecoveryHealth)
                {
                    shieldBroken = false;
                }
                if (shieldHealth > maxHealth)
                {
                    isRegenerating = false;

                    shieldHealth = maxHealth;
                }
            }
        }
        else
        {
            if(shieldBroken)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            if(shieldDuration >= 0)
            {
                shieldDuration -= Time.deltaTime;
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        shieldHealth -= damage;
        
        if(shieldHealth <= 0)
        {
            shieldHealth = 0;
            shieldBroken = true;
            timeUntilRegen = regenDelay;
        }
    }
}
