using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Raycaster
{

    public class UIRaycaster : MonoBehaviour
    {
        public GraphicRaycaster raycaster;
        public EventSystem eventSystem;

        [System.Obsolete]
        void Start()
        {
            if (raycaster == null)
            {
                raycaster = FindObjectOfType<GraphicRaycaster>();
            }
            if (eventSystem == null)
            {
                eventSystem = FindObjectOfType<EventSystem>();
            }
        }
        public void HighlightTile(GameObject tile)
        {
            Image tileImage = tile.GetComponent<Image>();
            if (tileImage != null)
            {
                tileImage.color = Color.white; // Change to highlight color
            }
        }

        public void DetectUIObject(Vector2 screenPosition)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = screenPosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                GameObject b = result.gameObject.transform.parent.gameObject;
                Debug.Log("Hit UI element: " + result.gameObject.name);
                HighlightTile(result.gameObject);
            }
        }
    }
}
