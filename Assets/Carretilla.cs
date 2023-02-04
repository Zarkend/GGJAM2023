using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carretilla : MonoBehaviour
{
    [SerializeField]
    private HealthBar nameplatePrefab;

    private HealthBar _nameplateInstance;

    // Start is called before the first frame update
    void Start()
    {
        _nameplateInstance = Instantiate(nameplatePrefab, GameObject.Find("Nameplates").transform);

        _nameplateInstance.Initialize(10);

        _nameplateInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        transform.DOShakePosition(0.2f, 0.15f, 10, 50).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
