using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    //Tracking AI
   public NavMeshAgent agent;
   public Transform playerPos;
   public LayerMask Terrain, player;
   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;
   public float sightRange;
   public bool playerInSightRange;
   
    //FOV

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    private void Awake()
    {
        playerPos = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (!playerInSightRange) Patroling();
        if (playerInSightRange) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Terrain))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        
        agent.SetDestination(playerPos.position);

    }
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, player);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, Terrain))
                    playerInSightRange = true;
                else 
                    playerInSightRange = false;
            }
            else if (playerInSightRange)
                playerInSightRange = false;
        }
    }
}
