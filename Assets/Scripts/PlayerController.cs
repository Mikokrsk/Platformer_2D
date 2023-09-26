using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speedPower;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJump;
    [SerializeField] private float _speedUp;
    [SerializeField] private float _speedNormal;
    [SerializeField] public float _horizontal;
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
    [SerializeField] private Shuriken _shurikenPref;
    //[SerializeField] private Transform _shurikenPos;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        //_shurikenPos = gameObject.transform.Find("ShurikenPos").GetComponentInChildren<Transform>().;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isDashing)
        {
            return;
        }
        _horizontal = Input.GetAxisRaw("Horizontal");
        if(_horizontal >0)
        {
            transform.localScale = new Vector2(1,1);
        }
        else if (_horizontal <0)
        {
            transform.localScale = new Vector2(-1,1);
        }
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
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speedPower = _speedUp;
            // rigidbody2D.mass = 1f;
        }
        else
        {
            _speedPower = _speedNormal;
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
        if(Input.GetButtonDown("Fire"))
        {
            var shuriken = Instantiate(_shurikenPref,transform.position,transform.rotation);
            shuriken._speedPower += this._speedPower;
        }
    }
    void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        _rigidbody2D.velocity = new Vector2(_horizontal * _speedPower, _rigidbody2D.velocity.y);
    }
    IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _trailRenderer.emitting = true;
        var defaultGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.velocity = new Vector2(_horizontal *  _dashPower, 0);
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        _trailRenderer.emitting = false;
        _rigidbody2D.gravityScale = defaultGravity;
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
