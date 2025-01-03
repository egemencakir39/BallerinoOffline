using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootPower", menuName = "Skill/ShootPower")]
public class ShootPowerSkill : AbilityStrategy
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private float shootPowerBoost = 2f;
    private CancellationTokenSource shootPowerCts;

    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            shootPowerCts = new CancellationTokenSource();
            ShootPower(player, shootPowerCts.Token).Forget(); 
            IsEffectActive = true;
        }
    }

    private async UniTaskVoid ShootPower(PlayerControl player, CancellationToken token)
    {
        player.shootPower += shootPowerBoost;
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token).SuppressCancellationThrow();
        player.shootPower -= shootPowerBoost;
        StartCooldown();
        IsEffectActive = false;
    }

    public override void RemoveEffect(PlayerControl player)
    {
        if (shootPowerCts != null)
        {
            shootPowerCts.Cancel(); 
            shootPowerCts.Dispose();
            shootPowerCts = null;
        }

        player.shootPower = 2f; 
        IsEffectActive = false;
        StartCooldown();
    }
}