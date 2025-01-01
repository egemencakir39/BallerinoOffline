using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Skill", menuName = "Skill/Block Skill")]
public class BlockSkill : AbilityStrategy
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Vector2 blockPrefabSpawnPoint;

    private CancellationTokenSource blockCts;
    private GameObject block;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            blockCts = new CancellationTokenSource();
            blockSpawn(player,blockCts.Token).Forget();
            IsEffectActive = true;
        }

    }
    private async UniTaskVoid blockSpawn(PlayerControl player, CancellationToken token)
    {
        block = Instantiate(blockPrefab, blockPrefabSpawnPoint, Quaternion.identity);
        SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
        
        await UniTask.Delay((TimeSpan.FromSeconds(duration * 70) /100), cancellationToken: token);
        
        
        block.GetComponent<Block>().FlashStart((duration * 30) / 100);
        await UniTask.Delay((TimeSpan.FromSeconds(duration*30)/100), cancellationToken: token);
        
        IsEffectActive = false;
        StartCooldown();
    }


    public override void RemoveEffect(PlayerControl player)
    {
        if (blockCts != null)
        {
            blockCts.Cancel();
            blockCts.Dispose();
        }
        if (block != null)
        {
            Destroy(block);
            block = null;
        }
        IsEffectActive = false;
        StartCooldown();
    }
}
