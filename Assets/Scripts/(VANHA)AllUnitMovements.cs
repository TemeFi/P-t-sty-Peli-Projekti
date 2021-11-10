using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitMovements : MonoBehaviour
{

    private List<GameObject> selectedUnits;
    private float distance;
    private Vector3 targetPos;
    public Dictionary<GameObject, Vector3> targetPoslist;
    public List<Vector3> preCalcPositonArrow = new List<Vector3>(40);
    public List<Vector3> preCalcPositonDiamond = new List<Vector3>(40);

    [Header("Arrow position settings")]
    public float awayFromTip1 = 4f;
    public float awayFromTip2 = 6f; 
    public float awayFromTip3 = 8f;
    public float awayFromFriend1 = 4f, awayFromFriend2= 6f, awayFromFriend3 = 8f;


    public bool positionArrow, positionDiamond = false;


    public  float xP, xM, yP, yM;


    // Start is called before the first frame update
    void Start()
    {
        //Precompute the positions
        PreCalcPositionArrow();
        PreCalcPositionDiamond();

        selectedUnits = gameObject.GetComponent<ClickPick>().selectedUnits;

        xP = gameObject.GetComponent<CameraController>().xPlusBoundary;
        xM = gameObject.GetComponent<CameraController>().xMinusBoundary;
        yP = gameObject.GetComponent<CameraController>().yPlusBoundary;
        yM = gameObject.GetComponent<CameraController>().yMinusBoundary;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1) && selectedUnits.Count > 0)
        {
            SetTargetPosition();
        }
    }
    public void SetTargetPosition()
    {

        foreach (GameObject unit in selectedUnits)
        {

            Vector3 toObjectVector = unit.transform.position - Camera.main.transform.position;
            Vector3 linearDistanceVector = Vector3.Project(toObjectVector, Camera.main.transform.forward);
            distance = (linearDistanceVector).magnitude;
            Vector3 mousPosition = Input.mousePosition;

            mousPosition.z = distance;

            targetPos = Camera.main.ScreenToWorldPoint(mousPosition);

            Vector2 from = targetPos - selectedUnits[0].transform.position;
            Vector2 to = Vector2.up;
            float angle = Vector2.SignedAngle(from, to);
            Debug.Log(selectedUnits.IndexOf(unit) + " " + angle);



            if (angle < 45 && angle >= 0 || angle > -45 && angle <= -0 || angle > 135 && angle <= 180 || angle < -135 && angle >= -180)
            {

                if (angle < 90 && angle >= 0 || angle > -90 && angle <= -0) //up +. Position y+ AKA up = 0-9 on array of preCalcPosition
                {
                    Debug.Log("UP");

                    if (positionArrow)
                        targetPos += preCalcPositonArrow[selectedUnits.IndexOf(unit)];
                    if (positionDiamond)
                        targetPos += preCalcPositonDiamond[selectedUnits.IndexOf(unit)];


                }
                else if (angle > 90 && angle <= 180 || angle < -90 && angle >= -180) //down -. Position y- AKA down = 10-19 on array of preCalcPosition
                {
                    Debug.Log("DOWN");

                    if (positionArrow)
                        targetPos += preCalcPositonArrow[int.Parse("1" + selectedUnits.IndexOf(unit).ToString())];
                    if (positionDiamond)
                        targetPos += preCalcPositonDiamond[int.Parse("1" + selectedUnits.IndexOf(unit).ToString())];
                }
            }
            else if (angle > 45 && angle < 135 || angle < -45 && angle > -135 || angle == 90 || angle == -90)
            {
                if (angle > 0 && angle < 180 || angle == 90) //right +.  Position x+  AKA right = 20-29 on array of preCalcPosition
                {
                    Debug.Log("RIGHT");

                    if (positionArrow)
                        targetPos += preCalcPositonArrow[int.Parse("2" + selectedUnits.IndexOf(unit).ToString())];
                    if (positionDiamond)
                        targetPos += preCalcPositonDiamond[int.Parse("2" + selectedUnits.IndexOf(unit).ToString())];
                }
                else if (angle < -0 && angle > -180 || angle == -90)//left -.  Position x-  AKA left = 30-39 on array of preCalcPosition
                {
                    Debug.Log("LEFT");

                    if (positionArrow)
                        targetPos += preCalcPositonArrow[int.Parse("3" + selectedUnits.IndexOf(unit).ToString())];
                    if (positionDiamond)
                        targetPos += preCalcPositonDiamond[int.Parse("3" + selectedUnits.IndexOf(unit).ToString())];
                }
            }

            //Make sure that the units do not go over the camera boundaries on the x Axis
            if (targetPos.x > xP) { targetPos.x = xP ; }
            if (targetPos.x < xM ) { targetPos.x = xM ; }

            //Make sure that the units do not go over the camera boundaries on the y Axis
            if (targetPos.y > yP ) { targetPos.y = yP ; }
            if (targetPos.y < yM ) { targetPos.y = yM ; }

            unit.GetComponent<CharacterController>().myVector3 = targetPos;
            unit.GetComponent<CharacterController>().myIndex = selectedUnits.IndexOf(unit);
            unit.GetComponent<CharacterController>().isMoving = true;
        }
    }

 

    void PreCalcPositionDiamond()
    {
        Vector3 tempPos = new Vector3(0, 0, 0);

        for (int i = 0; i < 40; i++)
        {
            preCalcPositonDiamond.Add(tempPos);
        }


        for (int i = 0; i < 10; i++)
        {
            //up on y axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3((float)-5, (float)-5, 0);
                    break;
                case 2:
                    tempPos += new Vector3((float)5, (float)-5, 0);
                    break;
                case 3:
                    tempPos += new Vector3(0, (float)-10, 0);
                    break;
                case 4:
                    tempPos += new Vector3((float)-5, (float)-15, 0);
                    break;
                case 5:
                    tempPos += new Vector3((float)5, (float)-15, 0);
                    break;
                case 6:
                    tempPos += new Vector3(0, (float)-20, 0);
                    break;
                case 7:
                    tempPos += new Vector3((float)-5, (float)-25, 0);
                    break;
                case 8:
                    tempPos += new Vector3((float)5, (float)-25, 0);
                    break;
                case 9:
                    tempPos += new Vector3(0, (float)-30, 0);
                    break;
            }
            preCalcPositonDiamond[i] = tempPos;
            tempPos = new Vector3(0, 0, 0);


            //down on y axis -
            switch (i)
            {
                case 1:
                    tempPos += new Vector3((float)-5, (float)5, 0);
                    break;
                case 2:
                    tempPos += new Vector3((float)5, (float)5, 0);
                    break;
                case 3:
                    tempPos += new Vector3(0, (float)10, 0);
                    break;
                case 4:
                    tempPos += new Vector3((float)-5, (float)15, 0);
                    break;
                case 5:
                    tempPos += new Vector3((float)5, (float)15, 0);
                    break;
                case 6:
                    tempPos += new Vector3(0, (float)20, 0);
                    break;
                case 7:
                    tempPos += new Vector3((float)-5, (float)25, 0);
                    break;
                case 8:
                    tempPos += new Vector3((float)5, (float)25, 0);
                    break;
                case 9:
                    tempPos += new Vector3(0, (float)30, 0);
                    break;
            }
            preCalcPositonDiamond[int.Parse("1" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);

            //right on x axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3((float)-5, (float)-5, 0);
                    break;
                case 2:
                    tempPos += new Vector3((float)-5, (float)5, 0);
                    break;
                case 3:
                    tempPos += new Vector3((float)-10, 0, 0);
                    break;
                case 4:
                    tempPos += new Vector3((float)-15, (float)-5, 0);
                    break;
                case 5:
                    tempPos += new Vector3((float)-15, (float)5, 0);
                    break;
                case 6:
                    tempPos += new Vector3((float)-20, 0, 0);
                    break;
                case 7:
                    tempPos += new Vector3((float)-25, (float)-5, 0);
                    break;
                case 8:
                    tempPos += new Vector3((float)-25, (float)5, 0);
                    break;
                case 9:
                    tempPos += new Vector3((float)-30, 0, 0);
                    break;
            }
            preCalcPositonDiamond[int.Parse("2" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);

            //right on x axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3((float)5, (float)-5, 0);
                    break;
                case 2:
                    tempPos += new Vector3((float)5, (float)5, 0);
                    break;
                case 3:
                    tempPos += new Vector3((float)10, 0, 0);
                    break;
                case 4:
                    tempPos += new Vector3((float)15, (float)-5, 0);
                    break;
                case 5:
                    tempPos += new Vector3((float)15, (float)5, 0);
                    break;
                case 6:
                    tempPos += new Vector3((float)20, 0, 0);
                    break;
                case 7:
                    tempPos += new Vector3((float)25, (float)-5, 0);
                    break;
                case 8:
                    tempPos += new Vector3((float)25, (float)5, 0);
                    break;
                case 9:
                    tempPos += new Vector3((float)30, 0, 0);
                    break;
            }
            preCalcPositonDiamond[int.Parse("3" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);
        }
    }


    void PreCalcPositionArrow()
    {
        Vector3 tempPos = new Vector3(0, 0, 0);

        for (int i = 0; i < 40; i++)
        {
            preCalcPositonArrow.Add(tempPos);
        }

        for (int i = 0; i < 10; i++)
        {
            //up on y axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3(-awayFromFriend1, -awayFromTip1, 0);
                    break;
                case 2:
                    tempPos += new Vector3(awayFromFriend1, -awayFromTip1, 0);
                    break;
                case 3:
                    tempPos += new Vector3(0, -awayFromTip1, 0);
                    break;
                case 4:
                    tempPos += new Vector3(-awayFromFriend2, -awayFromTip2, 0);
                    break;
                case 5:
                    tempPos += new Vector3(awayFromFriend2, -awayFromTip2, 0);
                    break;
                case 6:
                    tempPos += new Vector3(0, -awayFromTip2, 0);
                    break;
                case 7:
                    tempPos += new Vector3(-awayFromFriend3, -awayFromTip3, 0);
                    break;
                case 8:
                    tempPos += new Vector3(awayFromFriend3, -awayFromTip3, 0);
                    break;
                case 9:
                    tempPos += new Vector3(0, -awayFromTip3, 0);
                    break;
            }
            preCalcPositonArrow[i] = tempPos;
            tempPos = new Vector3(0, 0, 0);

            //down on y axis -
            switch (i)
            {
                case 1:
                    tempPos += new Vector3(-awayFromFriend1, awayFromTip1, 0);
                    break;
                case 2:
                    tempPos += new Vector3(awayFromFriend1, awayFromTip1, 0);
                    break;
                case 3:
                    tempPos += new Vector3(0, awayFromTip1, 0);
                    break;
                case 4:
                    tempPos += new Vector3(-awayFromFriend2, awayFromTip2, 0);
                    break;
                case 5:
                    tempPos += new Vector3(awayFromFriend2, awayFromTip2, 0);
                    break;
                case 6:
                    tempPos += new Vector3(0, awayFromTip2, 0);
                    break;
                case 7:
                    tempPos += new Vector3(-awayFromFriend3, awayFromTip3, 0);
                    break;
                case 8:
                    tempPos += new Vector3(awayFromFriend3, awayFromTip3, 0);
                    break;
                case 9:
                    tempPos += new Vector3(0, awayFromTip3, 0);
                    break;
            }
            preCalcPositonArrow[int.Parse("1" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);

            //right on x axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3(-awayFromTip1, -awayFromFriend1, 0);
                    break;
                case 2:
                    tempPos += new Vector3(-awayFromTip1, awayFromFriend1, 0);
                    break;
                case 3:
                    tempPos += new Vector3(-awayFromTip1, 0, 0);
                    break;
                case 4:
                    tempPos += new Vector3(-awayFromTip2, -awayFromFriend2, 0);
                    break;
                case 5:
                    tempPos += new Vector3(-awayFromTip2, awayFromFriend2, 0);
                    break;
                case 6:
                    tempPos += new Vector3(-awayFromTip2, 0, 0);
                    break;
                case 7:
                    tempPos += new Vector3(-awayFromTip3, -awayFromFriend3, 0);
                    break;
                case 8:
                    tempPos += new Vector3(-awayFromTip3, awayFromFriend3, 0);
                    break;
                case 9:
                    tempPos += new Vector3(-awayFromTip3, 0, 0);
                    break;
            }
            preCalcPositonArrow[int.Parse("2" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);

            //right on x axis +
            switch (i)
            {
                case 1:
                    tempPos += new Vector3(awayFromTip1, -awayFromFriend1, 0);
                    break;
                case 2:
                    tempPos += new Vector3(awayFromTip1, awayFromFriend1, 0);
                    break;
                case 3:
                    tempPos += new Vector3(awayFromTip1, 0, 0);
                    break;
                case 4:
                    tempPos += new Vector3(awayFromTip2, -awayFromFriend2, 0);
                    break;
                case 5:
                    tempPos += new Vector3(awayFromTip2, awayFromFriend2, 0);
                    break;
                case 6:
                    tempPos += new Vector3(awayFromTip2, 0, 0);
                    break;
                case 7:
                    tempPos += new Vector3(awayFromTip3, -awayFromFriend3, 0);
                    break;
                case 8:
                    tempPos += new Vector3(awayFromTip3, awayFromFriend3, 0);
                    break;
                case 9:
                    tempPos += new Vector3(awayFromTip3, 0, 0);
                    break;
            }
            preCalcPositonArrow[int.Parse("3" + i.ToString())] = tempPos;
            tempPos = new Vector3(0, 0, 0);

        }

    }

}


