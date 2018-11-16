using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {



    #region Serialized fields
    [SerializeField]
    private GameObject player;
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
    [SerializeField]
    private float attackDamage = 5f;
    #endregion


    private NavMeshAgent pathFinder;

    private bool isShooting = false;
    private float shootingDelay = 1f;
    private float timeSinceLastHit = 0f;
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
        timeSinceLastHit += Time.deltaTime;
        //Find the distance to the player
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        //Face the drone to the player
        direction = (player.transform.position - this.transform.position);
        direction.Normalize();
    }

    private void FixedUpdate()
    {
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

            currentState = CurrentState.Attacking;
            if(timeSinceLastHit < shootingDelay)
            {
                return;
            }
            else
            {
                timeSinceLastHit = 0f;
            }
            if (canShoot)
            {
                ShootPlayer();
            }
            else
            {
                HitPlayer();
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
        PlayerController.Instance.GetHit(attackDamage);
        isShooting = true;
        //Shoot player ...
    }

    private void HitPlayer()
    {
        PlayerController.Instance.GetHit(attackDamage);
        //Hit player
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.GetHit(attackDamage * 2);
            Destroy(gameObject);
        }
    }

}
