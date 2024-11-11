using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("OnCollisionEnter has been triggered with object: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Monster"))
        {
            TakeDamage(10); // 扣除10點血量
            UnityEngine.Debug.Log("碰到怪物，扣除10點血量");
        }
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
