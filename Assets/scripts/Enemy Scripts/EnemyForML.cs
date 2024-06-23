using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForML : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected GameObject explosionPrefab;

    [SerializeField] protected Animator animator;


    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            //damage animation
        }
        else if (health <= 0)
        {
            DeathSequence();
            //destroy animation
        }
    }

    public virtual void HurtSequence()
    {

    }

    public virtual void DeathSequence()
    {

    }
}
