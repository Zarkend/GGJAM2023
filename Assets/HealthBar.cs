using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI healthNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(int health)
    {
        slider.maxValue = health;

        SetHealth(health);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        healthNumber.text = health.ToString();
    }
}
