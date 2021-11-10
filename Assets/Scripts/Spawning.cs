using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Spawning : MonoBehaviour
{
    public List<GameObject> spawnableUnits;
    public List<GameObject> spawnPoints;
    public Transform spawner;
    public float rotationDirection = -90;
    private ShipSpawnMovement spawnMove;
    private GameObject unit;
    private GameObject spawnTo;
    public GameObject station;
    UnitStats stationStats;
    List<Transform> childs = new List<Transform>();
    Vector3 positionofThis;
    public int money;
    Economy eco;
    

    // Start is called before the first frame update
    void Start()
    {

        stationStats = station.GetComponent<UnitStats>();

        GetAllChildren(GameObject.Find("MainCanvas").transform, childs);

        foreach (Transform t in childs)
        {
            if (t.name == "Money")
            {
                eco = t.gameObject.GetComponent<Economy>();
            }
        }
    }

    public static void GetAllChildren(Transform parent, List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);
        }
    }

    public void SpawnFighter() 
    {
        unit = null;
        spawnTo = null;

        for (int i = 0; i < spawnableUnits.Count; i++)
        {
            if (spawnableUnits[i].name == "Fighter Squadron")
            {
                unit = spawnableUnits[i];
            }

            if (spawnPoints[i].name == "Fighter Squadron-SpawnMoveTo")
            {
                spawnTo = spawnPoints[i];
            }
        }

        unit.GetComponent<CharacterController>().enabled = false;
        unit.GetComponent<UnitStats>().enabled = false;

        Instantiate(unit, spawner.position, Quaternion.Euler(0f, 0f, rotationDirection));
        spawnMove = unit.GetComponent<ShipSpawnMovement>();

        spawnMove.moveTo = spawnTo;
        spawnMove.spawned = true;
        spawnMove.MoveToPosition();

        eco.money = eco.money - 10;
    }

    public void SpawnBomber()
    {
        unit = null;
        spawnTo = null;

        for (int i = 0; i < spawnableUnits.Count; i++)
        {
            if (spawnableUnits[i].name == "Bomber Squadron")
            {
                unit = spawnableUnits[i];
            }
            if (spawnPoints[i].name == "Bomber Squadron-SpawnMoveTo")
            {
                spawnTo = spawnPoints[i];
            }
        }

        unit.GetComponent<CharacterController>().enabled = false;
        unit.GetComponent<UnitStats>().enabled = false;

        Instantiate(unit, spawner.position, Quaternion.Euler(0f, 0f, rotationDirection));
        spawnMove = unit.GetComponent<ShipSpawnMovement>();

        spawnMove.moveTo = spawnTo;
        spawnMove.spawned = true;
        spawnMove.MoveToPosition();

        eco.money = eco.money - 100;
    }

    public void SpawnMultirole()
    {
        unit = null;
        spawnTo = null;

        for (int i = 0; i < spawnableUnits.Count; i++)
        {
            if (spawnableUnits[i].name == "Multirole Squadron")
            {
                unit = spawnableUnits[i];
            }

            if (spawnPoints[i].name == "Multirole Squadron-SpawnMoveTo")
            {
                spawnTo = spawnPoints[i];
            }
        }

        unit.GetComponent<CharacterController>().enabled = false;
        unit.GetComponent<UnitStats>().enabled = false;

        Instantiate(unit, spawner.position, Quaternion.Euler(0f, 0f, rotationDirection));
        spawnMove = unit.GetComponent<ShipSpawnMovement>();

        spawnMove.moveTo = spawnTo;
        spawnMove.spawned = true;

        eco.money = eco.money - 50;
    }

    public void SpawnFrigate()
    {
        unit = null;
        spawnTo = null;

        for (int i = 0; i < spawnableUnits.Count; i++)
        {
            if (spawnableUnits[i].name == "Frigate Unit")
            {
                unit = spawnableUnits[i];
            }
            if (spawnPoints[i].name == "Frigate Unit-SpawnMoveTo")
            {
                spawnTo = spawnPoints[i];
            }
        }

        unit.GetComponent<CharacterController>().enabled = false;
        unit.GetComponent<UnitStats>().enabled = false;

        Instantiate(unit, spawner.position, Quaternion.Euler(0f, 0f, rotationDirection));
        spawnMove = unit.GetComponent<ShipSpawnMovement>();

        spawnMove.moveTo = spawnTo;
        spawnMove.spawned = true;
        spawnMove.MoveToPosition();

        eco.money = eco.money - 200;
    }
}
