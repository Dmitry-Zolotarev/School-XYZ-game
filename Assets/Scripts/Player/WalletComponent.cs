using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WalletComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinParticles;
    
    
    [SerializeField] private int silverCoinValue = 1, goldenCoinValue = 5;
    
    [HideInInspector] public int coinAmount = 0;
    public UnityEvent onDrop, onGetCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SilverCoin") getCoin(silverCoinValue, collision.gameObject);
        if (collision.gameObject.tag == "GoldenCoin") getCoin(goldenCoinValue, collision.gameObject);
    }
    public void DropAllCoins()
    {
        if (coinAmount > 0)
        {
            coinAmount = 0;
            Instantiate(coinParticles, transform.position, Quaternion.identity);
            onDrop?.Invoke();
        }
    }
    private void getCoin(int amount, GameObject coin)
    {
        coinAmount += amount;
        Destroy(coin);
        onGetCoin?.Invoke();
    }
}
