using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private Gun gun;
    private bulletAR weapon;
    public float health, attack,speed,shotdelay,reloadtime;
    [SerializeField]
    private TMP_Text healthText,atackText, speedText, shotdelaytext;
    private Player player;
    public void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        player = GameObject.Find("Player").GetComponent<Player>();
        UpdateEquipStat();
        healthText.text = player.currentHP + " / " + health.ToString();
    }
    public void UpdateEquipStat()
    {     
       
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
        healthText.text = player.currentHP +" / " + health.ToString();
        speedText.text = speed.ToString();
        atackText.text = attack.ToString();
        shotdelaytext.text = shotdelay.ToString();
    }
    private void Update()
    {
        healthText.text = player.currentHP + " / " + health.ToString();
    }
}
