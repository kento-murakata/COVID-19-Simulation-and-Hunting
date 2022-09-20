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
            int infector = 0;//GameMangerから感染者数持ってきて置き換える。
            int timeCounter = 100; //日ごとか月ごとか何かしらの区切り要素？

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