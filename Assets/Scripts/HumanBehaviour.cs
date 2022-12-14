using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum HealthStatus
{
    negative,
    infectionNegative,
    infectionPositive,
    onsetAndQuarantine,
}

[System.Serializable]
public enum BehavioralPattern
{
    home,
    work,
    fun
}

[System.Serializable]
public class MyDestination : Destination
{
    [SerializeField]
    private BehavioralPattern behavioralPattern;

    public BehavioralPattern BehavioralPattern { get { return this.behavioralPattern; } }

    //constructor
    public MyDestination(Destination destination, BehavioralPattern behavioralPattern) : base(destination)
    {
        this.behavioralPattern = behavioralPattern;
    }

    protected MyDestination(MyDestination other) : base(other)
    {
        this.behavioralPattern = other.behavioralPattern;
    }
}

public class HumanBehaviour : MonoBehaviour
{
    [SerializeField]
    private int numberOfDestinations = 3;

    [SerializeField]
    private List<MyDestination> destinations = new List<MyDestination>();

    private GameManager gameManager;
    private DestinationManager destinationManager;
    private Infection infection;

    private NavMeshAgent m_navMesh;
    private HumanDetector m_detector;
    private Material m_bodyMaterial;
    private Rigidbody m_rbody;
    private CapsuleCollider m_collider;
    private NavMeshHit navHit = new NavMeshHit();
    private Coroutine coroutine;

    private HealthStatus currentHealthStatus;
    private BehavioralPattern currentBehavioralPattern;
    private MyDestination currentDestination;

    private float healthPoint = 100;

    private float detectRadius = 0.5f;

    private bool isHit = false;
    private bool isStatusChange = false;

    //Distance to others (with/without mask)
    public float DetectRadius
    {
        get { return detectRadius; }
        set
        {
            detectRadius = value;

            //Set radius for collider for judging contact with others
            SetDetectRadius(detectRadius);
        }
    }

    //Behavioral restriction flag (when true, only basic behavior (going back and forth between home and work))
    public bool IsBehaviouralRestriction { get; set; }

    //Restriction flag
    public bool IsRestricted { get; set; }

    public HealthStatus healthStatus { get { return currentHealthStatus; } }

    public BehavioralPattern BehavioralPattern { get { return currentBehavioralPattern; } }

    public float HealthPoint { get { return healthPoint; } }

    public List<MyDestination> FunDestinations
    {
        get { return GetTargetDestinations(BehavioralPattern.fun); }
    }

    public bool IsCure { get; set; } = true;


    //TODO detele
    public bool IsFaceMask { get; set; }

    //TODO detele
    public float MoveDuration
    {
        get { return moveDuration; }
        set { moveDuration = value; }
    }

    //TODO detele
    private float moveDuration = 5.0f;



    private void Awake()
    {
        GetComponents();

        UpdateManager updateManager = GameObject.Find("UpdateManager").GetComponent<UpdateManager>();
        updateManager.list.Add(this);
    }

    private void Start()
    {
        //Select a specified number of destinations randomly from the destination list
        SetDestinations();

        //get nearest destination
        GetTargetDestinations(BehavioralPattern.home);
    }

    public void UpdateMe()
    {
        SetDestination();
        CheckHealthStatus();
        ChangeBodyColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Change of course in the event of a collision between humans
        if (other.transform.CompareTag("Human") && gameObject != other.gameObject)
        {
            if (!IsBehaviouralRestriction & !isHit)
            {
                var human_other = other.gameObject.GetComponent<HumanBehaviour>();

                var funDestinations_other = human_other.FunDestinations;

                //Check for common interests
                List<MyDestination> sameDestinations = new List<MyDestination>();
                foreach (var otherDestination in funDestinations_other)
                {
                    foreach (var myDestination in FunDestinations)
                    {
                        if (myDestination.Position == otherDestination.Position)
                        {
                            sameDestinations.Add(myDestination);
                        }
                    }
                }

                if (sameDestinations.Count > 0)
                {
                    currentDestination = GetFurthestDestination(sameDestinations);
                    currentBehavioralPattern = BehavioralPattern.fun;
                    m_navMesh.SetDestination(currentDestination.Position);
                }
            }
        }

        //When attacked by a player
        if (other.transform.CompareTag("Player"))
        {
            isHit = true;
            isStatusChange = true;
            m_navMesh.enabled = false;
            m_rbody.isKinematic = false;
            m_collider.isTrigger = false;

            var direction = (transform.position - other.transform.position).normalized;
            m_rbody.AddForce(direction * 20, ForceMode.Impulse);
        }
    }

    //private void OnEnable()
    //{
    //    coroutine = StartCoroutine(SetDestination());
    //}

    //void OnDisable()
    //{
    //    StopCoroutine(coroutine);
    //}

    private void GetComponents()
    {
        // add GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // add DestinationManager
        destinationManager = GameObject.Find("DestinationManager").GetComponent<DestinationManager>();

        // add NavMeshAgent Component for object movement
        m_navMesh = GetComponent<NavMeshAgent>();

        // add HumanDetector Component
        m_detector = GetComponentInChildren<HumanDetector>();

        // add Infection Component
        infection = gameObject.GetComponent<Infection>();

        m_bodyMaterial = GetComponent<Renderer>().material;

        m_rbody = GetComponent<Rigidbody>();

        m_collider = GetComponent<CapsuleCollider>();
    }

