using TMPro;
using UnityEngine;

public class WalletComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountLabel;
    [SerializeField] private ParticleSystem coinParticles;
    [HideInInspector] public int coinAmount = 0;
    [SerializeField] private int silverCoinValue = 1, goldenCoinValue = 5;
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
            UpdateLabel();
            Instantiate(coinParticles, transform.position, Quaternion.identity);
        }
    }
    private void UpdateLabel()
    {
        coinAmountLabel.text = "© " + coinAmount.ToString();
    }
    private void getCoin(int amount, GameObject coin)
    {
        coinAmount += amount;
        UpdateLabel();
        Destroy(coin);
    }
}
