using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace InfectionSimulator
{
    public class infectedDataBase : MonoBehaviour
    {
        private void Awake()
        {
            MonthlyNewInfector();
        }
        private void MonthlyNewInfector()
        {
            //感染者リストをもらう。
            //Find でゲームオブジェクトの人を探す。　人に結びついてる時間を拾う。
            //int result =list.Find(infected => infected.Infected == "beInfectionNegativeTime==true");
            //int result =list.Find(infected => infected.Infected == "beInfectionNegativeTime==true").humanId;
            //int result =list.Find(infected => infected.Infected == "beInfectionNegativeTime==true").Vector2;


            private float newInfector = 1000f; // The number of new patiants. Get form Infected.cs 
            private float infectionCountTimer = Infection.beInfectionNegativeTime; //田場さんから感染タイミングのもらう     

            private float patiantsList = 100.0f;

            private float spendTime = Time.time;
            private float monthlyTime = 100.0f;

            private float janEndTime = monthlyTime;
            private float febEndTime = janEndTime + monthlyTime;
            private float marEndTime = febEndTime + monthlyTime;
            private float aprEndTime = marEndTime + monthlyTime;
            private float mayEndTime = aprEndTime + monthlyTime;
            private float junEndTime = mayEndTime + monthlyTime;
            private float julEndTime = junEndTime + monthlyTime;
            private float augEndTime = julEndTime + monthlyTime;
            private float sepEndTime = augEndTime + monthlyTime;
            private float octEndTime = sepEndTime + monthlyTime;
            private float novEndTime = octEndTime + monthlyTime;
            private float decEndTime = novEndTime + monthlyTime;

            private float newInfectorOfJan = 0f;
            private float newInfectorOfFeb = 0f;
            private float newInfectorOfMar = 0f;
            private float newInfectorOfApr = 0f;
            private float newInfectorOfMay = 0f;
            private float newInfectorOfJun = 0f;
            private float newInfectorOfJul = 0f;
            private float newInfectorOfAug = 0f;
            private float newInfectorOfSep = 0f;
            private float newInfectorOfOct = 0f;
            private float newInfectorOfNov = 0f;
            private float newInfectorOfDec = 0f;

            for (int i = 0; i <= patiantsList; i++)
            {
                if (infectionCountTimer <= janEndTime) { newInfectorOfJan++; }
                else if (infectionCountTimer <= febEndTime) { newInfectorOfFeb++; }
                else if (infectionCountTimer <= marEndTime) { newInfectorOfMar++; }
                else if (infectionCountTimer <= aprEndTime) { newInfectorOfApr++; }
                else if (infectionCountTimer <= mayEndTime) { newInfectorOfMay++; }
                else if (infectionCountTimer <= junEndTime) { newInfectorOfJun++; }
                else if (infectionCountTimer <= julEndTime) { newInfectorOfJul++; }
                else if (infectionCountTimer <= augEndTime) { newInfectorOfAug++; }
                else if (infectionCountTimer <= sepEndTime) { newInfectorOfSep++; }
                else if (infectionCountTimer <= octEndTime) { newInfectorOfOct++; }
                else if (infectionCountTimer <= novEndTime) { newInfectorOfNov++; }
                else if (infectionCountTimer <= decEndTime) { newInfectorOfDec++; }
            }


            List<float> monthlyInfector = new List<float>();
            monthlyInfector.Add(newInfectorOfJan);
            monthlyInfector.Add(newInfectorOfFeb);
            monthlyInfector.Add(newInfectorOfMar);
            monthlyInfector.Add(newInfectorOfApr);
            monthlyInfector.Add(newInfectorOfMay);
            monthlyInfector.Add(newInfectorOfJun);
            monthlyInfector.Add(newInfectorOfJul);
            monthlyInfector.Add(newInfectorOfAug);
            monthlyInfector.Add(newInfectorOfSep);
            monthlyInfector.Add(newInfectorOfOct);
            monthlyInfector.Add(newInfectorOfNov);
            monthlyInfector.Add(newInfectorOfDec);
        }
    }
}




//    float oneDay = Time.time / 365; //Time.timeの1日あたりの時間を決める。とりあえず365で割ってみた。
//    int[] date = new int[364];
//    List<>= new List<int, float>();
//        string[] month = new string[11];

//        //date に1-365まで代入する。閏年はなし。
//        for(int i = 1; i <=364; i++)
//        {
//            date[i]= date[i] + 1;
//        }

//string month =

//        float newInfector;
//        float currentInfector; // <= From Kawasaki-san,
//        float totalInfector;
//        float curedNumber; 

//        // Dictionaly<floatTime,floatStatus, personId> time.Time, status, Id(personID)




//        public void Count()
//        {
//            var monthlyInfector = new Dictionary<string, int>();


//        }



//        int dailyInfector = 0;//GameMangerから感染者数持ってきて置き換える。
//        int timeCounter = 100; //日ごとか月ごとか何かしらの区切り要素？
//        int dayCount = 0; //〇日目のカウントを持ってくる。
//        string[] date = new string[] { }; //日付をリストにいれる
//        int[] infectedNumber = new int[] { };  //感染者数をリストにいれる

//        private void Awake()
//        {
//            DailyInfectedNumber();
//            Dictionary<string, int> correctInfector =
//                new Dictionary<string, int>(date[], infectedNumber[]);
//        }

//        public void DailyInfectedNumber()
//        {
//            for (int i = 0; i < timeCounter; i++)
//            {
//                dayCount = dayCount + 1;
//                date[i] = $"Day{dayCount}";
//            }

//            for (int i = 0; i < timeCounter; i++)
//            {
//                infectedNumber[i] = dailyInfector;
//            }
//        }
//    }
//}