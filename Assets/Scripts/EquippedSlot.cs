using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class EquippedSlot : MonoBehaviour,IPointerClickHandler
{
    //Giao dien o
 
    [SerializeField] private Image SlotImage;
    [SerializeField] private TMP_Text SlotName;
    [SerializeField] private Image playerDisplayImage;

    //Du lieu o
    [SerializeField] private TypeItem itemtype = new TypeItem();
    [SerializeField] private GunType gunType = new GunType();
    private Sprite itemsprite;
    public string itemName;
    private Item item;
    //gia tri khac
    [SerializeField]public bool SlotInUse;
    public bool thisitemSelect;
    [SerializeField] public GameObject selectedShader;
    [SerializeField]
    private Sprite emptySprite;
    private Animator animator;
    private EquipmentSlot equipmentSlot;
    private EquippedSlot[] equippedSlots;
    private InventoryManager inventoryManager;
    private EquipmenrSoBly EquipmenrSoBly;
    public string itemtypecolor;
    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        EquipmenrSoBly= GameObject.Find("InventoryCanvas").GetComponent<EquipmenrSoBly>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        equipmentSlot = GetComponent<EquipmentSlot>();
    }
    public void EquipGear(Sprite itemSprite, string itemName, string itemtypecolor)
    {
        if (SlotInUse)
            UnEquipGear();
        this.itemtypecolor = itemtypecolor;
        this.itemsprite = itemSprite;
        SlotImage.sprite = this.itemsprite;
        SlotName.enabled = false;
        //Cap nhat du lieu
        this.itemName = itemName;
        playerDisplayImage.sprite = itemSprite;
        //Cap nhat chi so
        if (EquipmenrSoBly != null && EquipmenrSoBly.equipmentSOs != null)
        {
            for (int i = 0; i < EquipmenrSoBly.equipmentSOs.Length; i++)
            {
                var equipment = EquipmenrSoBly.equipmentSOs[i];
                if (equipment != null && equipment.itemName == this.itemName)
                {
                    equipment.EquipItem();
                }
            }
        }
        else
        {
            Debug.LogError("EquipmenrSoBly or equipmentSOs is null!");
        }
        SlotInUse = true;
        inventoryManager.SelectLayer();
    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        if(thisitemSelect && SlotInUse)
        {
            UnEquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlot();
            selectedShader.SetActive(true);
            thisitemSelect = true;
            
        }
    }
    public void OnRightClick()
    {
            UnEquipGear();
    }
    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlot();
        inventoryManager.Additem(itemName, 1, itemsprite,itemtype, itemtypecolor);
        SlotInUse = false;
        this.itemsprite = emptySprite;
        SlotImage.sprite = this.emptySprite;
        SlotName.enabled = true;
        playerDisplayImage.sprite = emptySprite;
        SlotInUse = false;
        //Cap nhat chi so
        for (int i = 0; i < EquipmenrSoBly.equipmentSOs.Length; i++)
        {
            if (EquipmenrSoBly.equipmentSOs[i].itemName == this.itemName)
            {
                EquipmenrSoBly.equipmentSOs[i].UnEquipItem();
            }
        }
        this.itemName = null;
        inventoryManager.SelectLayer();
        
    }
}
