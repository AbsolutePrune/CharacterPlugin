using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackScript : MonoBehaviour
{
    public int healValue;
    public float packDuration;

    public bool giveOverHealth;
    public float overHealthDuration;
    public bool onlyOverHealth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(packDuration > 0)
        {
            packDuration -= Time.deltaTime;
            if(packDuration < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterScript player))
        {
            if (!onlyOverHealth)
            {

                player.currentHealth += healValue;
                if (player.currentHealth > player.maxHealth && giveOverHealth == true)
                {
                    int overHealth = player.currentHealth - player.maxHealth;
                    if (overHealth > player.overHealth)
                    {
                        player.overHealth = overHealth;
                    }
                    player.overHealthTimer = overHealthDuration;
                }
            }
            else
            {
                if(player.overHealth < healValue)
                {
                    player.overHealth = healValue;
                    player.overHealthTimer = overHealthDuration;
                }
            }
            Destroy(gameObject);
        }
    }
}
