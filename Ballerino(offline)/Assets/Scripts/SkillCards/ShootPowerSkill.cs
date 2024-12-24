using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootPower", menuName = "Skill/ShootPower")]
public class ShootPowerSkill : AbilityStrategy
{
    public override void ApplyEffect(PlayerControl player)
    {
        Debug.Log("hız ekleniyor2");
    }

    public override void RemoveEffect(PlayerControl player)
    {
        Debug.Log("asd");
    }

}
