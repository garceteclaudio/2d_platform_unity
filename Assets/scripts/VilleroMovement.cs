using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VilleroMovement : MonoBehaviour
{
    public GameObject prefab; // El objeto que vas a disparar
    public Transform puntoDisparo; // Desde dónde sale

    private Animator animator;
    private Keyboard keyboard;
    private Rigidbody2D rb;

    public float speed = 5f;
    public float jumpForce = 8f;

    private bool isGrounded = true;


    private int lifePoints = 5;
    private GameObject[] lifeUI;

    private InventoryManager inventoryManager;


    void Start()
    {
        // inventoryManager = FindObjectOfType<InventoryManager>(); esta deprecada porque si hay más de uno, solo encuentra el primero y puede generar errores. FindAnyObjectByType es la nueva forma recomendada.
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        // Obtener todas las vidas del canvas
        lifeUI = GameObject.FindGameObjectsWithTag("lifePoints");

        animator = GetComponent<Animator>();
        keyboard = Keyboard.current;
        rb = GetComponent<Rigidbody2D>();
    }

    private void RestartGame()
    {
        // Recargar la escena activa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (keyboard == null) return;

        float moveX = 0;

        // SOLO movimiento horizontal derecha
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            moveX = 1;
        }   // SOLO movimiento horizontal izquierda
        else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) {
            moveX = -1;
        }
            
        // Aplicar movimiento (manteniendo velocidad en Y)
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);


        // Dispara con barra espaciadora 
        if (keyboard.ctrlKey.wasPressedThisFrame)
        {
            Disparar();
        }

        // Animaciones
        // Idle solo en suelo
        animator.SetBool("idle", moveX == 0 && isGrounded);

        // SOLO cuando salta
        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            isGrounded = false;
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("salto");
        }


        // SOLO permitir movimiento si está en el suelo
        animator.SetBool("derecha", moveX > 0 && isGrounded);
        animator.SetBool("izquierda", moveX < 0 && isGrounded);


    }

    // Detectar suelo (simple)
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("itemEspada"))
        {
            //Destroy(collision.gameObject);
            inventoryManager.AddEspada();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("itemPistola"))
        {
            inventoryManager.AddPistola();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("itemLifePoints"))
        {
            inventoryManager.AddLifePoints();
            Destroy(collision.gameObject);
        }



        if (collision.gameObject.CompareTag("suelo"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                /* contact.normal.y
                 Esto es la clave Es un vector que indica desde dónde viene la colisión.
                ✔️ solo detecta cuando estás parado arriba
                ❌ ignora paredes
                ❌ ignora techo
                 */
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("zombie"))
        {
            Debug.Log("Golpeado! Life points: " + lifePoints);
            lifePoints -= 1;

            UpdateLifeUI();

            if (lifePoints <= 0)
            {
                Debug.Log("Game Over!");
                RestartGame();
            }
        }
    }

    void Disparar()
    {
        GameObject objeto = Instantiate(prefab, puntoDisparo.position, prefab.transform.rotation);

        Rigidbody2D rbBala = objeto.GetComponent<Rigidbody2D>();
        // Solo ejecutá esto si el objeto realmente tiene ese componente
        if (rbBala != null)
        {
            rbBala.linearVelocity = Vector2.right * 6f;
        }

        Destroy(objeto, 2f);
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < lifeUI.Length; i++)
        {
            // Si el índice es menor que las vidas actuales → activo
            if (i < lifePoints)
                lifeUI[i].SetActive(true);
            else
                lifeUI[i].SetActive(false);
        }
    }


}