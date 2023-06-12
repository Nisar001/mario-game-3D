using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public LayerMask groundLayers;

    private bool isMovingRight = true;
    private bool isOnGround;

    void Update()
    {
        // Check if the Goomba is on the ground
        isOnGround = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayers);

        // Move the Goomba left or right
        float moveX = moveSpeed * (isMovingRight ? 1 : -1) * Time.deltaTime;
        transform.position += new Vector3(moveX, 0, 0);

        // Flip the Goomba's sprite based on its movement direction
        if (isMovingRight)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector3(0.3f, 0.3f, -0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario"))
        {
            if (collision.gameObject.transform.position.y > transform.position.y + 0.5f)
            {
                // Mario jumps on Goomba
                // do something like disabling Goomba
                gameObject.SetActive(false);
            }
            else
            {
                // Mario hit Goomba
                // do something like taking damage or losing a life
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            // Goomba reached an edge, change direction
            isMovingRight = !isMovingRight;
        }
    }
}
