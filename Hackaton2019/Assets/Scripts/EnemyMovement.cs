using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private GameObject player;


    private NavMeshAgent pathFinder;
    [SerializeField]
    private Transform target;
    

    [SerializeField]
    private float moveSpeed = 250;
    [SerializeField]
    private float minDistance = 50.0f;
    [SerializeField]
    private float maxDistance = 100.0f;
    [SerializeField]
    private float rotationDrag = 0.75f;
    [SerializeField]
    private bool canShoot = true;
    [SerializeField]
    private float brakeForce = 5f;

    private bool isShooting = false;
    private Vector3 direction;
    private float distance = 0.0f;

    public enum CurrentState { Idle, Following, Attacking };
    public CurrentState currentState;
    public bool debugGizmo = true;

    public float DistanceToPlayer { get { return distance; } }
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }

    private Rigidbody rb;

    void Start()
    {
        currentState = CurrentState.Idle;
        isShooting = false;
        rb = GetComponent<Rigidbody>();
        pathFinder = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Find the distance to the player
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        //Face the drone to the player
        direction = (player.transform.position - this.transform.position);
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        Debug.Log(distance);
        rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.angularDrag = rotationDrag;

        //If the player is in range move towards
        if (distance > minDistance && distance < maxDistance)
        {
            currentState = CurrentState.Following;
            EnemyMovesToPlayer();
        }

        //If the player is close enough shoot
        else if (distance < minDistance)
        {

            EnemyStopsMoving();

            if (canShoot)
            {
                currentState = CurrentState.Attacking;
                ShootPlayer();
            }
        }

        //If the player is out of range stop moving
        else
        {
            currentState = CurrentState.Idle;
            EnemyStopsMoving();
        }
    }

    private void EnemyStopsMoving()
    {
        isShooting = false;
        //rb.drag = (brakeForce);
        pathFinder.isStopped = true;
    }

    private void EnemyMovesToPlayer()
    {
        pathFinder.isStopped = false;
        isShooting = false;
        pathFinder.SetDestination(target.position);
    }

    private void ShootPlayer()
    {
        isShooting = true;
        //Shoot player ...
    }

    private void OnDrawGizmosSelected()
    {
        if (debugGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, maxDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, minDistance);
        }
    }
}
