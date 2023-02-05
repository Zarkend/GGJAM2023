using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public static int ScoreNumber;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(IncreaseScore), 0, 0.5f);
        ScoreNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void IncreaseScore()
    {
        ScoreNumber += 10;
        scoreText.text = ScoreNumber.ToString();
    }
}
