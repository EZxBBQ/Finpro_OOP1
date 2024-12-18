using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to make sure the bullet script has a method to give the damage value to the attackComponent
public interface IAttack
{
    int GetAttackDamage();
}
