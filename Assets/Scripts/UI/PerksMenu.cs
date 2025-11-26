using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PerksMenu : MonoBehaviour, IPointerClickHandler
{
    
    [SerializeField] private Sprite SilverButtonSprite, GreenButtonSprite;
    [SerializeField] private TextMeshProUGUI perkScoreLabel;
    [SerializeField] private List<Button> perkButtons;
    private string selectedPerkName = "";
    private int selectedButtonIndex;
    private PerksComponent perks;
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        perks = player?.GetComponent<PerksComponent>();
    }
    public void SelectPerk(string name)  => selectedPerkName = name;
    public void OnPointerClick(PointerEventData eventData) => selectedPerkName = "";
    public void SelectIndex(int i) => selectedButtonIndex = i;
    public void BuySelectedPerk()
    {
        if (perks.BuyPerk(selectedPerkName))
        {       
            int i = selectedButtonIndex;
            perkButtons[i].interactable = false;
            var buttonImage = perkButtons[i]?.GetComponent<Image>();
            if (buttonImage != null && SilverButtonSprite != null) buttonImage.sprite = SilverButtonSprite;
        }
    }
    public void Update()
    {
        int perkScore = perks != null ? perks.perkScore : 0;
        perkScoreLabel.SetText("Perk score: " + perkScore);
    }   
}
