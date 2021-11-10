using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuyUnits : MonoBehaviour
{
    public GameObject fighter, bomber, multirole, frigate;
   // Vector3 fighterPos, bomberPos, multiPos, friPos, curPos;

    public Camera cam;
    public bool buttonOpen = false;
    public float speed = 50;

    List<Transform> childs = new List<Transform>();
    public Text moneytxt;
    public int money;
    public int ParsedMoney;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        //moneytxt = GameObject.Find("money").GetComponent<Text>();

        cam = Camera.main;
        /* fighterPos = cam.WorldToScreenPoint(new Vector3(-9.000023f, 165,0));
         multiPos = new Vector3(-9.000023f, 136, 0);
         bomberPos = new Vector3(-9.000023f, 107, 0);
         friPos = new Vector3(-9.000023f, 78, 0);

         curPos = transform.position;*/

        fighter.SetActive(false);
        bomber.SetActive(false);
        multirole.SetActive(false);
        frigate.SetActive(false);

        GetAllChildren(GameObject.Find("MainCanvas").transform, childs);

        


    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in childs)
        {
            if (t.name == "Money")
            {
                money = t.gameObject.GetComponent<Economy>().money;

            }
        }

        foreach (Transform t in childs)
        {
            if (t.name == "SpawnFrigate")
            {
                btn = t.GetComponent<Button>();
                //Debug.Log("SpawnFrigate");
                if (money < 200)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
            }
            else if (t.name == "SpawnBomber")
            {
                btn = t.GetComponent<Button>();
                //Debug.Log("SpawnBomber");
                if (money < 100)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
            }
            else if (t.name == "SpawnMultirole")
            {
                btn = t.GetComponent<Button>();
                //Debug.Log("SpawnMultirole");
                if (money < 50)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
            }
            else if (t.name == "SpawnFighter")
            {
                btn = t.GetComponent<Button>();
                //Debug.Log("SpawnFighter");
                if (money < 10)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
            }
        }



        if (!buttonOpen)
        {
            StopCoroutine(SpawnButtons());

            fighter.SetActive(false);
            bomber.SetActive(false);
            multirole.SetActive(false);
            frigate.SetActive(false);
        }
    }

    public static void GetAllChildren(Transform parent, List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);
        }
    }

    public void OpenAndCloseButton()
    {
        if (buttonOpen)
        {
            buttonOpen = false;
        }
        else
        {
            StartCoroutine(SpawnButtons());
            buttonOpen = true;
        }
    }

    public IEnumerator SpawnButtons()
    {
        Debug.Log("ienumerable");
        yield return new WaitForSeconds(0.2f);
        fighter.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        multirole.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        bomber.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        frigate.SetActive(true);
    }
}
