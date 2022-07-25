using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Luis Angel Benítez Muñoz 2022
//https://github.com/Code0End
//lubenmun@gmail.com

public class manejador_juego : MonoBehaviour
{
    public TMP_Text puntos;
    public TMP_Text palabra;
    public GameObject elemento1,elemento2,elemento3,elemento4;

    public List<GameObject> objetivos;
    public List<string> y_spawneados;
    public List<GameObject> actuales;

    bool done = false;
    GameObject a_spawnear;
    int cuantos = 3;
    int id_respuesta;
    int score = 0;

    float posx, posy;
    public GameObject canvas;
    public void OnEnable()
    {
        iniciarronda();
        score = 0;
        puntos.text = score + "/5";
    }

    public void respuesta(int id)
    {
        flush();
        y_spawneados.Clear();
        actuales.Clear();
        if (id == id_respuesta)
        {
            score++;
            puntos.text = score + "/5";
            if (score >= 5)
            {
                elemento1.GetComponent<movimientos_ui>().StopAllCoroutines();
                elemento1.GetComponent<movimientos_ui>().salida();
                elemento2.GetComponent<movimientos_ui>().StopAllCoroutines();
                elemento2.GetComponent<movimientos_ui>().salida();
                elemento3.SetActive(true);
                elemento4.SetActive(true);
            }
        }
        if (score < 5)
        {
            iniciarronda();
        }


    }

    public void iniciarronda()
    {
        for (int i = 0; i < cuantos; i++)
        {
            while (done == false)
            {
                a_spawnear = objetivos[Random.Range(0, objetivos.Count)];
                if (!y_spawneados.Contains(a_spawnear.name))
                {
                    posx = Random.Range(-408f, 408f);
                    posy = Random.Range(-200f, 200f);
                    GameObject n_spawneado = Instantiate(a_spawnear, new Vector3(posx, posy, 0), Quaternion.identity);
                    n_spawneado.transform.SetParent(canvas.transform, false);
                    n_spawneado.transform.SetSiblingIndex(2);
                    n_spawneado.name = a_spawnear.name;
                    y_spawneados.Add(n_spawneado.name);
                    actuales.Add(n_spawneado);
                    done = true;
                }
            }
            done = false;
        }
        id_respuesta = actuales[0].GetComponent<boton>().ID;
        palabra.text = actuales[0].GetComponent<boton>().palabra;
        palabra.GetComponent<AudioSource>().clip = actuales[0].GetComponent<AudioSource>().clip;
    }

    public void flush()
    {
        LeanTween.cancelAll();
        foreach (GameObject elemento in actuales)
        {
            elemento.GetComponent<movimientos_ui>().destruir = true;
            elemento.GetComponent<movimientos_ui>().StopAllCoroutines();
            elemento.GetComponent<movimientos_ui>().salida();
        }
    }

}
