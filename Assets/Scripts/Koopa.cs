using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float patrolRadius = 10.0f;
    public Transform patrolCenter;
    public LayerMask groundLayers;

    private Vector3 patrolDestination;
    private bool isMovingRight = true;
    private bool isOnGround;

    void Start()
    {
        // Set the initial patrol destination
        patrolDestination = GetPatrolDestination();
    }

    void Update()
    {
        // Check if the Koopa is on the ground
        isOnGround = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayers);

        // Move the Koopa towards the patrol destination
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, patrolDestination, step);

        // Check if the Koopa has reached the patrol destination
        if (Vector3.Distance(transform.position, patrolDestination) < 0.1f)
        {
            // Change direction and set a new patrol destination
            isMovingRight = !isMovingRight;
            patrolDestination = GetPatrolDestination();
        }

        // Flip the Koopa's sprite based on its movement direction
        if (isMovingRight)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, -0.1f);
        }
    }

    private Vector3 GetPatrolDestination()
    {
        // Choose a new patrol destination within the patrol radius
        Vector3 destination = patrolCenter.position + new Vector3(patrolRadius * (isMovingRight ? 1 : -1), 0, 0);
        return destination;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario"))
        {
            Destroy(gameObject);
            if (collision.gameObject.transform.position.y > transform.position.y + 0.5f)
            {
                // Mario jumps on koopa
                // do something like disabling koopa or making it a shell
                gameObject.SetActive(false);
                
            }
            else
            {
                // Mario hit koopa
                // do something like taking damage or losing a life
            }
        }
    }
}
