using UnityEngine;
using UnityEngine.AI;
using Unity.Entities;
using Unity.Mathematics;

public class EnemyMovementTest : MonoBehaviour
{
    [SerializeField] private Entity[] wayPoints;

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

    private void Update()
    {
        
    }

    private void SetDestination()
    {
        if(wayPoints != null)
        {
            Vector3 targetVector = wayPoints[0];
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
