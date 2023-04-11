using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float knockbackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockbackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDestroyEnemyRoutine());
    }

    private IEnumerator CheckDestroyEnemyRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DestroyEnemy();

    }

    private void DestroyEnemy()
    {        
        if (currentHealth <= 0) 
            {
                Instantiate(deathVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

}
