using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace IndestructibleComponents
{
    public static class M2
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_M2, powertrain_M2;
        static GameObject tracks1, tracks2, radiator, engine, gearbox;

        public static void Config(MelonPreferences_Category cfg)
        {

            tracks_M2 = cfg.CreateEntry<bool>("M2 Tracks", true);
            tracks_M2.Description = "Enable invulnerability for the following components";
            powertrain_M2 = cfg.CreateEntry<bool>("M2 Powertrain", false);

        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "M2 Bradley") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                /*List<LateFollow> lateFollowers = (List<LateFollow>)vic.GetComponentInParent<LateFollowTarget>()._lateFollowers;

                int index = lateFollowers.FindIndex(1, a => a.name == "HULL");
                MelonLogger.Msg("HULL Index: " + index);*/

                //LateFollow hullFollowers = lateFollowers[1].name == "HULL" ? lateFollowers[1] : lateFollowers[3];
                //hullFollowers.transform.Find("Radiator_001").gameObject.SetActive(false);
                //hullFollowers.transform.Find("Radiator_001").gameObject.GetComponent<GHPC.Equipment.DestructibleComponent>();
                //Component.Destroy(hullFollowers);

                //LateFollow hullFollowers = lateFollowers.Where(x => x.name == "HULL").LastOrDefault();

                var lftHull = vic.GetComponent<LateFollowTarget>();

                try
                {
                    tracks1 = lftHull._lateFollowers[0].transform.Find("Left Track").gameObject;
                    tracks2 = lftHull._lateFollowers[0].transform.Find("Right Track").gameObject;

                    gearbox = lftHull._lateFollowers[3].transform.Find("Gearbox_HMPT-500").gameObject;
                    engine = lftHull._lateFollowers[3].transform.Find("Engine_Cummings-VT903").gameObject;
                    radiator = lftHull._lateFollowers[3].transform.Find("Radiator_001").gameObject;
                }

                catch
                {
                    tracks1 = lftHull._lateFollowers[1].transform.Find("Left Track").gameObject;
                    tracks2 = lftHull._lateFollowers[1].transform.Find("Right Track").gameObject;

                    gearbox = lftHull._lateFollowers[0].transform.Find("Gearbox_HMPT-500").gameObject;
                    engine = lftHull._lateFollowers[0].transform.Find("Engine_Cummings-VT903").gameObject;
                    radiator = lftHull._lateFollowers[0].transform.Find("Radiator_001").gameObject;
                }

                if (tracks_M2.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_M2.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
