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
    void Awake()
    {
        for (int i = 0; i <12 ; i++)// número de cartas
        {
            GameObject button=Instantiate(cartas);// cria clones
            button.name = "" + i; // nomeia as cartas na Hierarchy
            button.transform.SetParent(puzzleField,false);
            
        }
    }

}
