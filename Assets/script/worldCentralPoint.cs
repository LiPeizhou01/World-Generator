using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldCentralPoint : MonoBehaviour
{
    private static worldCentralPoint _instance;

    public static worldCentralPoint Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = (worldCentralPoint)FindObjectOfType(typeof(worldCentralPoint));
            }
            return _instance; 
        }
    }

    public GameObject block;

    public Transform CentralPoint
    {
        set;
        get;
    }

    public Vector3 centralPoint;

    public void WorldCentralPointSetter()
    {
        float standardXScale = block.transform.localScale.x;
        float standardYScale = block.transform.localScale.y;

        CentralPoint = transform.GetComponent<Transform>();
        CentralPoint.transform.position = new Vector3(((MapGenerator.Instance.mapWidth - 1) * MapGenerator.Instance.magnification + 1) / 2 * standardXScale, 0, ((MapGenerator.Instance.mapLength - 1) * MapGenerator.Instance.magnification + 1) / 2 * standardYScale);
        centralPoint = CentralPoint.transform.position;
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
