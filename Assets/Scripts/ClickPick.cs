using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ClickPick : MonoBehaviour
{
    private List<GameObject> allUnits;
    public List<GameObject> selectedUnits;
    public List<GameObject> us_list;
    public List<Sprite> hold_sprites;
    public RectTransform selectionSquare;
    
    private GameObject unit;
    public  GameObject enemy;
    int layer_mask;
    public List<UnitStats> uniStats;
    public List<GameObject> unitList;

    public Vector2 startPos;
    public SpriteRenderer spriteRenderer0;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;
    public SpriteRenderer spriteRenderer3;
    public SpriteRenderer spriteRenderer4;
    public SpriteRenderer spriteRenderer5;
    public SpriteRenderer spriteRenderer6;
    public SpriteRenderer spriteRenderer7;
    public SpriteRenderer spriteRenderer8;
    public SpriteRenderer spriteRenderer9;
    public Sprite[] spriteArray;
    
    public Slider us0_hp;
    public Slider us1_hp;
    public Slider us2_hp;
    public Slider us3_hp;
    public Slider us4_hp;
    public Slider us5_hp;
    public Slider us6_hp;
    public Slider us7_hp;
    public Slider us8_hp;
    public Slider us9_hp;

    Camera cam;

    public IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        layer_mask = LayerMask.GetMask("Player");

        coroutine = Updateselection(1.0f);
        StartCoroutine(coroutine);

        cam = Camera.main;

        if (!selectionSquare)
            selectionSquare = GameObject.Find("SelectionSquare").GetComponent<RectTransform>();
        
        //Deactivate the square selection image
        selectionSquare.gameObject.SetActive(false);
        allUnits = gameObject.GetComponent<MyUnits>().myUnits;
      

        //Sprite peli objectit
        spriteRenderer0 = GameObject.Find("us0").GetComponent<SpriteRenderer>();
        spriteRenderer1 = GameObject.Find("us1").GetComponent<SpriteRenderer>();
        spriteRenderer2 = GameObject.Find("us2").GetComponent<SpriteRenderer>();
        spriteRenderer3 = GameObject.Find("us3").GetComponent<SpriteRenderer>();
        spriteRenderer4 = GameObject.Find("us4").GetComponent<SpriteRenderer>();
        spriteRenderer5 = GameObject.Find("us5").GetComponent<SpriteRenderer>();
        spriteRenderer6 = GameObject.Find("us6").GetComponent<SpriteRenderer>();
        spriteRenderer7 = GameObject.Find("us7").GetComponent<SpriteRenderer>();
        spriteRenderer8 = GameObject.Find("us8").GetComponent<SpriteRenderer>();
        spriteRenderer9 = GameObject.Find("us9").GetComponent<SpriteRenderer>();

        us0_hp = GameObject.Find("us0_hp").GetComponent<Slider>();
        us1_hp = GameObject.Find("us1_hp").GetComponent<Slider>();
        us2_hp = GameObject.Find("us2_hp").GetComponent<Slider>();
        us3_hp = GameObject.Find("us3_hp").GetComponent<Slider>();
        us4_hp = GameObject.Find("us4_hp").GetComponent<Slider>();
        us5_hp = GameObject.Find("us5_hp").GetComponent<Slider>();
        us6_hp = GameObject.Find("us6_hp").GetComponent<Slider>();
        us7_hp = GameObject.Find("us7_hp").GetComponent<Slider>();
        us8_hp = GameObject.Find("us8_hp").GetComponent<Slider>();
        us9_hp = GameObject.Find("us9_hp").GetComponent<Slider>();


        us0_hp.gameObject.SetActive(false);
        us1_hp.gameObject.SetActive(false);
        us2_hp.gameObject.SetActive(false);
        us3_hp.gameObject.SetActive(false);
        us4_hp.gameObject.SetActive(false);
        us5_hp.gameObject.SetActive(false);
        us6_hp.gameObject.SetActive(false);
        us7_hp.gameObject.SetActive(false);
        us8_hp.gameObject.SetActive(false);
        us9_hp.gameObject.SetActive(false);

        //Load UI images

        AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/AddressableAssetsData/AssetGroups/ship_Bomber.png");
        spriteHandle.Completed += LoadSpritesWhenReady;

        spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/AddressableAssetsData/AssetGroups/ship_fighter.png");
        spriteHandle.Completed += LoadSpritesWhenReady;

        spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/AddressableAssetsData/AssetGroups/ship_Frigate.png");
        spriteHandle.Completed += LoadSpritesWhenReady;

        spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/AddressableAssetsData/AssetGroups/ship_Multirole.png");
        spriteHandle.Completed += LoadSpritesWhenReady;

        spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/AddressableAssetsData/AssetGroups/empty.png");
        spriteHandle.Completed += LoadSpritesWhenReady;

    }

    public IEnumerator Updateselection(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SetUnitImages();
        }
    }

    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            spriteArray = handleToCheck.Result;
            hold_sprites.Add(spriteArray[0]);
            
        }
        //Debug.Log(spriteArray);
        //Debug.Log(hold_sprites.Count);
    }

    // Update is called once per frame
    void Update()
    {
        // mouse down
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            unit = null;
            selectionOff();
            MousePick();
        }
        // mouse up
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionSquare();
            SetUnitImages();
            
        }
        // mouse held down
        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if(selectedUnits.Count == 0)
        {
            uniStats.Clear();
            unitList.Clear();
        }

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (!unitList.Contains(selectedUnits[i]))
            {
                uniStats.Add(selectedUnits[i].GetComponent<UnitStats>());
                unitList.Add(selectedUnits[i]);
            }
        }
    }

    void ReleaseSelectionSquare() 
    {
        selectionSquare.gameObject.SetActive(false);
        Vector2 min = selectionSquare.anchoredPosition - (selectionSquare.sizeDelta / 2);
        Vector2 max = selectionSquare.anchoredPosition + (selectionSquare.sizeDelta / 2);

        foreach (GameObject unit in allUnits)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y && !selectedUnits.Contains(unit) && selectedUnits.Count < 10)
            {
                selectedUnits.Add(unit);
                unit.GetComponent<UnitStats>().selected = 1;
            }
        }
    }

    void SetUnitImages()
    {
        var emptyUnit = hold_sprites.Where(r => r.name == "empty").FirstOrDefault();

        //Debug.Log("No selection");


        spriteRenderer0.sprite = emptyUnit;
        spriteRenderer1.sprite = emptyUnit;
        spriteRenderer2.sprite = emptyUnit;
        spriteRenderer3.sprite = emptyUnit;
        spriteRenderer4.sprite = emptyUnit;
        spriteRenderer5.sprite = emptyUnit;
        spriteRenderer6.sprite = emptyUnit;
        spriteRenderer7.sprite = emptyUnit;
        spriteRenderer8.sprite = emptyUnit;
        spriteRenderer9.sprite = emptyUnit;

        us0_hp.gameObject.SetActive(false);
        us1_hp.gameObject.SetActive(false);
        us2_hp.gameObject.SetActive(false);
        us3_hp.gameObject.SetActive(false);
        us4_hp.gameObject.SetActive(false);
        us5_hp.gameObject.SetActive(false);
        us6_hp.gameObject.SetActive(false);
        us7_hp.gameObject.SetActive(false);
        us8_hp.gameObject.SetActive(false);
        us9_hp.gameObject.SetActive(false);

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (!unitList.Contains(selectedUnits[i]))
            {
                uniStats.Add(selectedUnits[i].GetComponent<UnitStats>());
                unitList.Add(selectedUnits[i]);


            }
        }

        if (selectedUnits.Count != 0) { 
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            
                if (selectedUnits[i].name.Contains("Fighter Squadron"))
                {

                    switch (i)
                    {
                        case 0:
                            spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer0.sprite != emptyUnit)
                            {
                                us0_hp.maxValue = uniStats[i].maxHp;
                                us0_hp.value = uniStats[i].hp;
                                us0_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 1:
                            spriteRenderer1.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer1.sprite != emptyUnit)
                            {
                                us1_hp.maxValue = uniStats[i].maxHp;
                                us1_hp.value = uniStats[i].hp;
                                us1_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 2:
                            spriteRenderer2.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer2.sprite != emptyUnit) {
                                us2_hp.maxValue = uniStats[i].maxHp;
                                us2_hp.value = uniStats[i].hp;
                                us2_hp.gameObject.SetActive(true);
                            }
                           
                            break;
                        case 3:
                            spriteRenderer3.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer3.sprite != emptyUnit)
                            {
                                us3_hp.maxValue = uniStats[i].maxHp;
                                us3_hp.value = uniStats[i].hp;
                                us3_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 4:
                            spriteRenderer4.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer4.sprite != emptyUnit)
                            {
                                us4_hp.maxValue = uniStats[i].maxHp;
                                us4_hp.value = uniStats[i].hp;
                                us4_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 5:
                            spriteRenderer5.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer5.sprite != emptyUnit)
                            {
                                us5_hp.maxValue = uniStats[i].maxHp;
                                us5_hp.value = uniStats[i].hp;
                                us5_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 6:
                            spriteRenderer6.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer6.sprite != emptyUnit)
                            {
                                us6_hp.maxValue = uniStats[i].maxHp;
                                us6_hp.value = uniStats[i].hp;
                                us6_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 7:
                            spriteRenderer7.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer7.sprite != emptyUnit)
                            {
                                us7_hp.maxValue = uniStats[i].maxHp;
                                us7_hp.value = uniStats[i].hp;
                                us7_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 8:
                            spriteRenderer8.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer8.sprite != emptyUnit)
                            {
                                us8_hp.maxValue = uniStats[i].maxHp;
                                us8_hp.value = uniStats[i].hp;
                                us8_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 9:
                            spriteRenderer9.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();
                            if (spriteRenderer9.sprite != emptyUnit)
                            {
                                us9_hp.maxValue = uniStats[i].maxHp;
                                us9_hp.value = uniStats[i].hp;
                                us9_hp.gameObject.SetActive(true);
                            }
                            
                            break;


                    }
                    //spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_fighter").FirstOrDefault();

                    //Debug.Log("ship_fighter");
                }
                else if (selectedUnits[i].name.Contains("Bomber Squadron"))
                {
                    switch (i)
                    {
                        case 0:
                            spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer0.sprite != emptyUnit)
                            {

                                us0_hp.maxValue = uniStats[i].maxHp;
                                us0_hp.value = uniStats[i].hp;
                                us0_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 1:
                            spriteRenderer1.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer1.sprite != emptyUnit)
                            {
                                us1_hp.maxValue = uniStats[i].maxHp;
                                us1_hp.value = uniStats[i].hp;
                                us1_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 2:
                            spriteRenderer2.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer2.sprite != emptyUnit)
                            {
                                us2_hp.maxValue = uniStats[i].maxHp;
                                us2_hp.value = uniStats[i].hp;
                                us2_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 3:
                            spriteRenderer3.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer3.sprite != emptyUnit)
                            {
                                us3_hp.maxValue = uniStats[i].maxHp;
                                us3_hp.value = uniStats[i].hp;
                                us3_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 4:
                            spriteRenderer4.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer4.sprite != emptyUnit)
                            {
                                us4_hp.maxValue = uniStats[i].maxHp;
                                us4_hp.value = uniStats[i].hp;
                                us4_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 5:
                            spriteRenderer5.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer5.sprite != emptyUnit)
                            {
                                us5_hp.maxValue = uniStats[i].maxHp;
                                us5_hp.value = uniStats[i].hp;
                                us5_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 6:
                            spriteRenderer6.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer6.sprite != emptyUnit)
                            {
                                us6_hp.maxValue = uniStats[i].maxHp;
                                us6_hp.value = uniStats[i].hp;
                                us6_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 7:
                            spriteRenderer7.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer7.sprite != emptyUnit)
                            {
                                us7_hp.maxValue = uniStats[i].maxHp;
                                us7_hp.value = uniStats[i].hp;
                                us7_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 8:
                            spriteRenderer8.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer8.sprite != emptyUnit)
                            {
                                us8_hp.maxValue = uniStats[i].maxHp;
                                us8_hp.value = uniStats[i].hp;
                                us8_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 9:
                            spriteRenderer9.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                            if (spriteRenderer9.sprite != emptyUnit)
                            {
                                us9_hp.maxValue = uniStats[i].maxHp;
                                us9_hp.value = uniStats[i].hp;
                                us9_hp.gameObject.SetActive(true);
                            }
                            
                            break;


                    }
                    //spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Bomber").FirstOrDefault();
                    //Debug.Log("ship_Bomber");
                }
                else if (selectedUnits[i].name.Contains("Multirole Squadron"))
                {
                    switch (i)
                    {
                        case 0:
                            spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer0.sprite != emptyUnit)
                            {
                                us0_hp.maxValue = uniStats[i].maxHp;
                                us0_hp.value = uniStats[i].hp;
                                us0_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 1:
                            spriteRenderer1.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer1.sprite != emptyUnit)
                            {
                                us1_hp.maxValue = uniStats[i].maxHp;
                                us1_hp.value = uniStats[i].hp;
                                us1_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 2:
                            spriteRenderer2.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer2.sprite != emptyUnit)
                            {
                                us2_hp.maxValue = uniStats[i].maxHp;
                                us2_hp.value = uniStats[i].hp;
                                us2_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 3:
                            spriteRenderer3.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer3.sprite != emptyUnit)
                            {
                                us3_hp.maxValue = uniStats[i].maxHp;
                                us3_hp.value = uniStats[i].hp;
                                us3_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 4:
                            spriteRenderer4.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer4.sprite != emptyUnit)
                            {
                                us4_hp.maxValue = uniStats[i].maxHp;
                                us4_hp.value = uniStats[i].hp;
                                us4_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 5:
                            spriteRenderer5.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer5.sprite != emptyUnit)
                            {
                                us5_hp.maxValue = uniStats[i].maxHp;
                                us5_hp.value = uniStats[i].hp;
                                us5_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 6:
                            spriteRenderer6.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer6.sprite != emptyUnit)
                            {
                                us6_hp.maxValue = uniStats[i].maxHp;
                                us6_hp.value = uniStats[i].hp;
                                us6_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 7:
                            spriteRenderer7.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer7.sprite != emptyUnit)
                            {
                                us7_hp.maxValue = uniStats[i].maxHp;
                                us7_hp.value = uniStats[i].hp;
                                us7_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 8:
                            spriteRenderer8.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer8.sprite != emptyUnit)
                            {
                                us8_hp.maxValue = uniStats[i].maxHp;
                                us8_hp.value = uniStats[i].hp;
                                us8_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 9:
                            spriteRenderer9.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                            if (spriteRenderer9.sprite != emptyUnit)
                            {
                                us9_hp.maxValue = uniStats[i].maxHp;
                                us9_hp.value = uniStats[i].hp;
                                us9_hp.gameObject.SetActive(true);
                            }
                           
                            break;


                    }
                    //spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Multirole").FirstOrDefault();
                    //Debug.Log("ship_Multirole");
                }
                else if (selectedUnits[i].name.Contains("Frigate Unit"))
                {
                    switch (i)
                    {
                        case 0:
                            spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer0.sprite != emptyUnit)
                            {
                                us0_hp.maxValue = uniStats[i].maxHp;
                                us0_hp.value = uniStats[i].hp;
                                us0_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 1:
                            spriteRenderer1.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer1.sprite != emptyUnit)
                            {
                                us1_hp.maxValue = uniStats[i].maxHp;
                                us1_hp.value = uniStats[i].hp;
                                us1_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 2:
                            spriteRenderer2.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer2.sprite != emptyUnit)
                            {
                                us2_hp.maxValue = uniStats[i].maxHp;
                                us2_hp.value = uniStats[i].hp;
                                us2_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 3:
                            spriteRenderer3.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer3.sprite != emptyUnit)
                            {
                                us3_hp.maxValue = uniStats[i].maxHp;
                                us3_hp.value = uniStats[i].hp;
                                us3_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 4:
                            spriteRenderer4.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer4.sprite != emptyUnit)
                            {
                                us4_hp.maxValue = uniStats[i].maxHp;
                                us4_hp.value = uniStats[i].hp;
                                us4_hp.gameObject.SetActive(true);

                            }
                            
                            break;
                        case 5:
                            spriteRenderer5.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer5.sprite != emptyUnit)
                            {
                                us5_hp.maxValue = uniStats[i].maxHp;
                                us5_hp.value = uniStats[i].hp;
                                us5_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 6:
                            spriteRenderer6.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer6.sprite != emptyUnit)
                            {
                                us6_hp.maxValue = uniStats[i].maxHp;
                                us6_hp.value = uniStats[i].hp;
                                us6_hp.gameObject.SetActive(true);

                            }
                            
                            break;
                        case 7:
                            spriteRenderer7.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer7.sprite != emptyUnit)
                            {
                                us7_hp.maxValue = uniStats[i].maxHp;
                                us7_hp.value = uniStats[i].hp;
                                us7_hp.gameObject.SetActive(true);
                            }
                            
                            break;
                        case 8:
                            spriteRenderer8.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer8.sprite != emptyUnit)
                            {
                                us8_hp.maxValue = uniStats[i].maxHp;
                                us8_hp.value = uniStats[i].hp;
                                us8_hp.gameObject.SetActive(true);

                            }
                            
                            break;
                        case 9:
                            spriteRenderer9.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                            if (spriteRenderer9.sprite != emptyUnit)
                            {
                                us9_hp.maxValue = uniStats[i].maxHp;
                                us9_hp.value = uniStats[i].hp;
                                us9_hp.gameObject.SetActive(true);
                            }
                            
                            break;


                    }
                    //spriteRenderer0.sprite = hold_sprites.Where(r => r.name == "ship_Frigate").FirstOrDefault();
                    //Debug.Log("ship_Frigate");
                }
            }
     


        }
        else
        {
           

            //Debug.Log("No selection");


            spriteRenderer0.sprite = emptyUnit;
            spriteRenderer1.sprite = emptyUnit;
            spriteRenderer2.sprite = emptyUnit;
            spriteRenderer3.sprite = emptyUnit;
            spriteRenderer4.sprite = emptyUnit;
            spriteRenderer5.sprite = emptyUnit;
            spriteRenderer6.sprite = emptyUnit;
            spriteRenderer7.sprite = emptyUnit;
            spriteRenderer8.sprite = emptyUnit;
            spriteRenderer9.sprite = emptyUnit;

            us0_hp.gameObject.SetActive(false);
            us1_hp.gameObject.SetActive(false);
            us2_hp.gameObject.SetActive(false);
            us3_hp.gameObject.SetActive(false);
            us4_hp.gameObject.SetActive(false);
            us5_hp.gameObject.SetActive(false);
            us6_hp.gameObject.SetActive(false);
            us7_hp.gameObject.SetActive(false);
            us8_hp.gameObject.SetActive(false);
            us9_hp.gameObject.SetActive(false);

        }
        
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionSquare.gameObject.activeInHierarchy)
        {
            selectionSquare.gameObject.SetActive(true);
        }

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionSquare.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionSquare.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }


    void selectionOff()
    {
        foreach (GameObject unit in selectedUnits)
        {
            unit.GetComponent<UnitStats>().selected = 2;
        }
        selectedUnits.Clear();

        if (enemy != null)
        {
            enemy.GetComponent<EnemyStats>().Status(false);
            enemy = null;
        }
    }

    void MousePick()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, layer_mask);

        if (hit)
        {
            if (hitInfo.transform.parent != null)
            {
                unit = hitInfo.transform.parent.gameObject;

                if (unit.tag == "Unit" && !selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit.gameObject);
                    unit.GetComponent<UnitStats>().selected = 1;
                }
            }
        }
    }
}
