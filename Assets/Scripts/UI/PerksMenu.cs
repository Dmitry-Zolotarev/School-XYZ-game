using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerksMenu : MonoBehaviour
{
    
    [SerializeField] private Sprite SilverButtonSprite, GreenButtonSprite;
    [SerializeField] private TextMeshProUGUI perkScoreLabel;
    [SerializeField] private List<Button> perkButtons;  
    private string selectedPerkName = "";
    private PerksComponent perks;
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        perks = player?.GetComponent<PerksComponent>();
    }
    public void SelectPerkButton(string name)  => selectedPerkName = name;
    public void BuySelectedPerk()
    {
        if (perks.BuyPerk(selectedPerkName))
        {       
            int i = perks.GetIndex(selectedPerkName);
            perkButtons[i].interactable = false;
            var buttonImage = perkButtons[i]?.GetComponent<Image>();
            if (buttonImage != null && SilverButtonSprite != null) buttonImage.sprite = SilverButtonSprite;
            UpdatePerkScoreLabel();
        }
    }
    public void UpdatePerkScoreLabel()
    {
        int perkScore = perks != null ? perks.perkScore : 0;
        perkScoreLabel.SetText("Perk score: " + perkScore);
    }
}
