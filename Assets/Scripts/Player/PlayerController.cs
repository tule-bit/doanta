using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;


public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    public static PlayerController instance;
    [SerializeField]private float speed = 5.0f;
    [SerializeField]private float dashSpeed = 4.0f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerCOntroll PlayerCOntrolls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float startMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        PlayerCOntrolls = new PlayerCOntroll();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerCOntrolls.Combat.Dash.performed += _ => Dash();
        startMoveSpeed = speed;
    }
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            speed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRountine());
        }
    }

    private IEnumerator EndDashRountine()
    {
        float dashDuration = 0.3f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashDuration);
        speed = startMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    private void OnEnable()
    {
        PlayerCOntrolls.Enable();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void AdjustPlayerFacingDirection()
    {
       Vector3 MousPos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (MousPos.x < playerScreenPoint.x)
        {
            sprite.flipX = true;
            facingLeft = true;
        }
        else
        {
            sprite.flipX = false;
            facingLeft = false;
        }
    }

    private void PlayerInput()
    {
        movement = PlayerCOntrolls.Movement.Move.ReadValue<Vector2>();
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
