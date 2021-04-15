using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<GameObject> activeTowers;

    CreepManager creepManager;

    private void Start()
    {
        creepManager = GameObject.FindObjectOfType<CreepManager>();
    }

    public void RemoveTower(GameObject tower) 
    {
        for (int i = 0; i < activeTowers.Count; i++)
        {
            if (activeTowers[i] == null)
                activeTowers.Remove(activeTowers[i]);
        }


        activeTowers.Remove(tower);
        foreach (GameObject m in creepManager.activeCreeps)
            m.GetComponent<Creep>().RemoveTower(tower);
    }
}
