using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
 
    public List<Button> CartasButtons = new List<Button>();
    public Sprite[] CardsFace; //sprites das cartas
    public List<Sprite> gameCartas = new List<Sprite>();//uma lista de cardfaces
    [SerializeField]
    private Sprite cardBack; // intancia a imagem das cartas de background


    private bool firstPalpite, secondPalpite;
    private int countPalpites;
    private int countCorrectPalpites;
    private int gamePalpite;
    private int firstPalpiteIndex, secondPalpiteIndex;
    private string firstPalpitePuzzle, secondPalpitePuzzle;



    void Start()
    {
        GetButtons(); // adiciona imagens de cardback nos buttons.
        AddListeners();// verifica o click
        AddGameCartas();// adiciona uma lista de sprites de cardfaces.
        gamePalpite = gameCartas.Count / 2;
    }
    void GetButtons()
    {
        //adiciona em object a quantidade de buttons geradas pelos script AddButtons.
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CardsButton");
        for (int i = 0; i < objects.Length; i++)
        {
            CartasButtons.Add(objects[i].GetComponent<Button>());
            CartasButtons[i].image.sprite = cardBack; // adiciona as imagens cardback.
        }
    }
    void AddGameCartas()
    {
        int quantidade = CartasButtons.Count;
        int index = 0;
        for(int i = 0; i < quantidade; i++)
        {
            if (index == quantidade / 2) // faz repetir duas vezes as mesmas cartas
                index = 0;
            gameCartas.Add(CardsFace[index]);// total de cartas = 2* a metade
            index++;
        }
        //depois criar aqui o embaralhamento das cartas.
    }
    void AddListeners()
    {
        foreach(Button carta in CartasButtons)
       {
            carta.onClick.AddListener(() => PickCartas());// quando eu clico??
       }
    }
    public void PickCartas()
        //a primeira vez q aperto vai para if e na segunda vez que aperto vai para o else
    {
        if (!firstPalpite)
        {
            firstPalpite = true;
            firstPalpiteIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.
            currentSelectedGameObject.name);// converte a string em int
            firstPalpitePuzzle = gameCartas[firstPalpiteIndex].name;//recebe o nome da sprite
            CartasButtons[firstPalpiteIndex].image.sprite = gameCartas[firstPalpiteIndex];//adciona a sprite no button

        }
        else if (!secondPalpite)
        {
            secondPalpite = true;
            secondPalpiteIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.
            currentSelectedGameObject.name);
            secondPalpitePuzzle = gameCartas[secondPalpiteIndex].name;
            CartasButtons[secondPalpiteIndex].image.sprite = gameCartas[secondPalpiteIndex];
            countPalpites++;
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
            //cor das cartas ao acertar
            //CartasButtons[firstPalpiteIndex].image.color = new Color(1, 0, 0, 0);
            //CartasButtons[secondPalpiteIndex].image.color = new Color(1, 0, 0, 0);
            CheckIsFinished();
        }
        else {
            yield return new WaitForSeconds(.12f);
            CartasButtons[firstPalpiteIndex].image.sprite = cardBack;
            CartasButtons[secondPalpiteIndex].image.sprite = cardBack;
           // countPalpites++;
        }
        yield return new WaitForSeconds(.12f);
        firstPalpite = secondPalpite = false;
    }
    void CheckIsFinished()
    {
        countCorrectPalpites++;
        if (countCorrectPalpites == gamePalpite)
        {
            Debug.Log("Fim de jogo");
            Debug.Log("muito bem, foram " + countPalpites +" palpites para terminar o jogo");
        }
    }
    void Embaralhar(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}


