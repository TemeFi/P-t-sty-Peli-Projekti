using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject unit;
    GameObject station;
    GameObject tur;

    GameObject sceneController;
    StationStats stationStats;
    float speed;
    EnemyStats myStats;
    ClickPick clickPick;
    UnitStats unitStats;

    int index;
    public bool fighting = false;
    Vector2 direction;
    Vector3 targetVector3;

    float fromTargetDistance = 12;
    float randX;
    float randY;
    public bool hasArrived = false;
    public bool hasRandom = false;
    public Vector3 savedPosition;
    private List<GameObject> allUnits;
    public bool courutineStarted = false;

    public List<GameObject> attackedBy = new List<GameObject>();
    public List<GameObject> turrets = new List<GameObject>();
    private List<TurretStats> turStats = new List<TurretStats>();
    private List<GameObject> closeUnits = new List<GameObject>();

    public DamageCalculations damageCalcs;
    // Start is called before the first frame update
    void Start()
    {
        station = GameObject.FindWithTag("Station");
        sceneController = GameObject.FindWithTag("SceneContr");
        clickPick = sceneController.GetComponent<ClickPick>();
        myStats = gameObject.GetComponent<EnemyStats>();
        stationStats = station.GetComponent<StationStats>();
        allUnits = sceneController.GetComponent<MyUnits>().myUnits;
        damageCalcs = gameObject.GetComponent<DamageCalculations>();

        turrets = stationStats.turrets;

        for (int i = 0; i < turrets.Count; i++)
        {
            turStats.Add(turrets[i].GetComponent<TurretStats>());
        }


        attackedBy = myStats.attackedBy;
        ChooseTarget();
    }

    // Update is called once per frame
    void Update()
    {
        speed = myStats.movementSpeed;
        InFightWith();

        if (!fighting)
        {
            //if (myStats.uName.Contains("Squadron"))
                TheMovementLogic();
            //else
            //    BroodMovLogic();
        }

        if (fighting)
        {
            DogFight();
        }

        if(target == station && hasArrived && courutineStarted || target == tur && hasArrived && courutineStarted || target == unit && fighting && courutineStarted)
        {
            courutineStarted = false;
            StartCoroutine(Damage());
        }
      

        IsEnemyAttacking();

        //if (!broodRotating)
            FaceTarget();

        SelectionIcon();
        StillThere();
    }

    void ChooseTarget()
    {
        courutineStarted = true;

        if (turrets.Count != 0)
        {
            Debug.Log("testenemy");
            tur = turrets[Random.Range(0, turrets.Count)];
        }

        if (tur != null)
            target = tur;
        else
            target = station;

        if (myStats.uName == "Brood Unit")
            target = station;

    }

    void FaceTarget()
    {
        direction = new Vector2(targetVector3.x - transform.position.x, targetVector3.y - transform.position.y);
        transform.right = direction;
    }

    void SelectionIcon()
    {
        if (target == unit)
            myStats.Status(true);
        else
            myStats.Status(false);
    }



    void TheMovementLogic()
    {
        float step = speed * Time.deltaTime;
        if (!fighting)
        {
            if (Vector2.Distance(target.transform.position, transform.position) < fromTargetDistance)
                hasArrived = true;
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetVector3, step);
            }
        }

        if (hasArrived)
        {
            if (!hasRandom)
            {
                hasRandom = true;

                if (target == station)
                {
                    randX = Random.Range(-10, 10);
                    randY = Random.Range(-10, 10);
                }
                else
                {
                    randX = Random.Range(-4, 4);
                    randY = Random.Range(-4, 4);

                }
                targetVector3 = new Vector3(target.transform.position.x + randX, target.transform.position.y + randY, transform.position.z);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetVector3, step);

            if (transform.position == targetVector3)
            {
                hasRandom = false;
            }
        } else
        {
            targetVector3 = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
    }
    IEnumerator Damage()
    {
        courutineStarted = false;


        while (fighting)
        {
            Debug.Log("test2");

            yield return new WaitForSeconds(3);

            switch (myStats.uName)
            {
                case "Drone Squadron":
                    damageCalcs.CalculateDrone(unitStats, myStats);
                    break;

                case "Slugger Squadron":
                    damageCalcs.CalculateSlugger(unitStats, myStats);
                    break;

                case "Soldier Squadron":
                    damageCalcs.CalculateSoldier(unitStats, myStats);
                    break;

                case "Brood Unit":
                    damageCalcs.CalculateBrood(unitStats, myStats);
                    break;
            }
        }


        if (target == station)
            while (target == station && hasArrived)
            {
                yield return new WaitForSeconds(2);
                damageCalcs.CalculateEnemyVsStation(stationStats, myStats);
                Debug.Log("test");

            }

        if (target == tur)
            while (target == tur && hasArrived)
            {
                yield return new WaitForSeconds(2);
                damageCalcs.CalculateEnemyVsTurret(turStats[turrets.IndexOf(tur)], myStats);
            }

      


    }
    void InFightWith()
    {
        if (fighting)
        {
            if (attackedBy.Count != 0)
            {
                int selected = Random.Range(0, attackedBy.Count);

                for (int i = 0; i < attackedBy.Count; i++)
                {

                    if (attackedBy[i].gameObject != null && unit == null)
                    {
                        if (i == selected)
                        {
                            unit = attackedBy[i];
                            unitStats = unit.GetComponent<UnitStats>();
                            target = unit;
                            savedPosition = unit.transform.position;
                            hasArrived = true;
                            hasRandom = false;

                        }
                    }

                    if (attackedBy[i].gameObject != null && unit != null)
                    {
                        if (i == selected)
                        {
                            unit = attackedBy[i];
                            unitStats = unit.GetComponent<UnitStats>();
                            target = unit;
                            savedPosition = unit.transform.position;
                        }
                    }
                }
            }
        }
        else
        {
            if (target != unit && target != null)
            {
                GetCloseUnits();
                UnitsChooseClose();
            }
        }

        if (attackedBy.Count == 0 && unit != null || unit != null)
        {
            target = unit;
        }

        if (attackedBy.Count == 0 && unit == null && !target || !unit && target == unit)
        {
            fighting = false;
            target = null;

            ChooseTarget();

            if (target == station && Vector2.Distance(station.transform.position, transform.position) > fromTargetDistance)
                hasArrived = false;
            if (target == tur && Vector2.Distance(tur.transform.position, transform.position) > fromTargetDistance)
                hasArrived = false;
        }

    }

    void DogFight()
    {
        float step = speed * Time.deltaTime;

        if (!hasRandom)
        {
            savedPosition = unit.transform.position;

            if (!myStats.uName.Contains("Brood"))
            {
                hasRandom = true;
                randX = Random.Range(-4, 4);
                randY = Random.Range(-4, 4);
                targetVector3 = new Vector3(savedPosition.x + randX, savedPosition.y + randY, transform.position.z);
            }
            else
            {
                hasRandom = true;
                randX = Random.Range(-10, 10);
                randY = Random.Range(-10, 10);
                targetVector3 = new Vector3(savedPosition.x + randX, savedPosition.y + randY, transform.position.z);

            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetVector3, step);

        if (transform.position == targetVector3)
        {
            hasRandom = false;
        }

        if (Vector2.Distance(target.transform.position, transform.position) > fromTargetDistance)
        {
            fighting = false;
        }
    }

    void IsEnemyAttacking()
    {
        if (!fighting)
        {
            foreach (GameObject unit in attackedBy)
            {
                if (Vector2.Distance(unit.transform.position, transform.position) <= fromTargetDistance)
                {
                    fighting = true;
                }
                else
                {
                    fighting = false;
                }

                if (myStats.uName.Contains("Brood"))
                {
                    if (Vector2.Distance(unit.transform.position, transform.position) <= fromTargetDistance+25)
                    {
                        fighting = true;
                    }
                    else
                    {
                        fighting = false;
                    }
                }
            }
        }

        if(target == unit && !fighting)
        {
            if (Vector2.Distance(unit.transform.position, transform.position) <= 10f)
            if (Vector2.Distance(unit.transform.position, transform.position) <= 10f)
            {
                fighting = true;
                courutineStarted = true;
            }
            else
            {
                fighting = false;
                courutineStarted = false;

            }
        }
    }


    void GetCloseUnits()
    {
        for (int i = 0; i < allUnits.Count; i++)
        {
            if (!closeUnits.Contains(allUnits[i]) && Vector2.Distance(allUnits[i].transform.position, transform.position) <= fromTargetDistance + 2)
            {
                closeUnits.Add(allUnits[i]);
            }
        }

    }

    void UnitsChooseClose()
    {
        if (allUnits.Count != 0)
        {
            if (closeUnits.Count != 0)
            {
                switch (myStats.uName)
                {
                    case "Drone Squadron":
                        for (int i = 0; i < closeUnits.Count; i++)
                        {
                            if (closeUnits[i].name.Contains("Fighter"))
                            {
                                unit = closeUnits[i];
                                unitStats = unit.GetComponent<UnitStats>();

                                UnitGetTemplate();
                            }

                            if (unit != null && !unit.name.Contains("Fighter") || unit == null)
                            {
                                if (closeUnits[i].name.Contains("Bomber"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                                else
                                if (closeUnits[i].name.Contains("Multirole"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                                else
                                if (closeUnits[i].name.Contains("Frigate"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                            }
                        }
                        break;


                    case "Slugger Squadron":

                        for (int i = 0; i < closeUnits.Count; i++)
                        {
                            if (closeUnits[i].name.Contains("Frigate"))
                            {
                                unit = closeUnits[i];
                                unitStats = unit.GetComponent<UnitStats>();

                                UnitGetTemplate();
                            }

                            if (unit != null && !unit.name.Contains("Frigate") || unit == null)
                            {
                                if (closeUnits[i].name.Contains("Bomber"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                            }
                        }
                        break;

                    case "Soldier Squadron":
                        for (int i = 0; i < closeUnits.Count; i++)
                        {
                            if (closeUnits[i].name.Contains("Multirole"))
                            {
                                unit = closeUnits[i];
                                unitStats = unit.GetComponent<UnitStats>();

                                UnitGetTemplate();
                            }

                            if (unit != null && !unit.name.Contains("Multirole") || unit == null)
                            {
                                if (closeUnits[i].name.Contains("Bomber"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                                else
                                if (closeUnits[i].name.Contains("Fighter"))
                                {
                                    unit = closeUnits[i];
                                    unitStats = unit.GetComponent<UnitStats>();

                                    UnitGetTemplate();
                                }
                            }
                        }
                        break;

                    case "Brood Unit":
                        for (int i = 0; i < closeUnits.Count; i++)
                        {
                            if (closeUnits[i].name.Contains("Frigate"))
                            {
                                unit = closeUnits[i];
                                unitStats = unit.GetComponent<UnitStats>();

                                UnitGetTemplate();
                            }
                        }
                        break;
                }
            }
        }

        //for (int i = 0; i < allUnits.Count; i++)
        //{
        //    if (Vector2.Distance(allUnits[i].transform.position, transform.position) <= fromTargetDistance)
        //    {
        //        Debug.Log("Test2");
        //        unit = allUnits[i];
        //        target = unit;
        //        savedPosition = unit.transform.position;
        //        hasRandom = false;
        //        hasArrived = true;
        //    }
        //}


    }

    void StillThere()
    {
        //if(unit != null)
            //Debug.Log(unit);

    }

    void UnitGetTemplate()
    {
        target = unit;
        savedPosition = unit.transform.position;
        hasRandom = false;
        hasArrived = true;
    }



}
