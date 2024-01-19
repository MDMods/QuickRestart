using Il2CppAssets.Scripts.Database;
using MelonLoader;
using UnityEngine;

namespace QuickRestart
{
    public class Main : MelonMod
    {
        private MelonPreferences_Category _category;
        private MelonPreferences_Entry _restartKeybind;
        private MelonPreferences_Entry _exitKeybind;
        private bool _isBattleScene;

        /// <summary>
        /// Initialize the MelonLoader preferences for keybinds.
        /// </summary>
        public override void OnInitializeMelon()
        {
            _category = MelonPreferences.CreateCategory("QuickRestart");
            _restartKeybind = _category.CreateEntry("Restart Key", KeyCode.Backspace);
            _exitKeybind = _category.CreateEntry("Exit Song Key", KeyCode.Delete);
            MelonPreferences.Save();

            base.OnInitializeMelon();
        }

        /// <summary>
        /// Check for keybinds being pressed.
        /// </summary>
        public override void OnUpdate()
        {
            if (!_isBattleScene) return;

            if (Input.GetKeyDown((KeyCode)_restartKeybind.BoxedValue))
            {
                BattleHelper.GameRestart();
            }
            else if (Input.GetKeyDown((KeyCode)_exitKeybind.BoxedValue))
            {
                BattleHelper.GameFinish();
            }

            base.OnUpdate();
        }

        /// <summary>
        /// Ensure the battle scene is the active one, otherwise do not allow restarting.
        /// </summary>
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _isBattleScene = sceneName == "GameMain";

            base.OnSceneWasLoaded(buildIndex, sceneName);
        }
    }
}
