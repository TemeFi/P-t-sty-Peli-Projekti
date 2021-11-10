using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    float randomRange;
  
    //PLAYER
    public void CalculateFighter(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {
            case "Drone Squadron":

                if (randomRange <= 80f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;

            case "Slugger Squadron":
                if (randomRange <= 60f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;

            case "Soldier Squadron":
                if (randomRange <= 60f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;

            case "Brood Unit":
                if (randomRange <= 70f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;
        }
    }

    public void CalculateMultirole(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {

            case "Drone Squadron":

                if (randomRange <= 50f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;

            case "Slugger Squadron":

                if (randomRange <= 25f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;

            case "Soldier Squadron":

                if (randomRange <= 30f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;

            case "Brood Unit":

                if (randomRange <= 40f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateBomber(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {

            case "Drone Squadron":

                if (randomRange <= 5f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;

            case "Slugger Squadron":

                if (randomRange <= 10f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;

            case "Soldier Squadron":

                if (randomRange <= 10f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;

            case "Brood Unit":

                if (randomRange <= 40f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateFrigate(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {
            case "Brood Unit":

                if (randomRange <= 40f)
                {
                    targetStats.hp = targetStats.hp - unit.damage;
                    return;
                }
                break;
        }
    }

    public void CalculateStationTurret(TurretStats turret, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 1000.0f);
        switch (targetStats.uName)
        {

            case "Drone Squadron":

                if (randomRange <= 5f)
                {
                    targetStats.hp = targetStats.hp - turret.damage;
                    return;
                }
                break;

            case "Slugger Squadron":

                if (randomRange <= 8f)
                {
                    targetStats.hp = targetStats.hp - turret.damage;
                    return;

                }
                break;

            case "Soldier Squadron":

                if (randomRange <= 10f)
                {
                    targetStats.hp = targetStats.hp - turret.damage;
                    return;

                }
                break;

            case "Brood Unit":

                if (randomRange <= 20f)
                {
                    targetStats.hp = targetStats.hp - turret.damage;
                    return;
                }
                break;
        }
    }

    //ENEMY

    public void CalculateDrone(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (unit.uname)
        {

            case "Fighter Squadron":

                if (randomRange <= 50f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;
                }
                break;

            case "Bomber Squadron":

                if (randomRange <= 25f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Multirole Squadron":

                if (randomRange <= 40f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Frigate Unit":

                if (randomRange <= 40f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateSoldier(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (unit.uname)
        {

            case "Fighter Squadron":

                if (randomRange <= 20f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;
                }
                break;

            case "Bomber Squadron":

                if (randomRange <= 25f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Multirole Squadron":

                if (randomRange <= 30f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Frigate Unit":

                if (randomRange <= 40f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateSlugger(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (unit.uname)
        {

            case "Fighter Squadron":

                if (randomRange <= 5f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;
                }
                break;

            case "Bomber Squadron":

                if (randomRange <= 10f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Multirole Squadron":

                if (randomRange <= 10f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;

            case "Frigate Unit":

                if (randomRange <= 40f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateBrood(UnitStats unit, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (unit.uname)
        {
            case "Frigate Unit":

                if (randomRange <= 40f)
                {
                    unit.hp = unit.hp - targetStats.damage;
                    return;
                }
                break;
        }
    }

    public void CalculateEnemyVsTurret(TurretStats tur, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {
            case "Drone Squadron":

                if (randomRange <= 30f)
                {
                    tur.hp = tur.hp - targetStats.damage;
                    return;
                }
                break;

            case "Slugger Squadron":

                if (randomRange <= 30f)
                {
                    tur.hp = tur.hp - targetStats.damage;
                    return;

                }
                break;

            case "Soldier Squadron":

                if (randomRange <= 30f)
                {
                    tur.hp = tur.hp - targetStats.damage;
                    return;

                }
                break;
        }
    }

    public void CalculateEnemyVsStation(StationStats station, EnemyStats targetStats)
    {
        randomRange = Random.Range(0f, 100.0f);

        switch (targetStats.uName)
        {
            case "Drone Squadron":

                if (randomRange <= 40f)
                {
                    station.hp = station.hp - targetStats.damage;
                    return;
                }
                break;

            case "Slugger Squadron":

                if (randomRange <= 40f)
                {
                    station.hp = station.hp - targetStats.damage;
                    return;
                }
                break;

            case "Soldier Squadron":

                if (randomRange <= 40f)
                {
                    station.hp = station.hp - targetStats.damage;
                    return;
                }
                break;

            case "Brood Unit":

                if (randomRange <= 40f)
                {
                    station.hp = station.hp - targetStats.damage;
                    return;
                }
                break;
        }
    }

}
