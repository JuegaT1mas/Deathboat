using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    Puzzle2 puzzle;
    public int id;

    void Awake()
    {
        puzzle = GameObject.Find("Puzzle2").GetComponent(typeof(Puzzle2)) as Puzzle2;
    }

    private void OnMouseDown()
    {
      
        if(id==puzzle.dirFlecha)
        {
            puzzle.botonPulsado = true;
            puzzle.aciertos++;
            puzzle.estaGirando=false;
            puzzle.esGanador();
        }


    }

}