using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{
    public GameObject[] patrolPoints;

    public float speed = 2f;
    public float chaseRange = 3f;

    public enum EnemyState { Patrolling, Chasing }
    public EnemyState currentState = EnemyState.Patrolling;
    public GameObject target;
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if(patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned to the enemy.");
        }
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
        }
    }

    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState == EnemyState.Patrolling)
        {
            currentState = EnemyState.Chasing;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.Chasing)
        {
            currentState = EnemyState.Patrolling;
        }
    }
    bool IsPlayerInChaseRange()
    {
        if(player == null)
        {
            return false;
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        target = patrolPoints[currentPatrolPointIndex];
        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = target.transform.position - transform.position;

        direction.Normalize();

        rb.velocity = direction * speed;

        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else if (direction.x > 0)
        {
            sr.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
