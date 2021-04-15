using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    public List<GameObject> activeCreeps;

    TowerManager towerManager;

    private void Start()
    {
        towerManager = GameObject.FindObjectOfType<TowerManager>();
    }
    public void RemoveCreepFromList(GameObject creep) 
    {
        for (int i = 0; i < activeCreeps.Count; i++)
        {
            if (activeCreeps[i] == null)
                activeCreeps.Remove(activeCreeps[i]);
        }

        activeCreeps.Remove(creep);

        for (int i = 0; i < activeCreeps.Count; i++)
        {
            activeCreeps[i].GetComponent<Creep>().RemoveCreep(activeCreeps[i]);
        }

        for (int i = 0; i < towerManager.activeTowers.Count; i++)
        {
            towerManager.activeTowers[i].GetComponent<Tower>().RemoveCreep(towerManager.activeTowers[i]);
        }
    }
}
