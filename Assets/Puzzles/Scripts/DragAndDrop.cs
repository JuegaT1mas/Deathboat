using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Sensores sensores;
    Puzzle puzzle;
    
    void Awake()
    {
        sensores = GetComponentInChildren(typeof(Sensores)) as Sensores;
        puzzle = GameObject.Find("Scripts").GetComponent(typeof(Puzzle)) as Puzzle;
    }
 

     void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!sensores.ocupadoRight)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(1, 0, 0);
                puzzle.EsGanador();
                print("Hola");

            }
            else if (!sensores.ocupadoLeft)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(-1, 0, 0);
                puzzle.EsGanador();
                print("Hola");
            }
            else if (!sensores.ocupadoDown)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(0, -1, 0);
                puzzle.EsGanador();
                print("Hola");
            }
            else if (!sensores.ocupadoUp)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
                puzzle.EsGanador();
                print("Hola");
            }
        }
        
    }

   

}
