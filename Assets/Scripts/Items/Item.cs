using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Base Info")]
    public string Name;
    public int count = 1;
    public Sprite Icon;
    [Header("Model")]
    public GameObject modelPrefab; // <- теперь префаб, а не ссылка на сцену
    protected GameObject modelInstance;

    public bool IsWeapon()
    {
        return this is Melee || this is Range || this is RayGun;
    }
    public void Select()
    {
        if (modelInstance != null) modelInstance.SetActive(true);
    }
    public void Deselect()
    {
        if (modelInstance != null) modelInstance.SetActive(false);
    }
    public virtual void Attach(Transform transform)
    {
        if (modelPrefab == null) return;
        modelInstance = Instantiate(modelPrefab, transform);
        modelInstance.SetActive(true);
    }
    public virtual void Render(Transform hand, Vector3 offset, Vector3 localScale)
    {
        if (modelInstance == null && modelPrefab != null)
        {
            // —оздаЄм экземпл€р модели, если его ещЄ нет
            modelInstance = Instantiate(modelPrefab);
            modelInstance.name = $"{Name}_InHand";
            modelInstance.transform.SetParent(hand);
            modelInstance.transform.localPosition = Vector3.zero;
        }
        // ќбновл€ем позицию, если модель уже создана
        if (modelInstance != null) {
            localScale.y = 0; localScale.z = 0;
            localScale.Normalize();
            offset.x *= localScale.x;
            modelInstance.transform.position = hand.position + offset;
            modelInstance.transform.rotation = hand.rotation;
        }
        
    }
}
