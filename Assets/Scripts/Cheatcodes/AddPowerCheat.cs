using System.Collections.Generic;
using UnityEngine;

public class AddPowerCheat : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    private HPComponent health;
    private Inventory inventory;
    private Leveling leveling;
    [SerializeField] private int HPbonus = 900, XPbonus = 900;
    private void Start()
    {
        health = GetComponent<HPComponent>();
        inventory = GetComponent<Inventory>();
        leveling = GetComponent<Leveling>();
    }
    [ContextMenu("Activate")]
    public void ActivateCheat()
    {
        leveling?.GetXP(XPbonus);
        health?.UpdateMaxHP(HPbonus);
        foreach (var item in items) inventory?.PickItem(item);       
    }
    
}
