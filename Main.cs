using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.Audio;
using GHPC.Camera;
using GHPC.Player;
using GHPC.State;
using GHPC.Vehicle;
using IndestructibleComponents;
using MelonLoader;
using UnityEngine;



[assembly: MelonInfo(typeof(IndestructibleComponentsMod), "1 Indestructible Components", "1.0.0", "Cyance")]
[assembly: MelonGame("Radian Simulations LLC", "GHPC")]

namespace IndestructibleComponents
{
    public class IndestructibleComponentsMod : MelonMod
    {
        public static GameObject[] vic_gos;
        public static Vehicle[] vics;
        public static MelonPreferences_Category cfg;

        private GameObject game_manager;
        public static AudioSettingsManager audio_settings_manager;
        public static PlayerInput player_manager;
        public static CameraManager camera_manager;

        public IEnumerator GetVics(GameState _)
        {
            vic_gos = GameObject.FindGameObjectsWithTag("Vehicle");
            vics = GameObject.FindObjectsByType<Vehicle>(FindObjectsSortMode.None);

            yield break;
        }

        public override void OnInitializeMelon()
        {
            cfg = MelonPreferences.CreateCategory("IndestructibleComponents");
            M2.Config(cfg);
            M60.Config(cfg);
            M1.Config(cfg);
            BMP1.Config(cfg);
            BMP2.Config(cfg);
            PT76.Config(cfg);
            T55.Config(cfg);
            T62.Config(cfg);
            T64.Config(cfg);
            T72.Config(cfg);
            T80.Config(cfg);
        }


        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (Util.menu_screens.Contains(sceneName)) return;

            game_manager = GameObject.Find("_APP_GHPC_");
            audio_settings_manager = game_manager.GetComponent<AudioSettingsManager>();
            player_manager = game_manager.GetComponent<PlayerInput>();
            camera_manager = game_manager.GetComponent<CameraManager>();

            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(GetVics), GameStatePriority.Medium);

            M1.Init();
            M2.Init();
            BMP1.Init();
            BMP2.Init();
            M60.Init();
            PT76.Init();
            T55.Init();
            T62.Init();
            T64.Init();
            T72.Init();
            //if (sceneName != "TR01_showcase") { T72.Init(); }
            T80.Init();
        }
    }
}

