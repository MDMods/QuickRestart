using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Database;
using Assets.Scripts.PeroTools.Commons;
using Assets.Scripts.PeroTools.Managers;
using Assets.Scripts.UI.Panels;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace QuickRestart
{
    public class Main : MelonMod
    {
        private MelonPreferences_Category _category;
        private MelonPreferences_Entry _restartKeybind;
        private MelonPreferences_Entry _exitKeybind;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            _category = MelonPreferences.CreateCategory("QuickRestart");
            _restartKeybind = _category.CreateEntry<KeyCode>("Restart Key", KeyCode.Backspace);
            _exitKeybind = _category.CreateEntry<KeyCode>("Exit Song Key", KeyCode.Delete);
            MelonPreferences.Save();
        }

        private bool isBattleScene = false;
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!isBattleScene) return;
            if (Input.GetKeyDown((KeyCode)_restartKeybind.BoxedValue))
            {
                BattleHelper.GameRestart();
            }
            else if (Input.GetKeyDown((KeyCode)_exitKeybind.BoxedValue))
            {
                BattleHelper.GameFinish();
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
            if (sceneName == "GameMain") isBattleScene = true;
            else isBattleScene = false;
        }
    }
}
