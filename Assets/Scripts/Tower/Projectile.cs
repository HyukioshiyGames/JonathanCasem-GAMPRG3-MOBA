using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;

    public int damage;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       

        if (target == null)
            Destroy(this.gameObject);
        else 
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            if (this.transform.position == target.transform.position)
            {
                target.GetComponent<Health>().TakeDamage(this.gameObject, damage);
                Destroy(this.gameObject);
            }
        }
            
    }
}
