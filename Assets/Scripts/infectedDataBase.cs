using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace InfectionSimulator
{
    public class DataBase : MonoBehaviour
    {
        public enum month
        {
            Jan,
            Feb,
            Mar,
            Apr,
            May,
            Jun,
            Jul,
            Aug,
            Sep,
            Oct,
            Nov,
            Dec,
        }

        public void MonthlyNewInfector()
        {
            float newInfector = 1000; // The number of new patiants. Get form Infected.cs ?
            float infectionCountTimer = Infection.beInfectionNegativeTime; //�c�ꂳ�񂩂犴���^�C�~���O�̂��炤                    
            //�����҃��X�g�����炤�B
            float patiantsList = 100;

            float spendTime = Time.time;
            float monthlyTime = 100;

            float janEndTime = monthlyTime;
            float febEndTime = janEndTime + monthlyTime;
            float marEndTime = febEndTime + monthlyTime;
            float aprEndTime = marEndTime + monthlyTime;
            float mayEndTime = aprEndTime + monthlyTime;
            float junEndTime = mayEndTime + monthlyTime;
            float julEndTime = junEndTime + monthlyTime;
            float augEndTime = julEndTime + monthlyTime;
            float sepEndTime = augEndTime + monthlyTime;
            float octEndTime = sepEndTime + monthlyTime;
            float novEndTime = octEndTime + monthlyTime;
            float decEndTime = novEndTime + monthlyTime;

            float newInfectorOfJan = 0;
            float newInfectorOfFeb = 0;
            float newInfectorOfMar = 0;
            float newInfectorOfApr = 0;
            float newInfectorOfMay = 0;
            float newInfectorOfJun = 0;
            float newInfectorOfJul = 0;
            float newInfectorOfAug = 0;
            float newInfectorOfSep = 0;
            float newInfectorOfOct = 0;
            float newInfectorOfNov = 0;
            float newInfectorOfDec = 0;

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




//    float oneDay = Time.time / 365; //Time.time��1��������̎��Ԃ����߂�B�Ƃ肠����365�Ŋ����Ă݂��B
//    int[] date = new int[364];
//    List<>= new List<int, float>();
//        string[] month = new string[11];

//        //date ��1-365�܂ő������B�[�N�͂Ȃ��B
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



//        int dailyInfector = 0;//GameManger���犴���Ґ������Ă��Ēu��������B
//        int timeCounter = 100; //�����Ƃ������Ƃ���������̋�؂�v�f�H
//        int dayCount = 0; //�Z���ڂ̃J�E���g�������Ă���B
//        string[] date = new string[] { }; //���t�����X�g�ɂ����
//        int[] infectedNumber = new int[] { };  //�����Ґ������X�g�ɂ����

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