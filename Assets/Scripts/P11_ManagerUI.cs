using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class P11_ManagerUI : MonoBehaviour
{
    float damage;
    float vida;
    float duracion;
    float amplitud;
    float transcurrido;
    float tiempo;
    bool result;

    Image barraVida;
    TextMeshProUGUI tiempoUI;
    TextMeshProUGUI resultadoUI;
    Transform cam;

    private void Awake()
    {
        barraVida = GameObject.Find("Vida").GetComponent<Image>();
        tiempoUI = GameObject.Find("Tiempo").GetComponent<TextMeshProUGUI>();
        resultadoUI = GameObject.Find("Resultado").GetComponent<TextMeshProUGUI>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Start()
    {
        result = false;
        vida = 1;
        tiempo = 30f;
        damage = 0.005f;
        duracion = 0.01f;
        amplitud = 0.2f;
        barraVida.fillAmount = vida;

        tiempoUI.text = formatoTiempo(tiempo);
        StartCoroutine("tiempoJuego");
    }

    void Update()
    {
        if (tiempo <= 0 && vida > 0)
        {
            resultadoUI.text = "Ganaste";
            result = true;
        }
        else if (vida <= 0)
        {
            result = true;
            resultadoUI.text = "Perdiste";
        }

        if (result)
        {
            Invoke("ChangeScene", 2f);
            
            result = false;
        }
    }

    public void RecibirDaño()
    {
        vida -= damage;
        barraVida.fillAmount = vida;
    }

    public void shakeCamera()
    {
        StartCoroutine("shake");
    }

    string formatoTiempo(float tiempo)
    {
        int minutos = (int)tiempo / 60;
        int segundos = (int)tiempo % 60;
        return minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator shake()
    {
        Vector3 posicion_original = cam.position;

        transcurrido = 0;
        float x, y;
        while (transcurrido < duracion)
        {
            x = Random.Range(-1f, 1f) * amplitud;
            y = Random.Range(-1f, 1f) * amplitud;

            cam.position = new Vector3(posicion_original.x + x,
                posicion_original.y + y,
                posicion_original.z
                );

            transcurrido += Time.deltaTime;

            yield return null;
        }
        cam.position = posicion_original;
    }

    IEnumerator tiempoJuego()
    {
        while (tiempo > 0)
        {
            yield return new WaitForSeconds(1);
            tiempo--;
            tiempoUI.text = formatoTiempo(tiempo);
        }
    }
}
