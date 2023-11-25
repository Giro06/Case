using System;
using UnityEngine;
using UnityEngine.UI;


public class ProductionButton : MonoBehaviour
{
    public GridObjectData gridObjectData;
    public Image image;

    public void OnEnable()
    {
        image.sprite = gridObjectData.sprite;
    }

    public void OnClick()
    {
        EventManager.Instance.CreateGameView(gridObjectData);
    }
}