using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Slider slider;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth();

       
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.forward);
    }

    public void InitializeHealth() 
    {
        currentHealth = maxHealth;

        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void TakeDamage(GameObject attacker,int damage) 
    {
        currentHealth -= damage;
        slider.value = currentHealth;

        if(currentHealth <= 0) 
        {
            if (attacker.GetComponent<Creep>() != null) 
            {

                if (attacker.GetComponent<Creep>().enemiesInRange.Contains(attacker)) 
                {
                    attacker.GetComponent<Creep>().enemiesInRange.Remove(this.gameObject);
                }
                Destroy(this.gameObject);


            }
                
        }
    }
}
