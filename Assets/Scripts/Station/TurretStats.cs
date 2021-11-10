using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public float maxHp = 100;
    public float hp;
    public float damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Delete();

    }

    void Delete()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
