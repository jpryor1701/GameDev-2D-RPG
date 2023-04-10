using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool FacingLeft {get {return facingLeft;} set {facingLeft = value;} } // allows other classes to call this function to return the private facingLeft variable
    public static PlayerController Instance;

    [SerializeField] private float moveSpeed = 5f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer playerSprite;

    private bool facingLeft = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {

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
            FacingLeft = true;
        }
        else
        {
            playerSprite.flipX = false;
            FacingLeft = false;
        }
    }
}
