using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipSpawnMovement : MonoBehaviour
{

    public float speed = 5f;
    public GameObject moveTo;
    public Transform target;
    float step;
    public float rotSpeed = 50f;
    UnitStats UnitSt;
    CharacterController CharCont;
    public bool spawned = false;

    void Awake()
    {
      
    }

    void Start()
    {
        target = GameObject.Find("Station").transform;

        if (gameObject.name.Contains("Fighter"))
            moveTo = target.transform.Find("Fighter Squadron-SpawnMoveTo").gameObject;
        if (gameObject.name.Contains("Multirole"))
            moveTo = target.transform.Find("Multirole Squadron-SpawnMoveTo").gameObject;
        if (gameObject.name.Contains("Bomber"))
            moveTo = target.transform.Find("Bomber Squadron-SpawnMoveTo").gameObject;
        if (gameObject.name.Contains("Frigate"))
            moveTo = target.transform.Find("Frigate Unit-SpawnMoveTo").gameObject;

        UnitSt = gameObject.GetComponent<UnitStats>();
        CharCont = gameObject.GetComponent<CharacterController>();

        step = speed * Time.deltaTime;

    }

    void Update()
    {
        if(spawned)
            MoveToPosition();

        if (transform.position == moveTo.transform.position)
        {
            spawned = false;
            UnitSt.enabled = true;
            CharCont.enabled = true;
        }

        if (!spawned)
            OrbitStation();
        
        if (UnitSt.selected == 1)
            enabled = false;

    }


    public void MoveToPosition()
    {
        Vector2 direction = new Vector2(moveTo.transform.position.x - transform.position.x, moveTo.transform.position.y - transform.position.y);
        transform.right = direction;

        transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, step);
    }


    void OrbitStation()
    {
        transform.RotateAround(target.position, Vector3.forward, rotSpeed * Time.deltaTime);

        Vector3 station = target.position;

        Vector2 pointStation = new Vector2(
        station.x - transform.position.x,
        station.y - transform.position.y
        );

        transform.up = pointStation;
    }

}
