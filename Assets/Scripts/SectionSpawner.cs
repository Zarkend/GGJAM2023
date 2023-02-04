using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SectionSpawner : MonoBehaviour
{
    [SerializeField]
    private Section sectionPrefab;

    [SerializeField]
    private int initialSections;

    [SerializeField]
    private List<Transform> obstacles = new List<Transform>();

    private Queue<Section> sectionQueue;

    private void Awake()
    {
        sectionQueue = new Queue<Section>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialSections();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnInitialSections()
    {
        for (int i = 0; i < initialSections; i++)
        {
            SpawnSection();
        }
    }

    private void SpawnSection()
    {
        float spawnPositionZ = sectionQueue.Any() ? sectionPrefab.DepthSize + sectionQueue.Last().transform.position.z : sectionPrefab.DepthSize;

        Section instance = Instantiate(sectionPrefab, new Vector3(0, sectionPrefab.transform.position.y, spawnPositionZ), Quaternion.identity);

        instance.AddObstacle(GetRandomElement(obstacles));

        instance.Disabled += OnSectionDisabled;

        sectionQueue.Enqueue(instance);
    }

    private void OnSectionDisabled()
    {
        var instance = sectionQueue.Dequeue();

        instance.Disabled -= OnSectionDisabled;

        SpawnSection();
    }

    private static Random _random = new Random();

    public T GetRandomElement<T>(List<T> list)
    {
        int index = _random.Next(list.Count);
        return list.ElementAtOrDefault(index);
    }
}
