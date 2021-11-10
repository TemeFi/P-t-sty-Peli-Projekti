using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnits : MonoBehaviour
{
    public List<GameObject> myUnits;

    public void AddUnit(GameObject unit)
    {
        if (!myUnits.Contains(unit))
            myUnits.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        if (myUnits.Contains(unit))
            myUnits.Remove(unit);
    }
}
