using UnityEngine;
using UnityEngine.AI;

public class navigation_patrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Get the Animator component

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
        animator.Play("Walk"); // Replace "Walk" with your actual animation name in the Animator Controller
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Stop walking animation when the agent reaches the destination point
            animator.Play("Idle"); // Replace "Idle" with your idle or standing animation
            GotoNextPoint();
        }
    }
}
