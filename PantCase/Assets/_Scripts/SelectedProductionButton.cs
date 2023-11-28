using UnityEngine;
using UnityEngine.UI;

public class SelectedProductionButton : MonoBehaviour
{
    public GridObjectData gridObjectData;
    public Image image;

    public void SetButton(GridObjectData gridObjectData)
    {
        this.gridObjectData = gridObjectData;
        image.sprite = gridObjectData.sprite;
    }

    public void OnClick()
    {
        EventManager.Instance.SpawnOnSelected(gridObjectData);
    }
}