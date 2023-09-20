using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] private float _speedPower;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            var horizontalMove = new Vector2(Input.GetAxis("Horizontal"), 0);

            if (horizontalMove.x > 0)
            {//Move Rigth
                
               // rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);
                if (gameObject.transform.localScale.x !=1)
                {
                    gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
                }
            }
            else if(horizontalMove.x < 0)
            {//Move Left
               // rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);
                if (gameObject.transform.localScale.x != -1)
                {
                    gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
                }
            }
            rigidbody2D.transform.Translate(horizontalMove * Time.deltaTime * _speedPower);
        }
        if (Input.GetButtonDown("Jump") && (_isGrounded || _isDoubleJump))
        {
            if (_isGrounded == false)
            {
                _isDoubleJump = false;
            }
            rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isGrounded = true;
        _isDoubleJump = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
}
