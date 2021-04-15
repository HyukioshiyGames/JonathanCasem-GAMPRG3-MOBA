using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Tower : MonoBehaviour
{
    public enum TowerSide 
    {
        Radiant,
        Dire
    }

    public enum States 
    {
        Attack,
        Idle
    }
    public TowerSide towerSide;
    public States towerState;

    public List<GameObject> enemiesInRange;

    public float attackSpeed;
    public float current;

    public GameObject currentTarget;

    public GameObject projectilePrefab;

    public int damage;

    CreepManager creepManager;
    // Start is called before the first frame update
    void Start()
    {
        creepManager = GameObject.FindObjectOfType<CreepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (towerState)
        {
            case States.Attack:
                Attack();
                break;
            case States.Idle:
                Idle();
                break;
            default:
                break;
        }
    }

    public void Attack() 
    {
        if (current < attackSpeed)
            current += Time.deltaTime;
        else 
        {
            AttackTarget();
           
        }
           
    }

    void AttackTarget() 
    {
        
        if (enemiesInRange.Count > 0)
        {
            if (currentTarget == null)
            {
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] == null)
                        enemiesInRange.Remove(enemiesInRange[i]);
                }

                if(enemiesInRange.Count > 0)
                {

                    GameObject target;

                    target = enemiesInRange.OrderBy(t => Vector3.Distance(this.transform.position, t.transform.position)).FirstOrDefault();
                    if (target != null)
                    {
                        if (creepManager.activeCreeps.Contains(target))
                            currentTarget = target;
                    }
                }
            }

            GameObject projectileInstance = (GameObject)Instantiate(projectilePrefab, this.transform.position + Vector3.up * 2f, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().target = currentTarget;
            projectileInstance.GetComponent<Projectile>().damage = damage;
            current = 0;

        }

        else 
        {
            towerState = States.Idle;
        }

    }

    public void RemoveCreep(GameObject creep)
    {

        

        if (enemiesInRange.Contains(creep))
        {
            enemiesInRange.Remove(creep);

        }

        if (currentTarget == creep)
            currentTarget = null;


    }
    public void Idle() 
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Creep"))
        {
            if (other.gameObject.GetComponent<Creep>().creepSide.ToString() != towerSide.ToString())
            {
                
                if (!enemiesInRange.Contains(other.gameObject))
                    enemiesInRange.Add(other.gameObject);
                towerState = States.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creep"))
        {
            if (other.gameObject.GetComponent<Creep>().creepSide.ToString() != towerSide.ToString())
            {

                if (enemiesInRange.Contains(other.gameObject))
                    enemiesInRange.Remove(other.gameObject);
                if(enemiesInRange.Count == 0)
                    towerState = States.Idle;
            }
        }
    }
}
