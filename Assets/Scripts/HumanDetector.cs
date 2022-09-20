using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDetector : MonoBehaviour
{
    [SerializeField]
    private Color directionColor = Color.red;

    private new Rigidbody rigidbody;
    private new SphereCollider collider;
    private float detectRadius = 0.5f;

    //TODO change from List to Array
    private List<GameObject> contactHumans = new List<GameObject>();

    private GameObject humanObj;

    public float DetectRadius
    {
        get { return detectRadius; }
        set
        {
            if (value > 0)
            {
                detectRadius = value;
            }
            else
            {
                detectRadius = 0;
            }
        }
    }

    public List<GameObject> ContactHumans
    {
        get { return contactHumans; }
    }

    private void Awake()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectRadius;
    }

    private void Start()
    {
        humanObj = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            if (!contactHumans.Contains(other.gameObject))
            {
                contactHumans.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            if (contactHumans.Contains(other.gameObject))
            {
                contactHumans.Remove(other.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = directionColor;
        foreach (var pos in contactHumans)
        {
            var currentPos = humanObj.transform.position;
            var targetPos = pos.transform.position;

            Gizmos.DrawLine(currentPos, targetPos);
        }
    }
}
