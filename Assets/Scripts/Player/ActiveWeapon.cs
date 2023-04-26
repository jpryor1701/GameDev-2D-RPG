using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    
    private PlayerControls playerControls;
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
    }

    private void Update()
    {
        Attack();
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void NewWeapon (MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
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
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack(); // call current active weapon, use the iWeapon interface, and call attack
        }
        
    }
}

