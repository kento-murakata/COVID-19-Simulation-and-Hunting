using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private bool isPolicyAClick = false;
    private bool isPolicyBClick = false;
    private bool isPauseResumClick = false;
    public bool isStartClick = false;

    //Creat dataoutput Button
    public void onDataClick()
    {
        Debug.Log("DataOutput!!");
        GameObject.Find("GameManager").GetComponent<GameManager>().dataOutput("testfile");
    }

    //Creat PolicyA Button to Apply and Rest PolicyA
    public void onPolicyAClick()
    {
        if (isPolicyAClick == false)
        {
            Debug.Log("ApplyPolicyA!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().applyPolicy(covidpolicy.PolicyA);
            GameObject.Find("PolicyA").GetComponent<Image>().color =Color.black;
            GameObject.Find("T_PolicyA").GetComponent<Text>().color =Color.white;
            GameObject.Find("T_PolicyA").GetComponent<Text>().text = "Stop";
            isPolicyAClick = true;
        }
        else 
        {
            Debug.Log("Reset PolicyA!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().resetPolicy(covidpolicy.PolicyA);
            GameObject.Find("PolicyA").GetComponent<Image>().color = Color.white;
            GameObject.Find("T_PolicyA").GetComponent<Text>().color = Color.black;
            GameObject.Find("T_PolicyA").GetComponent<Text>().text = "Forced Mask";
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
            GameObject.Find("PolicyB").GetComponent<Image>().color = Color.black;
            GameObject.Find("T_PolicyB").GetComponent<Text>().color = Color.white;
            GameObject.Find("T_PolicyB").GetComponent<Text>().text = "Stop";
            isPolicyBClick = true;
        }
        else
        {
            Debug.Log("Reset PolicyB!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().resetPolicy(covidpolicy.PolicyB);
            GameObject.Find("PolicyB").GetComponent<Image>().color = Color.white;
            GameObject.Find("T_PolicyB").GetComponent<Text>().color = Color.black;
            GameObject.Find("T_PolicyB").GetComponent<Text>().text = "Behavioural Restriction";
            isPolicyBClick = false;
        }
    }

    //Creat Pause&Resum Button to Pause&Resum the Scene
    public void onPauseResumClick()
    {
        if (isPauseResumClick == false)
        {
            Debug.Log("Pause!");
            Time.timeScale = 0;
            GameObject.Find("Pause&ResumButton").GetComponent<Image>().color = Color.black;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().color = Color.white;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().text = "Resum";
            isPauseResumClick = true;
        }
        else
        {
            Debug.Log("Resum!!");
            Time.timeScale = 1;
            GameObject.Find("Pause&ResumButton").GetComponent<Image>().color = Color.white;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().color = Color.black;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().text = "Pasue";
            isPauseResumClick = false;
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
        Graph.MakeGraph();
        Debug.Log("Graphic Out!!");
    }

    //Creat Close Graphic Button
    public void onCloseGraphicClick()
    {
        Graph.onCloseGraphClick();
    }


}
