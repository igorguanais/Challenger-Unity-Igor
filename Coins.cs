using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private GameObject coin, canvas;
    private float time, velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 1.8f;
        coin = GameObject.FindGameObjectWithTag("CoinFinal");
    }

    //Esse script em si tem o objetivo de movimentar as moedas ate a hud e deletar elas
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, coin.transform.position, velocity * Time.deltaTime);

        if (transform.position.x <= coin.transform.position.x + 0.2f && transform.position.x >= coin.transform.position.x - 0.2f
            && transform.position.y <= coin.transform.position.y + 0.2f && transform.position.y >= coin.transform.position.y - 0.2f)
        {
            Destroy(gameObject);
        }
    }

}
