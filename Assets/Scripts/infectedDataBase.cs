using System;
using System.Collections.Generic;
using UnityEngine;

public static class infectedDataBase
{
    #region Fields

    //Declare the each month's end time
    private static float janEndTime;
    private static float febEndTime;
    private static float marEndTime;
    private static float aprEndTime;
    private static float mayEndTime;
    private static float junEndTime;
    private static float julEndTime;
    private static float augEndTime;
    private static float sepEndTime;
    private static float octEndTime;
    private static float novEndTime;
    private static float decEndTime;

    private static float dayTime = 1.0f; //1day in Unity = 1sec in real

    //Declare the number of new patients of each month
    private static float newInfectorOfJan;
    private static float newInfectorOfFeb;
    private static float newInfectorOfMar;
    private static float newInfectorOfApr;
    private static float newInfectorOfMay;
    private static float newInfectorOfJun;
    private static float newInfectorOfJul;
    private static float newInfectorOfAug;
    private static float newInfectorOfSep;
    private static float newInfectorOfOct;
    private static float newInfectorOfNov;
    private static float newInfectorOfDec;
    private static Dictionary<string, float> infectionNumDictionary;

    #endregion

    #region Constructor

    //Initialize the dictionary
    static infectedDataBase()
    {        
        infectionNumDictionary = new Dictionary<string, float>();
        infectionNumDictionary.Add("January", 0);
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

    #endregion

    #region Properties

    public static Dictionary<string, float> InfectionNumDictionary
    {
        get { return infectionNumDictionary; }
    }
    #endregion

    #region Methods

    //Set the end of each month
    private static void MonthSetting()
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

    //Count  patients of each month
    public static void MonthlyNewInfector(float beInfectionNegativeTime)
    {
        if (beInfectionNegativeTime <= janEndTime)
        {
            newInfectorOfJan++;
            infectionNumDictionary["January"] = newInfectorOfJan;
        }

        else if (beInfectionNegativeTime <= febEndTime)
        {
            newInfectorOfFeb++;
            infectionNumDictionary["February"] = newInfectorOfFeb;
        }

        else if (beInfectionNegativeTime <= marEndTime)
        {
            newInfectorOfMar++;
            infectionNumDictionary["March"] = newInfectorOfMar;
        }

        else if (beInfectionNegativeTime <= aprEndTime)
        {
            newInfectorOfApr++;
            infectionNumDictionary["April"] = newInfectorOfApr;
        }

        else if (beInfectionNegativeTime <= mayEndTime)
        {
            newInfectorOfMay++;
            infectionNumDictionary["May"] = newInfectorOfMay;
        }

        else if (beInfectionNegativeTime <= junEndTime)
        {
            newInfectorOfJun++;
            infectionNumDictionary["June"] = newInfectorOfJun;
        }

        else if (beInfectionNegativeTime <= julEndTime)
        {
            newInfectorOfJul++;
            infectionNumDictionary["July"] = newInfectorOfJul;
        }

        else if (beInfectionNegativeTime <= augEndTime)
        {
            newInfectorOfAug++;
            infectionNumDictionary["August"] = newInfectorOfAug;
        }

        else if (beInfectionNegativeTime <= sepEndTime)
        {
            newInfectorOfSep++;
            infectionNumDictionary["September"] = newInfectorOfSep;
        }

        else if (beInfectionNegativeTime <= octEndTime)
        {
            newInfectorOfOct++;
            infectionNumDictionary["Octorber"] = newInfectorOfOct;
        }

        else if (beInfectionNegativeTime <= novEndTime)
        {
            newInfectorOfNov++;
            infectionNumDictionary["Novenmber"] = newInfectorOfNov;
        }

        else if (beInfectionNegativeTime <= decEndTime)
        {
            newInfectorOfDec++;
            infectionNumDictionary["December"] = newInfectorOfDec;
        }
    }
}

#endregion







