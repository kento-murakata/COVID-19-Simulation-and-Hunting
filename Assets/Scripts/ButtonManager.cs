using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private bool isPolicyAClick = false;
    private bool isPolicyBClick = false;

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

    //Creat PolicyA Button to Apply and Rest PolicyA
    public void onPolicyAClick()
    {
        if (isPolicyAClick == false)
        {
            Debug.Log("ApplyPolicyA!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().applyPolicy(covidpolicy.PolicyA);
            isPolicyAClick = true;
        }
        else 
        {
            Debug.Log("Reset PolicyA!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().resetPolicy(covidpolicy.PolicyA);
            isPolicyAClick = false;
        }
    }

    //Creat PolicyB Button to Apply and Rest PolicyB
    public void onPolicyBClick()
    {
        if (isPolicyBClick == false)
        {
            Debug.Log("ApplyPolicyB!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().applyPolicy(covidpolicy.PolicyB);
            isPolicyBClick = true;
        }
        else
        {
            Debug.Log("Reset PolicyB!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().resetPolicy(covidpolicy.PolicyB);
            isPolicyBClick = false;
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
        //Graph.MakeGraph();
        Debug.Log("Graphic Out!!");
    }

    //Creat Close Graphic Button
    public void onCloseGraphicClick()
    {
        //Graph.onCloseGraphClick();
    }


}
