
[System.Serializable]
public struct PlayerData
{
    public int HP, maxHP, coins, damage;

    public PlayerData(int HP, int maxHP, int coins, int damage)
    {
        this.HP = HP;
        this.maxHP = maxHP;
        this.coins = coins;
        this.damage = damage;
    }
    public int Sum()
    {
        return HP + maxHP + coins + damage;
    }

}
