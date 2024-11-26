using System.Collections.Generic;
using UnityEngine;

namespace deVoid.UIFramework
{
    /// <summary>
    ///     This is a "helper" layer so Windows with higher priority can be displayed.
    ///     By default, it contains any window tagged as a Popup. It is controlled by the WindowUILayer.
    /// </summary>
    public class WindowParaLayer : MonoBehaviour
    {
        [SerializeField] private GameObject darkenBgObject;

        private readonly List<GameObject> containedScreens = new();

        public void AddScreen(Transform screenRectTransform)
        {
            screenRectTransform.SetParent(transform, false);
            containedScreens.Add(screenRectTransform.gameObject);
        }

        public void RefreshDarken()
        {
            for (var i = 0; i < containedScreens.Count; i++)
                if (containedScreens[i] != null)
                    if (containedScreens[i].activeSelf)
                    {
                        darkenBgObject.SetActive(true);
                        return;
                    }

            darkenBgObject.SetActive(false);
        }

        public void DarkenBG()
        {
            darkenBgObject.SetActive(true);
            darkenBgObject.transform.SetAsLastSibling();
        }
    }
}