using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Color stage1Color = Color.gray;
    public Color stage2Color = Color.yellow;
    public Color stage3Color = Color.red;
    public Color stage4Color = Color.black;
    public float moveDuration = 5.0f;
    public float detectRadius = 0.5f;
    public float collisionHoldingTime = 1.0f;
    public float healthPoint = 100f;

    //define control parameter
    private float simCount = 0;
    private List<GameObject> personList = new List<GameObject>();
    private int stage1Num = 0;
    private int stage2Num = 0;
    private int stage3Num = 0;
    private int stage4Num = 0;
    private float ratio = 0f;
    public Text textForStage3;
    public Text testForStage4;
    public Text textForTotal;
    public Text textForPercentage;
    public Text totalTime;

    public List<GameObject> PersonList { get { return personList; } }

    /////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        personClone(prefabs, amount, ratioOfHealth);
        stage3Num = checkStatus(HealthStatus.infectionPositive);
        stage4Num = checkStatus(HealthStatus.onsetAndQuarantine);
        textForStage3.text = stage3Num.ToString();
        testForStage4.text = stage4Num.ToString();
    }

    private void Update()
    {
        stage3Num = checkStatus(HealthStatus.infectionPositive);
        stage4Num = checkStatus(HealthStatus.onsetAndQuarantine);
        ratio = (stage3Num + stage4Num) / amount;
        textForTotal.text = amount.ToString();
        textForStage3.text = stage3Num.ToString();
        testForStage4.text = stage4Num.ToString();
        textForPercentage.text = ratio.ToString("p");
        totalTime.text = simCount.ToString();

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
            clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.negative);
            personList.Add(clonePersons);
        }
        for (int i = 0; i < (amount - Mathf.Ceil(amount * ratio)); i++)
        {
            Vector3 appearPos = LocationDeter();
            GameObject clonePersons = Instantiate(prefabs, appearPos, Quaternion.identity);
            int status = Random.Range(1, 3);
            switch (status) 
            {
                case 1:
                    clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.infectionNegative);
                    break;
                case 2:
                    clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.infectionPositive);
                    break;
            }
            personList.Add(clonePersons);
        }
    }

    //Creat a Random Position
    public Vector3 LocationDeter()
    {
        float maxPos = 100.0f;
        float minPos = -100.0f;

        Vector3 randomPos = new Vector3(
            Random.Range(minPos, maxPos),
            1,
            Random.Range(minPos, maxPos)
            );
        return randomPos;
    }

    //check number of each status
    public int checkStatus(HealthStatus health)
    {
        int number = 0;

        for (int i = 0; i < personList.Count; i++)
        {
            if (personList[i].GetComponent<HumanBehaviour>().healthStatus == health)
            {
                number++;
            }
        }
        return number;
    }


}

