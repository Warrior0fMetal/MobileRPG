using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreController : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryDisplay update;

    public void SaveGame()
    {
        inventory.Save();
    }

    public void LoadGame()
    {
        inventory.Clear();
        update.ClearDisplay();
        inventory.Load();
       
    }

    void Update()
    {
        
    }
}
