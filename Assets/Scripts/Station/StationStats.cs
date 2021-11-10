using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationStats : MonoBehaviour
{
    public float maxHp = 100;
    public float hp;
    public List<GameObject> turrets = new List<GameObject>();
    List<Transform> childs = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        GetAllChildren(gameObject.transform, childs);

        foreach (Transform t in childs)
        {
            if (t.name.Contains("stationTurret"))
            {
                if(!turrets.Contains(t.gameObject))
                    turrets.Add(t.gameObject);
            }
        }
        hp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
    }


    public static void GetAllChildren(Transform parent, List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);
        }
    }
}
