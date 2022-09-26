using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Infection : MonoBehaviour
{
    #region fields

    [SerializeField]
    public  float totalContactTime;
    [SerializeField]
    public float ElapsedTimeAfterInfection;
    [SerializeField]
    public float toInfectionNegativeTime;
    [SerializeField]
    public float toInfectionPositiveTime;
    [SerializeField]
    public float toOnsetAndQuarantineTime;
    [SerializeField]
    public float toNegativeTime = 20f * Day;



    private float preContactTime;
    private float elapsedTime;
    private float HealthRecovery = 3.0f;

    private float time;

    private static readonly float Minuts = 60.0f;
    private static readonly float Day = 86400.0f;

    private float toInfectionNegativeMinTime = 15f * Minuts;
    private float toInfectionNegativeMaxTime = 30f * Minuts;

    private float toInfectionPositiveMinTime = 1f * Day;
    private float toInfectionPositiveMaxTime = 7f * Day;

    private float toOnsetAndQuarantineMinTime = 3f * Day;
    private float toOnsetAndQuarantineMaxTime = 5f * Day;

    HealthStatus healthStatus;
    GameObject colPositive;

    #endregion fields

    #region properties
    public float beNegativeTime { get; private set; }
    public float beInfectionNegativeTime { get; private set; }
    public float beInfectionPositiveTime { get; private set; }
    public float beOnsetAndQuarantineTime { get; private set; }

    #endregion properties

#region methods

    private void Awake()
    {
        toInfectionNegativeTime = Random.Range(
                    toInfectionNegativeMinTime,
                    toInfectionNegativeMaxTime);

        toInfectionPositiveTime = Random.Range(
                toInfectionPositiveMinTime,
                toInfectionPositiveMaxTime);

        toOnsetAndQuarantineTime = Random.Range(
                toOnsetAndQuarantineMinTime,
                toOnsetAndQuarantineMaxTime);
    }

    //todo getcomponent の複数回呼出しをまとめる
    public HealthStatus Test(HumanBehaviour human, List<GameObject> collider)
    {
        time = Time.time;
        healthStatus = human.healthStatus;

        Isnpector();

        //getcomponentは重いためfindtagに変更するべき
        colPositive = collider.Find(col => (int)col.GetComponent<HumanBehaviour>().healthStatus >= (int)HealthStatus.infectionPositive);
        totalContactTime = GetTotalContactTime(colPositive);

        if (healthStatus == HealthStatus.negative)
        {
            if(totalContactTime > toInfectionNegativeTime)
            {
                healthStatus = HealthStatus.infectionNegative;
                beInfectionNegativeTime = time;
                infectedDataBase.MonthlyNewInfector(beInfectionNegativeTime);
            }
        }
        else if (healthStatus == HealthStatus.infectionNegative)
        {
            if(time - beInfectionNegativeTime >= toInfectionPositiveTime)
            {
                healthStatus = HealthStatus.infectionPositive;
                beInfectionPositiveTime = time;
            }
        }
        else if (healthStatus == HealthStatus.infectionPositive)
        {
            if (time - beInfectionPositiveTime >= toOnsetAndQuarantineTime)
            {
                healthStatus = HealthStatus.onsetAndQuarantine;
                beOnsetAndQuarantineTime = time;
            }
        }
        else if (healthStatus == HealthStatus.onsetAndQuarantine)
        {
            if (time - beOnsetAndQuarantineTime >= toNegativeTime)
            {
                healthStatus = HealthStatus.negative;
                beNegativeTime = time;
                totalContactTime = 0;
                ElapsedTimeAfterInfection = 0;
            }
        }
        return healthStatus;
    }

    private void Isnpector()
    {
        if ((int)healthStatus >= (int)HealthStatus.infectionNegative)
        {
            ElapsedTimeAfterInfection = time - beInfectionNegativeTime;
        }
    }

    private float GetTotalContactTime(GameObject colPositive)
    {
        elapsedTime = time - preContactTime;
        //接触していなかった時間分HPを回復させる
        if (colPositive != null)
        {
            //接触していたらHPをインクリメント
            totalContactTime += elapsedTime;
        }
        else
        {
            totalContactTime -= elapsedTime * HealthRecovery;
            totalContactTime = totalContactTime < 0f ? 0f : totalContactTime;
        }
        preContactTime = time;
        return totalContactTime;
    }
}
#endregion methods