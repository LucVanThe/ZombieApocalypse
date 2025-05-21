using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemname;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    };
    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player != null)
            {
            player.HoiMau(amountToChangeStat);
            }
           
        }
    }
}
