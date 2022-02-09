using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomPositionMover : MonoBehaviour
{
    private NavMeshAgent Agent;
    public NavMeshTriangulation Triangulation;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(MoveAgent());
    }

    private IEnumerator MoveAgent()
    {
        while (Agent.enabled)
        {
            Agent.SetDestination(AgentSpawner.ChooseRandomPointOnNavMesh(Triangulation));

            yield return new WaitUntil(() => Agent.enabled && Agent.remainingDistance < Agent.radius);
            yield return new WaitForSeconds(Random.value);
        }
    }
}
