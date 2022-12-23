using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class AdminMenu : MonoBehaviour
{
    public GameObject activeMenu;

    public string jsonPath;
    public string baneosActualesPath;
    public string historicoPath;

    [SerializeField]
    public List<Admin> listaAdmins;

    [SerializeField]
    public List<Baneo> listaBaneos;

    [SerializeField]
    public List<Baneo> listaHistorico;

    public TMP_InputField nombreAdmin;
    public TMP_InputField contraseñaAdmin;

    public GameObject elementos;

    public GameObject adminElementPrefab;

    public GameObject baneos;
    public GameObject baneoPrefab;
    public GameObject historico;
    public GameObject historicoPrefab;


    private void Start()
    {
        listaAdmins = new List<Admin>();
        jsonPath = Application.dataPath + "/Json/adminList.json";
        baneosActualesPath = Application.dataPath + "/Json/baneos.json";
        historicoPath = Application.dataPath + "/Json/historico.json";
        LeerListaAdmins();
        LeerListaBaneos();
        LeerListaHistorico();
        CreateListHistorico();
        UpdateList();
        UpdateListBaneos();
    }
    public void ChangeActiveMenu(GameObject newMenu)
    {
        activeMenu = newMenu;
        activeMenu.SetActive(true);
    }

    public void DeactivateActiveMenu()
    {
        if(activeMenu != null)
        activeMenu.SetActive(false);
    }

    public void SaveAdmin()
    {
        Admin admin = new Admin();
        if(nombreAdmin.text.Length!= 0)
        {
            admin.nombre = nombreAdmin.text;
            admin.contraseña = contraseñaAdmin.text;

            listaAdmins.Add(admin);

            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(listaAdmins));
            nombreAdmin.text = "";
            contraseñaAdmin.text = "";

            UpdateList();
        }
    }

    public void LeerListaAdmins()
    {
        listaAdmins = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(jsonPath));
    }

    public void LeerListaBaneos()
    {
        listaBaneos = JsonConvert.DeserializeObject<List<Baneo>>(File.ReadAllText(baneosActualesPath));
    }

    public void LeerListaHistorico()
    {
        listaHistorico = JsonConvert.DeserializeObject<List<Baneo>>(File.ReadAllText(historicoPath));
    }

    public void SaveList()
    {
        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(listaAdmins));
    }

    public void SaveBaneosList()
    {
        File.WriteAllText(baneosActualesPath, JsonConvert.SerializeObject(listaBaneos));
    }

    public void UpdateList()
    {
        foreach(Transform child in elementos.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        LeerListaAdmins();

        foreach(Admin a in listaAdmins)
        {
            GameObject obj = Instantiate(adminElementPrefab);
            obj.transform.SetParent(elementos.transform, false);
            obj.GetComponentInChildren<TMP_Text>().text = a.nombre;
            obj.GetComponentInChildren<Adminname>().name = a.nombre;
            obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameObject.FindGameObjectWithTag("AdminMenu").GetComponent<AdminMenu>().DeleteAdmin(obj); });
        }
    }

    public void UpdateListBaneos()
    {
        foreach (Transform child in baneos.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        LeerListaAdmins();

        foreach (Baneo a in listaBaneos)
        {
            GameObject obj = Instantiate(baneoPrefab);
            obj.transform.SetParent(baneos.transform, false);
            TMP_Text[] textos = obj.GetComponentsInChildren<TMP_Text>();
            textos[0].text = "Nombre: " + a.nombre + " - Id: " + a.id;
            textos[1].text = "Razon: " + a.razon;
            textos[2].text = "Apelacion: " + a.apelacion;
            obj.GetComponentInChildren<Adminname>().name = a.nombre;
            obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameObject.FindGameObjectWithTag("AdminMenu").GetComponent<AdminMenu>().Desbanear(obj); });
        }
    }

    public void CreateListHistorico()
    {
        foreach (Transform child in historico.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Baneo a in listaHistorico)
        {
            GameObject obj = Instantiate(historicoPrefab);
            obj.transform.SetParent(historico.transform, false);
            TMP_Text[] textos = obj.GetComponentsInChildren<TMP_Text>();
            textos[0].text = "Nombre: " + a.nombre + " - Id: " + a.id;
            textos[1].text = "Razon: " + a.razon;
            textos[2].text = "Apelacion: " + a.apelacion;
        }
    }

    public void DeleteAdmin(GameObject prefab)
    {
        string nombre = prefab.GetComponentInChildren<Adminname>().name;

        int indice = 0;
        bool changed = false;
        for (int i = 0; i < listaAdmins.Count; i++)
        {
            if (listaAdmins[i].nombre == nombre)
            {
                indice = i;
                changed = true;
            }
        }

        if(changed == true)
        {
            listaAdmins.RemoveAt(indice);
        }
        SaveList();
        UpdateList();
    }

    public void Desbanear(GameObject prefab)
    {
        string nombre = prefab.GetComponentInChildren<Adminname>().name;

        int indice = 0;
        bool changed = false;
        for (int i = 0; i < listaBaneos.Count; i++)
        {
            if (listaBaneos[i].nombre == nombre)
            {
                indice = i;
                changed = true;
            }
        }

        if (changed == true)
        {
            listaBaneos.RemoveAt(indice);
        }
        SaveBaneosList();
        UpdateListBaneos();
    }
}

[System.Serializable]
public class Admin
{
    public string nombre;
    public string contraseña;
}

[System.Serializable]
public class Baneo
{
    public string nombre;
    public string id;
    public string razon;
    public string apelacion;
}
