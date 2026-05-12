using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public PlayerStats player;

    public void UpdateBar (float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        healthBar.value = healthPercent;
    }
}
