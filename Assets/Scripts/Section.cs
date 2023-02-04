using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Section : MonoBehaviour
{
    public event Action Disabled;

    [SerializeField]
    private List<Transform> spawnPositions;

    [SerializeField]
    private Transform spawnPositionLeft;

    [SerializeField]
    private Transform spawnPositionRight;

    [SerializeField]
    private Transform spawnPositionCenter;

    public float DepthSize => depthSize;

    [SerializeField]
    private float depthSize;

    [SerializeField]
    public static float MoveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * -MoveSpeed);
    }

    public void AddObstacle(Obstacle objectPrefab)
    {
        Transform spawnPosition = null;

        switch (objectPrefab.Position)
        {
            case ObstaclePosition.Random:
                spawnPosition = GetRandomElement(spawnPositions);
                break;
            case ObstaclePosition.Left:
                spawnPosition = spawnPositionLeft;
                break;
            case ObstaclePosition.Right:
                spawnPosition = spawnPositionRight;
                break;
            case ObstaclePosition.Center:
                spawnPosition = spawnPositionCenter;
                break;
            default:
                break;
        }

        Obstacle obstacle = Instantiate(objectPrefab, spawnPosition);

        //obstacle.transform.localEulerAngles = Vector3.zero;
    }

    public void Disable()
    {
        Disabled?.Invoke();
        Destroy(gameObject);
    }


    private static System.Random _random = new System.Random();

    public T GetRandomElement<T>(List<T> list)
    {
        int index = _random.Next(list.Count);
        return list.ElementAtOrDefault(index);
    }
}
