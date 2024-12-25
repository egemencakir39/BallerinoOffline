using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedSkill2", menuName = "Skill/SpeedSkill2")]
public class SpeedSkill2 : AbilityStrategy
{

    [SerializeField] private float duration = 15f;
    [SerializeField] private float speedBoost = 2f;
    private Coroutine speedEffectCor2;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            speedEffectCor2 = player.StartCoroutine(ResetSpeedEffect(player));
            IsEffectActive = true;
        }
    }

    private IEnumerator ResetSpeedEffect(PlayerControl player)
    {
        player.moveSpeed = player.moveSpeed + speedBoost;
        yield return new WaitForSeconds(duration);
        player.moveSpeed = player.moveSpeed - speedBoost;
        StartCooldown();
        IsEffectActive = false;
    }

    public override void RemoveEffect(PlayerControl player)
    {
        if (speedEffectCor2 != null)
        {
            player.StopCoroutine(speedEffectCor2);
            speedEffectCor2 = null;
            player.moveSpeed = 7f;
            IsEffectActive = false;
            StartCooldown();
        }
    }
}
