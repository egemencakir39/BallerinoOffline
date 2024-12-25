using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Skill", menuName = "Skill/Block Skill")]
public class BlockSkill : AbilityStrategy
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Vector2 blockPrefabSpawnPoint;

    private Coroutine blockSpawnCor;
    private GameObject block;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            blockSpawnCor = player.StartCoroutine(BlockSpawn(player));
            IsEffectActive = true;
        }

    }
    private IEnumerator BlockSpawn(PlayerControl player)
    {
        block = Instantiate(blockPrefab, blockPrefabSpawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(duration);
        Destroy(block);
        IsEffectActive = false;
        StartCooldown();
      

    }
    public override void RemoveEffect(PlayerControl player)
    {
        if (blockSpawnCor != null)
        {
            player.StopCoroutine(blockSpawnCor);
            blockSpawnCor = null;
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
