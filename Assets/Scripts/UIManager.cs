using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI waveText, coinsText, healthText;

    void Awake(){ if(Instance!=null && Instance!=this){ Destroy(gameObject); return; } Instance=this; }
    public void UpdateWave(int w){ if(waveText) waveText.text=$"Wave: {w}"; }
    public void UpdateCoins(int c){ if(coinsText) coinsText.text=$"Coins: {c}"; }
    public void UpdateHealth(int hp,int max){ if(healthText) healthText.text=$"HP: {hp}/{max}"; }
}
