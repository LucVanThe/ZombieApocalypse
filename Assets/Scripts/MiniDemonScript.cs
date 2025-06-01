using UnityEngine;

public class MiniDemonScript : MonoBehaviour
{
    [SerializeField] private float energy = 20f;
    private BossMecha bossRef;
    private Enemy enemy;
    private bool isDealth =false;
    private void Start()
    {
        bossRef = FindAnyObjectByType<BossMecha>();
        enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        if (!isDealth)
        {
            Die();
        }
       
    }
    public void Die()
    {
        if (enemy.currentHP <= 0)
        {
            if (bossRef != null)
            {
                Debug.Log("tim thay boss");
               bossRef.TruNangLuong(energy);
            }
            isDealth = true;
        }
    }
}
