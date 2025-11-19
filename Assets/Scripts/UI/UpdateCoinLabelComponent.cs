using TMPro;
using UnityEngine;

public class UpdateCoinLabelComponent : MonoBehaviour
{
    private WalletComponent wallet;
    private TextMeshProUGUI label;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        wallet = player.GetComponent<WalletComponent>();
        label = GetComponent<TextMeshProUGUI>();

        if (wallet != null && label != null)
        {
            wallet.onDrop.AddListener(UpdateLabel);
            wallet.onGetCoin.AddListener(UpdateLabel);
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        label?.SetText("© " + wallet.coinAmount);
    }
}
