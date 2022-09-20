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
    //TODO move stageObj to GameManager
    [SerializeField]
    private GameObject stageObj;

    [SerializeField]
    private Color directionColor = Color.blue;

    [SerializeField]
    private float moveDuration = 5.0f;

    [SerializeField]
    private float collisionHoldingTime = 1.0f;

    [SerializeField]
    private float userHealthPoint = 100;

    [SerializeField]
    private float detectRadius = 0.5f;

    private Rigidbody rBody;
    private NavMeshAgent m_navMesh;
    private HumanDetector m_detector;
    private Renderer m_bodyRenderer;

    private float maxPositionX;
    private float minPositionX;
    private float maxPositionZ;
    private float minPositionZ;

    public HealthStatus healthStatus
    {
        get
        {
            Infection infection = new Infection();
            HumanDetector detector = new HumanDetector();

            return infection.Test(this, detector.ContactHumans);
        }
    }

    private void Awake()
    {
        SettingNavMeshAgent();
        SettingHumanDetector();

        rBody = gameObject.AddComponent<Rigidbody>();
        rBody.isKinematic = true;
    }

    private void Start()
    {
        DeployObject();
    }

    private void Update()
    {
        SetDestination();
        ChangeBodyColor(healthStatus);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Human"))
        {
            m_navMesh.ResetPath();
        }
    }

    // TODO setting NavMeshAgent
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

    private void ChangeBodyColor(HealthStatus status)
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