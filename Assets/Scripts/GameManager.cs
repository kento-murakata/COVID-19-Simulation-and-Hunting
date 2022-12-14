using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum covidpolicy
{
    PolicyA,
    PolicyB,
}

public class GameManager : MonoBehaviour
{
    #region Field
    //user input
    public float amount;
    public float ratioOfHealth;
    public float simTime;
    //define prefabs
    public GameObject prefabs;
    //define const
    public Color stage1Color = Color.cyan;
    public Color stage2Color = Color.yellow;
    public Color stage3Color = Color.red;
    public Color stage4Color = Color.black;
    public float moveDuration = 5.0f;
    public float detectRadius = 0.5f;
    public float collisionHoldingTime = 1.0f;
    public float healthPoint = 100f;
    private float maskEffect = 0.5f;
    private float REMOVECHECKTIME = 7;
    private float RETURNCHECKTIME = 2;
    //define control parameter
    private float simCount = 0;
    private List<GameObject> personList = new List<GameObject>();
    //private int stage1Num = 0;
    //private int stage2Num = 0;
    private int stage3Num = 0;
    private int stage4Num = 0;
    private float ratio = 0f;
    private float removeCheckTime = 0;
    private float returnCheckTime = 0;
    private bool isCursorlock = true;
    public Text textForStage3;
    public Text testForStage4;
    public Text textForTotal;
    public Text textForPercentage;
    public Text totalTime;
    public Text textper;
    public InputField amoutInput;
    public InputField ratioInput;
    public InputField simTimeInput;
    public Button startSim;
    public GameObject gm_Player;
    public GameObject gm_NYBlock01;
    #endregion

    #region Property
    public List<GameObject> PersonList { get; set; }
    #endregion

    #region Method_Main
 
    private void Start()
    {
        Time.timeScale = 0;
        gm_Player = GameObject.Find("Player");
        //gm_NYBlock01 = GameObject.Find("NYBlock01");
        gm_Player.SetActive(false);
        //gm_NYBlock01.SetActive(false);
        GameObject.Find("InputUI").SetActive(true);
        startSim.onClick.AddListener(gameStart);
    }

    private void Update()
    {
        updateText();
        checkSimTime();
        checkInfected();

        if (removeCheckTime >= REMOVECHECKTIME)
        {
            checkStage4Instance();
            removeCheckTime = 0;
        }
        else
        {
            removeCheckTime += Time.deltaTime;
        }

        if (returnCheckTime >= RETURNCHECKTIME)
        {
            returnStage4Instance();
            returnCheckTime = 0;
        }
        else
        {
            returnCheckTime += Time.deltaTime;
        }

    }

    //private void FixedUpdate()
    //{
    //    UpdateCursorLock();
    //}
    #endregion

    #region Method_Sub
    //Start Scene
    public void gameStart() 
    {
            updateInput();
            initializeText();
            personClone(prefabs, amount, ratioOfHealth);
            GameObject.Find("UpdateManager").GetComponent<UpdateManager>().toStart();
            checkPolicy(covidpolicy.PolicyA);
            checkPolicy(covidpolicy.PolicyB);
            Time.timeScale = 1;
            GameObject.Find("InputUI").SetActive(false);
    }

    //Creat the instance from Prefabs
    public void personClone(GameObject prefabs, float amount, float ratio)
    {
        for (int i = 0; i < Mathf.Ceil(amount * ratio); i++)
        {
            Vector3 appearPos = LocationDeter();
            GameObject clonePersons = Instantiate(prefabs, appearPos, Quaternion.identity);
            clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.negative);
            clonePersons.GetComponent<HumanBehaviour>().IsBehaviouralRestriction = false;
            clonePersons.GetComponent<HumanBehaviour>().IsRestricted = false;
            //clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = randomBool();

            int maskStatus = Random.Range(0, 2);
            switch (maskStatus)
            {
                case 0:
                    clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = false;
                    clonePersons.GetComponent<HumanBehaviour>().DetectRadius = detectRadius;
                    break;
                case 1:
                    clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = true;
                    clonePersons.GetComponent<HumanBehaviour>().DetectRadius = detectRadius * maskEffect;
                    break;
            }

            personList.Add(clonePersons);
        }
        for (int i = 0; i < (amount - Mathf.Ceil(amount * ratio)); i++)
        {
            Vector3 appearPos = LocationDeter();
            GameObject clonePersons = Instantiate(prefabs, appearPos, Quaternion.identity);
            int healthstatus = Random.Range(1, 3);
            switch (healthstatus) 
            {
                case 1:
                    clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.infectionNegative);
                    break;
                case 2:
                    clonePersons.GetComponent<HumanBehaviour>().ChangeHealthStatus(HealthStatus.infectionPositive);
                    break;
            }
            clonePersons.GetComponent<HumanBehaviour>().IsBehaviouralRestriction = false;
            clonePersons.GetComponent<HumanBehaviour>().IsRestricted = false;
            //clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = randomBool();

