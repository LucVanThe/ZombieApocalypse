using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private bulletAR weapon;
    public float health, attack,speed;
    [SerializeField]
    private TMP_Text healthText,atackText, speedText;
    private Player player;
    public void Start()
    {
       
        player = GameObject.Find("Player").GetComponent<Player>();
        UpdateEquipStat();
    }
    public void UpdateEquipStat()
    {     
        healthText.text = health.ToString();
        speedText.text = speed.ToString();
        atackText.text = attack.ToString();
        if (weapon != null)
        {
            weapon.UpdateDamage();
        }
        if (player != null)
        {
            player.UpdateHP();
        }
    }
}
