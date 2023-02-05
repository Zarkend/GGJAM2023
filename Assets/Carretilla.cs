using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carretilla : MonoBehaviour
{
    [SerializeField]
    private HealthBar nameplatePrefab;

    private HealthBar _nameplateInstance;

    [SerializeField]
    private int hits = 10;

    internal void Attack(int damage)
    {
        hits -= damage;

        _nameplateInstance.SetHealth(hits);

        if (hits <= 0)
        {
            //FINISH GAME
            SceneManager.LoadScene("FINISH");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _nameplateInstance = Instantiate(nameplatePrefab, GameObject.Find("Nameplates").transform);

        _nameplateInstance.Initialize(hits);

        _nameplateInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        transform.DOShakePosition(0.2f, 0.15f, 10, 50).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
