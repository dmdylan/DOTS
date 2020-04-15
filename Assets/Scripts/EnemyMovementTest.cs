using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTest : MonoBehaviour
{
    [SerializeField] private Transform destination;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent.Equals(null))
        {
            Debug.Log("no navmesh");
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
