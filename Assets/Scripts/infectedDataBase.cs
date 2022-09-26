using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class infectedDataBase : MonoBehaviour
{

    #region Fields

    //Declare the each end time
    private float janEndTime;
    private float febEndTime;
    private float marEndTime;
    private float aprEndTime;
    private float mayEndTime;
    private float junEndTime;
    private float julEndTime;
    private float augEndTime;
    private float sepEndTime;
    private float octEndTime;
    private float novEndTime;
    private float decEndTime;

    //
    private float dayTime = (1 / 86400); // GameManager.adjTimescale; Need to replace the valiable Kawasaki-san made.

    //Declare the number of new patients of each month
    private float newInfectorOfJan;
    private float newInfectorOfFeb;
    private float newInfectorOfMar;
    private float newInfectorOfApr;
    private float newInfectorOfMay;
    private float newInfectorOfJun;
    private float newInfectorOfJul;
    private float newInfectorOfAug;
    private float newInfectorOfSep;
    private float newInfectorOfOct;
    private float newInfectorOfNov;
    private float newInfectorOfDec;

    //Declare the dictionaluy has the number of new patients of each month
    private Dictionary<string, float> infectionNumDictionary;

    //Declare the dictionary of the infected area
    //private Dictionary<string, float> infectedAreaDictionary;

    //For area color map
    //private float area1;
    //private float area2;
    //private float area3;
    //private float area4;
    //private float area5;
    //private float area6;
    //private float area7;
    //private float area8;
    //private float area9;

    #endregion

    #region Constructor

    //Initialization the Monthly infector dictionary
    public infectedDataBase()
    {
        Debug.Log("CALL!!!!!!!!!!!!!");
        infectionNumDictionary = new Dictionary<string, float>();
        infectionNumDictionary.Add("January", 5);
        infectionNumDictionary.Add("February", 0);
        infectionNumDictionary.Add("March", 0);
        infectionNumDictionary.Add("April", 0);
        infectionNumDictionary.Add("May", 0);
        infectionNumDictionary.Add("June", 0);
        infectionNumDictionary.Add("July", 0);
        infectionNumDictionary.Add("August", 0);
        infectionNumDictionary.Add("September", 0);
        infectionNumDictionary.Add("Octorber", 0);
        infectionNumDictionary.Add("Novenmber", 0);
        infectionNumDictionary.Add("December", 0);
        MonthSetting();
    }

    //Initialization the infector area dictionary
    //public infectedAreaDataBase() //クラス起こす？
    //{
    //    infectedAreaDictionary = new Dictionary<string, float>();
    //    infectedAreaDictionary.Add("Area1", 5);
    //    infectedAreaDictionary.Add("Area2", 0);
    //    infectedAreaDictionary.Add("Area3", 0);
    //    infectedAreaDictionary.Add("Area4", 0);
    //    infectedAreaDictionary.Add("Area5", 0);
    //    infectedAreaDictionary.Add("Area6", 0);
    //    infectedAreaDictionary.Add("Area7", 0);
    //    infectedAreaDictionary.Add("Area8", 0);
    //    infectedAreaDictionary.Add("Area9", 0);
    //}

    #endregion

    #region Properties
    public Dictionary<string, float> InfectionNumDictionary
    {
        get { return infectionNumDictionary; }
    }
    #endregion

    #region Methods

    private void MonthSetting()
    {
        janEndTime = dayTime * DateTime.DaysInMonth(2021, 1);
        febEndTime = janEndTime + dayTime * DateTime.DaysInMonth(2021, 2);
        marEndTime = febEndTime + dayTime * DateTime.DaysInMonth(2021, 3);
        aprEndTime = marEndTime + dayTime * DateTime.DaysInMonth(2021, 4);
        mayEndTime = aprEndTime + dayTime * DateTime.DaysInMonth(2021, 5);
        junEndTime = mayEndTime + dayTime * DateTime.DaysInMonth(2021, 6);
        julEndTime = junEndTime + dayTime * DateTime.DaysInMonth(2021, 7);
        augEndTime = julEndTime + dayTime * DateTime.DaysInMonth(2021, 8);
        sepEndTime = augEndTime + dayTime * DateTime.DaysInMonth(2021, 9);
        octEndTime = sepEndTime + dayTime * DateTime.DaysInMonth(2021, 10);
        novEndTime = octEndTime + dayTime * DateTime.DaysInMonth(2021, 11);
        decEndTime = novEndTime + dayTime * DateTime.DaysInMonth(2021, 12);
    }
    
    public void MonthlyNewInfector(GameObject[] humanArr)
    {
        //GameObject[] humanArr = SearchHuman();
        Debug.Log("Human: " + humanArr.Length);
        for (int i = 0; i < humanArr.Length; i++)
        {
            if (humanArr[i].GetComponent<Infection>().beInfectionNegativeTime != 0)
            {
                float tempbeInfectionNegativeTime =
                humanArr[i].GetComponent<Infection>().beInfectionNegativeTime;

                if (tempbeInfectionNegativeTime <= janEndTime) { newInfectorOfJan++; }
                else if (tempbeInfectionNegativeTime <= febEndTime) { newInfectorOfFeb++; }
                else if (tempbeInfectionNegativeTime <= marEndTime) { newInfectorOfMar++; }
                else if (tempbeInfectionNegativeTime <= aprEndTime) { newInfectorOfApr++; }
                else if (tempbeInfectionNegativeTime <= mayEndTime) { newInfectorOfMay++; }
                else if (tempbeInfectionNegativeTime <= junEndTime) { newInfectorOfJun++; }
                else if (tempbeInfectionNegativeTime <= julEndTime) { newInfectorOfJul++; }
                else if (tempbeInfectionNegativeTime <= augEndTime) { newInfectorOfAug++; }
                else if (tempbeInfectionNegativeTime <= sepEndTime) { newInfectorOfSep++; }
                else if (tempbeInfectionNegativeTime <= octEndTime) { newInfectorOfOct++; }
                else if (tempbeInfectionNegativeTime <= novEndTime) { newInfectorOfNov++; }
                else if (tempbeInfectionNegativeTime <= decEndTime) { newInfectorOfDec++; }
            }
        }

        infectionNumDictionary["January"] = newInfectorOfJan;
        infectionNumDictionary["February"] = newInfectorOfFeb;
        infectionNumDictionary["March"] = newInfectorOfMar;
        infectionNumDictionary["April"] = newInfectorOfApr;
        infectionNumDictionary["May"] = newInfectorOfMay;
        infectionNumDictionary["June"] = newInfectorOfJun;
        infectionNumDictionary["July"] = newInfectorOfJul;
        infectionNumDictionary["August"] = newInfectorOfAug;
        infectionNumDictionary["September"] = newInfectorOfSep;
        infectionNumDictionary["Octorber"] = newInfectorOfOct;
        infectionNumDictionary["Novenmber"] = newInfectorOfNov;
        infectionNumDictionary["December"] = newInfectorOfDec;

        //Debug.Log("newInfectorOfJan: " + newInfectorOfJan);
        //Debug.Log("newInfectorOfFeb: " + newInfectorOfFeb);
        //Debug.Log("newInfectorOfMar: " + newInfectorOfMar);
        //Debug.Log("newInfectorOfApr: " + newInfectorOfApr);
        //Debug.Log("newInfectorOfMay: " + newInfectorOfMay);
        //Debug.Log("newInfectorOfJun: " + newInfectorOfJun);
        //Debug.Log("newInfectorOfJul: " + newInfectorOfJul);
        //Debug.Log("newInfectorOfAug: " + newInfectorOfAug);
        //Debug.Log("newInfectorOfSep: " + newInfectorOfSep);
        //Debug.Log("newInfectorOfOct: " + newInfectorOfOct);
        //Debug.Log("newInfectorOfNov: " + newInfectorOfNov);
        //Debug.Log("newInfectorOfDec: " + newInfectorOfDec);
    }

    //public void Infectedposition(GameObject[] humanArr)
    //{
    //    for (int i = 0; i < humanArr.Length; i++)
    //    {
    //        if (humanArr[i].GetComponent<Infection>().infectedPosition != 0)
    //        {
    //            float tempbeInfectionNegativeTime =
    //            humanArr[i].GetComponent<Infection>().infectedPosition;

    //            if (infectedPosition <= janEndTime) { area1++; }
    //            else if (infectedPosition <= febEndTime) { area2++; }
    //            else if (infectedPosition <= marEndTime) { area3++; }
    //            else if (infectedPosition <= aprEndTime) { area4++; }
    //            else if (infectedPosition <= mayEndTime) { area5++; }
    //            else if (infectedPosition <= junEndTime) { area6++; }
    //            else if (infectedPosition <= julEndTime) { area7++; }
    //            else if (infectedPosition <= augEndTime) { area8++; }
    //            else if (infectedPosition <= decEndTime) { area9++; }
    //        }
    //    }

    //    infectionNumDictionary["Area1"] = newInfectorOfJan;
    //    infectionNumDictionary["Area2"] = newInfectorOfFeb;
    //    infectionNumDictionary["Area3"] = newInfectorOfMar;
    //    infectionNumDictionary["Area4"] = newInfectorOfApr;
    //    infectionNumDictionary["Area5"] = newInfectorOfMay;
    //    infectionNumDictionary["Area6"] = newInfectorOfJun;
    //    infectionNumDictionary["Area7"] = newInfectorOfJul;
    //    infectionNumDictionary["Area8"] = newInfectorOfAug;
    //    infectionNumDictionary["Area9"] = newInfectorOfSep;
    //}
}

#endregion







