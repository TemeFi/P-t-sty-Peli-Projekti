using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private float xP, xM, yP, yM;
    PreCalculatePositions precalcPos;


    public UnitMovement()
    {
        xP = GameObject.Find("Scene Controller").GetComponent<CameraController>().xPlusBoundary;
        xM = GameObject.Find("Scene Controller").GetComponent<CameraController>().xMinusBoundary;
        yP = GameObject.Find("Scene Controller").GetComponent<CameraController>().yPlusBoundary;
        yM = GameObject.Find("Scene Controller").GetComponent<CameraController>().yMinusBoundary;

        precalcPos = new PreCalculatePositions();
    }

    public void MoveUnit(CharacterController charCon)
    {
        Vector3 targetPos;
        int myIndex;

        if (charCon.target != null) 
        {
            targetPos = charCon.target.transform.position;
        }
        else
        {
            Vector3 toObjectVector = charCon.gameObject.transform.position - Camera.main.transform.position;
            Vector3 linearDistanceVector = Vector3.Project(toObjectVector, Camera.main.transform.forward);
            float distance = (linearDistanceVector).magnitude;
            Vector3 mousPosition = Input.mousePosition;

            mousPosition.z = distance;
            targetPos = Camera.main.ScreenToWorldPoint(mousPosition);
        }
            
        myIndex = charCon.myIndex;

        Vector2 from = targetPos - charCon.gameObject.transform.position;
        Vector2 to = Vector2.up;
        float angle = Vector2.SignedAngle(from, to);

        if (angle < 45 && angle >= 0 || angle > -45 && angle <= -0 || angle > 135 && angle <= 180 || angle < -135 && angle >= -180)
        {
            if (angle < 90 && angle >= 0 || angle > -90 && angle <= -0) //ylös +. Sijainti y+ eli ylös = 0-9 preCalcPosition array:ssa
            {
                if (charCon.positionArrow)
                    targetPos += precalcPos.preCalcPositonArrow[myIndex];
                if (charCon.positionDiamond)
                    targetPos += precalcPos.preCalcPositonDiamond[myIndex];
            }
            else if (angle > 90 && angle <= 180 || angle < -90 && angle >= -180) //Alas -. Sijainti y- eli alas = 10-19 preCalcPosition array:ssa
            {
                if (charCon.positionArrow)
                    targetPos += precalcPos.preCalcPositonArrow[int.Parse("1" + myIndex.ToString())];
                if (charCon.positionDiamond)
                    targetPos += precalcPos.preCalcPositonDiamond[int.Parse("1" + myIndex.ToString())];
            }
        }
        else if (angle > 45 && angle < 135 || angle < -45 && angle > -135 || angle == 90 || angle == -90)
        {
            if (angle > 0 && angle < 180 || angle == 90) //Oikea +. Sijainti x+  eli oikea = 20-29 preCalcPosition array:ssa
            {
                if (charCon.positionArrow)
                    targetPos += precalcPos.preCalcPositonArrow[int.Parse("2" + myIndex.ToString())];
                if (charCon.positionDiamond)
                    targetPos += precalcPos.preCalcPositonDiamond[int.Parse("2" + myIndex.ToString())];
            }
            else if (angle < -0 && angle > -180 || angle == -90)//Vasen -.  Sijainti x- eli vasen = 30-39 preCalcPosition array:ssa
            {
                if (charCon.positionArrow)
                    targetPos += precalcPos.preCalcPositonArrow[int.Parse("3" + myIndex.ToString())];
                if (charCon.positionDiamond)
                    targetPos += precalcPos.preCalcPositonDiamond[int.Parse("3" + myIndex.ToString())];
            }
        }

        //Varmistaa että yksiköt eivät mene kameran rajojen yli x aksiksella
        if (targetPos.x > xP) { targetPos.x = xP; }
        if (targetPos.x < xM) { targetPos.x = xM; }

        //Varmistaa että yksiköt eivät mene kameran rajojen yli y aksiksella
        if (targetPos.y > yP) { targetPos.y = yP; }
        if (targetPos.y < yM) { targetPos.y = yM; }

        charCon.myVector3 = new Vector3(targetPos.x, targetPos.y, 0);
        charCon.isMoving = true;
    }

}
