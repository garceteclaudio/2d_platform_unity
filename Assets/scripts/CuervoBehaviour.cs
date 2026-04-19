using UnityEngine;

public class CuervoBehaviour : MonoBehaviour
{
    [Header("Attack Spawn")]
    public GameObject bloodPrefab;
    public Transform spawnPoint;

    [Header("Movement")]
    public Transform puntoA;
    public Transform puntoB;
    public float speed = 2f;

    private bool movingToB = true;

    [Header("Attack")]
    public float attackInterval = 3f;
    private float attackTimer;

    private Animator animator;

    void SpawnBlood() // llamado por Animation Event
    {
        Debug.Log("SPAWN BLOOD");
        Instantiate(bloodPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackTimer = attackInterval;
    }

    private void Update()
    {
        Move();
        HandleAttack();
    }

    void Move()
    {
        Transform target = movingToB ? puntoB : puntoA;

        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(target.position.x, target.position.y),
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
            Flip();
        }
    }

    void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            animator.SetTrigger("attack");
            attackTimer = attackInterval;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}