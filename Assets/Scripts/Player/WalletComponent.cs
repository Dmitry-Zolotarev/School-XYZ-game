using TMPro;
using UnityEngine;

public class WalletComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountLabel;
    [SerializeField] private ParticleSystem coinParticles;
    [HideInInspector] public int coinAmount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SilverCoin")
        {
            coinAmount++;
            UpdateLabel();
            Destroy(collision.gameObject);
        }
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
}
