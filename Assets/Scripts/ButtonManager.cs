using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private GameManager gameManager;
    private Image buttonA;
    private Image buttonB;

    private bool isPolicyA = false;
    private bool isPolicyB = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buttonA = GameObject.Find("PolicyA").GetComponent<Image>();
        buttonB = GameObject.Find("PolicyB").GetComponent<Image>();
    }

    //Creat ScenceChange Button
    public void onScenceChangeClick()
    {
        //SceneManager.LoadScene("Graphic");
    }

    //Creat dataoutput Button
    public void onDataClick()
    {
        Debug.Log("DataOutput!!");
    }

    //Creat PolicyA Button
    public void onPolicyAClick()
    {
        Debug.Log("PolicyA!!");

        isPolicyA = !isPolicyA;

        if (isPolicyA)
        {
            buttonA.color = Color.red;
        }
        else
        {
            buttonA.color = Color.white;
        }

         var persons = gameManager.PersonList;
        foreach(var person in persons)
        {
            var human = person.GetComponent<HumanBehaviour>();
            human.IsFaceMask = isPolicyA;
        }
    }

    //Creat PolicyB Button
    public void onPolicyBClick()
    {
        Debug.Log("PolicyB!!");

        isPolicyB = !isPolicyB;

        if (isPolicyB)
        {
            buttonB.color = Color.red;
        }
        else
        {
            buttonB.color = Color.white;
        }

        var persons = gameManager.PersonList;
        foreach (var person in persons)
        {
            var human = person.GetComponent<HumanBehaviour>();
            human.IsBehaviouralRestriction = isPolicyB;
        }
    }

    //Creat PolicyC Button
    public void onPolicyCClick()
    {
        Debug.Log("PolicyC!!");
    }

    //Creat Graphic Button
    public void onGraphicClick()
    {
        Debug.Log("Graphic Out!!");
    }

    
}
