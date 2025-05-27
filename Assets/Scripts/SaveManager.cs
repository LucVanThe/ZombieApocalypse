using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public Transform slotParent;
    public Transform slotParent2;
    private Player player;
    private InventoryManager inventoryManager;
    private static string savePath => Application.persistentDataPath + "/save.json";
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public void SaveGame(Vector3 playerPosition)
    {

        SaveData data = new SaveData();
        data.sceneName = SceneManager.GetActiveScene().name;
        data.playerPosition = new float[] { playerPosition.x, playerPosition.y, playerPosition.z };

        for (int i = 0; i < slotParent.childCount; i++)
        {
            Transform slotTransform = slotParent.GetChild(i);
            ItemSlot itemSlot = slotTransform.GetComponent<ItemSlot>();

            if (itemSlot != null && itemSlot.itemName!=null) // kiểm tra có vật phẩm không
            {
                SavedItemData savedItem = new SavedItemData();
                savedItem.itemName = itemSlot.itemName;
                savedItem.typeItem = itemSlot.TypeItem;
                savedItem.itemSprite = itemSlot.itemSprite;
                savedItem.itemTypeColor = itemSlot.itemtypecolor;
                savedItem.quantity = itemSlot.quantity;

                data.itemSlots.Add(savedItem);
            }
            else
            {
                // Nếu muốn lưu slot trống
                //data.itemSlots.Add(new SavedItemData());
            }
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game saved to " + savePath);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Game loaded from " + savePath);
            return data;
        }

        Debug.LogWarning("No save file found.");
        return null;
    }
   
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }
    public void LoadGameAndApply()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        SaveData data = LoadGame();
        Debug.Log("so itemslot = " + data.itemSlots.Count);
        Debug.Log("so slotParent = " + slotParent.childCount);
        if (data == null)
        {
            Debug.LogWarning("Không có save để load.");
            yield break;
        }
       
        for (int i = 0; i < slotParent2.childCount && i < data.itemSlots.Count; i++)
        {
            Transform slotTransform = slotParent2.GetChild(i);
            Item itemSlot = slotTransform.GetComponent<Item>();
           
            if (itemSlot != null)
            {
                SavedItemData savedItem = data.itemSlots[i];
                Debug.Log("name = " + data.itemSlots[i].itemName);
                itemSlot.itemName = data.itemSlots[i].itemName;
                Debug.Log("itemslot_name = " + itemSlot.itemName);
                
            }
            
        }
        Debug.Log("Load dữ liệu game hoàn tất.");

        // Load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

       
        yield return null;

        
      
        if (player == null)
        {
            Debug.LogError("Không tìm thấy Player trong scene!");
            yield break;
        }
        
        Vector3 pos = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        player.transform.position = pos;


        

       
       

        
    }
}
