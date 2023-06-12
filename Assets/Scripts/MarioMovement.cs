using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f;
    private Quaternion targetRotation;

    // Animator
    private Animator marioAnimator;

    // Jumping
    public float jumpForce = 10.0f;
    public float jumpCooldown = 0.5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayers;
    private float lastJumpTime;
    private Rigidbody rb;
    private bool isOnGround;

    // By me--
    bool isJumped = false;

    void Start()
    {
        marioAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        marioAnimator.SetBool("Idle", true);
    }

    void Update()
    {
        // For jumping
        isOnGround = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayers);

        if (Input.GetButtonDown("Jump") && Time.time > lastJumpTime + jumpCooldown && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
            StartCoroutine(WaitForJump());
        }

        // for running
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;

        if (horizontal < 0)
        {
            targetRotation = Quaternion.Euler(0, 270, 0);
        }
        else if (horizontal > 0)
        {
            targetRotation = Quaternion.Euler(0, 90, 0);
        }



        if(horizontal != 0)
        {
            marioAnimator.SetBool("Run", true);
            marioAnimator.SetBool("Idle", false);
        }
        else if (isOnGround)
        {
            marioAnimator.SetBool("Idle", true);
            marioAnimator.SetBool("Run", false);
            marioAnimator.SetBool("Jump", false);
        }

        if (isJumped && !isOnGround)
        {
            marioAnimator.SetBool("Jump", true);
        }
        else
        {
            marioAnimator.SetBool("Jump", false);
        }
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator WaitForJump()
    {
        isJumped = true;
        yield return new WaitForSeconds(1.85f);
        isJumped = false;
    }
}
