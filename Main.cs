using System.Reflection;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using MelonLoader;
using UnityEngine;
using Main = QuickRestart.Main;

[assembly: AssemblyVersion($"{Main.MelonVersion}.0")]
[assembly: AssemblyFileVersion($"{Main.MelonVersion}.0")]
[assembly: MelonInfo(typeof(Main), Main.MelonName, Main.MelonVersion, Main.MelonAuthor)]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]

namespace QuickRestart
{
    public class Main : MelonMod
    {
        public const string MelonName = "QuickRestart";
        public const string MelonVersion = "2.3.0";
        public const string MelonAuthor = "AshtonMemer";

        private const string BattleSceneName = "GameMain";

        private MelonPreferences_Category _category;
        private MelonPreferences_Entry<KeyCode> _restartKeybind;
        private MelonPreferences_Entry<KeyCode> _exitKeybind;
        private bool _isBattleScene;

        /// <summary>
        /// Initialize the MelonLoader preferences for keybinds.
        /// </summary>
        public override void OnInitializeMelon()
        {
            _category = MelonPreferences.CreateCategory("QuickRestart");
            _restartKeybind = _category.CreateEntry("Restart Key", KeyCode.Backspace);
            _exitKeybind = _category.CreateEntry("Exit Song Key", KeyCode.Delete);
            _category.SaveToFile(false);
        }

        /// <summary>
        /// Check for keybinds being pressed.
        /// </summary>
        public override void OnUpdate()
        {
            if (!_isBattleScene) return;

            var restartPressed = Input.GetKeyDown(_restartKeybind.Value);
            var exitPressed = Input.GetKeyDown(_exitKeybind.Value);

            if ((!restartPressed && !exitPressed) || !IsBattleInProgress()) return;

            if (restartPressed)
            {
                BattleHelper.GameRestart();
            }
            else
            {
                BattleHelper.GameFinish();
            }
        }

        /// <summary>
        /// Track whether the battle scene is the active one.
        /// </summary>
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _isBattleScene = sceneName == BattleSceneName;
        }

        /// <summary>
        /// Whether the song is actively being played, which is the only time restarting or exiting is safe.
        /// </summary>
        private static bool IsBattleInProgress()
        {
            var battle = Singleton<StageBattleComponent>.instance;
            return battle != null && battle.isInGame && !battle.isDead && !battle.isSucceed;
        }
    }
}
