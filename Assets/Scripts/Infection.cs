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

    //todo getcomponent �̕�����ďo�����܂Ƃ߂�
    public HealthStatus Test(HumanBehaviour human, List<GameObject> collider)
    {
        healthStatus = human.healthStatus;

        if (healthStatus == HealthStatus.onsetAndQuarantine) { return healthStatus; }

        if (collider.Count != 0 && collider[0].CompareTag("Person"))
        {
            Debug.Log(collider[0].GetComponent<HumanBehaviour>().healthStatus);
        }


        //getcomponent�͏d������findtag�ɕύX����ׂ�
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
        //�ڐG���Ă��Ȃ��������ԕ�HP���񕜂�����
        if (colPositive != null)
        {
            //�ڐG���Ă�����HP���C���N�������g
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
        //todo�����A���S���Y��
        timeInfection = 15 * Minuts; //15���Ɏb��ݒ�

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
