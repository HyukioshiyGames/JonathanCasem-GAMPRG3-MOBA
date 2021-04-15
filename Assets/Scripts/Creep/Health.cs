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

    Creep creep;

    TowerManager towerManager;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth();
        creep = GetComponent<Creep>();
        towerManager = GameObject.FindObjectOfType<TowerManager>();
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
            if (this.GetComponent<Creep>()) 
            {
                if (attacker.GetComponent<Creep>())
                    creep.creepManager.RemoveCreepFromList(this.gameObject);
                else if (attacker.GetComponent<Tower>()) 
                { 
                    creep.creepManager.RemoveCreepFromList(this.gameObject);
                    attacker.GetComponent<Tower>().RemoveCreep(this.gameObject);
                }
                
               
            }
            else if (this.GetComponent<Tower>()) 
            {
                towerManager.RemoveTower(this.gameObject);
                
            }
            Destroy(this.gameObject);
        }
    }
}
