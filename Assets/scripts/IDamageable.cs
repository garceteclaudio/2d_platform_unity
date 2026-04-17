public interface IDamageable
{
    /*
    Antes:
        "Si sos bala → te hago daño"
 
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


    Ahora:

        "Si podés recibir daño → te aplico daño"

        🔥 Esto es polimorfismo + desacoplamiento real
    */

    void TakeDamage(int amount);
}