using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject informationPanel;

    public TMP_Text selectedNameText;
    public Image selectedImage;

    public SelectedProductionButton selectedProductionButton;

    public Transform selectedProductionButtonParent;

    public void Start()
    {
        EventManager.Instance.OnSetSelected += SetSelected;
        EventManager.Instance.OnUnSetSelected += CloseInformationPanel;
    }

    public void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnSetSelected -= SetSelected;
            EventManager.Instance.OnUnSetSelected -= CloseInformationPanel;
        }
    }

    public void SetSelected(IProducer producer, GridObjectData gridObjectData)
    {
        selectedNameText.text = gridObjectData.name;
        selectedImage.sprite = gridObjectData.sprite;
        OpenInformationPanel();

        int childCount = selectedProductionButtonParent.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(selectedProductionButtonParent.transform.GetChild(i).gameObject);
        }

        foreach (var objectData in gridObjectData.productionData.productionData)
        {
            SelectedProductionButton button = Instantiate(selectedProductionButton, selectedProductionButtonParent);
            button.SetButton(objectData);
        }
    }

    public void OpenInformationPanel()
    {
        informationPanel.SetActive(true);
    }

    public void CloseInformationPanel()
    {
        informationPanel.SetActive(false);
    }
}