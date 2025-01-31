using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform orientacion;
    [SerializeField] Transform player;
    [SerializeField] Transform grafico;
    [SerializeField] Rigidbody rb;

    [SerializeField] float velRotacion = 7;

    private void Start()
    {
        //Bloquear el puntero y hacerlo invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Orientación de la rotación
        Vector3 dirVista = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientacion.forward = dirVista.normalized;

        //Rotar al jugador
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        Vector3 direccion = orientacion.forward * inputVertical + orientacion.right * inputHorizontal;

        if (direccion != Vector3.zero)
        {
            grafico.forward = Vector3.Slerp(grafico.forward, direccion.normalized, Time.deltaTime * velRotacion);
        }
    }
}
