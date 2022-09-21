using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Infection : MonoBehaviour
{
    #region fields

    [SerializeField]
    private float totalContactTime;
    [SerializeField]
    private float ElapsedTimeAfterInfection;

    [SerializeField]
    private float toInfectionNegativeTime;
    [SerializeField]
    private float toInfectionPositiveTime;
    [SerializeField]
    private float toOnsetAndQuarantineTime;
    [SerializeField]
    private float toNegativeTime = 20f * Day;


    private float preContactTime;
    private float elapsedTime;
    private float _beNegativeTime;
    private float _beInfectionNegativeTime;
    private float _beInfectionPositiveTime;
    private float _beOnsetAndQuarantineTime;

    private float HealthRecovery = 3.0f;
    private static readonly float Minuts = 60.0f;
    private static readonly float Day = 86400.0f;

    private float toInfectionNegativeMinTime = 15f * Minuts;
    private float toInfectionNegativeMaxTime = 30f * Minuts;

    private float toInfectionPositiveMinTime = 1f * Day;
    private float toInfectionPositiveMaxTime = 7f * Day;

    private float toOnsetAndQuarantineMinTime = 3f * Day;
    private float toOnsetAndQuarantineMaxTime = 5f * Day;

    HealthStatus healthStatus;

    #endregion fields

    #region properties
    public float beNegativeTime { get=> _beNegativeTime;}
    public float beInfectionNegativeTime { get=> _beInfectionNegativeTime;}
    public float beInfectionPositiveTime { get=> _beInfectionPositiveTime;}
    public float beOnsetAndQuarantineTime { get=> _beOnsetAndQuarantineTime;}

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
        healthStatus = human.healthStatus;

        if((int)healthStatus >= (int)HealthStatus.infectionNegative)
        {
            ElapsedTimeAfterInfection = Time.time - beInfectionNegativeTime;
        }

        //getcomponentは重いためfindtagに変更するべき
            var colPositive = collider.Find(col => (int)col.GetComponent<HumanBehaviour>().healthStatus >= (int)HealthStatus.infectionPositive);
        totalContactTime = GetTotalContactTime(colPositive);

        if (healthStatus == HealthStatus.negative)
        {
            if(totalContactTime > toInfectionNegativeTime)
            {
                healthStatus = HealthStatus.infectionNegative;
                _beInfectionNegativeTime = Time.time;
            }
        }
        else if (healthStatus == HealthStatus.infectionNegative)
        {
            if(Time.time - beInfectionNegativeTime >= toInfectionPositiveTime)
            {
                healthStatus = HealthStatus.infectionPositive;
                _beInfectionPositiveTime = Time.time;
            }
        }
        else if (healthStatus == HealthStatus.infectionPositive)
        {
            if (Time.time - beInfectionPositiveTime >= toOnsetAndQuarantineTime)
            {
                healthStatus = HealthStatus.onsetAndQuarantine;
                _beOnsetAndQuarantineTime = Time.time;
            }
        }
        else if (healthStatus == HealthStatus.onsetAndQuarantine)
        {
            if (Time.time - beOnsetAndQuarantineTime >= toNegativeTime)
            {
                healthStatus = HealthStatus.negative;
                _beNegativeTime = Time.time;
                totalContactTime = 0;
                ElapsedTimeAfterInfection = 0;
            }
        }
        return healthStatus;
    }


    private float GetTotalContactTime(GameObject colPositive)
    {
        elapsedTime = Time.time - preContactTime;
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
        preContactTime = Time.time;
        return totalContactTime;
    }
}
#endregion methods