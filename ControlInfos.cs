using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlInfos : MonoBehaviour
{
    [Header("Objetos do canvas de infos")]
    [SerializeField] GameObject[] infos;
    public static bool moneyActive;
    public static bool felicityActive;

    [SerializeField] TextMeshProUGUI[] valueUn;
    [SerializeField] TextMeshProUGUI[] valueTotal;
    [SerializeField] GameControl gControl;
    private int[] valueTotalBuilds;

    [SerializeField] TextMeshProUGUI[] valueFelicity;
    // Start is called before the first frame update
    void Start()
    {
        valueTotalBuilds = new int[5];
        infos[0].SetActive(false);
        infos[1].SetActive(false);
        moneyActive = false;
        felicityActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Essas condições verifica se pode ou nao ativar asa infos
        if(felicityActive == true)
        {
            ChecksFelicity();
        }

        if (moneyActive == true)
        {
            ChecksMoney();
        }
    }
    //Todas as funções em baixo verifica as informação
    public void ActiveInfoMoney()
    {
        ChecksMoney();
        moneyActive = !moneyActive;
        infos[0].SetActive(moneyActive);
        infos[1].SetActive(false);
    }

    public void ActiveInfoFelicty()
    {
        ChecksFelicity();
        felicityActive = !felicityActive;
        infos[0].SetActive(false);
        infos[1].SetActive(felicityActive);
    }

    private void ChecksMoney()
    {
        valueUn[0].text = gControl.moneyHouses.ToString("C");
        valueUn[1].text = gControl.moneyFactory.ToString("C");
        valueUn[2].text = gControl.moneyMall.ToString("C");
        valueUn[3].text = gControl.moneyPark.ToString("C");
        valueUn[4].text = gControl.moneyFarm.ToString("C");

        valueTotalBuilds[0] = gControl.moneyHouses * gControl.amountHouses;
        valueTotalBuilds[1] = gControl.moneyFactory * gControl.amountFactory;
        valueTotalBuilds[2] = gControl.moneyMall * gControl.amountMall;
        valueTotalBuilds[3] = gControl.moneyPark * gControl.amountPark;
        valueTotalBuilds[4] = gControl.moneyFarm * gControl.amountFarm;

        valueTotal[0].text = valueTotalBuilds[0].ToString("C");
        valueTotal[1].text = valueTotalBuilds[1].ToString("C");
        valueTotal[2].text = valueTotalBuilds[2].ToString("C");
        valueTotal[3].text = valueTotalBuilds[3].ToString("C");
        valueTotal[4].text = valueTotalBuilds[4].ToString("C");
    }

    private void ChecksFelicity()
    {
        valueFelicity[0].text = gControl.felicityValue.ToString("F2") + "%";
        valueFelicity[1].text = gControl.maxFelicity.ToString() + "%";
        valueFelicity[2].text = gControl.felicity.ToString("F2") +"%";
    }
}
