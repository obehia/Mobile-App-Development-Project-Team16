using UnityEngine;
using UnityEngine.AI;

public class navigation_patrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Animator animator; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 

        agent.autoBraking = false;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        int newDestPoint = 0;
        do
        {
            newDestPoint = Random.Range(0, points.Length);
        } while (destPoint == newDestPoint);

        destPoint = newDestPoint;

        // Trigger walking animation
        animator.Play("Walk"); 
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}
