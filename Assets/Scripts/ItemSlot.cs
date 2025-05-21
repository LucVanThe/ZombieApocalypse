using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static InventoryManager;
public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public TypeItem TypeItem;
    public string itemtypecolor;
  
    [SerializeField] private TMP_Text quantitytext;
    [SerializeField] private Image image;
    public GameObject selectedShader;
    public bool thisitemSelect;
    private InventoryManager inventoryManager;
    [SerializeField] private int maxNumberitem;
    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemname, int quantity, Sprite sprite, TypeItem typeitem,string itemtypecolor)
    {
        //kiem tra cho da day
       if (isFull)
           return quantity;
        //kiem tra loai
        this.itemtypecolor = itemtypecolor;
       // this.gunType = gunType;
        this.TypeItem = typeitem;
        //cap nhat ten
        this.itemName = itemname;
        //cap nhat hinh anh
        this.itemSprite = sprite;
        image.sprite = sprite;
        //cap nhat so luong
        this.quantity += quantity;
        if(this.quantity >= maxNumberitem)
        {
            quantitytext.text = maxNumberitem.ToString();
            quantitytext.enabled = true;
            isFull = true;
            
        int extraItem = this.quantity - maxNumberitem;
        this.quantity = maxNumberitem;
        return extraItem;
        }    
        //cap nhat van ban so luong
        quantitytext.text = this.quantity.ToString();
        quantitytext.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button== PointerEventData.InputButton.Left)
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
            inventoryManager.UseItem(itemName); 
            this.quantity -= 1;
            quantitytext.text = this.quantity.ToString();
            if (this.quantity <= 0)
            {
                EmptyLost();
            }
        }
        else
        {
        inventoryManager.DeselectAllSlot();
        selectedShader.SetActive(true);
        thisitemSelect = true;
        }
       
    }

    private void EmptyLost()
    {
        quantitytext.enabled = false;
        image.sprite = null;
    }

    public void OnRightClick()
    {
        GameObject itemtoDrop = new GameObject(itemName);
        Item newItem = itemtoDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.itemSprite = itemSprite;

        SpriteRenderer sr = itemtoDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Default";

        itemtoDrop.AddComponent<BoxCollider2D>();

        itemtoDrop.transform.position = GameObject.FindWithTag("Player").transform.position +new Vector3(0.5f,0,0);
        this.quantity -= 1;
        quantitytext.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptyLost();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
