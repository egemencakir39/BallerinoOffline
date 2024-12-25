using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootPowerSkill2",menuName = "Skill/ShootPowerSkill2")]
public class ShootPowerSkill2 : AbilityStrategy
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private float shootPowerBoost = 2f;
    private Coroutine shootPowerCor2;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            shootPowerCor2 = player.StartCoroutine(ShootPower(player));
            IsEffectActive = true;
        }
    }

    private IEnumerator ShootPower(PlayerControl player)
    {
        player.shootPower = player.shootPower + shootPowerBoost;
        yield return new WaitForSeconds(duration);
        player.shootPower = player.shootPower - shootPowerBoost;
        StartCooldown();
        IsEffectActive = false;
    }

    public override void RemoveEffect(PlayerControl player)
    {
        if (shootPowerCor2 != null) 
        {
            player.StopCoroutine(shootPowerCor2);
            player.shootPower = 2f;
            shootPowerCor2 = null;
            IsEffectActive = false;
            StartCooldown();
        }
    }
}
