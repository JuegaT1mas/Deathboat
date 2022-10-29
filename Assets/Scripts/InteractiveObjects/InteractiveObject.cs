using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject[] listaCamaras;

    public GameLoop gameLoop; //La referencia del gameLoop

    public bool isEscape; //Si el objeto es salida o no

   public void ActivarObjeto()
   {
        puzzle.GetComponent<Puzzle1>().IniciarPuzzle();
        listaCamaras[0].gameObject.SetActive(false);
        listaCamaras[1].gameObject.SetActive(true);
   }

    public void Victory()
    {
        gameLoop.GameOverVictory();
    }

    public void Decide()
    {
        if (!isEscape)
        {
            ActivarObjeto();
        }
        else
        {
            Victory();
        }
    }
}
