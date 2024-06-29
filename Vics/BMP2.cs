using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class BMP2
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> powertrain_BMP2;
        static GameObject oilRes;
        public static void Config(MelonPreferences_Category cfg)
        {
            powertrain_BMP2 = cfg.CreateEntry<bool>("BMP-2 Powertrain", false);
        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "BMP-2") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();


                var lftHull = vic.GetComponent<LateFollowTarget>();

                try
                {
                    oilRes = lftHull._lateFollowers[3].transform.Find("In Manual, this is labeled as oil tank, the cap on top IS oil cap, but this thing is huge, no idea whats going on inside it/").gameObject;
                }
                catch
                {
                    oilRes = lftHull._lateFollowers[2].transform.Find("In Manual, this is labeled as oil tank, the cap on top IS oil cap, but this thing is huge, no idea whats going on inside it/").gameObject;
                }

                if (powertrain_BMP2.Value)
                {
                    Component.Destroy(oilRes.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }
            yield break;
        }
        public static void Init()
        {
            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Medium);
        }

    }
}
