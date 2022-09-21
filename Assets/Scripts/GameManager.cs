using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour
    {
    //user input
    public float amount;
    public float ratioOfHealth;
    public float simTime;

    //define prefabs
    [SerializeField]
    private GameObject prefabs;

    //define const
    private float mintime = 60f;
    private float hourtime = 3600f;
    private float daytime = 3600 * 24f;

    //define parameter of human behavior
    public float MoveDuration { get { return moveDuration; } }
    public float DetectRadius { get { return detectRadius; } }
    public float CollisionHoldingTime { get { return collisionHoldingTime; } }
    public float HealthPoint { get { return healthPoint; } }

    private float moveDuration = 5.0f;
    private float detectRadius = 0.5f;
    private float collisionHoldingTime = 1.0f;
    private float healthPoint = 100f;

    //define control parameter
    private float simCount = 0;
    private List<GameObject> personList = new List<GameObject>();
    
    /////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        personClone(prefabs, amount, ratioOfHealth);
    }

    private void Update()
    {
        if (simCount < simTime)
        {
            simCount += Time.deltaTime;
        }
        else
        {
            simCount = 0;
            SceneManager.LoadScene("Graphic");
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////

    //Creat the instance from Prefabs
    private void personClone(GameObject prefabs, float amount, float ratio)
    {
        for (int i = 0; i < Mathf.Ceil(amount * ratio); i++)
        {
            Vector3 appearPos = LocationDeter();
            GameObject clonePersons = Instantiate(prefabs, appearPos, Quaternion.identity);
            //gameobjectの状態取得方法×平野さん：clonePersons.GetComponent<HumanBehaviour>().healthStatus = HumanBehaviour.HealthStatus.negative;
            personList.Add(clonePersons);
        }
        for (int i = 0; i < (amount - Mathf.Ceil(amount * ratio)); i++)
        {
            Vector3 appearPos = LocationDeter();
            GameObject clonePersons = Instantiate(prefabs, appearPos, Quaternion.identity);
            //gameobjectの状態取得方法×平野さん：clonePersons.GetComponent<HumanBehaviour>().healthStatus = (HumanBehaviour.HealthStatus)Random.Range(1, 3);
            personList.Add(clonePersons);
        }
    }

    //Creat a Random Position
    public Vector3 LocationDeter()
    {
        float maxPos = 20.0f;
        float minPos = -20.0f;

        Vector3 randomPos = new Vector3(
            Random.Range(minPos, maxPos),
            1,
            Random.Range(minPos, maxPos)
            );
        return randomPos;
    }
}

