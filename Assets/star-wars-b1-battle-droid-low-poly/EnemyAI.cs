using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Moving,
        Aiming,
        Shooting,
        Knocked,
        Dead,
        Idle
    }

    public enum AIMode
    {
        Training,
        Attack
    }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    public Transform player;
    public float health;
    public EnemyState currentState; // The current state of the enemy

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    public float minDistanceBetweenShots;
    public float maxDistanceBetweenShots;

    [Header("Training Mode Settings")]
    public AIMode mode = AIMode.Attack; // The type of AI the enemy is using.
    public float moveRadius = 10f;
    public float turnSpeed;

    private WaveManager waveManager;

    public AudioSource audioS;


    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        if (mode == AIMode.Attack)
        {
            waveManager = FindAnyObjectByType<WaveManager>();
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Camera.main.transform;
        Move();
    }

    private void Update()
    {
        CheckStateTransitions();
    }

    // Enemy starts by moving to a destination.
    // If enemy reaches destination -> proceed to shoot, log a shot.
        // At the end of the animation, transition to has shot.
    // If enemy has shot, pick a new destination to walk to.
    // If the enemy is knocked down, do not allow it to do anything.
    // Once the enemy has gotten back up set the state to moving and pick a new destination.
    private void CheckStateTransitions()
    {
        if (currentState == EnemyState.Knocked || currentState == EnemyState.Shooting || currentState == EnemyState.Idle)
            return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance || currentState == EnemyState.Aiming)
        {
            currentState = EnemyState.Aiming;
            Attack();
        }
    }

    private void Attack()
    {
        if (mode == AIMode.Training)
        {
            StartCoroutine(RotateToFaceTarget());
        }
        else
        {
            animator.SetTrigger("Shoot");
            currentState = EnemyState.Shooting;
        }
    }

    public void KnockedDown()
    {
        currentState = EnemyState.Knocked;
        agent.ResetPath();
        animator.SetTrigger("Knocked");
    }

    private void Move()
    {
        Vector3 nextDestination;
        if (mode == AIMode.Attack)
        {
            nextDestination = GetRandomPointBetween();
            if (Vector3.Distance(transform.position, nextDestination) < 0.5f)
            {
                StartCoroutine(WaitToShoot());
                return;
            }
        }
        else
        {
            GetRandomPointOnNavMesh(out nextDestination);
        }
        nextDestination.y = 0;
        agent.SetDestination(nextDestination);
        currentState = EnemyState.Moving;
        animator.SetTrigger("Walk");
    }

    private Vector3 GetRandomPointBetween()
    {
        float randomT = Random.Range(minDistanceBetweenShots, maxDistanceBetweenShots);
        return Vector3.Lerp(transform.position, player.position, randomT);
    }

    private void FinishShot()
    {
        currentState = EnemyState.Moving;
        Move();
    }

    private IEnumerator WaitToShoot()
    {
        currentState = EnemyState.Idle;
        yield return new WaitForSeconds(3f);
        if (currentState == EnemyState.Idle)
        {
            currentState = EnemyState.Aiming;
        }
    }

    public void SpawnShot()
    {
        // Calculate direction from spawn position to target
        Vector3 direction = ((player.position - new Vector3(0, 0.5f, 0)) - bulletSpawnPos.position);

        // Create a rotation that looks in that direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Instantiate the object with the calculated rotation
        Instantiate(bulletPrefab, bulletSpawnPos.position, rotation);

        audioS.Play();
    }

    // Training Mode functions.

    private bool GetRandomPointOnNavMesh(out Vector3 result)
    {
        for (int i = 0; i < 30; i++) // Try multiple times to find a valid point
        {
            Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
            randomDirection += transform.position;
            randomDirection.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 1f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = transform.position;
        return false;
    }

    private IEnumerator RotateToFaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep rotation horizontal

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            yield return null; // Wait for next frame
        }
        transform.rotation = targetRotation;
        animator.SetTrigger("Shoot");
        currentState = EnemyState.Shooting;
    }

    private void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.DecreaseEnemyCount();
        }
    }
}
