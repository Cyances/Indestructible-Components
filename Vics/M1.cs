using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class M1
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_M1, powertrain_M1, optics_M1, gun_M1;
        static MelonPreferences_Entry<bool> tracks_M1IP, powertrain_M1IP, optics_M1IP, gun_M1IP;

        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_M1 = cfg.CreateEntry<bool>("M1 Tracks", true);
            optics_M1 = cfg.CreateEntry<bool>("M1 Optics", false);
            gun_M1 = cfg.CreateEntry<bool>("M1 Gun System", false);
            powertrain_M1 = cfg.CreateEntry<bool>("M1 Powertrain", false);

            tracks_M1IP = cfg.CreateEntry<bool>("M1IP Tracks", true);
            optics_M1IP = cfg.CreateEntry<bool>("M1IP Optics", false);
            gun_M1IP = cfg.CreateEntry<bool>("M1IP Gun System", false);
            powertrain_M1IP = cfg.CreateEntry<bool>("M1IP Powertrain", false);
        }
        public static IEnumerator Convert(GameState _)
        {
            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;

                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "M1") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("IPM1_rig/HULL/TURRET/GUN").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("IPM1_rig/HULL/TURRET/").GetComponent<LateFollowTarget>();

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("HULLARMOR/TRACK_R.001/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("HULLARMOR/TRACK_L.001/").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("AARHULL/Engine/").gameObject;
                GameObject transmission = lftHull._lateFollowers[0].transform.Find("AARHULL/Transmission/").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("ARMORGUN/lp_gun.002/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("ARMORGUN/Gun Breech.002/").gameObject;

                GameObject gunOptics1 = lftTurret._lateFollowers[0].transform.Find("AARTURRET/Cube.001/").gameObject;
                GameObject gunOptics2 = lftTurret._lateFollowers[0].transform.Find("AARTURRET/Cube.002/").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AARTURRET/LRF collider/").gameObject;

                if (tracks_M1.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_M1.Value)
                {
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(transmission.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }


                if (gun_M1.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_M1.Value)
                {
                    Component.Destroy(gunOptics1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunOptics2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;

                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "M1IP") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("IPM1_rig/HULL/TURRET/GUN").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("IPM1_rig/HULL/TURRET/").GetComponent<LateFollowTarget>();

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("HULLARMOR/TRACK_R.001/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("HULLARMOR/TRACK_L.001/").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("AARHULL/Engine/").gameObject;
                GameObject transmission = lftHull._lateFollowers[0].transform.Find("AARHULL/Transmission/").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("ARMORGUN/lp_gun.002/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("ARMORGUN/Gun Breech.002/").gameObject;

                GameObject gunOptics1 = lftTurret._lateFollowers[0].transform.Find("AARTURRET/Cube.001/").gameObject;
                GameObject gunOptics2 = lftTurret._lateFollowers[0].transform.Find("AARTURRET/Cube.002/").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AARTURRET/LRF collider/").gameObject;

                if (tracks_M1IP.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_M1IP.Value)
                {
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(transmission.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }


                if (gun_M1IP.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_M1IP.Value)
                {
                    Component.Destroy(gunOptics1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunOptics2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
