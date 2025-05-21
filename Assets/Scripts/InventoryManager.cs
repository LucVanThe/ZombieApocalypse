using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;

    public ItemSlot[] itemSlots;
    public EquipmentSlot[] equipmentSlots;
    public EquippedSlot[] equippedSlot;
    public ItemSO[] itemSOs;
    private Animator animator;
    public bool isCursor; 

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryMenu"))
        {
            Inventory();
           

        }
        if (Input.GetButtonDown("EquipmentMenu"))
        {
            Equipment();
           
        }
        if (InventoryMenu.activeSelf || EquipmentMenu.activeSelf)
        {
            isCursor = false; 
        }
        else
        {
            isCursor = true; 
        }

    }
    public void UseItem(string itemname)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemname == itemname)
            {
                itemSOs[i].UseItem();
            }
        }
    }
    public void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
        }
    }
    public void Equipment()
    {

        if (EquipmentMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
        }
    }
    public int Additem(string itemname, int quantity, Sprite sprite, TypeItem typeitem, string itemtypecolor)
    {
        if (
             typeitem == TypeItem.bullet || typeitem == TypeItem.health)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].isFull == false && itemSlots[i].itemName == itemname || itemSlots[i].quantity == 0)
                {
                    int leftOverItem = itemSlots[i].AddItem(itemname, quantity, sprite, typeitem,itemtypecolor);
                    if (leftOverItem > 0)
                    { leftOverItem = Additem(itemname, leftOverItem, sprite, typeitem, itemtypecolor); }

                    return leftOverItem;
                }
            }

            return quantity;
        }
        else
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].isFull == false && (equipmentSlots[i].itemName == itemname || equipmentSlots[i].quantity == 0))
                {
                    int leftOverItem = equipmentSlots[i].AddItem(itemname, quantity, sprite, typeitem,itemtypecolor);
                    if (leftOverItem > 0)
                    { leftOverItem = Additem(itemname, leftOverItem, sprite, typeitem, itemtypecolor); }

                    return leftOverItem;
                }
            }
            return quantity;
        }
    }

    public void DeselectAllSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisitemSelect = false;
        }
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].selectedShader.SetActive(false);
            equipmentSlots[i].thisitemSelect = false;
        }
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisitemSelect = false;
        }
    }
    public void SelectLayer()
    {
        for (int j = 1; j < animator.layerCount; j++)
        {
            animator.SetLayerWeight(j, 0f);
        }

        // Ưu tiên điều kiện phức hợp trước
        if (equippedSlot[1].itemtypecolor == "Xanh" && equippedSlot[3].itemName == "M4A1")
        {
            animator.SetLayerWeight(7, 1f);
        }
        else if (equippedSlot[1].itemtypecolor == "Trang" && equippedSlot[3].itemName == "M4A1")
        {
            animator.SetLayerWeight(8, 1f);
        }
        else if (equippedSlot[1].itemtypecolor == "Tim" && equippedSlot[3].itemName == "M4A1")
        {
            animator.SetLayerWeight(11, 1f);
        }
        else if (equippedSlot[1].itemtypecolor == "Trang" && equippedSlot[3].itemName == "AR15")
        {
            animator.SetLayerWeight(9, 1f);
        }
        else if (equippedSlot[1].itemtypecolor == "Tim" && equippedSlot[3].itemName == "AR15")
        {
            animator.SetLayerWeight(12, 1f);
        }
        if (equippedSlot[1].itemtypecolor == "Xanh" && equippedSlot[3].itemName == "AR15")
        {
            animator.SetLayerWeight(10, 1f);
        }
        switch (equippedSlot[1].itemtypecolor)
        {
            case "Xanh":
                animator.SetLayerWeight(4, 1f);
                break;
            case "Tim":
                animator.SetLayerWeight(5, 1f);
                break;
            case "Trang":
                animator.SetLayerWeight(6, 1f); 
                break;
            default:
                animator.SetLayerWeight(1, 1f);
                break;
        }
        switch (equippedSlot[3].itemName)
        {
            case "M4A1":
                animator.SetLayerWeight(2, 1f);
                break;
            case "AR15":
                animator.SetLayerWeight(3, 1f);
                break;
            default:
                animator.SetLayerWeight(1, 1f);
                break;
        }
    
    }
}
    public enum TypeItem
    {
        consumable,
        phukien,
        gun,
        armo,
        shoe,
        mu,
        bullet,
        health,
        none,
    };
    public enum GunType
    {
        ak, scar, none,
    };
