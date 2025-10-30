using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; public float iFrameTime = 0.6f;
    [HideInInspector] public int currentHealth; float nextDamageTime;

    void Awake(){ currentHealth = maxHealth; UIManager.Instance.UpdateHealth(currentHealth, maxHealth); }
    public void Heal(int amt){ currentHealth = Mathf.Min(maxHealth, currentHealth + amt); UIManager.Instance.UpdateHealth(currentHealth, maxHealth); }
    public void TakeDamage(int dmg){
        if(Time.time < nextDamageTime) return; nextDamageTime = Time.time + iFrameTime;
        currentHealth -= dmg; UIManager.Instance.UpdateHealth(currentHealth, maxHealth);
        if(currentHealth <= 0){ Time.timeScale = 0f; Debug.Log("Player died."); }
    }
}
