using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    Touch touch;
    [Header("Libera movimentação da camera")]
    [SerializeField] bool canMove = true;
    private string myTag;
    private bool ready = false;

    [Header("Variaveis para calcular o rendimento por segundo")]
    [SerializeField] float timeMoney = 0.999f;
    [SerializeField] int moneyConstruction;

    private GameControl gControl;
    private GameObject gControlObject;
    public ParticleSystem ps;

    [SerializeField] GameObject[] checksConstruction;


    private SpriteRenderer renderers;

    private RaycastHit2D hit, hit2, hit3, hit4, hit5;
    [Header("Layer para ser detectado")]
    private int myValue;
    [SerializeField] LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        //gControlObject = GameObject.FindGameObjectWithTag("GameControl");
        gControl = (gControlObject = GameObject.FindGameObjectWithTag("GameControl")).GetComponent<GameControl>();
        renderers = gameObject.GetComponent<SpriteRenderer>();
        ready = false;
        myTag = gameObject.tag;
        canMove = true;
        StartCoroutine(MoveObject());

        //A chamada para verificar qual objeto essa script esta vinculado
        //Assim essa scrip se torna uma script generica para todos os objetos
        switch (myTag)
        {
            case "Houses":
                myValue = Fases.houseStatic;
                break;
            case "Factory":
                myValue = Fases.factoryStatic;
                break;
            case "Mall":
                myValue = Fases.mallStatic;
                break;
            case "Farm":
                myValue = Fases.farmStatic;
                break;
            case "Park":
                myValue = Fases.parkStatic;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Se tiver pronto começa a caclular dinheiro conforme se passa 1 segundo 
        if (ready == true)
        {
            if (timeMoney > 0)
            {
                timeMoney -= Time.deltaTime;
            }
            else
            {
                timeMoney = 0.999f;

                switch (myTag)
                {
                    case "Houses":
                        moneyConstruction = gControl.moneyHouses;
                        break;
                    case "Factory":
                        moneyConstruction = gControl.moneyFactory;
                        break;
                    case "Mall":
                        moneyConstruction = gControl.moneyMall;
                        break;
                    case "Farm":
                        moneyConstruction = gControl.moneyFarm;
                        break;
                    case "Park":
                        moneyConstruction = gControl.moneyPark;
                        break;
                }
                //Aqui chama o controle do jogo para adicionar o valor atual
                gControl.AddMoney(moneyConstruction, gameObject.transform, renderers.isVisible);
            }
        }
    }
    //Essa Coroutine faz um looping de frama a frame até o jogador colocar o objeto na posição preferida, depois ela da stop e não carrega mais 
    IEnumerator MoveObject()
    {
        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        while (canMove == true)
        {
            yield return endOfFrame;
            if (Input.GetMouseButton(0)) //Enquanto o jogador estiver carregando o objeto com o dedo ele verifica se tem outro objeto em baixo dele em retorno da função canPut
            {
                Vector3 screenPos = Input.mousePosition;
                screenPos.z = 10f;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                transform.position = worldPos;
                //Debug.Log("nao clickei" + canMove);

                if (CanPut() == false)
                {
                    renderers.color = new Color(1, 0, 0, 0.5f);
                }
                else
                {
                    renderers.color = new Color(1, 1, 1, 1f);
                }
            }
            //Quando o jogador soltar e estiver em cima de outra contrução o a contrução atual some, se não ela ativa no jogo, ativa a particula de poeira
            //Remove o valor dela no dinheiro total
            //E se move em layer para Build que é o Layer que tem colisão de raycast com as proximas contruções
            if (Input.GetMouseButton(0) == false)
            {
                if (CanPut() == true)
                {
                    canMove = false;
                    Handheld.Vibrate();
                    ps.Play();
                    gControl.Storebuildings(myTag);
                    gControl.moveOn = true;
                    gControl.RemoveMoney(myValue);
                    gameObject.layer = LayerMask.NameToLayer("Build");
                    CallReady();
                }
                else
                {
                    gControl.moveOn = true;
                    canMove = false;
                    Destroy(gameObject);
                }

            }

        }
        //Debug.Log("Sai");
        StopCoroutine(MoveObject());
    }
    //Essa chamada da um valor diferente para começar a render após se colocada em jogo
    void CallReady()
    {
        switch (myTag)
        {
            case "Houses":
                StartCoroutine(Ready(1));
                gControl.AddMaxFelicity();
                break;
            case "Factory":
                StartCoroutine(Ready(2f));
                break;
            case "Mall":
                StartCoroutine(Ready(2.5f));
                break;
            case "Farm":
                StartCoroutine(Ready(3));
                break;
            case "Park":
                StartCoroutine(Ready(3.5f));
                break;
        }

    }
    //Coroutine faz a chgamada por segundo e ativa o rendimento
    IEnumerator Ready(float time)
    {
        yield return new WaitForSeconds(time);
        ready = true;

        //Debug.Log(myTag + " " + ready);
        StopCoroutine(Ready(0));
    }

    //A funçaõ CanPut verifica em uma box de line cast se existe outra contrução no local, se não tiver a contrução pode ser colocada junto com a função MoveObject
    private bool CanPut()
    {
        hit = Physics2D.Linecast(checksConstruction[0].transform.position, checksConstruction[1].transform.position, mask);
        hit2 = Physics2D.Linecast(checksConstruction[2].transform.position, checksConstruction[3].transform.position, mask);
        hit3 = Physics2D.Linecast(checksConstruction[4].transform.position, checksConstruction[5].transform.position, mask);
        hit4 = Physics2D.Linecast(checksConstruction[6].transform.position, checksConstruction[7].transform.position, mask);
        hit5 = Physics2D.Linecast(checksConstruction[8].transform.position, checksConstruction[9].transform.position, mask);

        if (hit.collider != null || hit2.collider != null || hit3.collider != null || hit4.collider != null || hit5.collider != null)
        {

            return false;
        }

        return true;
    }
}
