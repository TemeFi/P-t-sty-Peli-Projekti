using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string uName;
    public float maxHp = 100;
    public float hp;
    public float movementSpeed = 10;
    public float damage = 10;
    public float worth = 10;

    public SpriteRenderer selectionIcon;

    List<Transform> childs = new List<Transform>();

   public List<GameObject> attackedBy = new List<GameObject>();


    public bool? selected;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        Name();

        GetAllChildren(gameObject.transform, childs);

        foreach (Transform t in childs)
        {
            if (t.name == "Selection Icon")
            {
                selectionIcon = t.gameObject.GetComponent<SpriteRenderer>();
                selectionIcon.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Delete();
    }

    public void Status(bool selected)
    {
        if(selected)
        {
            selectionIcon.enabled = true;
        } 
        else
        {
            selectionIcon.enabled = false;
        }

    }

    public static void GetAllChildren(Transform parent, List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);
        }
    }

    void Delete()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Name()
    {
        if (gameObject.name.Contains("drone_enemy_Squadron"))
        {
            uName = "Drone Squadron";
        }

        if (gameObject.name.Contains("slugger_enemy_Squadron"))
        {
            uName = "Slugger Squadron";
        }

        if (gameObject.name.Contains("soldier_enemy_Squadron"))
        {
            uName = "Soldier Squadron";
        }

        if (gameObject.name.Contains("brood_enemy_unit"))
        {
            uName = "Brood Unit";
        }
    }


}
