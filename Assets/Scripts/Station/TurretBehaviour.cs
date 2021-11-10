using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    float speed = 5;
    Vector3 vectorToTarget;
    public Transform targetTransform;
    public GameObject target;
    public EnemyStats targetStats;
    public DamageCalculations damageCalcs;
    TurretStats turretStats;
    GameObject parent;

    float randValue;
    public  bool hitting;

    // Start is called before the first frame update
    void Start()
    {
        randValue = Random.value;
        damageCalcs = gameObject.GetComponent<DamageCalculations>();
        turretStats = gameObject.GetComponent<TurretStats>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();

        if (targetTransform != null && !hitting)
        {
            hitting = true;
            StartCoroutine(Damage());
        }
        else {
            hitting = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!parent.name.Contains("Frigate"))
        {
            if (other.transform.parent.gameObject.tag == "Enemy")
            {
                targetTransform = other.transform.parent.gameObject.transform;
                target = targetTransform.gameObject;
                targetStats = target.GetComponent<EnemyStats>();
            }
        } else
        {
            if (other.transform.parent.gameObject.tag == "Enemy" && !other.transform.parent.gameObject.name.Contains("brood_enemy_unit"))
            {
                targetTransform = other.transform.parent.gameObject.transform;
                target = targetTransform.gameObject;
                targetStats = target.GetComponent<EnemyStats>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.transform == targetTransform)
        {
            targetTransform = null;
            target = null;
            targetStats = null;
        }
    }

    void Rotation()
    {
        if (targetTransform != null)
        {
            vectorToTarget = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        } else
        {
            if (randValue < 0.5f)
                transform.Rotate(0, 0, 20  * Time.deltaTime);
            else
                transform.Rotate(0, 0, -20 * Time.deltaTime);
        }
    }

    IEnumerator Damage()
    {
        while (hitting)
        {
            yield return new WaitForSeconds(5);
            damageCalcs.CalculateStationTurret(turretStats, targetStats);
        }
    }
}
