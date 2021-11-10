using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public string uname;
    public float maxHp = 100;
    public float hp;
    public float movementSpeed = 10;
    public float damage = 5;
    public float price = 10;

    private SpriteRenderer selectionIcon;
    private MyUnits myUnits;

    [Space]
    [Header("selected 0 = none")]
    [Header("selected 1 = selected")]
    [Header("selected 2 = not selected")]
    [Space]
    public int selected = 0;

    List<Transform> childs = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        //When unit is in the scene, include them in a list of all units
        myUnits = GameObject.Find("Scene Controller").GetComponent<MyUnits>();
        myUnits.AddUnit(gameObject);

        hp = maxHp;
        Name();

        GetAllChildren(gameObject.transform,  childs);

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
        Status();
        Delete();
    }

    void Delete()
    {
        if(hp <= 0)
        {
            myUnits.RemoveUnit(gameObject);
            Destroy(gameObject);
        }
    }

    void Status()
    {
        switch (selected)
        {
            case 1:
                selectionIcon.enabled = true;
                break;

            case 2:
                selectionIcon.enabled = false;
                selected = 0;
                break;
        }
    }

    public static void GetAllChildren(Transform parent, List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);
        }
    }

    void Name()
    {
        if(gameObject.name.Contains("Fighter Squadron"))
        {
            uname = "Fighter Squadron";
        }

        if (gameObject.name.Contains("Multirole Squadron"))
        {
            uname = "Multirole Squadron";
        }

        if (gameObject.name.Contains("Bomber Squadron"))
        {
            uname = "Bomber Squadron";
        }

        if (gameObject.name.Contains("Frigate Unit"))
        {
            uname = "Frigate Unit";
        }

    }

}
