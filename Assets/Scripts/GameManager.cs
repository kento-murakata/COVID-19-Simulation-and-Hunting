using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum covidpolicy
{
    PolicyA,
    PolicyB,
}

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
    public Color stage1Color = Color.cyan;
    public Color stage2Color = Color.yellow;
    public Color stage3Color = Color.red;
    public Color stage4Color = Color.black;
    public float moveDuration = 5.0f;
    public float detectRadius = 0.5f;
    public float collisionHoldingTime = 1.0f;
    public float healthPoint = 100f;

    //define control parameter
    private float simCount = 0;
    public List<GameObject> PersonList { get; set; }
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
    public Text textper;

    /////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        personClone(prefabs, amount, ratioOfHealth);
        initializeText();
        checkPolicy(covidpolicy.PolicyA);
        checkPolicy(covidpolicy.PolicyB);
    }

    private void Update()
    {
        updateText();
        checkSimTime();
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
            clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = randomBool();
            clonePersons.GetComponent<HumanBehaviour>().IsBehaviouralRestriction = randomBool();
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
            clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = randomBool();
            clonePersons.GetComponent<HumanBehaviour>().IsBehaviouralRestriction = randomBool();
            personList.Add(clonePersons);
        }
    }

    //Creat a Random Position 
    //size is depend on the plane existed
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

    //Creat a Random Boolean
    private bool randomBool()
    {
        int result = Random.Range(0, 2);
        if (result == 0) { return false; }
        else { return true; }
    }

    //Count number of each Healthstatus
    private int checkStatus(HealthStatus health)
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

    //Caculate Ratio of Each Policy
    public float checkPolicy(covidpolicy policy)
    {
        int number = 0;
        float ratio;
        switch (policy)
        {
            case covidpolicy.PolicyA:
                for (int i = 0; i < personList.Count; i++)
                {
                    if (personList[i].GetComponent<HumanBehaviour>().IsFaceMask == true)
                    {
                        number++;
                    }
                }
                break;
            case covidpolicy.PolicyB:
                for (int i = 0; i < personList.Count; i++)
                {
                    if (personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction == true)
                    {
                        number++;
                    }
                }
                break;
            default:
                break;
        }
        ratio = number / amount;
        Debug.Log(policy+": " + ratio);
        return ratio;
    }

    //Check the SimTime
    private void checkSimTime() 
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

    //Initialize the TextContent
    private void initializeText()
    {
        stage3Num = checkStatus(HealthStatus.infectionPositive);
        stage4Num = checkStatus(HealthStatus.onsetAndQuarantine);
        textForStage3.text = stage3Num.ToString();
        testForStage4.text = stage4Num.ToString();
    }

    //Update the TextContent
    private void updateText()
    {
        stage3Num = checkStatus(HealthStatus.infectionPositive);
        stage4Num = checkStatus(HealthStatus.onsetAndQuarantine);
        ratio = (stage3Num + stage4Num) / amount;
        textForTotal.text = amount.ToString();
        textForStage3.text = stage3Num.ToString();
        testForStage4.text = stage4Num.ToString();
        textForPercentage.text = ratio.ToString("p1");
        totalTime.text = simCount.ToString(".0");
        if (ratio > 0.5)
        {
            textForPercentage.color = Color.red;
            textper.color = Color.red;
        }
    }

    //Apply the Policy
    public void applyPolicy(covidpolicy policy)
    {
        switch (policy)
        {
            case covidpolicy.PolicyA:
                for (int i = 0; i < personList.Count; i++)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsFaceMask = true;
                }
                checkPolicy(covidpolicy.PolicyA);
                break;
            case covidpolicy.PolicyB:
                for (int i = 0; i < personList.Count; i++)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = true;
                }
                checkPolicy(covidpolicy.PolicyB);
                break;
            default:
                break;
        }
    }


    //Reset Policy
    public void resetPolicy(covidpolicy policy) 
    {
        switch (policy)
        {
            case covidpolicy.PolicyA:
                for (int i = 0; i < personList.Count; i++)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsFaceMask = randomBool();
                }
                checkPolicy(covidpolicy.PolicyA);
                break;
            case covidpolicy.PolicyB:
                for (int i = 0; i < personList.Count; i++)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = randomBool();
                }
                checkPolicy(covidpolicy.PolicyB);
                break;
            default:
                break;
        }

    }

    //Output data to jsonfile
    public void dataOutput(string filename)
    {
        DataSave data = new DataSave();
        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + filename + ".json", false);
        for (int i = 0; i < personList.Count; i++)
        {
            data.id = i;
            data.healthstatus = (personList[i].GetComponent<HumanBehaviour>().healthStatus).ToString();
            data.IsFaceMask = personList[i].GetComponent<HumanBehaviour>().IsFaceMask;
            data.IsBehaviouralRestriction = personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction;
            string json = JsonUtility.ToJson(data);
            writer.WriteLine(json);
        }
        writer.Flush();
        writer.Close();
        Debug.Log("end of output");
    }


    //For debug
    private void debugCount()
    {
        for (int i = 0; i < personList.Count; i++)
        {
            Debug.Log("Person" + i +" IsMask: " + personList[i].GetComponent<HumanBehaviour>().IsFaceMask);
            Debug.Log("Person" + i + " IsBehaviouralRestriction: " + personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction);
        }

    }
}

