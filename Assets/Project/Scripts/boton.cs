using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Luis Angel Benítez Muñoz 2022
//https://github.com/Code0End
//lubenmun@gmail.com

public class boton : MonoBehaviour
{
    public bool salida = false;
    public bool intro = false;
    public int accion = 0;
    public int ID;
    public string palabra;
    public List<GameObject> elementos_apagar;
    public List<GameObject> elementos_encender;

    public AudioSource audio;
    public manejador_juego manejador;
    public void presionar()
    {
        LeanTween.cancelAll();
        if (intro == true)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject elemento in elementos_apagar)
        {
            elemento.GetComponent<movimientos_ui>().StopAllCoroutines();
            elemento.GetComponent<movimientos_ui>().salida();
        }

        if (salida == false)
        {
            foreach (GameObject elemento in elementos_encender)
            {
                elemento.SetActive(true);
            }
        }
        else
        {
            LeanTween.delayedCall(1.4f,cambiarescena);
        }
    }
    public void cambiarescena()
    {
        SceneManager.LoadScene(0);
    }

    public void accionboton(int n)
    {
        //acciones del botón
        switch (n)
        {
            case 1:
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case 2:
                manejador = GameObject.FindGameObjectWithTag("manejador").GetComponent<manejador_juego>();
                manejador.respuesta(ID);
                break;
            default:
                break;
        }
    }
}
