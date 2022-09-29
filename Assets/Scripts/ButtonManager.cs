using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region field

    public bool isPolicyAClick = false;
    public bool isPolicyBClick = false;
    private bool isPauseResumClick = false;
    private bool isFastForwardClick = false;

    #endregion

    #region method

    //Creat dataoutput Button
    public void onDataClick()
    {
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
            Time.timeScale = 0;
            GameObject.Find("Pause&ResumButton").GetComponent<Image>().color = Color.black;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().color = Color.white;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().text = "Resum";
            isPauseResumClick = true;
        }
        else
        {
            Time.timeScale = 1;
            GameObject.Find("Pause&ResumButton").GetComponent<Image>().color = Color.white;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().color = Color.black;
            GameObject.Find("T_Pause&Resum").GetComponent<Text>().text = "Pause";
            isPauseResumClick = false;
        }
    }

    //Creat FastForward Button
    public void onFastForwardClick()
    {
        if (isFastForwardClick == false)
        {
            Time.fixedDeltaTime = 0.01f;
            Time.timeScale =10;
            GameObject.Find("FastForwardButton").GetComponent<Image>().color = Color.black;
            GameObject.Find("T_FastForward").GetComponent<Text>().color = Color.white;
            GameObject.Find("T_FastForward").GetComponent<Text>().text = "Stop";
            isFastForwardClick = true;
        }
        else
        {
            Time.fixedDeltaTime = 0.02f;
            Time.timeScale = 1;
            GameObject.Find("FastForwardButton").GetComponent<Image>().color = Color.white;
            GameObject.Find("T_FastForward").GetComponent<Text>().color = Color.black;
            GameObject.Find("T_FastForward").GetComponent<Text>().text = ">>";
            isFastForwardClick = false;
        }
    }

    //Creat ExitButton
    public void onExitClick()
    {
        Application.Quit();
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
    }

    //Creat Close Graphic Button
    public void onCloseGraphicClick()
    {
        Graph.onCloseGraphClick();
    }
    #endregion

}
