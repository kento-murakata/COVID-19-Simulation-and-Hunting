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

public class HumanBehaviour : MonoBehaviour
{
    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private GameObject stageObj; //インスタンス生成座標特定用

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float moveDuration = 5.0f; //最大移動距離

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float detectRadius = 0.5f; //他人との接触判定距離

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float collisionHoldingTime = 1.0f; //人同士の衝突保持時間

    //TODO GameManagerから取得するように変更予定
    [SerializeField]
    private float healthPoint = 100; //HP

    public bool IsFaceMask { get; set; } //マスク有無
    public bool IsBehaviouralRestriction { get; set; } //行動制限有無

    [SerializeField]
    private Color directionColor = Color.blue; //進路可視化(デバッグ用)

    private Rigidbody rBody;
    private NavMeshAgent m_navMesh;
    private HumanDetector m_detector;
    private Renderer m_bodyRenderer;

    private HealthStatus currentStatus;
    private HealthStatus preStatus;

    private Infection infection;
    private HumanDetector detector;

    private float maxPositionX;
    private float minPositionX;
    private float maxPositionZ;
    private float minPositionZ;

    public HealthStatus healthStatus
    {
        get { return currentStatus; }
    }

    private void Awake()
    {
        SettingNavMeshAgent();
        SettingHumanDetector();
        SettingRigidBody();
    }

    private void Start()
    {
        infection = new Infection();
        detector = new HumanDetector();

        DeployObject();
    }

    private void Update()
    {
        SetDestination();

        CheckHealthStatus();

        //Debug.Log(healthStatus);

        //ChangeBodyColor(healthStatus);
    }

    //人同士の衝突時に進路変更を実施
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Human"))
        {
            m_navMesh.ResetPath();
        }
    }

    // setting NavMeshAgent
    private void SettingNavMeshAgent()
    {
        // add NavMeshAgent Component
        m_navMesh = gameObject.AddComponent<NavMeshAgent>();
        m_navMesh.speed = 1.0f;
    }

    private void SettingHumanDetector()
    {
        m_detector = GetComponentInChildren<HumanDetector>();
        m_detector.DetectRadius = detectRadius;
    }

    private void SettingRigidBody()
    {
        rBody = gameObject.AddComponent<Rigidbody>();
        rBody.isKinematic = true;
    }

    private void DeployObject()
    {
        // spawn range
        maxPositionX = stageObj.transform.position.x + stageObj.transform.lossyScale.x * 5;
        minPositionX = stageObj.transform.position.x - stageObj.transform.lossyScale.x * 5;
        maxPositionZ = stageObj.transform.position.z + stageObj.transform.lossyScale.z * 5;
        minPositionZ = stageObj.transform.position.z - stageObj.transform.lossyScale.z * 5;

        // spawn object to target position
        var randomPosition = new Vector3(
           Random.Range(minPositionX, maxPositionX),
           transform.localScale.y,
           Random.Range(minPositionZ, maxPositionZ));

        transform.position = randomPosition;
    }

    private void SetDestination()
    {
        if (m_navMesh != null)
        {
            if (!m_navMesh.hasPath)
            {
                float moveDurationX = Random.Range(-moveDuration, moveDuration);
                float moveDurationZ = Random.Range(-moveDuration, moveDuration);

                Vector3 targetPosition = new Vector3(
                   transform.position.x + moveDurationX,
                   transform.position.y,
                   transform.position.z + moveDurationZ);

                m_navMesh.isStopped = false;
                m_navMesh.SetDestination(targetPosition);
            }
        }
    }



    private void CheckHealthStatus()
    {
        currentStatus = infection.Test(this, detector.ContactHumans);
    }

    private void ChangeBodyColor(HealthStatus status)
    {
        if (preStatus != status)
        {
            m_bodyRenderer = GetComponent<Renderer>();

            switch (status)
            {
                case HealthStatus.infectionNegative:
                    m_bodyRenderer.material.color = Color.magenta;
                    break;
                case HealthStatus.infectionPositive:
                    m_bodyRenderer.material.color = Color.magenta;
                    break;
                case HealthStatus.onsetAndQuarantine:
                    m_bodyRenderer.material.color = Color.magenta;
                    break;
                default:
                    break;
            }

            preStatus = status;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
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