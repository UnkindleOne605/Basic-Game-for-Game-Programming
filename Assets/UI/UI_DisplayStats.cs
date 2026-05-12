using UnityEngine;
using System.Collections;
using TMPro;

public class UI_DisplayStats : MonoBehaviour
{
    public PlayerStats playerStats;
    public TMPro.TextMeshProUGUI attackText;
    public TMPro.TextMeshProUGUI moveSpeedText;
    public TMPro.TextMeshProUGUI armorText;
    public TMPro.TextMeshProUGUI healthRegenText;
    public TMPro.TextMeshProUGUI rangeText;
    public TMPro.TextMeshProUGUI fireRateText;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI goldText;
    public TMPro.TextMeshProUGUI timerText;

    void Update()
    {
        {
            if (PauseMenu.isPaused) 
            {
                return;
            } 
            else 
            {
                healthText.text = "Health: " + playerStats.currentHealth.ToString() + "/" + playerStats.modifiedMaxHealth.ToString();

                attackText.text = "Attack Damage: " + playerStats.modifiedAttackDamage.ToString();
                moveSpeedText.text = "Move Speed: " + playerStats.modifiedMoveSpeed.ToString();
                armorText.text = "Armor: " + playerStats.modifiedArmor.ToString();
                healthRegenText.text = "Health Regen: " + playerStats.modifiedHealthRegen.ToString();
                rangeText.text = "Attack Range: " + playerStats.modifiedAttackRange.ToString();
                fireRateText.text = "Fire Rate: " + playerStats.modifiedFireRate.ToString();

                goldText.text = "Gold: " + playerStats.goldAmount.ToString();

                float worldTimer = Time.timeSinceLevelLoad;
                int minutes = Mathf.FloorToInt(worldTimer / 60);
                timerText.text = Mathf.Floor(worldTimer / 60) + ":" + Mathf.Floor(worldTimer - (minutes * 60));
            }
        }
    } 
}
