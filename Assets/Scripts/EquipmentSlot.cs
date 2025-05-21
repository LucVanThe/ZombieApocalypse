using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject thongbao;
    //Thong tin vat pham
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public TypeItem itemType;
    public GunType gunType;
    private Animator animator;
    public SpriteRenderer GunObject;
    public SpriteRenderer ArmoObject;
    public string itemtypecolor;
    [SerializeField] Button buttonthongbao;
    //O vat pham
    [SerializeField] private Image image;
    public GameObject selectedShader;
    public bool thisitemSelect;
    private InventoryManager inventoryManager;
  

    //O trang bi
    [SerializeField] private EquippedSlot Armo, gunSlot, Shoe,Mu;
    [SerializeField] private EquippedSlot Phukien, Phukien2 , Phukien3; 


    //  [SerializeField] private int maxNumberitem;
    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        //GunObject = GameObject.Find("GunObject").GetComponent<SpriteRenderer>();
        //ArmoObject = GameObject.Find("Armo").GetComponent<SpriteRenderer>();
        
        

    }
    public int AddItem(string itemname, int quantity, Sprite sprite, TypeItem typeitem, string itemtypecolor)
    {
        //kiem tra cho da day
        //if (isFull)
        //    return quantity;
        //kiem tra loai
        this.itemtypecolor = itemtypecolor;
        this.itemType = typeitem;
        //cap nhat ten
        this.itemName = itemname;
        //cap nhat hinh anh
        this.itemSprite = sprite;
        image.sprite = sprite;
        //cap nhat so luong
        this.quantity =1;
        isFull = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
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
        if (thisitemSelect)
        {
            EquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlot();
            selectedShader.SetActive(true);
            thisitemSelect = true;
        }

    }
     
    private void EquipGear()
    {
        
        if (itemType == TypeItem.armo)
        {
            Armo.EquipGear(itemSprite, itemName,itemtypecolor);
            EmptyLost();
        }
        if (itemType == TypeItem.shoe)
        {
            Shoe.EquipGear(itemSprite, itemName,itemtypecolor);
            EmptyLost();

        }
        if (itemType == TypeItem.mu)
        {
            Mu.EquipGear(itemSprite, itemName, itemtypecolor);
            EmptyLost();

        }
      
        if (itemType == TypeItem.gun )
        {

            gunSlot.EquipGear(itemSprite, itemName, itemtypecolor);
            EmptyLost();
        }
      
        EquippedSlot[] phukien = { Phukien, Phukien2, Phukien3 };
        if (itemType == TypeItem.phukien)
        {
            bool equipped = false;
            for (int i = 0; i < phukien.Length; i++)
            {
                if (!phukien[i].SlotInUse)
                {
                    phukien[i].EquipGear(itemSprite, itemName, itemtypecolor);
                    equipped = true;
                    EmptyLost();
                    break;
                }
            }        
            if (!equipped)
            {
                Debug.LogWarning("Không còn chỗ trống để trang bị phụ kiện!");
                thongbao.SetActive(true);
                buttonthongbao.onClick.AddListener(() => {
                    thongbao.SetActive(false);
                });
            }
        }
       
    }
    private void EmptyLost()
    {
        this.itemName = "";
        this.itemtypecolor = "";
        this.quantity = 0;
        image.sprite = null ;
        isFull = false;
    }

    public void OnRightClick()
    {
        GameObject itemtoDrop = new GameObject(itemName);
        Item newItem = itemtoDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.itemSprite = itemSprite;
        newItem.typeItem = this.itemType;

        SpriteRenderer sr = itemtoDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Default";

        itemtoDrop.AddComponent<BoxCollider2D>();

        itemtoDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2f, 0, 0);
        this.quantity -= 1;
        if (this.quantity <= 0)
        {
            EmptyLost();
        }
    }


}
