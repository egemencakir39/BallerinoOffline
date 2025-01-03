using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(fileName = "InvisibleBall",menuName = "Skill/InvisibleBall")]
public class InvisibleBall : AbilityStrategy
{
    [SerializeField] private float duration = 10f;
    private GameObject Ball;
    private SpriteRenderer BallRenderer;
    private CancellationTokenSource ınvisibleBallCts;
 
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            ınvisibleBallCts = new CancellationTokenSource();
            Invisibleball(player,ınvisibleBallCts.Token).Forget();
            IsEffectActive = true;
            
        }
    }

    private async UniTaskVoid Invisibleball(PlayerControl player, CancellationToken token)
    {
        Ball = player.ball;
        BallRenderer = Ball.GetComponent<SpriteRenderer>();
        BallRenderer.enabled = false;
        await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        BallRenderer.enabled = true;

        if (!token.IsCancellationRequested)
        {
            IsEffectActive = false;
            StartCooldown();
        }
    }

    public override void RemoveEffect(PlayerControl player)
    {
        ınvisibleBallCts.Cancel();
        ınvisibleBallCts.Dispose();
        ınvisibleBallCts = null;
        IsEffectActive = false;
        StartCooldown();
    }
}
