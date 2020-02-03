using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Fases : MonoBehaviour
{
    //Variaveis static que serve para o jogo se adaptar conforme o desafio
    [Header("Variaveis statics de valores")]
    public static int moneyStatic;
    public static int contructionBegin;
    public static int houseStatic;
    public static int factoryStatic;
    public static int mallStatic;
    public static int parkStatic;
    public static int farmStatic;

    [Header("Variaveis statics para ativ os botões")]
    public static bool tutorial = true, free = false, ch1 = false, ch2 = false, ch3 = false, ch4 = false, ch5 = false;
    [SerializeField] Button[] buttons;
    [SerializeField] TextMeshProUGUI descrition;
    [SerializeField] GameObject beginob;

    [Header("Controle de animação dos botões")]
    [SerializeField] Animator[] animbuttons;

    public void Start()
    {
       buttons[0].interactable = free;
        buttons[1].interactable = ch1;
        buttons[2].interactable = ch2;
        buttons[3].interactable = ch3;
        buttons[4].interactable = ch4;
        buttons[5].interactable = ch5;
        beginob.SetActive(false);

        animbuttons[0].SetBool("Anim", tutorial);
        animbuttons[1].SetBool("Anim", free);
        animbuttons[2].SetBool("Anim", ch1);
        animbuttons[3].SetBool("Anim", ch2);
        animbuttons[4].SetBool("Anim", ch3);
        animbuttons[5].SetBool("Anim", ch4);
        animbuttons[6].SetBool("Anim", ch5);
    }
    //Todas as funções e baixo é ativada conforme o jogador seleciona os modos que quer jogar
    public void ModeFree()
    {

        descrition.text = "Modo Livre \n Você entra no papel de um prefeito e tem que crescer sua cidade e deixar os seus moradores com 100% de felicidade";
        moneyStatic = 100;
        houseStatic = 100;
        factoryStatic = 110;
        mallStatic = 100;
        parkStatic = 200;
        farmStatic = 180;
        contructionBegin = 0;
        beginob.SetActive(true);

    }

    public void Chanlleger1()
    {
        descrition.text = "Desafio 1 \nPrefeito " + Begin.nickNameStatic + " você vai precisar gerar empregos e melhorar a felicidade da cidade";
        moneyStatic = 4700;
        houseStatic = 300;
        factoryStatic = 500;
        mallStatic = 100;
        parkStatic = 800;
        farmStatic = 800;
        contructionBegin = 1;
        beginob.SetActive(true);

    }
    public void Chanlleger2()
    {
        descrition.text = "Desafio 2 \nPrefeito " + Begin.nickNameStatic + " por favor acabe com o desemprego e melhore nossa cidade";
        moneyStatic = 850;
        houseStatic = 100;
        factoryStatic = 100;
        mallStatic = 250;
        parkStatic = 1000;
        farmStatic = 200;
        contructionBegin = 2;
        beginob.SetActive(true);

    }
    public void Chanlleger3()
    {
        descrition.text = "Desafio 3 \nPrefeito " + Begin.nickNameStatic + " deixe nossa cidade mais moderna?";
        moneyStatic = 2600;
        houseStatic = 500;
        factoryStatic = 150;
        mallStatic = 250;
        parkStatic = 200;
        farmStatic = 1000;
        contructionBegin = 3;
        beginob.SetActive(true);

    }
    public void Chanlleger4()
    {
        descrition.text = "Desafio 4 \nPrefeito " + Begin.nickNameStatic + " estamos sem matéria prima para vender em nossos shoppings";
        moneyStatic = 2300;
        houseStatic = 1000;
        factoryStatic = 300;
        mallStatic = 100;
        parkStatic = 500;
        farmStatic = 300;
        contructionBegin = 4;
        beginob.SetActive(true);

    }
    public void Chanlleger5()
    {
        descrition.text = "Desafio 5 \nPrefeito " + Begin.nickNameStatic + " essa cidade parece um pântano abandonado, olha o tanto de parques";
        moneyStatic = 11200;
        houseStatic = 1250;
        factoryStatic = 300;
        mallStatic = 200;
        parkStatic = 800;
        farmStatic = 500;
        contructionBegin = 5;
        beginob.SetActive(true);

    }

    public void Tutorial()
    {
        descrition.text = "Modo Tutorial \n Você vai aprender como se joga o Mini City";
        moneyStatic = 300;
        houseStatic = 100;
        factoryStatic = 10000;
        mallStatic = 10000;
        parkStatic = 10000;
        farmStatic = 10000;
        contructionBegin = 6;
        beginob.SetActive(true);

    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Game");
    }
}
