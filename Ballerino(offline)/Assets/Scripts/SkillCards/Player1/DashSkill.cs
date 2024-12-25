using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "Skill/DashSkill")]
public class DashSkill : AbilityStrategy
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float dashForce = 10f;
    private Coroutine dashCor;
    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            dashCor = player.StartCoroutine(Dash(player));
            IsEffectActive = true;
        }
    }

    private IEnumerator Dash(PlayerControl player)
    {
        Vector2 dashDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (dashDir != Vector2.zero)
        {
            player.rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(duration);
        IsEffectActive = false;
        StartCooldown();
    }

    public override void RemoveEffect(PlayerControl player)
    {
        if (dashCor != null)
        {
            player.StopCoroutine(dashCor);
            dashCor = null;
            IsEffectActive = false;
            StartCooldown();
        }
        
    }
}
