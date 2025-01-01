using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "Skill/DashSkill")]
public class DashSkill : AbilityStrategy
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float dashForce = 10f;
    private CancellationTokenSource dashCts;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            dashCts = new CancellationTokenSource();
            dash(player,dashCts.Token).Forget();
            IsEffectActive = true;
            
        }
    }

    private async UniTaskVoid dash(PlayerControl player, CancellationToken token)
    {
        Vector2 dashDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (dashDir != Vector2.zero)
        {
            player.rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        if (!token.IsCancellationRequested)
        {
            IsEffectActive = false;
            StartCooldown();
        }
    }

    public override void RemoveEffect(PlayerControl player)
    {
        if (dashCts != null)
        {
            dashCts.Cancel();
            dashCts.Dispose();
            dashCts = null;
            IsEffectActive = false;
            StartCooldown();
        }
        
    }
}
