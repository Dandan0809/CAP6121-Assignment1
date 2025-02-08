using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrainingAI : MonoBehaviour
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

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    public Transform player;
    public float health;
    public EnemyState currentState; // The current state of the enemy

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    private void Awake()
    {
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
        animator.SetTrigger("Shoot");
        currentState = EnemyState.Shooting;
    }

    public void KnockedDown()
    {
        currentState = EnemyState.Knocked;
        agent.ResetPath();
        animator.SetTrigger("Knocked");
    }

    private void Move()
    {
        Vector3 nextDestination = GetRandomPointBetween();
        nextDestination.y = 0;
        if (Vector3.Distance(transform.position, nextDestination) < 2f)
        {
            StartCoroutine(WaitToShoot());
            return;
        }
        agent.SetDestination(nextDestination);
        currentState = EnemyState.Moving;
        animator.SetTrigger("Walk");
    }

    private Vector3 GetRandomPointBetween()
    {
        float randomT = Random.Range(0.2f, 0.4f);
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
        Vector3 direction = (player.position - bulletSpawnPos.position);

        // Create a rotation that looks in that direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Instantiate the object with the calculated rotation
        Instantiate(bulletPrefab, bulletSpawnPos.position, rotation);
    }

}
