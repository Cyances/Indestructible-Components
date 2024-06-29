using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class M60
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_M60A1, powertrain_M60A1, optics_M60A1, gun_M60A1;
        static MelonPreferences_Entry<bool> tracks_M60A3, powertrain_M60A3, optics_M60A3, gun_M60A3;

        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_M60A1 = cfg.CreateEntry<bool>("M60A1 Tracks", true);
            optics_M60A1 = cfg.CreateEntry<bool>("M60A1 Optics", false);
            gun_M60A1 = cfg.CreateEntry<bool>("M60A1 Gun System", false);
            powertrain_M60A1 = cfg.CreateEntry<bool>("M60A1 Powertrain", false);

            tracks_M60A3 = cfg.CreateEntry<bool>("M60A3 Tracks", true);
            optics_M60A3 = cfg.CreateEntry<bool>("M60A3 Optics", false);
            gun_M60A3 = cfg.CreateEntry<bool>("M60A3 Gun System", false);
            powertrain_M60A3 = cfg.CreateEntry<bool>("M60A3 Powertrain", false);
        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;

                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "M60A3 TTS") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("M60A3TTS_rig/hull/turret/main gun mantlet").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("M60A3TTS_rig/hull/turret").GetComponent<LateFollowTarget>();

                GameObject tracksL = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_Armour/track hitzone/").gameObject;
                GameObject tracksR = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_Armour/track hitzone001/").gameObject;

                GameObject engine = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/Engine.001/").gameObject;
                GameObject gearbox = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/Transmission").gameObject;
                GameObject radiator = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/radiator/").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("mantlet/barrel/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("Gun Breech/").gameObject;
                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("Gun Breech/240/").gameObject;
                GameObject gunLift = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Gunlift").gameObject;
                GameObject gunControls = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Controls").gameObject;

                GameObject gunOptics = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Main Sight").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/LRF").gameObject;
                GameObject gunFCS = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/FCS1").gameObject;

                if (tracks_M60A3.Value)
                {
                    Component.Destroy(tracksL.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracksR.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_M60A3.Value)
                {
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }


                if (gun_M60A3.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLift.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunControls.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_M60A3.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunFCS.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }


            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;

                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "M60A1 RISE (Passive)") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();


                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("M60A3TTS_rig/hull/turret/main gun mantlet").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("M60A3TTS_rig/hull/turret").GetComponent<LateFollowTarget>();

                GameObject tracksL = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_Armour/track hitzone/").gameObject;
                GameObject tracksR = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_Armour/track hitzone001/").gameObject;

                GameObject engine = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/Engine.001/").gameObject;
                GameObject gearbox = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/Transmission").gameObject;
                GameObject radiator = lftHull._lateFollowers[0].transform.Find("M60A3TTS_HULL_AAR/radiator/").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("mantlet/barrel/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("Gun Breech/").gameObject;
                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("Gun Breech/240/").gameObject;
                GameObject gunLift = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Gunlift").gameObject;
                GameObject gunControls = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Controls").gameObject;

                GameObject gunOptics = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/Main Sight").gameObject;
                GameObject gunORF = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/M17 range finder").gameObject;
                GameObject gunFCS = lftTurret._lateFollowers[0].transform.Find("M60A3TTS_TURRET_AAR/FCS1").gameObject;

                if (tracks_M60A1.Value)
                {
                    Component.Destroy(tracksL.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracksR.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_M60A1.Value)
                {
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }


                if (gun_M60A1.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunFCS.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLift.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunControls.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_M60A1.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunORF.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
