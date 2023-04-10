using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown = false;
    private bool isAttacking = false;
    [SerializeField] float attackSpeed = 1f;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private GameObject slashAnim;

    private void Awake()
    {
        playerControls = new PlayerControls(); 
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }
    
    private void OnEnable()
    {
        playerControls.Enable();
    }
    
    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking(); // declares that nothing (_) will be passed through to the StartAttack funtion
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    private void Attack()
    {
        // fire sword animation
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent; // instantiates the slash animation to the parent
            StartCoroutine(AttackCoolDownRoutine());
        }
    }

    private void CompleteAttackAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition; // get the position of the mouse
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position); // get position of player object in relation to camera (e.g. bottom left 0,0)

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // returns nagle in rad whose tan is y/x
        
        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SwingUpAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
