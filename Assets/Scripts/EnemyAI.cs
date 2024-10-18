using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; 
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRadius = 10f;
    public float chaseDuration = 5f;
    public float fieldOfViewAngle = 110f;

    private NavMeshAgent agent;
    private Transform player;
    private int currentPatrolIndex;
    private bool isChasing = false;
    private float chaseTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chaseTimer = chaseDuration;
        currentPatrolIndex = 0;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextPatrolPoint();
            }
            DetectPlayer();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; 
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude < detectionRadius && angleToPlayer < fieldOfViewAngle / 2)
        {
            if (IsPlayerVisible())
            {
                isChasing = true;
                chaseTimer = chaseDuration;
            }
        }
    }

    void ChasePlayer()
    {
        if (chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            
            
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            isChasing = false; 
            GoToNextPatrolPoint(); 
        }
    }

    bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;

        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRadius))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true; 
            }
        }
        return false; 
    }

    void OnDrawGizmos()
    {
        // Рисуем поле зрения врага
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * detectionRadius;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // Рисуем круг радиуса обнаружения
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
