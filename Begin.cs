using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Begin : MonoBehaviour
{
    [Header("Objetos dos canvas para o nickname")]
    [SerializeField] TMP_InputField nickname;
    [SerializeField] GameObject nicknameOb, objCanvas;
    public static string nickNameStatic;

    public void Start()
    {
        nicknameOb.SetActive(false);
        objCanvas.SetActive(true);
    }
    //Função onde o coloca as telas de nickname
    public void nick()
    {
        nicknameOb.SetActive(true);
        objCanvas.SetActive(false);
    }
    //função que começa o jogo
    public void Beginn()
    {
        nickNameStatic = nickname.text;
        SceneManager.LoadScene("MenuSelection");
    }
   
    public void Quit()
    {
        Application.Quit();
    }
}
