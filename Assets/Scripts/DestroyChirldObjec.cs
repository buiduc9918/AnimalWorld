using UnityEngine;

public class DestroyChildObject : MonoBehaviour
{
    // Phương thức để phá hủy tất cả các đối tượng con
    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Phương thức để phá hủy đối tượng hiện tại và tất cả các đối tượng con
    public void DestroySelfAndChildren()
    {
        DestroyAllChildren();
        Destroy(gameObject);
    }
}
