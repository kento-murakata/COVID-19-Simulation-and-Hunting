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

    private List<Collider> contactPersons = new List<Collider>();

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

    public List<Collider> ContactPersonList
    {
        get { return contactPersons; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            if (!contactPersons.Contains(other))
            {
                contactPersons.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            if (contactPersons.Contains(other))
            {
                contactPersons.Remove(other);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = directionColor;
        foreach (var pos in contactPersons)
        {
            var currentPos = humanObj.transform.position;
            var targetPos = pos.transform.position;

            Gizmos.DrawLine(currentPos, targetPos);
        }
    }
}
