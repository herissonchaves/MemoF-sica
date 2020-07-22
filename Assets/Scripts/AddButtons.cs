using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adiciona os clones das buttons cards no painel da unity
public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private GameObject cartas;
    public int QuantidadeCartas; //recebe da unity a quantidade de cartas para colocar na fase
    void Awake()
    {
        for (int i = 0; i < QuantidadeCartas; i++)// número de cartas
        {
            GameObject button=Instantiate(cartas);// cria clones
            button.name = "" + i; // nomeia as cartas na Hierarchy
            button.transform.SetParent(puzzleField,false);
            
        }
    }

}
