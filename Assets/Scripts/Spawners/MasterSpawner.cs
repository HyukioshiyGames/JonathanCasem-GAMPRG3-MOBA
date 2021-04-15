using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    [Header("Type of Waves")]
    [SerializeField] private GameObject[] normalWave;
    [SerializeField] private GameObject[] siegeWave;

    [Header("Barracks")]
    private GameObject barracksParent;
    [SerializeField] private List<GameObject> barracks;
    public int waveCounter;

    // Start is called before the first frame update
    void Start()
    {
        barracksParent = GameObject.Find("Barracks");

        for(int i = 0; i < barracksParent.transform.childCount; i++) 
        {
           if(!barracks.Contains(barracksParent.transform.GetChild(i).gameObject))
                barracks.Add(barracksParent.transform.GetChild(i).gameObject);
        }
    }

    public GameObject[] getCurrentCreepWave()
    {
        GameObject [] currentCreepWave = null;
        currentCreepWave = waveCounter % 10 == 0 && waveCounter != 0 ? siegeWave : normalWave;
        
        return currentCreepWave;
    }

    public void SpawnCreeps() 
    {
        for (int i = 0; i < barracks.Count; i++)
            barracks[i].GetComponent<Barracks>().StartSpawningCreeps();
    }
}
