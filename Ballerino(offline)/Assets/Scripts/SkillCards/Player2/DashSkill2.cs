using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill2", menuName = "Skill/DashSkill2")]
public class DashSkill2 : AbilityStrategy
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float dashForce = 10f;
    private Coroutine dashCor2;

    public override void ApplyEffect(PlayerControl player)
    {
        if (!IsOnCooldown && !IsEffectActive)
        {
            dashCor2 = player.StartCoroutine(Dash2(player));
            IsEffectActive = true;
        }
    }

    private IEnumerator Dash2(PlayerControl player)
    {
        Vector2 dashDir = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")).normalized;
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
        if (dashCor2 != null)
        {
            player.StopCoroutine(dashCor2);
            dashCor2 = null;
            IsEffectActive = false;
            StartCooldown();
        }

    }
}
