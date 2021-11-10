using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public Text moneytxt;
    public int money;
    public IEnumerator coroutine;
    // Start is called before the first frame update


    void Start()
    {
        coroutine = CreateMoney(1.0f);
        StartCoroutine(coroutine);

        moneytxt = GameObject.Find("money").GetComponent<Text>();

    }
    

    public IEnumerator CreateMoney(float waitTime)
    {
        money = 50;
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            money = money + 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneytxt.text = money.ToString() + " SC";
    }




}
