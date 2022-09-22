using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDetector : MonoBehaviour
{
    [SerializeField]
    private Color directionColor = Color.red;

    //TODO change from List to Array
    private List<GameObject> contactHumans = new List<GameObject>();

    private GameObject humanObj;
    private new SphereCollider collider;

    public float DetectRadius
    {
        get { return collider.radius; }
        set
        {
            if (value > 0)
            {
                collider.radius = value;
            }
            else
            {
                collider.radius = 0;
            }
        }
    }

    public List<GameObject> ContactHumans
    {
        get { return contactHumans; }
    }

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        humanObj = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human") && humanObj != other.gameObject)
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = directionColor;
    //    foreach (var pos in contactHumans)
    //    {
    //        var currentPos = humanObj.transform.position;
    //        var targetPos = pos.transform.position;

    //        Gizmos.DrawLine(currentPos, targetPos);
    //    }
    //}
}
