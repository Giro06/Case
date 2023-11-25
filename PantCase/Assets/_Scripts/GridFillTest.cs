using System;
using Giroo.Utility;
using UnityEngine;

namespace _Scripts
{
    public class GridFillTest : MonoBehaviour
    {
        public GridObjectView gridObjectViewPrefab;
        public GridObjectData gridObjectData;

        public void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);

            if (Input.GetMouseButtonDown(0))
            {
                GridObjectView gridObjectView = Instantiate(gridObjectViewPrefab, mousePos, Quaternion.identity);
                gridObjectView.Initialize(gridObjectData);
            }
        }
    }
}