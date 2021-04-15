using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    #region Enums
    public enum BarracksLane
    {
        Top,
        Middle,
        Bottom
    }

    public enum BarracksSide
    {
        Radiant,
        Dire
    }
    #endregion

    #region Declarations
    public BarracksLane barrackLane;
    public BarracksSide barracksSide;

    public GameObject[] currentCreepWave;
    private MasterSpawner masterSpawner;
    public List <Transform> laneWaypoints;

    public Transform spawnPositon;
    
    [SerializeField] private float spawnInterval;
    private float counter;

    
    int spawnIndex = 0;

    public bool spawningCreeps;

    public GameObject creepParent;

    CreepManager creepManager;
    #endregion

    private void Awake()
    {
        masterSpawner = GameObject.Find("MasterSpawner").GetComponent<MasterSpawner>();
        creepManager = GameObject.Find("CreepManager").GetComponent<CreepManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeWaypoint();
        creepParent = GameObject.Find("Creeps");
        spawnPositon = this.transform.GetChild(0);
        creepManager = GameObject.Find("CreepManager").GetComponent<CreepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawningCreeps) 
        {
            SpawnCreeps();
        }
    }

    public void StartSpawningCreeps() 
    {
        spawningCreeps = true;
        counter = 0;
        spawnIndex = 0;
    }
    public void SpawnCreeps()
    {
        currentCreepWave = masterSpawner.getCurrentCreepWave();

        if(spawnIndex < currentCreepWave.Length) 
        {
            if (counter >= spawnInterval)
            {
                GameObject creepPrefab = (GameObject)Instantiate(currentCreepWave[spawnIndex], spawnPositon.transform.position, this.transform.rotation);
                creepManager.activeCreeps.Add(creepPrefab);
                Creep creep = creepPrefab.GetComponent<Creep>();
                creep.laneWaypoints = laneWaypoints;
                creep.creepSide = barracksSide == BarracksSide.Radiant ? Creep.CreepSide.Radiant : Creep.CreepSide.Dire;
                creepPrefab.transform.SetParent(creepParent.transform);
                spawnIndex++;
                counter = 0;
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
        else 
        {
            spawningCreeps = false;
        }
    }

    public void InitializeWaypoint() 
    {
        string wayPointString = "";

        wayPointString = barrackLane == BarracksLane.Middle ? "Waypoint Mid" :
            barrackLane == BarracksLane.Top ? "Waypoint Top" : "Waypoint Bottom";

        GameObject wayPointHolder = GameObject.Find(wayPointString);

        for (int i = 0; i < wayPointHolder.transform.childCount; i++)
        {
            laneWaypoints.Add(wayPointHolder.transform.GetChild(i));
        }
    }
}
