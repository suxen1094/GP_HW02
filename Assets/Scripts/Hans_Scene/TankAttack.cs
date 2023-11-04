using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public Transform fireballSpawnPoint; 
    public float fireballSpeed = 50f;
    public float fireRate = 2.5f;
    public float attackRange = 50f;  
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip damageSound;

    private GameObject player;
    private Animator animator;
    private AudioSource audioSource;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.FindGameObjectWithTag("Player"); 
        animator = GetComponent<Animator>(); 
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
        StartCoroutine(FireAtPlayer());

    }

    IEnumerator FireAtPlayer()
    {
        while (true) 
        {
            if (player != null) 
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    animator.SetBool("atack", true); 
                    FireBall(); 
                }
                else
                {
                    animator.SetBool("atack", false); 
                }
            }
            yield return new WaitForSeconds(fireRate); 
        }
    }

    void FireBall()
    {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.transform.position - fireballSpawnPoint.position).normalized;
            rb.velocity = direction * fireballSpeed;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(fireballSound);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); 
    }
}
