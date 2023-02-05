using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    [SerializeField]
    private MoveDirection goblinMoveDirection;

    [SerializeField]
    private Goblin goblinPrefab;

    [SerializeField]
    private int spawnDelayAtStart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnGoblin());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnGoblin()
    {
        yield return new WaitForSeconds(spawnDelayAtStart);

        while (true)
        {
            var goblin = Instantiate(goblinPrefab);
            goblin.MoveDirection = goblinMoveDirection;

            goblin.transform.position = transform.position;

            yield return new WaitForSeconds(Random.Range(1, 10));
        }
    }
}
