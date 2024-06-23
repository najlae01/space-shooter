using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected GameObject explosionPrefab;

    [SerializeField] protected Animator animator;

    [Header("Score"), SerializeField]
    protected int scoreValue;

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
        EndGameManager.endManager.UpdateScore(scoreValue);
    }
}
