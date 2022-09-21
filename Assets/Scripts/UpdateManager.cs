using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public List<HumanBehaviour> list = new List<HumanBehaviour>();

    //private HumanBehaviour[] array;

    //private void Start()
    //{
    //    array = list.ToArray();
    //}

    //Update is called once per frame
    void Update()
    {
        var count = list.Count;
        for (var i = 0; i < count; i++)
        {
            list[i].UpdateMe();
        }
    }
}