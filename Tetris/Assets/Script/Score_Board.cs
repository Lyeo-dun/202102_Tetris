using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Board : MonoBehaviour
{
    public Text Score;
    public static Score_Board S;
    public int score_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        Score.text = "Score : ";
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score : " + S.score_.ToString();
    }
}
