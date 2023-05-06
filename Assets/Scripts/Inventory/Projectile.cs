using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject projectileVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;    
    }
    
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
            this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
            this.moveSpeed = moveSpeed;
    }
    
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        InDestructable inDestructable = other.GetComponent<InDestructable>();
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || inDestructable || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform); // if the player health exisit, then call take damange
                Destroy(gameObject);
                Instantiate(projectileVFX, transform.position, Quaternion.identity);
            } else if (!other.isTrigger && inDestructable)
            {
                Destroy(gameObject);
                Instantiate(projectileVFX, transform.position, Quaternion.identity);
            }
        }
    }
    
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPos) > projectileRange)
        {
            Destroy(gameObject);
        }
    }
}
