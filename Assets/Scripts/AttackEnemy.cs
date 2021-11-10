using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    public ClickPick clickPick;
    public List<GameObject> pickedUnits;
    int layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        layer_mask = LayerMask.GetMask("Player", "Enemy");
        clickPick = gameObject.GetComponent<ClickPick>();
        pickedUnits = clickPick.selectedUnits;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
                EnemyPick();
    }

    void EnemyPick()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, layer_mask);
        GameObject enemy;

        if (hit)
        {
            if (hitInfo.transform.gameObject.tag == "Enemy")
            {
                enemy = hitInfo.transform.parent.gameObject;
                var enemyStats = enemy.GetComponent<EnemyStats>();
                var attackedBy = enemyStats.attackedBy;

                foreach (GameObject unit in pickedUnits)
                {
                    if(!attackedBy.Contains(unit))
                        attackedBy.Add(unit);

                    if (unit.name.Contains("Squadron"))
                    {
                        unit.GetComponent<CharacterController>().target = enemy;
                        unit.GetComponent<CharacterController>().targetStats = enemyStats;
                    }
                    if (unit.name.Contains("Frigate"))
                    {
                        if (enemyStats.uName.Contains("Brood Unit"))
                        {
                            unit.GetComponent<CharacterController>().target = enemy;
                            unit.GetComponent<CharacterController>().targetStats = enemyStats;
                        } 
                    }
                }
            }
            else
            {
                foreach (GameObject unit in pickedUnits)
                {
                    unit.GetComponent<CharacterController>().target = null;
                }
            }
        }
        
    }
}
