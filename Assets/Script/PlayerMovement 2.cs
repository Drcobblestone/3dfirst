using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        Idle,
        Walking,
        Running,
        Airborne,
    }
    public float walkingSpeed = 10.0f;
    public float runningSpeed = 20.0f;
    public MovementState currentState = MovementState.Idle;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody rgb;
    [SerializeField] UnityEvent playerJumped;
    //[SerializeField] UnityEvent playerStartedS
    public bool isGrounded = false;

    private void Start()
    {   
        rgb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // Input for Movement
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        GroundCheck();
        UpdateState();
        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerJumped.Invoke();
            rgb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        ApplyMovement();
        
    }
    void GroundCheck()
    {
        //Ground Check
        float rayLength = 2.0f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, LayerMask.GetMask("Default"));
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);

    }
    void UpdateState()
    {
        // State Check
        // variable = bool ? valueIfTrue : valueIfFalse; // Ternary Conditional Operator'
        currentState = Input.GetKey(KeyCode.LeftShift) ? MovementState.Running : MovementState.Walking;
        currentState = moveDir == Vector3.zero ? MovementState.Idle : currentState;
        currentState = !isGrounded ? MovementState.Airborne : currentState;
    }
    void ApplyMovement()
    {
        Vector3 appliedVelocity = moveDir;
        // State Handling
        switch (currentState)
        {
            /*case MovementState.Idle:
                break;*/
            case MovementState.Airborne:
            //does not break, acts the same as walking
            case MovementState.Walking:
                appliedVelocity *= walkingSpeed;
                break;
            case MovementState.Running:
                appliedVelocity *= runningSpeed;
                break;
        }
        //Apply Movement
        appliedVelocity.y = rgb.velocity.y;
        //rgb.AddForce(transform.TransformDirection(appliedVelocity));
        rgb.velocity = transform.TransformDirection(appliedVelocity);
        //Vector3 clampedVelocity = rgb.velocity;
        //clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -10.0f, 10.0f);
        //clampedVelocity.z = Mathf.Clamp(clampedVelocity.z, -10.0f, 20.0f);
        //rgb.velocity = clampedVelocity;
    }
}
