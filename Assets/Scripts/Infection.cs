using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    private float preContactTime = 0f;
    private float curContactTime = 0f;
    private float interruptionTime = Time.fixedDeltaTime * 2; //2frame��
    private float totalContactTime = 0;


    private const float HealthRecovery = 3;


    public class Human 
    {
        public Health health = Health.negative;
    }

    public enum Health 
    { 
        negative,
        infectionNegative,
        infectionPositive,
        onsetAndQuarantine,
    }


    //�����悤��coroutine���������邽�߂܂Ƃ߂�
    public Health Test(Human human, List<GameObject> collider)
    {
        if (human.health == Health.onsetAndQuarantine) { return Health.onsetAndQuarantine; }

        //�ڐG���Ă���l�ɗz���҂����Ȃ��ꍇ��colPositive��null�ɂȂ�
        GameObject colPositive = collider.Find(col => col.GetComponent<Human>().health != Health.negative);

        if (colPositive.GetComponent<Human>().health != Health.negative)
        {
            float totalContactTime = GetTotalContactTime(colPositive);

            switch (human.health)
            {
                case Health.negative:
                    return toInfectionNegative(totalContactTime);

                case Health.infectionNegative:
                    StartCoroutine(toInfectionPositive());
                    return Health.infectionPositive;

                case Health.infectionPositive:
                    StartCoroutine(toOnsetAndQuarantine());
                    return Health.onsetAndQuarantine;

            }
        }

        preContactTime = curContactTime;
        return Health.onsetAndQuarantine;
    }


    private float GetTotalContactTime(GameObject colPositive)
    {
        curContactTime = Time.time;

        //�ڐG���Ă��Ȃ��������ԕ�HP���񕜂�����
        if (colPositive == null)
        {
            totalContactTime -= Time.fixedDeltaTime * HealthRecovery;
            totalContactTime = totalContactTime < 0f ? 0 : totalContactTime;
        }
        else
        {
            //�ڐG���Ă�����HP���C���N�������g
            totalContactTime += Time.fixedDeltaTime;
        }

        return totalContactTime;
    }



    //private float GetTotalContactTime(GameObject colPositive)
    //{
    //    curContactTime = Time.time;
        
    //    �ڐG���Ă��Ȃ��������ԕ�HP���񕜂�����
    //    if (curContactTime - preContactTime > interruptionTime)
    //    {
    //        totalContactTime -= curContactTime - preContactTime;
    //        totalContactTime = totalContactTime < 0f ? 0 : totalContactTime;
    //    }
    //    else
    //    {
    //        �ڐG���Ă�����HP���C���N�������g
    //        totalContactTime += curContactTime - preContactTime;
    //    }

    //    return totalContactTime;
    //}


    private Health toInfectionNegative(float totalContactTime)
    {
        //todo�����A���S���Y��

        return Health.infectionNegative;
    }



    IEnumerator toInfectionPositive()
    {
        float waitTime = 3;




        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator toOnsetAndQuarantine()
    {
        float waitTime = 3;
        yield return new WaitForSeconds(waitTime);
    }

}
