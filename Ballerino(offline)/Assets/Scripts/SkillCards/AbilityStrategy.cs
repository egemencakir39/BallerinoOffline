using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityStrategy : ScriptableObject
{
    
    public string cardName;
    public Sprite cardImage;
    public float cooldownTime;
    public bool IsEffectActive { get; protected set; } = false;
    private float cooldownTimer;

    public bool IsOnCooldown => cooldownTimer > 0;
    public abstract void ApplyEffect(PlayerControl player);


    public abstract void RemoveEffect(PlayerControl player);
    public void StartCooldown()
    {

        if (!IsOnCooldown)
        {
            cooldownTimer = cooldownTime;
           
        }
    }

    public void UpdateCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0;
              
            }
        }
    }


}

