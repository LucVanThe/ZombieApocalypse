using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private Gun gun;
    private bulletAR weapon;
    public float health, attack,speed,shotdelay;
    [SerializeField]
    private TMP_Text healthText,atackText, speedText, shotdelaytext;
    private Player player;
    public void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        player = GameObject.Find("Player").GetComponent<Player>();
        UpdateEquipStat();
    }
    public void UpdateEquipStat()
    {     
        healthText.text = health.ToString();
        speedText.text = speed.ToString();
        atackText.text = attack.ToString();
        shotdelaytext.text = shotdelay.ToString();
        if (weapon != null)
        {
            weapon.UpdateDamage();
        }
        if (player != null)
        {
            player.UpdateHP();
        }
        if (gun != null)
        {
            
            gun.UpdateShotdelay();
            Debug.Log("tim thay sung");
        }
    }
}
