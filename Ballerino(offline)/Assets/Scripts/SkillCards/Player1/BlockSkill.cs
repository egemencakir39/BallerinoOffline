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
        SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
        
        
        Debug.Log((duration * 70) / 100);
        yield return new WaitForSeconds((duration * 70) / 100);
        
        
        block.GetComponent<Block>().FlashStart((duration * 30) / 100);
        Debug.Log((duration * 30) / 100);
        yield return new WaitForSeconds(((duration * 30) / 100));
        
        Debug.Log("bitti");
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
