    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Creep : MonoBehaviour
{
    #region Enum Declarations
    public enum CreepType 
    {
        Melee,
        Ranged
    }

    public enum CreepLevel 
    { 
        Normal,
        Mega
    }

    public enum CreepState 
    { 
        Walk,
        Chase,
        Attack,
        Idle
    }

    public enum CreepSide 
    { 
        Radiant,
        Dire
    }
    #endregion

    #region Declarations
    [Header("Descriptions")]
    public CreepType creepType;
    public CreepLevel creepLevel;
    public CreepSide creepSide;
    public CreepState creepState;

    [Header("Waypoint")]
    public List <Transform> laneWaypoints;
    [HideInInspector]public List<Transform> tempWaypoint;
    public Transform targetWaypoint;
    public int targetWaypointIndex;
    public Transform currentTargetWaypoint;

    [Header("Attack Radius/Range")]
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackRange;

    [Header("Stats")]
    public float movementSpeed;
    public float stoppingDistance;
    public int bounty;
    public int damage;
    

    [Header("Components")]
    public Rigidbody rb;
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    [Header("Enemies")]
    public List <GameObject> enemiesInRange;
    public GameObject currentTarget;

    

    
    #endregion

    #region Builtin Functions
    private void Start()
    {
        InitializeWaypoints();
        this.GetComponent<SphereCollider>().radius = attackRadius;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        MoveToTargetNavMesh(targetWaypoint);
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.speed = movementSpeed;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(creepState == CreepState.Walk) 
        {
            Walk();
        }
        if (creepState == CreepState.Attack)
        {
            Attack();
        }

        if(creepState == CreepState.Idle) 
        {
            Idle();
        }
    }
    #endregion

    #region Creep Actions
    public void Walk() 
    {
        if(navMeshAgent.isStopped)
            navMeshAgent.isStopped = false;

        if (DistanceFromTarget(targetWaypoint) <= 1) 
        {
            targetWaypointIndex++;
            if (targetWaypointIndex >= laneWaypoints.Count)
                targetWaypoint = null;
            else 
            {
                targetWaypoint = laneWaypoints[targetWaypointIndex];
                currentTargetWaypoint = targetWaypoint;
            }
                
            if (targetWaypoint == null) 
            {
                animator.Play("Idle");
                creepState = CreepState.Idle;
                navMeshAgent.isStopped = true;
                
            }
            else
                MoveToTargetNavMesh(targetWaypoint);

        }
     
    }

    public void Attack() 
    {
        if (enemiesInRange.Count > 0)
        {
            if (currentTarget == null)
            {
                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null) 
                    {
                        currentTarget = enemiesInRange[i];
                    }
                        
                    else
                        enemiesInRange.Remove(enemiesInRange[i]);
                }
            }
            else 
            {
                if (DistanceFromTarget(currentTarget.transform) <= attackRange)
                {
                    navMeshAgent.isStopped = true;
                    rb.velocity = Vector3.zero;
                    animator.Play("Attack");
                    this.transform.LookAt(currentTarget.transform);

                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    print("Stopped");
                }
                else
                {
                    animator.Play("Run");
                    navMeshAgent.isStopped = false;
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
        }
        else 
        {
            currentTarget = null;

            animator.Play("Run");
            MoveToTargetNavMesh(targetWaypoint);
            currentTargetWaypoint = targetWaypoint;
            creepState = CreepState.Walk;
        }
    }

    public void Idle() 
    {
        print("Idling");
    }

    public void Chase() 
    {

    }

    public void DealDamage() 
    { 
        if(currentTarget != null) 
        {
            currentTarget.GetComponent<Health>().TakeDamage(this.gameObject,damage);
        }
    }

    public void MoveToTargetNavMesh(Transform target)
    {
        navMeshAgent.SetDestination(target.position);

    }
    #endregion

    

    #region Triggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Creep")) 
        {
            if (other.gameObject.GetComponent<Creep>().creepSide != creepSide)
            {
                creepState = CreepState.Attack;
                if(!enemiesInRange.Contains(other.gameObject))
                    enemiesInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creep")) 
        {
            if (other.gameObject.GetComponent<Creep>().creepSide != creepSide)
            {
                if (enemiesInRange.Contains(other.gameObject))
                    enemiesInRange.Remove(other.gameObject);
            }
        }
    }
#endregion
    

    public float DistanceFromTarget(Transform target) 
    {
        return Vector3.Distance(this.transform.position, target.position);
    }
    public void InitializeWaypoints()
    {
        if (creepSide == CreepSide.Dire)
        {
            for (int i = laneWaypoints.Count - 1; i >= 0; i--)
            {
                tempWaypoint.Add(laneWaypoints[i]);
            }
            laneWaypoints = tempWaypoint;
        }

        targetWaypointIndex = 0;
        targetWaypoint = laneWaypoints[targetWaypointIndex];
    }


}
