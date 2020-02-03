using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Variaveis de controle do tutorial")]
    [SerializeField] Animator anim;
    private int step = -1;

    [SerializeField] TextMeshPro descrition;
    private GameControl gControl;
    private GameObject gControlObject;
    [SerializeField] GameObject painelTutorial, tabdescrition, movTopo, movDown, objectMain;
    // Start is called before the first frame update
    void Start()
    {
        gControl = (gControlObject = GameObject.FindGameObjectWithTag("GameControl")).GetComponent<GameControl>();
        step = -1;
        painelTutorial = GameObject.FindGameObjectWithTag("PainelTutorial1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Todas as funções verifica qual passo esta no tutorial e ativa a animação e escrita
        NextText();
        if (Input.GetMouseButtonUp(0))
        {
            
            if ((step < 0) || (step < 4 && step > 0) || (step > 5 && step < 8) || (step > 8))
            {
                next();
            }
        }

        if (step == 0)
        {
            if (ControlInfos.moneyActive == true)
            {
                next();
            }
        }

        if (step == 4)
        {
            if(ControlInfos.moneyActive == false)
            {
                next();
            }
        }

        if (step == 5)
        {
            if (ControlInfos.felicityActive == true)
            {
                next();
            }
        }

        if (step == 8)
        {
           if(ControlInfos.felicityActive == false)
            {
                next();
                descrition.transform.position = movTopo.transform.position;
                tabdescrition.transform.position = movTopo.transform.position;
                descrition.text = "Aqui fica todas as estruturas que você pode pegar e colocar na sua cidade. \nCLICK AQUI";
            }
        }

        if(gControl.amountHouses >= 1 && step >=10)
        {
            tabdescrition.transform.position = movDown.transform.position;
            descrition.transform.position = movDown.transform.position;
            painelTutorial.SetActive(true);
            descrition.text = "E após colocar a sua casa a felicidade e o dinheiro começa aumentar \nfelicidade 100% = VITÓRIA";
            gControl.felicity = 100;
            gControl.StartCoroutine(gControl.QuitTutorial());
            StartCoroutine(QuitTutorial());
        }
        
    }
    //Faz o proximo passo na animação
    private void next()
    {
        step+=1;
        anim.SetInteger("TutorialSeq", step);
    }
    //Verifica qua texto e muda conforme a etapa do tutorial
    private void NextText()
    {
        switch (step)
        {
            case -1:
                descrition.text = "Oi " + Begin.nickNameStatic + " \nEu vou te ajudar a entender o sistema do nosso gabinete" + " \nAqui fica seu nome" + " \nCLICK AQUI para continuar...";
                break;
            case 0:
                descrition.text = "Aqui fica o dinheiro da prefeitura. \nToque em cima para ver mais detalhes.";
                break;
            case 1:
                descrition.text = "Essa é a planilha de detalhes da sua cidade. \nCLICK AQUI";
                break;
            case 2:
                descrition.text = "Aqui fica todas as estruturas que voce pode colocar na sua cidade.";
                break;
            case 3:
                descrition.text = "Aqui fica os valores e status atuais da sua cidade. \nValor bruto é o valor padrão que cada estrutura gera por segundo.  \nvalor unitario é o valor atual que as estruturas está gerando \nValor total é a somatória de todas as estruturas de cada tipo. \nCLICK AQUI";
                break;
            case 4:
                descrition.text = "Click no dinheiro para fechar as infos";
                break;
            case 5:
                descrition.text = "Aqui é a felicidade da sua cidade, chegue a 100% para conquistar todos \nCLICK na felicidade para abrir as infos";
                break;
            case 6:
                descrition.text = "Esses são os detalhes da felicidade \nfelicidade atual, valor por segundo e valor maximo que a sua cidade pode chegar \nPara aumentar o valor maximo coleque mais CASAS na cidade, com 20 casas você vai ter o valor maximo de 100% \nCLICK AQUI";
                break;
            case 7:
               descrition.text = "CLICK na felicidade para fechar as infos.";
                painelTutorial.SetActive(false);
                break;
            case 9:
                gControl.AddMoney(100, gameObject.transform, false);
                descrition.text = "Aqui fica todas as estruturas que você pode pegar e colocar na sua cidade. \nArraste a casa para a sua cidade.";
                break;

        }
    }
    //Finaliza o processo do tutorial
     IEnumerator QuitTutorial()
    {
        yield return new WaitForSeconds(4f);
        painelTutorial.SetActive(false);
        Destroy(objectMain);
    }
}
