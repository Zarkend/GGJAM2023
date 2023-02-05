using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreUI;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.text = Score.ScoreNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
