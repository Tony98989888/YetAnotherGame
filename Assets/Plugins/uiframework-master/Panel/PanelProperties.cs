using System;
using UnityEngine;

namespace deVoid.UIFramework
{
    /// <summary>
    ///     Properties common to all panels
    /// </summary>
    [Serializable]
    public class PanelProperties : IPanelProperties
    {
        [SerializeField]
        [Tooltip(
            "Panels go to different para-layers depending on their priority. You can set up para-layers in the Panel Layer.")]
        private PanelPriority priority;

        public PanelPriority Priority
        {
            get => priority;
            set => priority = value;
        }
    }
}