using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreCalculatePositions : MonoBehaviour
{
    public float awayFromTip1, awayFromTip2, awayFromTip3, awayFromFriend1, awayFromFriend2, awayFromFriend3;


    public List<Vector3> preCalcPositonArrow = new List<Vector3>(40);

    public List<Vector3> preCalcPositonDiamond = new List<Vector3>(40);


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public PreCalculatePositions()
    {
        awayFromTip1 = 8f;
        awayFromTip2 = 12f;
        awayFromTip3 = 16f;
        awayFromFriend1 = 8f;
        awayFromFriend2 = 12f;
        awayFromFriend3 = 16f;
        PreCalcPositionDiamond();
        PreCalcPositionArrow();
    }

    public PreCalculatePositions(float away1, float away2, float away3)
    {
        awayFromTip1 = away1;
        awayFromTip2 = away2;
        awayFromTip3 = away3;
        awayFromFriend1 = away1;
        awayFromFriend2 = away2;
        awayFromFriend3 = away3;
        PreCalcPositionDiamond();
        PreCalcPositionArrow();
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
