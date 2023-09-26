using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private float _horizontal;
    [SerializeField] private float _startTrailTime;
    [SerializeField] private float _isAlifeTime;
    [SerializeField] public float _speedPower;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private TrailRenderer _trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _horizontal = PlayerController.Instance.transform.localScale.x;
        StartCoroutine(StartOfFlight());       
    }

    // Update is called once per frame
    void Update()
    {
                
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, transform.rotation.eulerAngles.z + _rotateSpeed);
        _rigidbody2D.velocity = new Vector2(_horizontal * _speedPower, _rigidbody2D.velocity.y);
    }

    private IEnumerator StartOfFlight() 
    {
        yield return new WaitForSeconds(_startTrailTime);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_isAlifeTime);
        Destroy(this.gameObject);
    }
}
