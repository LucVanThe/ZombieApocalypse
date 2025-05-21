using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    public string itemTypeColor;
    [SerializeField]
    public string itemName;
    [SerializeField]
    public int quantity;
    [SerializeField] public Sprite itemSprite;
    private InventoryManager InventoryManager;
    public TypeItem typeItem;
    public GunType gunType;
    void Start()
    {
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            int leftOverItem=InventoryManager.Additem(itemName, quantity, itemSprite,typeItem,itemTypeColor);
            Debug.Log("so luong = " + leftOverItem);
            if(leftOverItem <= 0)           
                Destroy(gameObject);           
            else         
                quantity = leftOverItem;         
           
        }
        //if (collision.gameObject.CompareTag("Player"))
        //{

        //    int leftOverItem = InventoryManager.Additem2(itemName, quantity, itemSprite, typeItem);
        //    Debug.Log("so luong = " + leftOverItem);
        //    if (leftOverItem <= 0)
        //        Destroy(gameObject);
        //    else
        //        quantity = leftOverItem;

        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
