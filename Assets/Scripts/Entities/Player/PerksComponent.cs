using System.Collections.Generic;
using UnityEngine;

public class PerksComponent : MonoBehaviour
{
    [System.Serializable]
    class Perk
    {
        public string name = "";
        public bool unlocked = false;
    }

    [SerializeField] private List<Perk> perks;
    public int perkScore = 0;

    public bool BuyPerk(string name)
    {
        int i = GetIndex(name);
        if (i >= 0 && perkScore > 0 && !perks[i].unlocked)
        {
            perkScore--;
            perks[i].unlocked = true;
            return perks[i].unlocked;
        }
        return false;
    }

    public bool IsUnlocked(string name)
    {
        int i = GetIndex(name);
        if (i == -1) return false;
        return perks[i].unlocked;
    }

    public string GetName(int index)
    {
        if (index < 0 || index >= perks.Count)
        {
            Debug.LogError($"[Perks] GetName: индекс {index} вне диапазона!");
            return "";
        }
        return perks[index].name;
    }

    public int GetIndex(string name)
    {
        for (int i = 0; i < perks.Count; i++)
        {
            if (perks[i].name == name) return i;

        }
        return -1;
    }
}
