using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    Puzzle puzzle1;
   public void ActivarObjeto()
    {
        puzzle1 = GameObject.Find("Puzzle1").GetComponent<Puzzle>();
        puzzle1.IniciarPuzzle();

    }
}
