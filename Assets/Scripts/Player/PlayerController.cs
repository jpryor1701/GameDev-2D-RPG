using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft {get {return facingLeft;} } // allows other classes to call this function to return the private facingLeft variable

    [SerializeField] private float startingMoveSpeed = 5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTR;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer playerSprite;
    private Knockback knockback;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake() // this will overried the singleton class awake method
    {
        base.Awake(); // this will call the awake function in the singleton class, then contiune with the local awake function
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        moveSpeed = startingMoveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Ondisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // get vector2 from the keyboard controls (WASD)
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.IsDead == true) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition; // get the position of the mouse
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); // get position of current game object (player) in relation to camera (e.g. bottom left 0,0)

        if (mousePos.x < playerScreenPoint.x)
        {
            playerSprite.flipX = true;
            facingLeft = true;
        }
        else
        {
            playerSprite.flipX = false;
            facingLeft = false;
        }
    }
    
    private void Dash()
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)    
        {   
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTR.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTR.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
