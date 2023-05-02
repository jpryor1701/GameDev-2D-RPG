using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    
    private PlayerControls playerControls;
    private float attackCD;

    private bool attackButtonDown = false;
    private bool isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking(); // declares that nothing (_) will be passed through as a lambda into StartAttack funtion
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;
    }

    public void NewWeapon (MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        attackCD = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }
    
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    public void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack(); // call current active weapon, use the iWeapon interface, and call attack
        }
        
    }
}

