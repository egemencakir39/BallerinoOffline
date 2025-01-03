using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TeleportBall", menuName = "Skill/TeleportBall")]
public class TeleportBall : AbilityStrategy
{
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            Teleportball(player);
            IsEffectActive = true;
        }
    }

    private void Teleportball(PlayerControl player)
    {
        if (player.ball != null)
        {
            Vector2 playerPos = player.transform.position;
            Rigidbody2D playerRB = player.ball.GetComponent<Rigidbody2D>();
            Vector2 dir = playerRB.velocity.normalized;
            Vector2 newBallPos = playerPos + dir * 1f;
            player.ball.transform.position = newBallPos;
            IsEffectActive = false;
            StartCooldown();
        }
    }

    public override void RemoveEffect(PlayerControl player)
    {
        IsEffectActive = false;
        StartCooldown();
    }
}