            //release after hirano san pull
            int maskStatus = Random.Range(0, 2);
            switch (maskStatus)
            {
                case 0:
                    clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = false;
                    clonePersons.GetComponent<HumanBehaviour>().DetectRadius = detectRadius;
                    break;
                case 1:
                    clonePersons.GetComponent<HumanBehaviour>().IsFaceMask = true;
                    clonePersons.GetComponent<HumanBehaviour>().DetectRadius = detectRadius * maskEffect;
                    break;
            }
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
            Time.timeScale=0;
        }
    }

    //Check All Infected
    private void checkInfected() 
    {
        if (ratio >= 1 && (stage4Num == amount))
        {
            Time.timeScale = 0;
        }
    }

    //Check Stage4Instance
    private void checkStage4Instance()
    {
        for (int i = 0; i < personList.Count; i++)
        {
            if ((personList[i].GetComponent<HumanBehaviour>().healthStatus == HealthStatus.onsetAndQuarantine)
                && (personList[i].GetComponent<HumanBehaviour>().IsRestricted == false))
            {
                personList[i].GetComponent<NavMeshAgent>().Warp(new Vector3(-160, 1,0));
                personList[i].GetComponent<NavMeshAgent>().speed = 0.05f;
                personList[i].GetComponent<HumanBehaviour>().IsRestricted =true;
                personList[i].GetComponent<HumanBehaviour>().DetectRadius = 0;

            }
        }
    }

    //Return Stage4Instance
    private void returnStage4Instance()
    {
        for (int i = 0; i < personList.Count; i++)
        {
            if ((personList[i].GetComponent<HumanBehaviour>().healthStatus == HealthStatus.negative)
                && (personList[i].GetComponent<HumanBehaviour>().IsRestricted ==true))
            {
                personList[i].GetComponent<NavMeshAgent>().Warp(LocationDeter());
                personList[i].GetComponent<NavMeshAgent>().speed = 3.5f;
                personList[i].GetComponent<HumanBehaviour>().IsRestricted = false;

                if (GameObject.Find("PolicyA").GetComponent<ButtonManager>().isPolicyAClick == true)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsFaceMask = true;
                    personList[i].GetComponent<HumanBehaviour>().DetectRadius = detectRadius * maskEffect;
                }
                else
                {
                    personList[i].GetComponent<HumanBehaviour>().IsFaceMask = false;
                    personList[i].GetComponent<HumanBehaviour>().DetectRadius = detectRadius;
                }
                if (GameObject.Find("PolicyB").GetComponent<ButtonManager>().isPolicyBClick == true)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = true;
                }
                else
                {
                    personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = false;
                }
            }
        }
    }

    //Initialize the TextContent
    public void initializeText()
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
        else 
        {
            textForPercentage.color = Color.black;
            textper.color = Color.black;
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
                    if (personList[i].GetComponent<HumanBehaviour>().IsRestricted == false)
                    {
                        personList[i].GetComponent<HumanBehaviour>().IsFaceMask = true;
                        personList[i].GetComponent<HumanBehaviour>().DetectRadius = detectRadius * maskEffect;
                    }
                }
                checkPolicy(covidpolicy.PolicyA);
                break;
            case covidpolicy.PolicyB:
                for (int i = 0; i < personList.Count; i++)
                {
                    if (personList[i].GetComponent<HumanBehaviour>().IsRestricted == false)
                    { 
                        personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = true;
                    }
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
                    int maskStatus = Random.Range(0, 2);
                    switch (maskStatus)
                    {
                        case 0:
                            personList[i].GetComponent<HumanBehaviour>().IsFaceMask = false;
                            personList[i].GetComponent<HumanBehaviour>().DetectRadius = detectRadius;
                            break;
                        case 1:
                            personList[i].GetComponent<HumanBehaviour>().IsFaceMask = true;
                            personList[i].GetComponent<HumanBehaviour>().DetectRadius = detectRadius * maskEffect;
                            break;
                    }
                }
                checkPolicy(covidpolicy.PolicyA);
                break;
            case covidpolicy.PolicyB:
                for (int i = 0; i < personList.Count; i++)
                {
                    personList[i].GetComponent<HumanBehaviour>().IsBehaviouralRestriction = false;
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
            data.IsRestricted = personList[i].GetComponent<HumanBehaviour>().IsRestricted;
            string json = JsonUtility.ToJson(data);
            writer.WriteLine(json);
        }
        writer.Flush();
        writer.Close();
        Debug.Log("end of output");
    }

    //CheckInput
    public void updateInput() 
    {
        amount = float.Parse(amoutInput.GetComponent<InputField>().text.ToString());
        ratioOfHealth = float.Parse(ratioInput.GetComponent<InputField>().text.ToString());
        simTime = float.Parse(simTimeInput.GetComponent<InputField>().text.ToString());
    }

    //CurorLock When PlayerMode
    private void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorlock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            isCursorlock = true;
        }
        if (isCursorlock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!isCursorlock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion

    #region Method_Debug
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
        Debug.Log(policy + ": " + ratio);
        return ratio;
    }
    #endregion
}



