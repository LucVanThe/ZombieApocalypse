using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    [SerializeField] public float dropChance = 1f;
}
public class EnemyDropItem : MonoBehaviour
{
    public List<DropItem> dropItems;
    private Enemy enemy;
    private bool  enable=false;
    private void Start()
    {
        enemy = GetComponent<Enemy>();

    }
    private void Update()
    {
        Die();
    }
    public void Die()
    {
        if (enable == true) return;
        if (enemy.currentHP <= 0)
        {
            Debug.Log("ke dich da chet");
            TryDropRandomItem();
            enable = true;
           
        }
    }

    void TryDropRandomItem()
    {
        if (dropItems.Count == 0) return;     
        int randomIndex = Random.Range(0, dropItems.Count);
        DropItem selectedItem = dropItems[randomIndex];      
        if (Random.value <= selectedItem.dropChance)
        {
            Instantiate(selectedItem.itemPrefab, transform.position, Quaternion.identity);
        }
        
    }

}
