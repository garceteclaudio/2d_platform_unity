using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Puntos de patrulla")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distanciaMinima = 0.1f;

    private Transform objetivo;
    private Rigidbody2D rb;

    private int direccion = 1; // 1 derecha, -1 izquierda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objetivo = puntoB;
    }

    void Update()
    {
        if (puntoA == null || puntoB == null) return;

        VerificarCambioDireccion(); // 👈 PRIMERO
        Mover();
        AplicarFlip();
    }

    void Mover()
    {
        float diferencia = objetivo.position.x - transform.position.x;

        // Evitar dirección 0
        if (Mathf.Abs(diferencia) > 0.01f)
        {
            direccion = diferencia > 0 ? 1 : -1;
        }

        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);
    }

    void VerificarCambioDireccion()
    {
        float distancia = Mathf.Abs(transform.position.x - objetivo.position.x);

        if (distancia < distanciaMinima)
        {
            objetivo = (objetivo == puntoA) ? puntoB : puntoA;

            // Cambiar dirección inmediatamente
            direccion *= -1;
        }
    }

    void AplicarFlip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccion;
        transform.localScale = scale;
    }
}