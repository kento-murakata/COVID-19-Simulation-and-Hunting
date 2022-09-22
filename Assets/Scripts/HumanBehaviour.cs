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
    [SerializeField]
    private Color directionColor = Color.blue; //進路可視化(デバッグ用)

    [SerializeField]
    private HealthStatus initHealthStatus = HealthStatus.negative;

    [SerializeField]
    private HealthStatus currentStatus;

    private HealthStatus preStatus;

    public bool IsFaceMask { get; set; } //マスク有無

    public bool IsBehaviouralRestriction { get; set; } //行動制限有無

    public HealthStatus healthStatus
    {
        get { return currentStatus; }
    }

    public float HealthPoint
    {
        get { return healthPoint; }
    }

    private GameManager gameManager;
    private NavMeshAgent m_navMesh;
    private HumanDetector m_detector;
    private Material m_bodyMaterial;
    private Infection infection;

    private float moveDuration = 5.0f; //最大移動距離 (行動制限有無)
    private float detectRadius = 0.5f; //他人との接触判定距離
    private float moveVelocity = 3.5f;
    private float collisionHoldingTime = 3.0f; //人同士の衝突保持時間
    private float healthPoint = 100; //HP
    private float faceMaskEffect = 2.0f; //マスク有の時の接触判定距離縮小効果
    
    private void Awake()
    {
        GetComponents();

        UpdateManager updateManager = GameObject.Find("UpdateManager").GetComponent<UpdateManager>();
        updateManager.list.Add(this);
    }

    private void Start()
    {
        SetDetectRadius(detectRadius);
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

    private void GetComponents()
    {
        // add GameManager Component
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        moveDuration = gameManager.moveDuration;
        detectRadius = gameManager.detectRadius;

        // add NavMeshAgent Component for object movement
        m_navMesh = GetComponent<NavMeshAgent>();

        // add HumanDetector Component
        m_detector = GetComponentInChildren<HumanDetector>();

        // add Infection Component
        infection = gameObject.GetComponent<Infection>();

        m_bodyMaterial = GetComponent<Renderer>().material;
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
    public void SetDetectRadius(float radius)
    {
        m_detector.DetectRadius = radius;
    }

    private void ChangeBodyColor()
    {
        if (preStatus != currentStatus)
        {
            switch (currentStatus)
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
        preStatus = currentStatus;
    }

    private IEnumerator Wait(float waitTime)
    {
        var preTime = Time.time;
        yield return new WaitForSeconds(waitTime);
        var pastTime = Time.time;
        //Debug.Log("WaitTime: " + (pastTime - preTime));
    }

    //private void OnDrawGizmos()
    //{
    //    if (m_navMesh && m_navMesh.enabled)
    //    {
    //        Gizmos.color = directionColor;
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