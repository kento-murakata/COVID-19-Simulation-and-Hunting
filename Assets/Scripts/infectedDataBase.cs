using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace InfectionSimulator
{
    public class DataBase : MonoBehaviour
    {
        public void DailyInfectedNumber()
        {
            int infector = 0;//GameManger���犴���Ґ������Ă��Ēu��������B
            int timeCounter = 100; //�����Ƃ������Ƃ���������̋�؂�v�f�H

            string[] day = new string[] { };
            int dayCount=0;            
            for(int i=0; i<timeCounter; i++)
            {
                dayCount = dayCount + 1;
                day[i]= "Day{dayCount}";
            }
            
            int[] infectedNumber = new int[] { };
            for (int i = 0; i<timeCounter; i++)
            {
                infectedNumber[i] = 1;
            }
        }
    }
}