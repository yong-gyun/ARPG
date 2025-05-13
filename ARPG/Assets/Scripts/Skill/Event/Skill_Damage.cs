using UnityEngine;

public class Skill_Damage : Skill_Base
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") == true)
        {
            Creature target = other.GetComponent<Creature>();
            if (target != null)
            {
                if (_targetType.IsTarget(_owner, target) == true)
                {

                }
            }
        }
    }
}