using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] private float _speedPower;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJump;
    [SerializeField] private float _speedUp;
    [SerializeField] private float _horizontal;
    // Dash
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _dashPower;
    //[SerializeField] private float _dashLength;
    // [SerializeField] private float _dashResetTime;
    [SerializeField] private bool _canDash;
    [SerializeField] private bool _isDashing;
    [SerializeField] private float _dashingCooldown;
    [SerializeField] private float _dashingTime;
    // [SerializeField] private float _dashingTimeNow;
    // [SerializeField] private Vector2 _dashMove;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_isDashing)
        {
            return;
        }
        _horizontal = Input.GetAxisRaw("Horizontal");
        /*if (Input.GetAxis("Horizontal") != 0)
        {
            var horizontalMove = new Vector2(Input.GetAxis("Horizontal"), 0);

            if (horizontalMove.x > 0)
            {//Move Rigth

                // rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);
                if (gameObject.transform.localScale.x != 1)
                {
                    gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
                }
            }
            else if (horizontalMove.x < 0)
            {//Move Left
             // rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);
                if (gameObject.transform.localScale.x != -1)
                {
                    gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
                }
            }
            rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);

            */
        /*if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                rigidbody2D.AddForce(horizontalMove * _strafePower, ForceMode2D.Impulse);
                //transform.Translate(horizontalMove * _strafePower);
                //rigidbody2D.mass = 0.1f;
            }*//*
        }*/
        if (Input.GetButtonDown("Jump") && (_isGrounded || _canDoubleJump))
        {
            if (_isGrounded == false)
            {
                _canDoubleJump = false;
            }
            rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speedPower *= _speedUp;
            // rigidbody2D.mass = 1f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speedPower /= _speedUp;
        }
        if (_canDash && Input.GetKey(KeyCode.LeftControl) && _horizontal != 0)
        {
            /*            var moveX = Input.GetAxis("Horizontal");
                        if (moveX > 0)
                        {
                            moveX = 1;
                        }
                        else
                        {
                            moveX = -1;
                        }
                        _dashMove = new Vector2(moveX, 0);*/
            StartCoroutine(Dash());
        }
    }
    void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        rigidbody2D.velocity = new Vector2(_horizontal * _speedPower, rigidbody2D.velocity.y);
    }
    IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _trailRenderer.emitting = true;
        var defaultGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = new Vector2(_horizontal * transform.localScale.x * _dashPower, 0);
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        _trailRenderer.emitting = false;
        rigidbody2D.gravityScale = defaultGravity;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
        /* _canDash = false;
         rigidbody2D.gravityScale = 0;

         while (_dashingTimeNow >= _dashingTime)
         {            
             _dashingTimeNow += Time.deltaTime;
             rigidbody2D.transform.Translate(_dashMove * _dashSpeed * Time.deltaTime);
         }
         yield return new WaitForSeconds(1f);
         _canDash = true;
         rigidbody2D.gravityScale = 1;*/
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isGrounded = true;
        _canDoubleJump = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
}
