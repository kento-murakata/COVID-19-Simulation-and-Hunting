using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public List<HumanBehaviour> list = new List<HumanBehaviour>();

    private HumanBehaviour[] array;

    public void toStart()
    {
        array = list.ToArray();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        var count = array.Length;
        for (var i = 0; i < count; i++)
        {
            array[i].UpdateMe();
        }
    }
}