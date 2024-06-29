using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class BMP1
    {
        ////MelonPreferences.cfg 
        static MelonPreferences_Entry<bool> tracks_BMP1, powertrain_BMP1;
        static MelonPreferences_Entry<bool> tracks_BMP1P, powertrain_BMP1P;
        static GameObject tracks1, tracks2, radiator, engine, gearbox, oilRes;
        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_BMP1 = cfg.CreateEntry<bool>("BMP-1 Tracks", true);
            tracks_BMP1.Description = "////NVA & USSR////";
            powertrain_BMP1 = cfg.CreateEntry<bool>("BMP-1 Powertrain", false);

            tracks_BMP1P = cfg.CreateEntry<bool>("BMP-1P Tracks", true);
            powertrain_BMP1P = cfg.CreateEntry<bool>("BMP-1P Powertrain", false);
        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "BMP-1") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();

                try
                {
                    tracks1 = lftHull._lateFollowers[1].transform.Find("Hull ARMOR/Track L/").gameObject;
                    tracks2 = lftHull._lateFollowers[1].transform.Find("Hull ARMOR/Track R/").gameObject;
                    radiator = lftHull._lateFollowers[1].transform.Find("Hull AAR/radiator hit zone/").gameObject;
                    engine = lftHull._lateFollowers[1].transform.Find("Hull AAR/Engine_UTD-20/").gameObject;
                    gearbox = lftHull._lateFollowers[1].transform.Find("Hull AAR/Gearbox/").gameObject;
                    oilRes = lftHull._lateFollowers[1].transform.Find("Hull AAR/oil reservoir/").gameObject;
                }

                catch
                {
                    tracks1 = lftHull._lateFollowers[0].transform.Find("Hull ARMOR/Track L/").gameObject;
                    tracks2 = lftHull._lateFollowers[0].transform.Find("Hull ARMOR/Track R/").gameObject;
                    radiator = lftHull._lateFollowers[0].transform.Find("Hull AAR/radiator hit zone/").gameObject;
                    engine = lftHull._lateFollowers[0].transform.Find("Hull AAR/Engine_UTD-20/").gameObject;
                    gearbox = lftHull._lateFollowers[0].transform.Find("Hull AAR/Gearbox/").gameObject;
                    oilRes = lftHull._lateFollowers[0].transform.Find("Hull AAR/oil reservoir/").gameObject;
                }

                if (tracks_BMP1.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_BMP1.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(oilRes.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "BMP-1P") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();

                try
                {
                    tracks1 = lftHull._lateFollowers[1].transform.Find("Hull ARMOR/Track L/").gameObject;
                    tracks2 = lftHull._lateFollowers[1].transform.Find("Hull ARMOR/Track R/").gameObject;
                    radiator = lftHull._lateFollowers[1].transform.Find("Hull AAR/radiator hit zone/").gameObject;
                    engine = lftHull._lateFollowers[1].transform.Find("Hull AAR/Engine_UTD-20/").gameObject;
                    gearbox = lftHull._lateFollowers[1].transform.Find("Hull AAR/Gearbox/").gameObject;
                    oilRes = lftHull._lateFollowers[1].transform.Find("Hull AAR/oil reservoir/").gameObject;
                }

                catch
                {
                    tracks1 = lftHull._lateFollowers[0].transform.Find("Hull ARMOR/Track L/").gameObject;
                    tracks2 = lftHull._lateFollowers[0].transform.Find("Hull ARMOR/Track R/").gameObject;
                    radiator = lftHull._lateFollowers[0].transform.Find("Hull AAR/radiator hit zone/").gameObject;
                    engine = lftHull._lateFollowers[0].transform.Find("Hull AAR/Engine_UTD-20/").gameObject;
                    gearbox = lftHull._lateFollowers[0].transform.Find("Hull AAR/Gearbox/").gameObject;
                    oilRes = lftHull._lateFollowers[0].transform.Find("Hull AAR/oil reservoir/").gameObject;
                }

                if (tracks_BMP1P.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_BMP1P.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
