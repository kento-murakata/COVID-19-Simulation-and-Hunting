using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject stageObj;

    [SerializeField]
    private Color directionColor = Color.blue;

    //TODO change randomly according to character
    [SerializeField]
    private float moveDuration = 5.0f;

    [SerializeField]
    private float userHealthPoint = 100;

    // health status threshold
    public float threshold_notContagious;
    public float threshold_Contagious;
    public float threshold_isolation;

    private NavMeshAgent m_navMesh;

    //TODO move spawnRange to GameManager
    private float maxPositionX;
    private float minPositionX;
    private float maxPositionZ;
    private float minPositionZ;

    public enum UserStatus
    {
        Health,
        NotContagious,
        Contagious,
        Isolation,
    }

    public UserStatus CurrentStatus
    {
        get
        {
            //感染_伝染不可
            if(userHealthPoint < threshold_notContagious)
            {
                return UserStatus.NotContagious;
            }
            //感染_伝染可能
            if (userHealthPoint < threshold_Contagious)
            {
                return UserStatus.Contagious;
            }
            //発症隔離
            if (userHealthPoint < threshold_isolation)
            {
                return UserStatus.Isolation;
            }
            //健康
            else
            {
                return UserStatus.Health;
            }
        }
    }

    private void Awake()
    {
        m_navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
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

    private void Update()
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
