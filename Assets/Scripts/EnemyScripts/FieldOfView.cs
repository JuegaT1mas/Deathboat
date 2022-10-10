using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;


    public void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player"); //Encontrar la referencia al jugador al empezar
        StartCoroutine(FOVRoutine()); //Comenzar la corrutina
    }

    private IEnumerator FOVRoutine() //Crear una corrutina para buscar al jugador
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);//El delay de cada cuanto va a comprobar la corrutina que se busque al jugador

        while (true) //El true se puede cambiar por una variable para decidir si se quiere buscar o no
        {
            yield return wait; //Esperar el tiempo del delay
            FieldOfViewCheck(); //Metodo de comprobaci�n de la presencia del jugador
        }
    }

    private void FieldOfViewCheck()
    {
        //Comprueba los colliders que se sobreponen a la esfera de radio definido antes en la m�scara de capa espec�fica
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        //Mirar como funciona porque a lo mejor hay que cambiar el esfera por una caja del tama�o del techo para que no mire en otros pisos.
    
        if(rangeChecks.Length != 0) //Si hemos encontrado una colisi�n, por la naturaleza del juego este solo va a ser el jugador
        {
            Transform target = rangeChecks[0].transform; //pillamos el transform del jugador
            Vector3 directionToTarget = (target.position - transform.position).normalized; //Miramos la direcci�n hacia el objetivo

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) //Si el �ngulo de visi�n del enemigo ve al jugador
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position); //la distancia al jugador

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                //Si lanzamos un rayo mirando hacia el jugador y con la distancia al jugador y no se choca con nada entonces le vemos
                {
                    canSeePlayer = true;
                }
                else //Si no le vemos
                {
                    canSeePlayer = false;
                }
            }
            else //El jugador no esta dentro del campo de visi�n del enemigo
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer) //Si no recibimos ning�n collider
        {
            canSeePlayer = false; //entonces no le estamos viendo
        }
    }
}
