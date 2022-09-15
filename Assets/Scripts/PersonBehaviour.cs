using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonBehaviour : MonoBehaviour
{
    public GameObject person;
    public float maxPositionX = 25.0f;
    public float minPositionX = -25.0f;
    public float maxPositionZ = 25.0f;
    public float minPositionZ = -25.0f;
    public float randomTime = 10.0f;

    private Vector3 personPosition;
    private Vector3 targetPosition;
    private float time;


    private void Awake()
    {
        targetPosition = new Vector3 (
            Random.Range(minPositionX,maxPositionX),
            1,
            Random.Range(minPositionZ,maxPositionZ));
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time <= randomTime)
        {
            personPosition = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                0.01F);
        }
        else
        {
            time = 0;
            targetPosition = new Vector3(
            Random.Range(minPositionX, maxPositionX),
            1,
            Random.Range(minPositionZ, maxPositionZ));
        }
        transform.position = personPosition;

        transform.position = personPosition;

    }
   
}
