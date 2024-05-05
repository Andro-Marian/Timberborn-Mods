using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace HideRangePath.ToggleButton
{
    internal class HideRangePathTogglePanel : ILoadableSingleton, IInputProcessor
    {
        private VisualElement _checkBox;
        
        private readonly VisualElementLoader _visualElementLoader;
        private readonly UILayout _uiLayout;
        private readonly ITooltipRegistrar _tooltipRegistrar;
        private readonly InputService _inputService;
        
        private readonly Texture2D _enabledIcon;
        private readonly Texture2D _disabledIcon;
        private readonly Texture2D _temporaryIcon;
        
        private static bool _status = false;
        
        public HideRangePathTogglePanel(VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, InputService inputService)
        {
            _visualElementLoader = visualElementLoader;
            _uiLayout = uiLayout;
            _tooltipRegistrar = tooltipRegistrar;
            _inputService = inputService;
            
            _enabledIcon = new Texture2D(52, 52, TextureFormat.RGB24, false);
            _disabledIcon = new Texture2D(52, 52, TextureFormat.RGB24, false);
            _temporaryIcon = new Texture2D(52, 52, TextureFormat.RGB24, false);
            
            _enabledIcon.LoadImage(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADQAAAA0CAYAAADFeBvrAAAFWmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iNTIiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iNTIiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249Ijk2LzEiCiAgIHRpZmY6WVJlc29sdXRpb249Ijk2LzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSI1MiIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjUyIgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxl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"));
            _disabledIcon.LoadImage(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADQAAAA0CAYAAADFeBvrAAAFUWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iNTIiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iNTIiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249Ijk2LzEiCiAgIHRpZmY6WVJlc29sdXRpb249Ijk2LzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSI1MiIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjUyIgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxl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"));
            _temporaryIcon.LoadImage(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADQAAAA0CAYAAADFeBvrAAAFWmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iNTIiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iNTIiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249Ijk2LzEiCiAgIHRpZmY6WVJlc29sdXRpb249Ijk2LzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSI1MiIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjUyIgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxl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"));
        }
        
        public void Load()
        {
            var visualElement = _visualElementLoader.LoadVisualElement("Common/SquareToggle");
            var toggle = visualElement.Q<Toggle>("Toggle");
            
            toggle.AddToClassList("water-opacity-toggle-panel");
            toggle.RegisterCallback(delegate(ChangeEvent<bool> changeEvent)
            {
                UpdateStyle(changeEvent.newValue);
            });
            
            _checkBox = visualElement.Q<VisualElement>("unity-checkmark");
            
            _tooltipRegistrar.RegisterLocalizable(visualElement, () => TooltipLocKey);
            _uiLayout.AddTopRightButton(visualElement, 10);
            _inputService.AddInputProcessor(this);
            
            UpdateStyle(_status);
        }

        private void UpdateStyle(bool state)
        {
            _checkBox.style.backgroundImage = state ? _enabledIcon : _disabledIcon;
            _status = state;
        }
        
        private string TooltipLocKey
        {
            get => _status ? "HideRangePath.Hide" : "HideRangePath.Show";
        }
        public bool Status
        {
            get => _status;
        }

        public bool ProcessInput()
        {
            if (_inputService._keyboard.IsKeyDown(Key.N))
            {
                UpdateStyle(!_status);
                return true;
            }
            return false;
        }
    }
}