using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 1.5f;
    private GameObject _player;

    public GameObject TextDetect;
    GameObject ultimoReconocido=null;

    public bool puzzleActivado = false;

    private void Awake()
    {
        _player = GameObject.Find("PlayerCapsule");
    }
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        //TextDetect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Raycast(origen,direcci�n,out hit, distancia, m�scara)
        RaycastHit hit;

        if (!puzzleActivado)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
            {
                Deselect();
                SelectedObject(hit.transform);
                if (hit.collider.tag == "Objeto Interactivo")
                {
                    if (_player.GetComponent<StarterAssetsInputs>().interact)
                    {
                        AbrirPuzzle(hit);
                        puzzleActivado = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
            else
            {
                Deselect();
            }
        }
    }
    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;//cuando el rayo impacte con el objeto se cambiar� el color del objeto
        ultimoReconocido = transform.gameObject;
        
    }

    void AbrirPuzzle(RaycastHit hit)
    {
        hit.collider.transform.GetComponent<InteractiveObject>().ActivarObjeto();
        Deselect();
    }
    void Deselect()
    {
        if (ultimoReconocido)
        {
            ultimoReconocido.GetComponent<Renderer>().material.color = Color.white;
            ultimoReconocido = null;
        }
    }

     void OnGUI()
    {

        //if (ultimoReconocido != null)
        //{
        //    TextDetect.SetActive(true);
        //}
        //else
        //{
        //    TextDetect.SetActive(false);
        //}
    }
}
