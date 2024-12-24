using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpeedSkill", menuName = "Skill/SpeedSkill")]

public class freeSkill : AbilityStrategy
{
    [SerializeField] private float duration = 15f;
    [SerializeField] private float speedBoost = 2f;
    private Coroutine speedEffectCor;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            speedEffectCor = player.StartCoroutine(ResetSpeedEffect(player));
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
        if (speedEffectCor != null)
        {
            player.StopCoroutine(speedEffectCor);
            speedEffectCor = null;
            player.moveSpeed = 7f;
            IsEffectActive = false;
            StartCooldown();
        }
    }
   
}
