using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Moving,
        Shooting,
        HasShot,
        Knocked,
        Dead
    }

    [SerializeField] private NavMeshAgent agent;
    public Transform player;
    public float health;
    public EnemyState currentState; // The current state of the enemy

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                break;

            case EnemyState.Shooting:
                break;

            case EnemyState.Knocked:
                break;

            case EnemyState.Dead:
                break;
        }

        CheckStateTransitions();
    }

    private void Attack()
    {

    }

    // Enemy starts by moving to a destination.
    // If enemy reaches destination -> proceed to shoot, log a shot.
        // At the end of the animation, transition to has shot.
    // If enemy has shot, pick a new destination to walk to.
    // If the enemy is knocked down, do not allow it to do anything.
    // Once the enemy has gotten back up set the state to moving and pick a new destination.
    private void CheckStateTransitions()
    {
        if (currentState == EnemyState.Knocked)
            return;

        /*if (distanceToPlayer < attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else if (distanceToPlayer < detectionRange)
        {
            currentState = EnemyState.Chasing;
        }
        else if (currentState == EnemyState.Chasing)
        {
            currentState = EnemyState.Idle; // Return to idle if player escapes
        }*/
    }

    public void KnockedDown()
    {
        currentState = EnemyState.Knocked;
    }
}