    private void SetDestinations()
    {
        var destinationList = destinationManager.DestinationList;
        var selectDestinations = new List<Destination>();

        //Select randomly as many as numberOfDestinations
        while (selectDestinations.Count < numberOfDestinations)
        {
            var selectDestination = destinationList[Random.Range(0, destinationList.Count)];

            if (!selectDestinations.Contains(selectDestination))
            {
                selectDestinations.Add(selectDestination);
            }
        }

        //Assign KindOdDestination
        for (int i = 0; i < numberOfDestinations; i++)
        {
            MyDestination myDestination;

            //home
            if (i == 0)
            {
                myDestination = new MyDestination(selectDestinations[i], BehavioralPattern.home);
            }

            //work
            else if (i == 1)
            {
                myDestination = new MyDestination(selectDestinations[i], BehavioralPattern.work);
            }

            //fun
            else
            {
                myDestination = new MyDestination(selectDestinations[i], BehavioralPattern.fun);
            }

            destinations.Add(myDestination);
        }
    }

    private void SetDestination()
    {
        if (m_navMesh != null)
        {
            if (!m_navMesh.isOnNavMesh)
            {

                //m_navMesh.updatePosition = false;
                //m_rbody.isKinematic = false;

                //// Navmeshとの距離が0.1になるまで待つ
                //// 距離が詰まったら、移動モードをNavmeshに切替して、Navmeshの位置を直近のNavmeshに更新
                //yield return new WaitWhile(() => NavMesh.SamplePosition(m_navMesh.transform.localPosition, out navHit, 0.1f, NavMesh.AllAreas) == false);

                //m_navMesh.Resume();
                //m_navMesh.Warp(navHit.position);
                //m_navMesh.updatePosition = true;
                //m_rbody.isKinematic = true;
            }
            else
            {
                if (!IsRestricted && !isHit)
                {
                    //If no destination is set, or if a destination is reached, the next destination is set
                    if (m_navMesh.hasPath && m_navMesh.remainingDistance < 2.1f)
                    {
                        m_navMesh.ResetPath();

                        // change destination
                        if (currentBehavioralPattern == BehavioralPattern.home)
                        {
                            currentBehavioralPattern = BehavioralPattern.work;
                        }
                        else if (currentBehavioralPattern == BehavioralPattern.work)
                        {
                            currentBehavioralPattern = BehavioralPattern.home;
                        }
                        else
                        {
                            currentBehavioralPattern = BehavioralPattern.home;
                        }
                    }

                    if (!m_navMesh.hasPath)
                    {
                        var currentDestinations = GetTargetDestinations(currentBehavioralPattern);
                        currentDestination = currentDestinations[Random.Range(0, currentDestinations.Count)];

                        //TODO error handling
                        m_navMesh.SetDestination(currentDestination.Position);
                    }
                }
            }
        }

        //TODO escape movement
    }

    //Get nearest destination
    private MyDestination GetNearestDestination(List<MyDestination> destinations)
    {
        destinations.Sort(delegate (MyDestination a, MyDestination b) {
            return Vector3.Distance(transform.position, a.Position).CompareTo(Vector3.Distance(transform.position, b.Position));
        });

        return destinations[0];
    }

    //Get furthest destination
    private MyDestination GetFurthestDestination(List<MyDestination> destinations)
    {
        destinations.Sort(delegate (MyDestination a, MyDestination b) {
            return Vector3.Distance(transform.position, b.Position).CompareTo(Vector3.Distance(transform.position, a.Position));
        });

        return destinations[0];
    }

    private List<MyDestination> GetTargetDestinations(BehavioralPattern bp)
    {
        return destinations.FindAll(x => x.BehavioralPattern == bp);
    }

    private void GetMovementStatus()
    {
        currentBehavioralPattern = currentDestination.BehavioralPattern;
    }

    private void CheckHealthStatus()
    {
        if (!isHit)
        {
            currentHealthStatus = infection.Test(this, m_detector.ContactHumans);
        }
        else
        {
            if (isStatusChange)
            {
                if (IsCure)
                {
                    if (healthStatus != HealthStatus.negative)
                    {
                        currentHealthStatus = HealthStatus.negative;
                    }
                    else
                    {
                        currentHealthStatus = HealthStatus.infectionNegative;
                    }
                }
                else
                {
                    if (healthStatus != HealthStatus.negative)
                    {
                        currentHealthStatus = HealthStatus.onsetAndQuarantine;
                    }

                }

                isStatusChange = false;
            }


            if (m_rbody.IsSleeping())
            {
                isHit = false;
                m_navMesh.enabled = true;
                m_rbody.isKinematic = true;
                m_collider.isTrigger = true;
            }
        }
    }

    public void ChangeHealthStatus(HealthStatus status)
    {
        currentHealthStatus = status;
    }
    public void SetDetectRadius(float radius)
    {
        m_detector.DetectRadius = radius;
    }

    private void ChangeBodyColor()
    {
        switch (currentHealthStatus)
        {
            case HealthStatus.infectionNegative:
                m_bodyMaterial.color = gameManager.stage2Color;
                break;
            case HealthStatus.infectionPositive:
                m_bodyMaterial.color = gameManager.stage3Color;
                break;
            case HealthStatus.onsetAndQuarantine:
                m_bodyMaterial.color = gameManager.stage4Color;
                break;
            default:
                m_bodyMaterial.color = gameManager.stage1Color;
                break;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (m_navMesh && m_navMesh.enabled && currentBehavioralPattern == BehavioralPattern.fun)
    //    {
    //        Gizmos.color = Color.blue;
    //        var prePos = transform.position;
    //        prePos.y = 0;
    //        foreach (var pos in m_navMesh.path.corners)
    //        {
    //            Gizmos.DrawLine(prePos, pos);
    //            prePos = pos;
    //        }
    //    }
    //}
}