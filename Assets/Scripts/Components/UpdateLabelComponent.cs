using TMPro;
using UnityEngine;

public class UpdateLabelComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string text;
    public void UpdateLabel() => label.text = text;
}
