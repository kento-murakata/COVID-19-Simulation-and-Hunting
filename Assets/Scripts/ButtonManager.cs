using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //Creat ScenceChange Button
    public void onScenceChangeClick()
    {
        SceneManager.LoadScene("Graphic");
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
    }

    //Creat PolicyB Button
    public void onPolicyBClick()
    {
        Debug.Log("PolicyB!!");
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
