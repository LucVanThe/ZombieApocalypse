using UnityEngine;
[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public float health, attack, speed;
    public void EquipItem()
    {
        PlayerStat playerStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        playerStat.health += health;
        playerStat.attack += attack;
        playerStat.speed += speed;
        playerStat.UpdateEquipStat();
    }
    public void UnEquipItem()
    {
        PlayerStat playerStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        playerStat.health -= health;
        playerStat.attack -= attack;
        playerStat.speed -= speed;
        playerStat.UpdateEquipStat();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
