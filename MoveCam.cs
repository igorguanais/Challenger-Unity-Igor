using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    Vector3 vectorOld;
    Vector3 vectorCurrent;
    Vector3 vectorCurrentY;
    private GameControl gControl;
    private GameObject gControlObject;

    // Start is called before the first frame update
    void Start()
    {
        vectorCurrent = new Vector3(0.1f, 0, 10.0f);
        vectorCurrentY = new Vector3(0, 0.1f, 0);
        gControl = (gControlObject = GameObject.FindGameObjectWithTag("GameControl")).GetComponent<GameControl>();
    }

    //No update temos a verificação do mouse
    //Escolhi fazer com o mouse pelo fato que funciona tanto no celular, tanto no computador e assim trabalhando com um input só, ainda mais que o não utilizo algumas chamadas excluvisas do mobile
    void Update()
    {
        screenPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if (Input.GetMouseButtonDown(0))
        {
            vectorOld = worldPos;
        }

        if (Input.GetMouseButton(0) && gControl.moveOn == true)
        {
            if (worldPos.x - 1 <= vectorOld.x)
            {
                vectorCurrent.x += 0.6f;
            }

            if (worldPos.x + 1 >= vectorOld.x)
            {
                vectorCurrent.x -= 0.6f;
            }

            if (worldPos.y - 1 <= vectorOld.y)
            {
                vectorCurrent.y += 0.6f;
            }

            if (worldPos.y + 1 >= vectorOld.y)
            {
                vectorCurrent.y -= 0.6f;
            }
            vectorCurrent.z = -10f;
            transform.position = vectorCurrent;
        }

    }
    //Botão de foco faz a camera retonar para o local padrão se o jogador se perder no mapa
    public void Focus()
    {
        transform.position = new Vector3(0, 0, -10);
        vectorCurrent.x = 0;
        vectorCurrent.y = 0;
    }
}
