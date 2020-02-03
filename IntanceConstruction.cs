using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntanceConstruction : MonoBehaviour
{
    [Header("Todos os objetos de contruções em prafabs")]
    [SerializeField] GameObject[] construictions;
    [SerializeField] GameObject[] bottonConstructions;
    private Touch touch;
    private Vector2 worldPoint;
    private RaycastHit2D hit;

    private GameControl gControl;
    private GameObject gControlObject;

    [SerializeField] GameObject buildList;

    private int houseValue, factoryValue, mallValue, parkValue, farmValue;

    // Start is called before the first frame update
    void Start()
    {
        gControl = (gControlObject = GameObject.FindGameObjectWithTag("GameControl")).GetComponent<GameControl>();


    }

    // Update is called once per frame
    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Verificar melhor forma
        houseValue = Fases.houseStatic;
        factoryValue = Fases.factoryStatic;
        mallValue = Fases.mallStatic;
        parkValue = Fases.parkStatic;
        farmValue = Fases.farmStatic;
    }
    //Todas as funções ativa um prefab para ser colocado em jogo
    public void ButtonHouse()
    {
        if (gControl.GetMoney() >= houseValue)
        {
            GameObject house;
            house = Instantiate(construictions[0], worldPoint, bottonConstructions[0].transform.rotation, buildList.transform);
            gControl.moveOn = false;
        }

    }

    public void ButtonFactory()
    {
        if (gControl.GetMoney() >= factoryValue)
        {
            GameObject Factory;
            Factory = Instantiate(construictions[1], worldPoint, bottonConstructions[1].transform.rotation, buildList.transform);
            gControl.moveOn = false;
        }
    }
    public void ButtonMall()
    {
        if (gControl.GetMoney() >= mallValue)
        {
            GameObject mall;
            mall = Instantiate(construictions[2], worldPoint, bottonConstructions[2].transform.rotation, buildList.transform);
            gControl.moveOn = false;
        }
    }
    public void ButtonPark()
    {
        if (gControl.GetMoney() >= parkValue)
        {
            GameObject park;
            park = Instantiate(construictions[3], worldPoint, bottonConstructions[3].transform.rotation, buildList.transform);
            gControl.moveOn = false;
        }
    }

    public void ButtonFarm()
    {
        if (gControl.GetMoney() >= farmValue)
        {
            GameObject farm;
            farm = Instantiate(construictions[4], worldPoint, bottonConstructions[4].transform.rotation, buildList.transform);
            gControl.moveOn = false;
        }
    }
}
