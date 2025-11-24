using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExcuteRole()
    {
        Debug.Log("체력 회복: " + healingPoint);
        return true;
    }
}
