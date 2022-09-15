using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    private float preContactTime = 0f;
    private float curContactTime = 0f;
    private float interruptionTime = Time.fixedDeltaTime * 2; //2frameï∂
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


    //éóÇΩÇÊÇ§Ç»coroutineÇ™ï°êîÇ†ÇÈÇΩÇﬂÇ‹Ç∆ÇﬂÇÈ
    public Health Test(Human human, List<GameObject> collider)
    {
        if (human.health == Health.onsetAndQuarantine) { return Health.onsetAndQuarantine; }

        //ê⁄êGÇµÇƒÇ¢ÇÈêlÇ…ózê´é“Ç™Ç¢Ç»Ç¢èÍçáÇÕcolPositiveÇÕnullÇ…Ç»ÇÈ
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

        //ê⁄êGÇµÇƒÇ¢Ç»Ç©Ç¡ÇΩéûä‘ï™HPÇâÒïúÇ≥ÇπÇÈ
        if (colPositive == null)
        {
            totalContactTime -= Time.fixedDeltaTime * HealthRecovery;
            totalContactTime = totalContactTime < 0f ? 0 : totalContactTime;
        }
        else
        {
            //ê⁄êGÇµÇƒÇ¢ÇΩÇÁHPÇÉCÉìÉNÉäÉÅÉìÉg
            totalContactTime += Time.fixedDeltaTime;
        }

        return totalContactTime;
    }



    //private float GetTotalContactTime(GameObject colPositive)
    //{
    //    curContactTime = Time.time;
        
    //    ê⁄êGÇµÇƒÇ¢Ç»Ç©Ç¡ÇΩéûä‘ï™HPÇâÒïúÇ≥ÇπÇÈ
    //    if (curContactTime - preContactTime > interruptionTime)
    //    {
    //        totalContactTime -= curContactTime - preContactTime;
    //        totalContactTime = totalContactTime < 0f ? 0 : totalContactTime;
    //    }
    //    else
    //    {
    //        ê⁄êGÇµÇƒÇ¢ÇΩÇÁHPÇÉCÉìÉNÉäÉÅÉìÉg
    //        totalContactTime += curContactTime - preContactTime;
    //    }

    //    return totalContactTime;
    //}


    private Health toInfectionNegative(float totalContactTime)
    {
        //todoä¥êıÉAÉãÉSÉäÉYÉÄ

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
