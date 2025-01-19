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
    public float dashSpeed;
    public float dashDuration;


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

            if (agent.hasPath == true)
            {
                agent.updateRotation = true;
                agent.velocity = Vector3.zero;
                agent.ResetPath();
                agent.isStopped = true;
            }

            dashDirection = lineLengthController.worldPosition;

            Vector3 direction = dashDirection - transform.position;
            direction.y = 0; // Ignore vertical difference
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            isDashing = false;
            agent.updateRotation = false;
            agent.isStopped = false;
        }
    }
}