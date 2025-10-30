using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public int coins = 0;

    void Awake(){ if(Instance!=null && Instance!=this){ Destroy(gameObject); return; } Instance=this; }
    public void AddCoins(int amt){ coins += amt; UIManager.Instance.UpdateCoins(coins); }
    public bool SpendCoins(int amt){ if(coins<amt) return false; coins -= amt; UIManager.Instance.UpdateCoins(coins); return true; }
}
