using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;

    private List<Collider> alreadyColliedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyColliedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) {return;}

        if (alreadyColliedWith.Contains(other)) {return;}
        {
            alreadyColliedWith.Add(other);
        }

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
