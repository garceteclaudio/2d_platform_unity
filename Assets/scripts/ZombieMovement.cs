using UnityEngine;

public class ZombieMovement : MonoBehaviour, IDamageable
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private bool movingRight = true;
    private float threshold = 0.1f;

    private int lifePoints = 4;


    void Start()
    {
        // Esto quita a los puntos de adentro del zombie y los deja sueltos en la escena
        // pero mantienen la posición que les diste en el editor.
        if (pointA != null) pointA.parent = null;
        if (pointB != null) pointB.parent = null;
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            // Mover hacia pointB
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(pointB.position.x, transform.position.y),
                speed * Time.deltaTime
            );

            // Si llegó a pointB
            if (Mathf.Abs(transform.position.x - pointB.position.x) < threshold)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            // Mover hacia pointA
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(pointA.position.x, transform.position.y),
                speed * Time.deltaTime
            );

            // Si llegó a pointA
            if (Mathf.Abs(transform.position.x - pointA.position.x) < threshold)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // invierte el sprite
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            // Dibuja una línea entre los puntos
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);

            // Dibuja esferas en los extremos
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.position, 0.3f);
        }
    }



    /*
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("bala"))
    {
        lifePoints -= 1;
        Destroy(collision.gameObject);
        if (lifePoints == 0) {
            // Destruir el zombie (opcional pero recomendado)
            Destroy(gameObject);
        }

    }
}
*/


    public void TakeDamage(int amount)
    {
        lifePoints -= amount;

        Debug.Log("Zombie recibió daño. Vida restante: " + lifePoints);

        if (lifePoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}