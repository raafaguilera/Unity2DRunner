using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    private int CoinScore = 0;
    private int EnemyScore = 0;

    public Text CoinScoreText;
    public Text EnemyScoreText;
    public void AddCoinScore(int n) {
        CoinScore += n;
        CoinScoreText.text = CoinScore.ToString();
        
    }
    public void AddEnemyScore() {
        EnemyScore++;
        EnemyScoreText.text = EnemyScore.ToString();
    }

}
