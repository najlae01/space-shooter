using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private Animator animator;

    [SerializeField] private Image healthFill;

    [SerializeField] protected GameObject explosionPrefab;

    private bool canPlayAnimation = true;

    void Start()
    {
        health = maxHealth;
        healthFill.fillAmount = health / maxHealth;
        EndGameManager.endManager.gameOver = false;
    }

    public void PlayerTakeDamage(float damage)
    {
        health -= damage;
        healthFill.fillAmount = health / maxHealth;
        if(canPlayAnimation)
        {
            animator.SetTrigger("Damage");
            StartCoroutine(AntiSpamAnimation());
        }
        if (health <= 0)
        {
            EndGameManager.endManager.gameOver = true;
            EndGameManager.endManager.StartResolvesequence();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnimation = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnimation = true;
    }
}
