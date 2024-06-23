using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletForML : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D otherCollid)
    {
        EnemyForML enemy = otherCollid.GetComponent<EnemyForML>();
        enemy.TakeDamage(damage);
        Destroy(gameObject);

        // Get the parent agent (ship) if it exists
        var shipAgent = transform.parent?.GetComponent<ShipAgent>();
        if (shipAgent != null)
        {
            // Increase the reward of the parent agent (ship) by one
            shipAgent.AddReward(1f);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
