using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    private float preContactTime = 0f;
    private float curContactTime = 0f;
    private float interruptionTime = Time.fixedDeltaTime * 2; //2frame��
    private float totalContactTime = 0;

    private float timeInfection;

    List<GameObject> collider = new List<GameObject>();
    Human human;

    GameObject colPositive;

    private const float HealthRecovery = 3;
    private const float Minuts = 60;

    private void Update()
    {
        Debug.Log("update ");
        Test(human, collider);
    }


    public class Human
    {
        public Health health = Health.infectionPositive;
    }

    public enum Health
    {
        negative,
        infectionNegative,
        infectionPositive,
        onsetAndQuarantine,
    }



    //todo getcomponent �̕�����ďo�����܂Ƃ߂�
    public Health Test(Human human, List<GameObject> collider)
    {
        if (human.health == Health.onsetAndQuarantine) { return human.health; }

        colPositive = collider.Find(col => (int)col.GetComponent<Human>().health >= (int)Health.infectionPositive);

        float totalContactTime = GetTotalContactTime(colPositive);

        if (colPositive.GetComponent<Human>() == null) { return human.health; }

        human.health = ToInfectionNegative(totalContactTime);

        if ((int)human.health >= (int)Health.infectionNegative)
        {
            StartCoroutine(ToInfectionPositive());

            human.health = Health.infectionPositive;

            StartCoroutine(ToOnsetAndQuarantine());

            human.health = Health.onsetAndQuarantine;
        }



        return human.health;
    }






    private float GetTotalContactTime(GameObject colPositive)
    {
        //�ڐG���Ă��Ȃ��������ԕ�HP���񕜂�����
        if (colPositive.GetComponent<Human>() != null)
        {
            //�ڐG���Ă�����HP���C���N�������g
            totalContactTime += Time.fixedDeltaTime;
        }
        else
        {
            totalContactTime -= Time.fixedDeltaTime * HealthRecovery;
            totalContactTime = totalContactTime < 0f ? 0f : totalContactTime;
        }
        preContactTime = curContactTime;
        return totalContactTime;
    }


    private Health ToInfectionNegative(float totalContactTime)
    {
        //todo�����A���S���Y��
        timeInfection = 15 * Minuts; //15���Ɏb��ݒ�

        return timeInfection < totalContactTime ? Health.infectionNegative : Health.negative;
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
