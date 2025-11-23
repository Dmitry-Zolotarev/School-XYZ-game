using TMPro;
using UnityEngine;

public class UpdateHPLabelComponent : MonoBehaviour
{
    private HPComponent health;
    private Leveling leveling;
    private TextMeshProUGUI label;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        health = player.GetComponent<HPComponent>();
        leveling = player.GetComponent<Leveling>();
        label = GetComponent<TextMeshProUGUI>();

        if (health != null && label != null)
        {
            health.onDamage.AddListener(UpdateLabel);
            health.onHeal.AddListener(UpdateLabel);
            leveling.OnLevelUP.AddListener(UpdateLabel);
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        label.SetText("♥ " + health.HP);
    }
}
