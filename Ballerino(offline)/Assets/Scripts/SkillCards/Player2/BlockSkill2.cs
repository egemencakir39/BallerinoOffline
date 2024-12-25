using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Block Skill2", menuName = "Skill/Block Skill2")]
public class BlockSkill2 : AbilityStrategy
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Vector2 blockPrefabSpawnPoint;

    private Coroutine blockSpawnCor2;
    private GameObject block;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            blockSpawnCor2 = player.StartCoroutine(BlockSpawn(player));
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
        if (blockSpawnCor2 != null)
        {
            player.StopCoroutine(blockSpawnCor2);
            blockSpawnCor2 = null;
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
