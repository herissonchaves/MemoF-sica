using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{ 
    public List<Button> CartasButtons = new List<Button>(); // lista de buttons
    public Sprite[] CardsFace; //sprites das cartas
    public List<Sprite> deckCartas = new List<Sprite>();//uma lista de cardfaces
    [SerializeField]
    private Sprite cardBack; // intancia a imagem das cartas de background

    // cronometro
    private float deltaTime = 0;
    private int segundos;
    private int minutos = 0;
    private bool comecarCronometro = false;
    public Text CronometroSegundosUI;
    public Text CronometroMinutosUI;

    public Text Movimentos;

    private bool firstPalpite, secondPalpite;
    private int countPalpites;
    private int countCorrectPalpites;
    private int firstPalpiteIndex, secondPalpiteIndex;
    private string firstPalpitePuzzle, secondPalpitePuzzle;
        //cardsfaces são cartas não ocultas e cardback são cartas viradas para baixo.
    void Start()
    {
        GetButtons(); // adiciona imagens de cardback nos buttons.
        AddGameCartas();// adiciona uma lista de sprites de cardfaces.
        AddListeners();// verifica se o usuario clicou na carta.
    }
    void GetButtons()
    {
        //adiciona em object a quantidade de buttons geradas pelos script AddButtons.
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CardsButton"); // lista de clones dos buttons
        for (int i = 0; i < objects.Length; i++)
        {
            CartasButtons.Add(objects[i].GetComponent<Button>()); // adiciona na lista os buttons
            CartasButtons[i].image.sprite = cardBack; // adiciona as imagens cardback.
        }
    }
    void AddGameCartas()
    {
        //cria uma lista de números aleatórios repetidos apenas duas vezes
        int quantidade = CartasButtons.Count/2; // quantidade de cartas divido por 2
        List<int> numerosAleatorios = new List<int>();
        int j = 0; // serve apenas para controlar o laço while
        while (j < 2)// esse laço serve para construir o baralho com números aletorios repetidos apenas duas vezes.
        {
            for (int i = 0; i < quantidade; i++)
            {
                int randomIndex = Random.Range(0, quantidade); // variável para guardar os números aleatórios
                while (numerosAleatorios.Contains(randomIndex))
                {
                    if (numerosAleatorios.Count >= quantidade)// serve para quebrar o laço quando ficar em loop infinito
                        break;
                    else
                        randomIndex = Random.Range(0, quantidade);
                }
                numerosAleatorios.Add(randomIndex);//adiciona o número aleatórios na lista
                deckCartas.Add(CardsFace[numerosAleatorios[i]]);// constroi o deck adicionando cartas aleatórias 
            }
            j++;
            numerosAleatorios.Clear(); // necessario para voltar a repetir a ter numeros aleatórios não repetidos.
        }
    }
    void AddListeners()
    {
        foreach(Button carta in CartasButtons)
       {
            carta.onClick.AddListener(() => PickCartas());
           
       }
    }
    public void PickCartas()
    {
        if (!firstPalpite)
        {
            firstPalpite = true;
            firstPalpiteIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.
            currentSelectedGameObject.name);// converte a string em int
            firstPalpitePuzzle = deckCartas[firstPalpiteIndex].name;//recebe o nome da sprite
            CartasButtons[firstPalpiteIndex].image.sprite = deckCartas[firstPalpiteIndex];//adciona a sprite no button
        }
        else if (!secondPalpite)
        {
            secondPalpite = true;
            comecarCronometro = true;
            secondPalpiteIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.
            currentSelectedGameObject.name);
            secondPalpitePuzzle = deckCartas[secondPalpiteIndex].name;
            CartasButtons[secondPalpiteIndex].image.sprite = deckCartas[secondPalpiteIndex];
            countPalpites++;
           Movimentos.text = countPalpites.ToString("");
            StartCoroutine(CheckMatch());
        }
    }
    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.25f);
        if (firstPalpitePuzzle == secondPalpitePuzzle)// se as cartas são iguais.
        {
            yield return new WaitForSeconds(.12f);
            CartasButtons[firstPalpiteIndex].enabled = false; // desabilita o button
            CartasButtons[secondPalpiteIndex].enabled = false; // desabilita o button
            CheckIsFinished();
        }
        else {
            yield return new WaitForSeconds(.12f);
            CartasButtons[firstPalpiteIndex].image.sprite = cardBack;
            CartasButtons[secondPalpiteIndex].image.sprite = cardBack;
        }
        yield return new WaitForSeconds(.12f);
        firstPalpite = secondPalpite = false;
    }
    void CheckIsFinished()
    {
        countCorrectPalpites++;
        if (countCorrectPalpites == deckCartas.Count / 2)
        {
            Debug.Log("Fim de jogo");
            Debug.Log("muito bem, foram " + countPalpites +" palpites para terminar o jogo");
        }
    }
    void Update()
    {
        if (comecarCronometro)
       {
            deltaTime += Time.deltaTime;
            segundos = (int)deltaTime;
            if ( segundos == 60)
            {
                deltaTime = 0;
                minutos++;
            }
            CronometroMinutosUI.text = minutos.ToString();
            CronometroSegundosUI.text = segundos.ToString();
       }
    }
}


