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

    public void BuyPerk(string name)
    {
        int i = GetIndex(name);

        if (i >= 0 && perkScore > 0 && !perks[i].unlocked)
        {
            perkScore--;
            perks[i].unlocked = true;
            Debug.Log($"[Perks] Перк \"{name}\" куплен.");
        }       
    }

    public bool IsUnlocked(string name)
    {
        int i = GetIndex(name);
        if (i == -1)
        {
            Debug.LogError($"[Perks] IsUnlocked: не найден перк \"{name}\"");
            return false;
        }
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
            if (perks[i].name == name)
                return i;
        }

        Debug.LogWarning($"[Perks] GetIndex: нет перка с именем \"{name}\"");
        return -1;
    }
}
