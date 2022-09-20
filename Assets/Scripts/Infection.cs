using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Infection : MonoBehaviour
{
    private float preContactTime = 0f;
    [SerializeField]
    private float totalContactTime = 0;
    private float elapsedTime = 0;

    private float timeInfection;

    private const float HealthRecovery = 3;
    private const float Minuts = 60;


    HealthStatus healthStatus;


    //todo getcomponent の複数回呼出しをまとめる
    public HealthStatus Test(HumanBehaviour human, List<GameObject> collider)
    {
        healthStatus = human.healthStatus;

        if (healthStatus == HealthStatus.onsetAndQuarantine) { return healthStatus; }

        if (collider.Count != 0 && collider[0].CompareTag("Person"))
        {
            Debug.Log(collider[0].GetComponent<HumanBehaviour>().healthStatus);
        }

        //getcomponentは重いためfindtagに変更するべき
        var colPositive = collider.Find(col => (int)col.GetComponent<HumanBehaviour>().healthStatus >= (int)HealthStatus.infectionPositive);
        totalContactTime = GetTotalContactTime(colPositive);

        if (colPositive == null) { return healthStatus; }

        healthStatus = ToInfectionNegative(totalContactTime);

        if ((int)healthStatus >= (int)HealthStatus.infectionNegative)
        {
            StartCoroutine(ToInfectionPositive());

            healthStatus = HealthStatus.infectionPositive;

            StartCoroutine(ToOnsetAndQuarantine());

            healthStatus = HealthStatus.onsetAndQuarantine;
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

    private HealthStatus ToInfectionNegative(float totalContactTime)
    {
        //todo感染アルゴリズム
        timeInfection = 15 * Minuts; //15分に暫定設定

        return timeInfection < totalContactTime ? HealthStatus.infectionNegative : HealthStatus.negative;
    }

    IEnumerator ToInfectionPositive()
    {
        float waitTime = 3;
        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator ToOnsetAndQuarantine()
    {
        float waitTime = 3;
        yield return new WaitForSeconds(waitTime);
    }
}
