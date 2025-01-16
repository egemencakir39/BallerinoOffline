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
    [SerializeField] private string hInput = "Horizontal";
    [SerializeField] private string vInput = "Vertical";
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
        Vector2 dashDir = new Vector2(Input.GetAxis(hInput), Input.GetAxis(vInput)).normalized;
        if (dashDir != Vector2.zero)
        {
            player.rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
            player.trailRenderer.emitting = true;
        }
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        player.trailRenderer.emitting = false;
        IsEffectActive = false; 
        StartCooldown();
        
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
