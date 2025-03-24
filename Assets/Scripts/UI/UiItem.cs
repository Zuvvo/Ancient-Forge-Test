using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _image;

    public void Setup(ItemData data)
    {
        _image.sprite = data.Sprite;
    }

    public void UpdateState(ItemState state)
    {
        _amountText.text = state.Amount.ToString();
        gameObject.SetActive(state.Amount > 0);
    }
}
