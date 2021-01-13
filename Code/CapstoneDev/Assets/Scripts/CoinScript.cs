using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col) {
        ScoreTextScript.coinAmount += 10;
        Destroy (gameObject);
    }
}


