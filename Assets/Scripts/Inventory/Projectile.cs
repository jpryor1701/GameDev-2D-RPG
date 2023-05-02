using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject projectileVFX;

    private WeaponInfo weaponInfo;
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

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
            this.weaponInfo = weaponInfo;
    }
    
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        InDestructable inDestructable = other.GetComponent<InDestructable>();

        if (!other.isTrigger && (enemyHealth || inDestructable))
        {
            //enemyHealth?.TakeDamage(weaponInfo.weaponDamaage); // if enemy health exists, then call take damage
            Destroy(gameObject);
            Instantiate(projectileVFX, transform.position, Quaternion.identity);
        }
    }
    
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPos) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }
}
