using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum HealthStatus
{
    negative,
    infectionNegative,
    infectionPositive,
    onsetAndQuarantine,
}

public class HumanBehaviour : MonoBehaviour
{
    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private GameObject stageObj; //インスタンス生成座標特定用

    [SerializeField]
    private HealthStatus initHealthStatus = HealthStatus.negative;

    [SerializeField]
    private HealthStatus currentStatus;

    private HealthStatus preStatus;

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float moveDuration = 5.0f; //最大移動距離 (行動制限有無)

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float moveVelocity = 3.5f;

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float detectRadius = 0.5f; //他人との接触判定距離

    [SerializeField]
    private float faceMaskEffect = 2.0f; //マスク有の時の接触判定距離縮小効果

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float collisionHoldingTime = 3.0f; //人同士の衝突保持時間

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float healthPoint = 100; //HP

    [SerializeField]
    private Color directionColor = Color.blue; //進路可視化(デバッグ用)

    public bool IsFaceMask { get; set; } //マスク有無

    public bool IsBehaviouralRestriction { get; set; } //行動制限有無

    private GameManager gameManager;
    //private UpdateManager updateManager;
    private NavMeshAgent m_navMesh;
    private HumanDetector m_detector;
    private Rigidbody m_rBody;
    private Renderer m_bodyRenderer;
    private Infection infection;

    private float maxPositionX;
    private float minPositionX;
    private float maxPositionZ;
    private float minPositionZ;

    public HealthStatus healthStatus
    {
        get { return currentStatus; }
    }

    public float HealthPoint
    {
        get { return healthPoint; }
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        SettingHumanObject();
        SettingHumanDetector();
        SettingInfectionComponent();

        UpdateManager updateManager = GameObject.Find("UpdateManager").GetComponent<UpdateManager>();
        updateManager.list.Add(this);
    }

    private void Start()
    {
        //TODO gameManagerで実装後、削除予定
        //DeployObject(RandomPosition());
        //ChangeHealthStatus(initHealthStatus);
    }

    public void UpdateMe()
    {
        SetDestination();
        CheckHealthStatus();
        ChangeBodyColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Human"))
        {
            ChangeDestination();
        }
    }

    private void SettingHumanObject()
    {
        // add NavMeshAgent Component for object movement
        m_navMesh = GetComponent<NavMeshAgent>();

        // add Rigidbody Component for collision detection
        m_rBody = GetComponent<Rigidbody>();

        // set initial health status
        currentStatus = initHealthStatus;
    }

    private void SettingHumanDetector()
    {
        m_detector = GetComponentInChildren<HumanDetector>();
        m_detector.DetectRadius = detectRadius;
    }

    private void SettingInfectionComponent()
    {
        infection = gameObject.GetComponent<Infection>();
    }

    private void DeployObject(Vector3 generatePosition)
    {
        transform.position = generatePosition;
    }

    private Vector3 RandomPosition()
    {
        // spawn range
        maxPositionX = stageObj.transform.position.x + stageObj.transform.lossyScale.x * 5;
        minPositionX = stageObj.transform.position.x - stageObj.transform.lossyScale.x * 5;
        maxPositionZ = stageObj.transform.position.z + stageObj.transform.lossyScale.z * 5;
        minPositionZ = stageObj.transform.position.z - stageObj.transform.lossyScale.z * 5;

        return new Vector3(
           Random.Range(minPositionX, maxPositionX),
           transform.localScale.y,
           Random.Range(minPositionZ, maxPositionZ));
    }

    private void SetDestination()
    {
        if (m_navMesh != null)
        {
            if (m_navMesh.remainingDistance < 0.1f)
            {
                StartCoroutine(Wait(collisionHoldingTime));

                float moveDurationX = Random.Range(-moveDuration, moveDuration);
                float moveDurationZ = Random.Range(-moveDuration, moveDuration);

                Vector3 targetPosition = new Vector3(
                   transform.position.x + moveDurationX,
                   transform.position.y,
                   transform.position.z + moveDurationZ);

                m_navMesh.SetDestination(targetPosition);
            }
        }
    }

    //人同士の衝突時に進路変更を実施
    private void ChangeDestination()
    {
        m_navMesh.ResetPath();
    }

    private void CheckHealthStatus()
    {
        currentStatus = infection.Test(this, m_detector.ContactHumans);
    }

    public void ChangeHealthStatus(HealthStatus status)
    {
        currentStatus = status;
    }

    private void ChangeBodyColor()
    {
        if (preStatus != currentStatus)
        {
            m_bodyRenderer = GetComponent<Renderer>();

            switch (currentStatus)
            {
                case HealthStatus.infectionNegative:
                    m_bodyRenderer.material.color = Color.magenta;
                    //m_bodyRenderer.material.color = gameManager.stage2Color;
                    break;
                case HealthStatus.infectionPositive:
                    m_bodyRenderer.material.color = Color.red;
                    //m_bodyRenderer.material.color = gameManager.stage3Color;
                    break;
                case HealthStatus.onsetAndQuarantine:
                    m_bodyRenderer.material.color = Color.gray;
                    //m_bodyRenderer.material.color = gameManager.stage4Color;
                    break;
                default:
                    m_bodyRenderer.material.color = Color.cyan;
                    //m_bodyRenderer.material.color = gameManager.stage1Color;
                    break;
            }
        }
        preStatus = currentStatus;
    }

    private IEnumerator Wait(float waitTime)
    {
        var preTime = Time.time;
        yield return new WaitForSeconds(waitTime);
        var pastTime = Time.time;
        Debug.Log("WaitTime: " + (pastTime - preTime));
    }


    private void OnDrawGizmos()
    {
        if (m_navMesh && m_navMesh.enabled)
        {
            Gizmos.color = directionColor;
            var prePos = transform.position;
            prePos.y = 0;
            foreach (var pos in m_navMesh.path.corners)
            {
                Gizmos.DrawLine(prePos, pos);
                prePos = pos;
            }
        }
    }
}