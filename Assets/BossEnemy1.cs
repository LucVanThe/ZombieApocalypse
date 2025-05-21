using UnityEngine;

public class BossEnemy1 : Enemy
{
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject minienemy;
    [SerializeField] private float skillCD = 5f;
    private float nextskilltimw = 0f;
    protected override void Update()
    {
        base.Update();
        if(Time.time >= nextskilltimw)
        {
            useskill();
        }
    } 
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            player.TakeDamage(Enterdamage);
        }
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(Staydamage);
        }
    }
    private void CreateZombie()
    {
        Instantiate(minienemy, transform.position,Quaternion.identity);
    }
    private void HoiMau(float health)
    {
        currentHP = Mathf.Min(currentHP + health, MaxHp);
        UPdateHPbar();
    }
    private void ramdomskill()
    {
    int randomskill = Random.Range(0, 2);
            switch (randomskill) {
                case 0: 
                    HoiMau(hpValue); break;
                case 1:
                    CreateZombie();break;
            }
    }
    private void useskill()
    {

        nextskilltimw = Time.time + skillCD;
        ramdomskill();
    }
}
