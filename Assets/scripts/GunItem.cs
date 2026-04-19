using UnityEngine;

public class GunItem : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        FindFirstObjectByType<InventoryManager>().AddPistola();
        Destroy(gameObject);
    }
}