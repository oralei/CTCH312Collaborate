using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera mCamera;
    public NavMeshAgent agent;
    public LineLengthController lineLengthController;

    // Dash Variables
    public bool isDashing = false;
    public Vector3 dashDirection;
    public float dashSpeed = 0.1f;
    public float dashDuration = 0.2f;
    public float dashDistance = 0.3f;
    private float dashElapsedTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Disable NavMeshAgent auto-rotation
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateToTarget();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DodgeRoll();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            abilityQ();
        }
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(mCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Ground")
                {
                    agent.SetDestination(hit.point);
                    agent.stoppingDistance = 0;
                }
            }
        }
    }

    void RotateToTarget()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon) // If moving
        {
            // Calculate the target rotation based on the agent's velocity
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);

            // Remove any X and Z tilt from the target rotation
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            // Smoothly interpolate to the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
        }
    }

    void DodgeRoll()
    {
        if (isDashing == false)
        {
            isDashing = true;
            dashElapsedTime = 0f;
            dashElapsedTime += Time.deltaTime;

            if (agent.hasPath == true)
            {
                agent.updateRotation = true;
                agent.velocity = Vector3.zero;
                agent.ResetPath();
                agent.isStopped = true;
            }

            // Note here that this variable is a Vector3 that gets mouseposition and turns it into world position
            dashDirection = lineLengthController.worldPosition;

            Vector3 direction = dashDirection - transform.position;
            direction.y = 0; // Ignore vertical difference

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        Vector3 startPosition = transform.position;
        Vector3 dashEnd = transform.position + transform.forward * dashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float progress = elapsedTime / dashDuration;
            transform.position = Vector3.Lerp(startPosition, dashEnd, progress);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the player reaches the exact end position
        transform.position = dashEnd;

        // End dash
        isDashing = false;
        agent.updateRotation = false;
        agent.isStopped = false;
    }

    void abilityQ()
    {
        if (isDashing == false)
        {
            isDashing = true;
            dashElapsedTime = 0f;
            dashElapsedTime += Time.deltaTime;

            if (agent.hasPath == true)
            {
                agent.updateRotation = true;
                agent.velocity = Vector3.zero;
                agent.ResetPath();
                agent.isStopped = true;
            }

            // Note here that this variable is a Vector3 that gets mouseposition and turns it into world position
            dashDirection = lineLengthController.worldPosition;

            Vector3 direction = dashDirection - transform.position;
            direction.y = 0; // Ignore vertical difference

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // allow movement
            isDashing = false;
            agent.updateRotation = false;
            agent.isStopped = false;
        }
    }
}

