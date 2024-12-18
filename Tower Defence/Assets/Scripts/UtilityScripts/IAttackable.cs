using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for objects that can take damage
public interface IAttackable
{
    void TakeDamage(int damage);
}
