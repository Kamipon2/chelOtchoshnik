using UnityEngine;
using UnityEngine.AI;
using System.Collections; 

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float detectionRange = 10f; 
    public float fieldOfViewAngle = 110f; 
    public float chaseSpeed = 3.5f; 
    public float stoppingDistance = 1.5f; 
    public float chaseDuration = 6f; 

    private NavMeshAgent agent;
    private bool isChasing = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            DetectPlayer();
        }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
                {
                    if (hit.transform == player)
                    {
                        Debug.Log("Игрок обнаружен!");
                        isChasing = true;
                        StartCoroutine(EndChaseAfterTime(chaseDuration)); 
                    }
                }
            }
        }
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            agent.isStopped = true; 
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private IEnumerator EndChaseAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isChasing = false;
        Debug.Log("Преследование завершено!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * detectionRange;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
