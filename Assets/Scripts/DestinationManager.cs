using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    [System.Serializable]
    public class Destination
    {
        public int number;
        public Vector3 position;
        public GameObject obj;
        public Color color;
    }

    [SerializeField]
    private GameObject destinationObj;

    public int numberOfDestinations = 5;
    public List<Destination> destinations = new List<Destination>();

    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < numberOfDestinations; i++)
        {
            Destination destination = new Destination();
            destination.number = i;
            destination.position = gameManager.LocationDeter();
            destination.color = new Color(Random.value, Random.value, Random.value, 1.0f);
            destinations.Add(destination);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
