using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer; //Referencia al mezclado de audio general

    public Resolution[] resolutions; //Lista de las resoluciones disponibles en la pantalla del jugador

    public TMP_Dropdown resolutionDropdown; //El elemento de la UI que lleva la lista de las resoluciones

    private void Start()
    {
        resolutions = Screen.resolutions; //LLenamos el array de las resoluciones con las de la pantalla de juego

        resolutionDropdown.ClearOptions(); //Dejamos vac�as las opciones del desplegable
        addResolutions(); //Rellenamos el desplegable
    }

    public void addResolutions()
    {
        List<string> options = new List<string>();//Creamos una lista de strings

        int currentResolutionIndex = 0; //El �ndice de la resolucion actual

        for (int i = 0; i < resolutions.Length; i++) //Recorremos el array de resoluciones
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "  @" + resolutions[i].refreshRate + "Hz"; //Por cada resolucion creamos un string
            options.Add(option); //A�adimos la opci�n a la lista

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                //Unity no deja comparar resoluciones as� que primero comprobamos la anchura y luego al altura
                currentResolutionIndex = i; //le asignamos el �ndice
            }
        }

        resolutionDropdown.AddOptions(options); //A�adimos esa lista de strings al desplegable
        resolutionDropdown.value = currentResolutionIndex; //le ponemos que el valor seleccionado sea el actual de la pantalla
        resolutionDropdown.RefreshShownValue(); //Recarga el texto de la opci�n
    }

    public void SetVolume(float volume) //Cambia el volumen segun el float que le proporciona el slider
    {
        mainMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQuality(int qualityIndex) //Cambia la calidad en base al �ndice de las calidades del proyecto
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen) //Dependiendo del booleano del Toggle se pondr� en pantalla completa o no
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex) //Cambia la resoluci�n
    {
        Resolution resolution = resolutions[resolutionIndex]; //Primero creamos una resoluci�n con los valores actuales seleccionados
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); //Luego se lo notificamos al sistema
    }
}
