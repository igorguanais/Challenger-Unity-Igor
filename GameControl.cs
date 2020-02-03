using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Header("Objetos dos canvas para dinheiro e felicidade")]
    //fazer os adds de felicidade e dinheiro
    [SerializeField] int money, moneyFormat;
    public int maxFelicity;
    public float felicity, felicityValue, timeFelicity = 0.999f;
    [SerializeField] TextMeshProUGUI moneyText, felictyText;
    [SerializeField] GameObject coin, coinsList;
    [SerializeField] Button[] buttonBuild;
    public bool moveOn;

    [Header("Respawn das contruções dos desafios")]
    [SerializeField] GameObject[] constructionsFase;
    [SerializeField] GameObject rewpawn, camTutorial;

    [Header("Valores das contruções")]
    [SerializeField] TextMeshProUGUI[] valueBuilds;

    [Header("Quantidade de cada construção")]
    public int amountHouses, amountFactory, amountMall, amountPark, amountFarm;
    [Header("Valor de cada contrução")]
    public int moneyHouses, moneyFactory, moneyMall, moneyPark, moneyFarm;

    //private bool canActivate = false;
    [Header("Itens do canvas para vitoria e derrota")]
    [SerializeField] TextMeshProUGUI nickname;
    [SerializeField] TextMeshProUGUI descritionVitory;
    [SerializeField] GameObject victoryPainel;
    [SerializeField] TextMeshProUGUI descritionDefeat;
    [SerializeField] GameObject DefeatPainel;

    [Header("Itens do canvas para Pause no jogo")]
    private bool pause = false;
    [SerializeField] GameObject textPause;

    [SerializeField] GameObject painelTutorial;
    // Start is called before the first frame update
    void Start()
    {
        descritionVitory.text = "";
        nickname.text = "";
        victoryPainel.SetActive(false);
        DefeatPainel.SetActive(false);
        nickname.text = Begin.nickNameStatic;
        amountHouses = 0;
        amountFactory = 0;
        amountMall = 0;
        amountPark = 0;
        amountFarm = 0;

        moneyHouses = 5;
        moneyFactory = 20;
        moneyMall = 20;
        moneyPark = 5;
        moneyFarm = 18;
        felicityValue = 0.4f;

        //o Switch serve para ver qual desafio ou modo livre o jogador escolheu para jogar e pre carrega os prefabs de contruições na cena de jogo
        switch (Fases.contructionBegin)
        {
            case 0:
                painelTutorial.SetActive(false);
                break;
            case 1:
                painelTutorial.SetActive(false);
                Instantiate(constructionsFase[0], rewpawn.transform.position, rewpawn.transform.rotation, rewpawn.transform);
                break;
            case 2:
                painelTutorial.SetActive(false);
                Instantiate(constructionsFase[1], rewpawn.transform.position, rewpawn.transform.rotation, rewpawn.transform);
                break;
            case 3:
                painelTutorial.SetActive(false);
                Instantiate(constructionsFase[2], rewpawn.transform.position, rewpawn.transform.rotation, rewpawn.transform);
                break;
            case 4:
                painelTutorial.SetActive(false);
                Instantiate(constructionsFase[3], rewpawn.transform.position, rewpawn.transform.rotation, rewpawn.transform);
                break;
            case 5:
                painelTutorial.SetActive(false);
                Instantiate(constructionsFase[4], rewpawn.transform.position, rewpawn.transform.rotation, rewpawn.transform);
                break;
            case 6:
                Instantiate(constructionsFase[5], rewpawn.transform.position, rewpawn.transform.rotation, camTutorial.transform);
                break;
        }
        //Essa sequencia atualiza os valores novos de cada contrução pegando a static do menu
        valueBuilds[0].text = Fases.houseStatic.ToString("C");
        valueBuilds[1].text = Fases.factoryStatic.ToString("C");
        valueBuilds[2].text = Fases.mallStatic.ToString("C");
        valueBuilds[3].text = Fases.parkStatic.ToString("C");
        valueBuilds[4].text = Fases.farmStatic.ToString("C");

        money = Fases.moneyStatic;

        timeFelicity = 0.999f;
        maxFelicity = 0;
        moveOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Nessa parte é feita as atualizações de valores de dinheiro e felicidade
        ButtonEnable();
        moneyText.text = money.ToString("C");
        felictyText.text = felicity.ToString("F2") + "%";
        Defeat();

        if (felicity < maxFelicity + 1)
        {
            if (timeFelicity > 0)
            {
                timeFelicity -= Time.deltaTime;
            }
            else
            {
                timeFelicity = 0.999f;
                IncreaseFelicity();
            }
        }
    }
    //Função que verifica se o jogador ganhou, se estiver no tutorial libera os outros modos de jogo
    public void Victory()
    {
        if (felicity >= 100)
        {
            if (Fases.free == false)
            {
                Fases.free = true;
                Fases.tutorial = false;
            }

            if (Fases.free == true)
            {
                Fases.ch1 = true;
                Fases.ch2 = true;
                Fases.ch3 = true;
                Fases.ch4 = true;
                Fases.ch5 = true;
            }

            descritionVitory.text = "PARABÉNS \nVocê é o melhor prefeito de todos " + Begin.nickNameStatic;
            victoryPainel.SetActive(true);


        }
    }
    //Função verifica a derrota por dinheiro
    public void Defeat()
    {
        if (money < -500)
        {
            descritionDefeat.text = "INFELIZMENTE \nInfelizmente você sofreu impeachment por falencia, prefeito " + Begin.nickNameStatic;
            DefeatPainel.SetActive(true);
        }
    }
    //Essa função recebe os valores de cada contrução e adiciona no valor total de dinheiro
    public void AddMoney(int Newmoney, Transform posCoin, bool canSend)
    {
        money += Newmoney;

        if (canSend == true)
        {
            Instantiate(coin, posCoin.position, posCoin.rotation, coinsList.transform);
        }

    }
    //Aqui uma função que retonar os valores de dinheiro
    public int GetMoney()
    {
        return money;
    }
    //Função que remove o dinheiro geral
    public void RemoveMoney(int RevMoney)
    {
        money -= RevMoney;
    }
    //Função que é chamada pela casa quando é colocada em game
    public void AddMaxFelicity()
    {
        maxFelicity += 5;
    }
    //Essa função é chamada por segundo assim aumentando ou diminuindo a felicidade
    public void IncreaseFelicity()
    {
        if (felicity >= -maxFelicity)
        {
            felicity += felicityValue;

            if (felicity > maxFelicity)
            {
                felicity = maxFelicity;
            }

            if (felicity < -maxFelicity)
            {
                felicity = -maxFelicity;
            }
        }
        Victory();
    }
    //função que ativa os botões se tiver dinheiro suficiente para comprar as contruções
    void ButtonEnable()
    {

        if (money >= Fases.houseStatic)
        {
            buttonBuild[0].interactable = true;

        }
        else
        {
            buttonBuild[0].interactable = false;
        }

        if (money >= Fases.factoryStatic)
        {
            buttonBuild[1].interactable = true; ;
        }
        else
        {
            buttonBuild[1].interactable = false;
        }

        if (money >= Fases.mallStatic)
        {
            buttonBuild[2].interactable = true;
        }
        else
        {
            buttonBuild[2].interactable = false;
        }

        if (money >= Fases.parkStatic)
        {
            buttonBuild[3].interactable = true;
        }
        else
        {
            buttonBuild[3].interactable = false;
        }

        if (money >= Fases.farmStatic)
        {
            buttonBuild[4].interactable = true;
        }
        else
        {
            buttonBuild[4].interactable = false;
        }
    }
    //Função que amarzena todas as contruções que existe atualmente em jogo
    //Essa função tambem faz as multiplicações de valores conforme as quantidade que tem em jogo (Aumentando a dificuldade)
    public void Storebuildings(string whatBuild)
    {
        switch (whatBuild)
        {
            case "Houses":
                amountHouses++;
                addsValuesHouse();
                break;

            case "Factory":
                amountFactory++;
                if (amountFactory == 2 || (amountFactory == 6))
                {
                    addsValuesFactory();
                }
                break;

            case "Mall":
                amountMall++;
                if (amountMall == 2 || amountMall == 7 || amountMall == 14)
                {
                    addsValuesMall();
                }
                break;

            case "Farm":
                amountFarm++;
                addsValuesFarm();
                break;

            case "Park":
                amountPark++;
                addsValuesPark();
                break;
        }
        AddRules();

    }
    //Função do BÔNUS 2: THE DEPTH UPDATE onde a mesma calcula todos os novos valores de dinheiro e felicidade
    private void AddRules()
    {
        // canActivate = false;
        felicityValue = 0.4f;
        if (amountHouses < (amountMall * 3))
        {
            int surplus = (amountMall * 3) - amountHouses;
            int newValue = 2 * surplus;
            moneyMall = 20 - newValue;
        }
        else
        {
            moneyMall = 20;
        }

        if (amountHouses < (amountFarm * 2))
        {
            int surplus = (amountFarm * 2) - amountHouses;
            int newValue = 3 * surplus;
            moneyFarm = 18 - newValue;
        }
        else
        {
            moneyFarm = 18;
        }


        if (amountFactory > (amountPark * 1.5f))
        {
            if (felicityValue == 0.4f)
            {
                felicityValue = -0.25f;
            }
            else
            {
                felicityValue += -0.25f;
            }
            
        }


        if (amountHouses > ((amountFactory + amountFarm) * 2))
        {
            if (felicityValue == 0.4f)
            {
                felicityValue = -0.3f;
            }
            else
            {
                felicityValue += -0.3f;
            }
        }


        if (amountMall < (amountHouses * 0.2))
        {
            if (felicityValue == 0.4f)
            {
                felicityValue = -6f;
            }
            else
            {
                felicityValue += -6f;
            }
        }

    }
    //Função de pause do jogo, voltar os menu e sair do jogo
    public void PauseGame()
    {
        pause = !pause;
        textPause.SetActive(pause);
        if (pause == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Menu()
    {
        PauseGame();
        SceneManager.LoadScene("MenuSelection");
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Todas as funções a baixo são responsaveis em multiplicar os valores em conjunto a função que amarzena as quantidades
    private void addsValuesHouse()
    {
        if (amountHouses <= 3)
        {
            Fases.houseStatic = Fases.houseStatic * 3;
        }
        else if (amountHouses < 10 && amountHouses > 5)
        {
            Fases.houseStatic = Fases.houseStatic * 2;
        }
        ValueBuilds();
    }

    private void addsValuesFactory()
    {
        Fases.factoryStatic = Fases.factoryStatic * 2;
        ValueBuilds();
    }

    private void addsValuesMall()
    {
        Fases.mallStatic = Fases.mallStatic * 2;
        ValueBuilds();
    }

    private void addsValuesPark()
    {
        if (amountPark == 11 || amountPark == 14)
        {
            Fases.parkStatic = Fases.parkStatic * 2;
        }
        ValueBuilds();
    }

    private void addsValuesFarm()
    {
        if (amountFarm == 3 || amountFarm == 10 || amountFarm == 11)
        {
            Fases.farmStatic = Fases.farmStatic * 2;
        }
        ValueBuilds();
    }

    private void ValueBuilds()
    {
        valueBuilds[0].text = Fases.houseStatic.ToString("C");
        valueBuilds[1].text = Fases.factoryStatic.ToString("C");
        valueBuilds[2].text = Fases.mallStatic.ToString("C");
        valueBuilds[3].text = Fases.parkStatic.ToString("C");
        valueBuilds[4].text = Fases.farmStatic.ToString("C");
    }

    public void Continue()
    {
        SceneManager.LoadScene("MenuSelection");
    }

    public IEnumerator QuitTutorial()
    {
        yield return new WaitForSeconds(6f);
        Victory();
    }
}
