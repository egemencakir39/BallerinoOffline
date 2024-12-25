using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootPower", menuName = "Skill/ShootPower")]
public class ShootPowerSkill : AbilityStrategy
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private float shootPowerBoost = 2f;
    private Coroutine shootPowerCor;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            shootPowerCor = player.StartCoroutine(ShootPower(player));
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
        if (shootPowerCor != null)
        {
            player.StopCoroutine(shootPowerCor);
            shootPowerCor = null;
            player.shootPower = 2f;
            IsEffectActive = false;
            StartCooldown();
        }
    }

}
