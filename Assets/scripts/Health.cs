using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private GameObject[] lifeUI;

    void Start()
    {
        currentHealth = maxHealth;
        lifeUI = GameObject.FindGameObjectsWithTag("lifePoints");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUI();
        if (currentHealth <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < lifeUI.Length; i++)
            lifeUI[i].SetActive(i < currentHealth);
    }
}