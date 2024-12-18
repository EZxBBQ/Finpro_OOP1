using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private int attackDamage;

    private void Awake()
    {
        IAttack attackInterface = GetComponentInParent<IAttack>();
        if (attackInterface != null)
        {
            attackDamage = attackInterface.GetAttackDamage();
        }
        else
        {
            Debug.LogError("IAttack is not found in parent object.");

            // quit the editor if the interface is not found
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    // need to be public so weapon can access it
    public void DealDamage(GameObject target)
    {
        IAttackable attackable = target.GetComponentInChildren<IAttackable>();
        if (attackable != null)
        {
            attackable.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogError("Target cant be damaged.");
        }
    }
}
