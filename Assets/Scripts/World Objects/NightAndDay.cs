using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightAndDay : MonoBehaviour
{
    public enum TypeOfDay
    {
        Day,
        Night
    }

    #region Declarations

    [Header("Light Color")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Color32 dayColor;
    [SerializeField] private Color32 nightColor;

    [Header("Day Type")]
    [SerializeField] private TypeOfDay dayType;

    [Header("Timers")]
    [SerializeField] private float currentSecond;
    [SerializeField] private int currentMinute;
    [SerializeField] private float secondsPerMinute;
    [SerializeField] private int dayNightShift;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private int spawnCreepInterval;

    [SerializeField] private MasterSpawner masterSpawner;

    bool spedUp;


    #endregion
    #region UnityFunctions
    void Start()
    {
        dayType = TypeOfDay.Day;
        directionalLight.color = dayColor;

        masterSpawner = GameObject.Find("MasterSpawner").GetComponent<MasterSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        AddTime();

        if (Input.GetKeyDown(KeyCode.Space))
            SpeedUpTime();
    }

    #endregion
    #region Functions
    void ChangeDay()
    {
        if (currentMinute % dayNightShift == 0)
        {
            dayType = dayType == TypeOfDay.Day ? TypeOfDay.Night : TypeOfDay.Day;
            ChangeLightColor();
        }
    }
    void ChangeLightColor()
    {
        if (dayType == TypeOfDay.Day)
            directionalLight.color = dayColor;
        else
            directionalLight.color = nightColor;
    }


    public void AddTime()
    {
        if (currentSecond < secondsPerMinute)
        {
            CheckSpawnTime();
            currentSecond += Time.deltaTime;
        }
        else
        {
            currentMinute++;
            currentSecond = 0;

            ChangeDay();
        }
    }

    public void SpeedUpTime()
    {
        spedUp = !spedUp;
        if (spedUp)
            Time.timeScale = speedMultiplier;
        else
            Time.timeScale = 1;
    }

    public void CheckSpawnTime()
    {
        if ((int)currentSecond % spawnCreepInterval == 0 && (int) currentSecond != 0)
        {
            masterSpawner.SpawnCreeps();
        }
    }
    #endregion
}
