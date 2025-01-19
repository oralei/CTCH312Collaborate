using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Vector3 location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        location = target.transform.position;
        agent.SetDestination(location);
    }

    // Update is called once per frame
    void Update()
    {
        location = target.transform.position;
        agent.SetDestination(location);
    }
}
