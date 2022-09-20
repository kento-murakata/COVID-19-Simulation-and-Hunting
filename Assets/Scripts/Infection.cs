using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    private float preContactTime = 0f;
    private float curContactTime = 0f;
    private float interruptionTime = Time.fixedDeltaTime * 2; //2frame•¶
    private float totalContactTime = 0;

    private float timeInfection;

    List<GameObject> collider = new List<GameObject>();
    HumanBehaviour human;

    GameObject colPositive;

    private const float HealthRecovery = 3;
    private const float Minuts = 60;

    private void Update()
    {
        Debug.Log("update ");
        Test(human, collider);
    }


    //public class Human
    //{
    //    public Health health = Health.infectionPositive;
    //}

    //public enum Health
    //{
    //    negative,
    //    infectionNegative,
    //    infectionPositive,
    //    onsetAndQuarantine,
    //}



    //todo getcomponent ‚Ì•¡”‰ñŒÄo‚µ‚ğ‚Ü‚Æ‚ß‚é
    public HealthStatus Test(HumanBehaviour human, List<GameObject> collider)
    {
        if (human.healthStatus == HealthStatus.onsetAndQuarantine) { return human.healthStatus; }

        colPositive = collider.Find(col => (int)col.GetComponent<HumanBehaviour>().healthStatus >= (int)HealthStatus.infectionPositive);

        float totalContactTime = GetTotalContactTime(colPositive);

        if (colPositive.GetComponent<HumanBehaviour>() == null) { return human.healthStatus; }

        var health = toInfectionNegative(totalContactTime);

        if ((int)human.healthStatus >= (int)HealthStatus.infectionNegative)
        {
            StartCoroutine(toInfectionPositive());

            health = HealthStatus.infectionPositive;

            StartCoroutine(toOnsetAndQuarantine());

            health = HealthStatus.onsetAndQuarantine;
        }
        return human.healthStatus;
    }

    private float GetTotalContactTime(GameObject colPositive)
    {
        //ÚG‚µ‚Ä‚¢‚È‚©‚Á‚½ŠÔ•ªHP‚ğ‰ñ•œ‚³‚¹‚é
        if (colPositive.GetComponent<HumanBehaviour>() != null)
        {
            //ÚG‚µ‚Ä‚¢‚½‚çHP‚ğƒCƒ“ƒNƒŠƒƒ“ƒg
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



    private IEnumerator<HealthStatus> InfectionProcess()
    {
        float waitTime = 3;

        new WaitForSeconds(waitTime);

        yield return HealthStatus.infectionPositive;


        new WaitForSeconds(waitTime);

        yield return HealthStatus.onsetAndQuarantine;
    }







    private HealthStatus toInfectionNegative(float totalContactTime)
    {
        //todoŠ´õƒAƒ‹ƒSƒŠƒYƒ€
        timeInfection = 15 * Minuts; //15•ª‚Éb’èİ’è

        return timeInfection < totalContactTime ? HealthStatus.infectionNegative : HealthStatus.negative;
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
