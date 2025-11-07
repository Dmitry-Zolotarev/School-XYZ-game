using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDestroy;

    private void Start()
    {
        if(_objectToDestroy == null) _objectToDestroy = this.gameObject;
    }
    public void DestroyObject()
    {
        Destroy(_objectToDestroy);
    }
}
