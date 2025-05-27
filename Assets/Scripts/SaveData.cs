using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData 
{   
        public string sceneName;
        public float[] playerPosition;
        public List<SavedItemData> itemSlots = new List<SavedItemData>();

}
[System.Serializable]
public class SavedItemData
{
  
    public string itemTypeColor;
  
    public string itemName;
    
    public int quantity;
   public Sprite itemSprite;
 
    public TypeItem typeItem;
}
