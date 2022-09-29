using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Destination
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Vector3 position;

    public int Id { get { return id; } }
    public Vector3 Position { get { return position; } }

    // constructor
    public Destination(int number, Vector3 position)
    {
        this.id = number;
        this.position = position;
    }

    // copy constructor
    protected Destination(Destination other)
    {
        this.id = other.id;
        this.position = other.position;
    }
}

public class DestinationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stageObj;

    [SerializeField]
    private GameObject destinationObj;

    [SerializeField]
    private float circleRatio = 0.8f;

    [SerializeField]
    private Color destinationColor = Color.gray;

    public int MaximumNumberOfDestinations = 10;

    [SerializeField]
    private List<Destination> destinationList = new List<Destination>();
    private float radius;

    public List<Destination> DestinationList
    {
        get { return destinationList; }
    }

    private void Awake()
    {
        float xSize = stageObj.GetComponent<Renderer>().bounds.size.x;
        float zSize = stageObj.GetComponent<Renderer>().bounds.size.z;
        float diameter = xSize < zSize ? xSize * circleRatio : zSize * circleRatio;
        radius = diameter / 2;

        for (int i = 0; i < MaximumNumberOfDestinations; i++)
        {
            Destination destination = new Destination(i, SetCirclePosition(i));
            destinationList.Add(destination);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Destination destination in destinationList)
        {
            GameObject obj = Instantiate(
                destinationObj, 
                destination.Position, 
                destinationObj.transform.rotation,
                transform);
            obj.GetComponent<Renderer>().material.color = destinationColor;
        }
    }

    // set destination on a circle
    private Vector3 SetCirclePosition(int currentNum)
    {
        int maxNum = MaximumNumberOfDestinations;
        var x = radius * Mathf.Cos(2 * Mathf.PI * currentNum / maxNum);
        var z = radius * Mathf.Sin(2 * Mathf.PI * currentNum / maxNum);

        return new Vector3(x, 0, z);
    }

    //TODO set destination manualy
}
