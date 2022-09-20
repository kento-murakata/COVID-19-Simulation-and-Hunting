using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace InfectionSimulator
{
    //    /*
    //    1日当たりの陽性数 List
    //    1日当たりの死者数 List
    //    トータル死者数。１日目のTOTAL、２日目のTOTALみたいに日々のトータルを出す
    //    トータル感染者。上と一緒
    //    病院使用率

    //floatのリスト=> Graphへ

    //     */
    //    public class infectedDataBase : MonoBehaviour
    //    {
    //        public enum infectedStatus
    //        {
    //            NEGATIVE,
    //            EXPOSE,
    //            ASYMPOTOMATIC,
    //            SYMPTONS,
    //            INFECTED;
    //        }
    public class EnemyManager : MonoBehaviour
    {
        GameObject[] enemyObjects;
        int enemyNum;
        void Start()
        {
            enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            enemyNum = enemyObjects.Length
        }
    }
    public void Day()
    {
        int dayCount; // ～日目の "～"の部分の変数
        dayCount = 1;
        string day = $"Day{dayCount}"; //Day1 を day に代入
    }

    //        public void CountDailyInfector()
    //        {
    //            int[] dailyInfector = new int[];
    //        }

    //        public void CountDailyDeath()
    //        {
    //            int[] dailydeath = new int[];
    //        }

    //        public void CountTotalInfector()
    //        {
    //            dailyInfector.CountDailyInfector();
    //            num.Sum(); // 15
    //            int totalInfector =

    //        }

    //        public void CountTodalDeath()
    //    {
    //        int totalDeath;
    //    }


}









