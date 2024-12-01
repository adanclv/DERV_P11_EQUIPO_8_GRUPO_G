using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P11_CampoVision : MonoBehaviour
{
    public bool golpea_jugador;
    public bool entra_angulo_vision_enemigo;
    bool onHit;

    float anguloEntreVectores;
    float anguloVision;
    float radioVision = 50f;

    Vector3 puntoFrontal;

    Transform transform_jugador;

    P11_Laser laser;
    P11_ManagerUI managerUI;

    private void Awake()
    {
        transform_jugador = GameObject.Find("Jugador").GetComponent<Transform>();
    }

    void Start()
    {
        laser = GetComponentInChildren<P11_Laser>();
        managerUI = GameObject.Find("ManagerUI").GetComponent<P11_ManagerUI>();

        anguloVision = 90;
    }

    void Update()
    {
        float angulo = 50f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, angulo, 0) * transform.rotation;
        
        float tiempo = Time.deltaTime;
        puntoFrontal = transform.position + transform.forward * radioVision;
        entra_angulo_vision_enemigo = IsOnAngleOfView();

        if (entra_angulo_vision_enemigo)
        {
            golpea_jugador = GolpeaJugador();
            if (golpea_jugador)
            {
                laser.SetActive(true);
                laser.SetPosition(laser.transform.position, transform_jugador.position);

                tiempo += Time.deltaTime;
                if (tiempo >= 0.01f)
                {
                    managerUI.shakeCamera();
                    managerUI.RecibirDaño();
                    tiempo = 0;
                }
            }
        }
        else
        {
            laser.SetActive(false);
        }

    }

    bool IsOnAngleOfView()
    {
        Vector3 vEnemigo_a_Jugador = transform_jugador.position - transform.position;
        vEnemigo_a_Jugador.y = 0;

        Vector3 vEnemigo_a_PuntoFrontal = puntoFrontal - transform.position;
        vEnemigo_a_PuntoFrontal.y = 0;

        anguloEntreVectores = Vector3.Angle(vEnemigo_a_PuntoFrontal, vEnemigo_a_Jugador);

        return anguloEntreVectores < anguloVision / 2;

    }

    bool GolpeaJugador()
    {
        Vector3 origenRayo = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 destinoRayo = new Vector3(transform_jugador.position.x, transform_jugador.position.y + 0.2f, transform_jugador.position.z);

        Vector3 direccionRayo = destinoRayo - origenRayo;

        RaycastHit hit;

        if (Physics.Raycast(origenRayo, direccionRayo, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
