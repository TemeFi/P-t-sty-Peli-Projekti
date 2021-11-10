using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public bool isMoving = false;
    bool isFasing = true;

    private UnitStats thisUnitStats;
    private List<GameObject> selectedUnits;
    public Dictionary<GameObject, Vector3> targetPoslist;
    List<Transform> childs = new List<Transform>();
    List<Transform> childOfChild = new List<Transform>();
    List<Transform> shootPoints = new List<Transform>();

    Vector3 vectorToTarget;

    //public GameObject leader;

    public Vector3 myVector3;
    public int myIndex;

    public GameObject target;
    public EnemyStats targetStats;
    public GameObject laser;

    bool hasRandom = false;

    public bool isAttacking;

    DamageCalculations damageCalcs;

   // AllUnitMovements unitMovements;
    UnitMovement unitMovements;

    public bool positionArrow = true;
    public bool positionDiamond = false;
    public float fromEnemyDistance = 12;
    Vector2 direction;

    bool isCourutineStarted;

    float randX;
    float randY;
    Vector3 savedPosition;
    bool? selectingTarget;

    void Start()
    {

        unitMovements = new UnitMovement();

        speed = gameObject.GetComponent<UnitStats>().movementSpeed;
        selectedUnits = GameObject.Find("Scene Controller").GetComponent<ClickPick>().selectedUnits;


        GetAllChildren(gameObject.transform, childs);

        for (int i = 0; i < childs.Count; i++)
        {
            GetAllChildren(childs[i].transform, childOfChild);
        }

        for (int i = 0; i < childOfChild.Count; i++)
        {
            GetAllChildren(childOfChild[i].transform, shootPoints);
        }

        //Get the stats of the unit
        thisUnitStats = gameObject.GetComponent<UnitStats>();

        damageCalcs = gameObject.GetComponent<DamageCalculations>();

    }

    void Update()
    {
        if (!isAttacking)
            StopCoroutine(Damage());

        if (target)
        {
            FaceEnemy();
            CheckDistanceFromTarget();

            if (!isAttacking)
            {
                MoveToChaseEnemy();
            }
        }
        else
        {
            isAttacking = false;
            hasRandom = false;
            selectingTarget = null;
        }

        //Annna unitin paikka listasta
        if (thisUnitStats.selected == 1)
            myIndex = selectedUnits.IndexOf(gameObject);


        if (Input.GetMouseButtonDown(1) && thisUnitStats.selected == 1)
        {
            if (target)
            {
                target.GetComponent<EnemyStats>().attackedBy.Remove(gameObject);
                target = null;
            }

            isFasing = false;
            unitMovements.MoveUnit(this); //Liikuttaa unittia
        }

        if (Input.GetMouseButtonDown(1) && thisUnitStats.selected == 0 && !target)
            myIndex = 0;

        if (isMoving)
        {
            Move();
            isFasing = true;
        }

        if (isCourutineStarted)
        {
            StartCoroutine(Damage());
        }

        TurretMove();
    }

    IEnumerator Damage()
    {
        isCourutineStarted = false;

        while (isAttacking)
        {
            yield return new WaitForSeconds(2);

            switch (thisUnitStats.uname)
            {
                case "Fighter Squadron":
                    damageCalcs.CalculateFighter(thisUnitStats, targetStats);
                    Fire();
                    break;
                case "Multirole Squadron":
                    damageCalcs.CalculateMultirole(thisUnitStats, targetStats);
                    Fire();
                    break;
                case "Bomber Squadron":
                    damageCalcs.CalculateBomber(thisUnitStats, targetStats);
                    Fire();
                    break;
                case "Frigate Unit":
                    damageCalcs.CalculateFrigate(thisUnitStats, targetStats);
                    Fire();
                    break;
            }
        }
    }

    void Fire()
    {
        if (thisUnitStats.uname != "Frigate Unit")
        {
            foreach (Transform t in shootPoints)
            {
                if (t.name.Contains("ShootPoint"))
                {
                    Instantiate(laser, t.position, t.rotation);
                }
            }
        }
        else
        {
            foreach (Transform t in shootPoints)
            {
                if (t.name.Contains("ShootPointFrig"))
                {
                    Instantiate(laser, t.position, t.rotation);
                }
            }
        }
    }

    public void Move()
    {
        FaceMouse();

        switch (myIndex)
        {
            case 0:
                TheMovementLogic(0);
                break;
            case 1:
            case 2:
                TheMovementLogic(1);
                break;
            case 3:
            case 4:
                TheMovementLogic(2);
                break;
            case 5:
            case 6:
                TheMovementLogic(3);
                break;
            case 7:
            case 8:
                TheMovementLogic(4);
                break;
            case 9:
            case 10:
                TheMovementLogic(5);
                break;
        }

        if (transform.position == myVector3)
        {
            isMoving = false;
        }
    }

    void TheMovementLogic(float value)
    {
        transform.position = Vector3.MoveTowards(transform.position, myVector3, (speed - value) * Time.deltaTime); //Float value määrittää joukkojen nopeuden kun ne lähtevät liikkumaan muodossa

        if (transform.position.y - myVector3.y < transform.position.y - myVector3.y + 0.5 && transform.position.x - myVector3.x < transform.position.x - myVector3.x + 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, myVector3, speed * Time.deltaTime);
        }
    }

    public void MoveToChaseEnemy()
    {
        if (myVector3 != target.transform.position)
        {
            myVector3 = new Vector3(target.transform.position.x, target.transform.position.y, 0);
            Move();
        }
    }

    void FaceEnemy()
    {
        direction = new Vector2(myVector3.x - transform.position.x, myVector3.y - transform.position.y);
        transform.right = direction;
    }

    void FaceMouse()
    {
        direction = new Vector2(myVector3.x - transform.position.x, myVector3.y - transform.position.y);

        if (!isFasing)
        {
            transform.right = direction;
        }
    }

    void CheckDistanceFromTarget()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= 20)
        {
            if (selectingTarget == null)
                selectingTarget = true;

            isAttacking = true;
            DogFight();
        }
        else
        {
            isAttacking = false;
            hasRandom = false;
            selectingTarget = null;
        }
    }


    void DogFight()
    {
        if (target)
        {
            if (thisUnitStats.uname != "Frigate Unit")
            {
                if (targetStats.uName != "Brood Unit")
                {
                    if (selectingTarget == true)
                    {
                        savedPosition = target.transform.position;
                        selectingTarget = false;
                        isCourutineStarted = true;
                    }
                }
                else
                {
                    savedPosition = target.transform.position;
                }

                float step = speed * Time.deltaTime;

                if (!hasRandom)
                {
                    hasRandom = true;
                    randX = Random.Range(-5, 5);
                    randY = Random.Range(-5, 5);
                    myVector3 = new Vector3(savedPosition.x + randX, savedPosition.y + randY, transform.position.z);
                }

                transform.position = Vector3.MoveTowards(transform.position, myVector3, step);

                if (transform.position == myVector3)
                {
                    hasRandom = false;
                }
            }
            else
            {
                if (selectingTarget == true)
                {
                    savedPosition = target.transform.position;
                    selectingTarget = false;
                }

                float step = speed * Time.deltaTime;

                if (!hasRandom)
                {
                    hasRandom = true;
                    randX = Random.Range(-10, 10);
                    randY = Random.Range(-10, 10);
                    myVector3 = new Vector3(savedPosition.x + randX, savedPosition.y + randY, transform.position.z);
                }

                transform.position = Vector3.MoveTowards(transform.position, myVector3, step);

                if (transform.position == myVector3)
                {
                    hasRandom = false;
                }
            }
        }
    }

    void TurretMove()
    {
        if (isAttacking)
        {
            if (thisUnitStats.uname != "Frigate Unit")
            {
                foreach (Transform t in childOfChild)
                {
                    if (t.name.Contains("Turret"))
                    {
                        vectorToTarget = target.transform.position - t.transform.position;
                        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        t.transform.rotation = Quaternion.Slerp(t.transform.rotation, q, Time.deltaTime * speed);
                    }
                }
            }
            else
            {
                foreach (Transform t in childOfChild)
                {
                    if (t.name.Contains("frigateHeavyTurret"))
                    {
                        //Debug.Log(t);
                        vectorToTarget = target.transform.position - t.transform.position;
                        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        t.transform.rotation = Quaternion.Slerp(t.transform.rotation, q, Time.deltaTime * speed);
                    }
                }
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
}

