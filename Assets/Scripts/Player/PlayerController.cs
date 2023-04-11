using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool FacingLeft {get {return facingLeft;} } // allows other classes to call this function to return the private facingLeft variable
    public static PlayerController Instance;

    [SerializeField] private float startingMoveSpeed = 5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTR;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer playerSprite;

    private bool facingLeft = false;
    private bool isDashing = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        moveSpeed = startingMoveSpeed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
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

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
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
        if (!isDashing)    
        {   
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
