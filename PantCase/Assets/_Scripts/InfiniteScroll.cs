using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public RectTransform contentPanelTransform;
    public VerticalLayoutGroup verticalLayoutGroup;

    public RectTransform[] itemList;

    private Vector2 _oldVelocity;
    private bool _isUpdated;

    public void Start()
    {
        _isUpdated = false;
        _oldVelocity = Vector2.zero;
        int itemToAdd = Mathf.CeilToInt(viewPortTransform.rect.height / (itemList[0].rect.height + verticalLayoutGroup.spacing));

        for (int i = 0; i < itemToAdd; i++)
        {
            int num = itemList.Length - i - 1;

            while (num < 0)
            {
                num += itemList.Length;
            }

            RectTransform RT = Instantiate(itemList[num], contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        contentPanelTransform.localPosition = new Vector3((contentPanelTransform.localPosition.x),
            0 - (itemList[0].rect.height + verticalLayoutGroup.spacing) * itemToAdd,
            contentPanelTransform.localPosition.z);
    }

    private void Update()
    {
        if (_isUpdated)
        {
            _isUpdated = false;
            scrollRect.velocity = _oldVelocity;
        }

        if (contentPanelTransform.localPosition.y > 0)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(0, itemList.Length * (itemList[0].rect.height + verticalLayoutGroup.spacing), 0);
            _isUpdated = true;
        }

        if (contentPanelTransform.localPosition.y < 0 - (itemList.Length * (itemList[0].rect.height + verticalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(0, itemList.Length * (itemList[0].rect.height + verticalLayoutGroup.spacing), 0);
            _isUpdated = true;
        }
    }
}