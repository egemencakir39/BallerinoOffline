using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "SpeedSkill", menuName = "Skill/SpeedSkill")]

public class SpeedSkill : AbilityStrategy
{
    [SerializeField] private float duration = 15f;
    [SerializeField] private float speedBoost = 2f;
    private CancellationTokenSource speedCts;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            speedCts = new CancellationTokenSource();
            speedEffect(player, speedCts.Token).Forget();
            IsEffectActive = true;
        }
    }
    private async UniTaskVoid speedEffect(PlayerControl player, CancellationToken token)
    {
        player.moveSpeed = player.moveSpeed + speedBoost;
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        player.moveSpeed = player.moveSpeed - speedBoost; 
        StartCooldown(); 
        IsEffectActive = false;
        
    }
    public override void RemoveEffect(PlayerControl player)
    {
        if (speedCts != null)
        {
            speedCts.Cancel();
            speedCts.Dispose();
            speedCts = null;
            player.moveSpeed = 7f;
            IsEffectActive = false;
            StartCooldown();
        }
    }
   
}
