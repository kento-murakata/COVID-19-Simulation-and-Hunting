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
    private Dictionary<string, float> inspectionNumDictionary;

    //
    private float dayTime = 3600 * 24f; // GameManager.dayTime; Need to replace the valiable Kawasaki-san made.
    private float infectionCountTimer; // Infection.beInfectionNegativeTime Need to use the valiable form Taba-san


    #endregion


    #region Constructor
    public infectedDataBase()
    {
        inspectionNumDictionary = new Dictionary<string, float>();
        inspectionNumDictionary.Add("January", 0);
        inspectionNumDictionary.Add("February", 0);
        inspectionNumDictionary.Add("March", 0);
        inspectionNumDictionary.Add("April", 0);
        inspectionNumDictionary.Add("May", 0);
        inspectionNumDictionary.Add("June", 0);
        inspectionNumDictionary.Add("July", 0);
        inspectionNumDictionary.Add("August", 0);
        inspectionNumDictionary.Add("September", 0);
        inspectionNumDictionary.Add("Octorber", 0);
        inspectionNumDictionary.Add("Novenmber", 0);
        inspectionNumDictionary.Add("December", 0);
    }
    //Initialization the dictionary


    #endregion


    #region Properties
    public Dictionary<string, float> InspectionNumDictionary
    {
        get { return inspectionNumDictionary; }
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

    private GameObject[]  SearchHuman()
    {
        GameObject[] humanArr = GameObject.FindGameObjectsWithTag("Human");

        return humanArr;
    }
    public void MonthlyNewInfector(GameObject[] humanArr)
    {
        for (int i = 0; i <= humanArr.Length; i++)
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
    }

    #endregion
}






