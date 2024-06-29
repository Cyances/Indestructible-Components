using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class T72
    {
        static MelonPreferences_Entry<bool> tracks_T72M1, powertrain_T72M1, gun_T72M1, optics_T72M1;
        //static MelonPreferences_Entry<bool> tracks_T72M, powertrain_T72M, gun_T72M, optics_T72M;

        static GameObject tracks1, tracks2, gearbox, engine;

        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_T72M1 = cfg.CreateEntry<bool>("T-72M1 Tracks", true);
            optics_T72M1 = cfg.CreateEntry<bool>("T-72M1 Optics", false);
            gun_T72M1 = cfg.CreateEntry<bool>("T-72M1 Gun System", false);
            powertrain_T72M1 = cfg.CreateEntry<bool>("T-72M1 Powertrain", false);

            /*tracks_T72M = cfg.CreateEntry<bool>("T-72M Tracks", true);
            optics_T72M = cfg.CreateEntry<bool>("T-72M Optics", false);
            gun_T72M = cfg.CreateEntry<bool>("T-72M Gun System", false);
            powertrain_T72M = cfg.CreateEntry<bool>("T-72M Powertrain", false);*/
        }


        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-72M1") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("---MESH---/HULL/TURRET/GUN/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("---MESH---/HULL/TURRET").GetComponent<LateFollowTarget>();
                var lftLuna = vic.transform.Find("---MESH---/HULL/TURRET/LUNA").GetComponent<LateFollowTarget>();

                tracks1 = lftHull._lateFollowers[0].transform.Find("ARMOR/Track/").gameObject;
                tracks2 = lftHull._lateFollowers[0].transform.Find("ARMOR/Track.001 (1)/").gameObject;

                gearbox = lftHull._lateFollowers[0].transform.Find("AAR/Transmission").gameObject;
                engine = lftHull._lateFollowers[0].transform.Find("AAR/Engine").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("ARMOR/Cannon/").gameObject;
                GameObject gunAssembly = lftGun._lateFollowers[0].transform.Find("ARMOR/Gun assembly/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("AAR/Breech/").gameObject;

                GameObject autoLoader = lftTurret._lateFollowers[0].transform.Find("AAR/Autoloader Arm").gameObject;

                GameObject gunOptics = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner Sight/fire control system hit zone").gameObject;

                GameObject gunIR = lftLuna._lateFollowers[0].transform.Find("Luna").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AAR/LRF hit zone").gameObject;


                if (tracks_T72M1.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T72M1.Value)
                {
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T72M1.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunAssembly.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T72M1.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunIR.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }

            //T-72M not added yet because of some fuckery with the lateFollower indexing
            /*foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-72M") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                //Regular M Hull LFT at 0
                ///Gills M Hull LFT at 2
                
                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("T72M_skirts_rig/HULL/TURRET/GUN/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("T72M_skirts_rig/HULL/TURRET").GetComponent<LateFollowTarget>();
                var lftLuna = vic.transform.Find("T72M_skirts_rig/HULL/TURRET/LUNA").GetComponent<LateFollowTarget>();

                try
                {
                    tracks1 = lftHull._lateFollowers[2].transform.Find("ARMOR/Track/").gameObject;
                    tracks2 = lftHull._lateFollowers[2].transform.Find("ARMOR/Track.001 (1)/").gameObject;
                    MelonLogger.Msg("T-72M[2] Tracks Loaded");

                    gearbox = lftHull._lateFollowers[2].transform.Find("AAR/Transmission").gameObject;
                    engine = lftHull._lateFollowers[2].transform.Find("AAR/Engine").gameObject;
                    MelonLogger.Msg("T-72M[2] Gearbox and Engine Loaded");
                }

                catch
                {
                    tracks1 = lftHull._lateFollowers[0].transform.Find("ARMOR/Track/").gameObject;
                    tracks2 = lftHull._lateFollowers[0].transform.Find("ARMOR/Track.001 (1)/").gameObject;
                    MelonLogger.Msg("T-72M[0] Tracks Loaded");

                    gearbox = lftHull._lateFollowers[0].transform.Find("AAR/Transmission").gameObject;
                    engine = lftHull._lateFollowers[0].transform.Find("AAR/Engine").gameObject;
                    MelonLogger.Msg("T-72M[0] Gearbox and Engine Loaded");
                }

                //This block is somehow causing issues
                try
                {
                    gunBarrel = lftGun._lateFollowers[0].transform.Find("ARMOR/Cannon/").gameObject;
                    gunAssembly = lftGun._lateFollowers[0].transform.Find("ARMOR/Gun assembly/").gameObject;
                    gunBreech = lftGun._lateFollowers[0].transform.Find("AAR/Breech/").gameObject;
                    MelonLogger.Msg("T-72M[0] Gun Loaded");
                }
                
                catch
                {
                    gunBarrel = lftGun._lateFollowers[2].transform.Find("ARMOR/Cannon/").gameObject;
                    gunAssembly = lftGun._lateFollowers[2].transform.Find("ARMOR/Gun assembly/").gameObject;
                    gunBreech = lftGun._lateFollowers[2].transform.Find("AAR/Breech/").gameObject;
                    MelonLogger.Msg("T-72M[2] Gun Loaded");
                }

                GameObject autoLoader = lftTurret._lateFollowers[0].transform.Find("AAR/Autoloader Arm").gameObject;
                MelonLogger.Msg("T-72M Autoloader Loaded");

                GameObject gunOptics = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner Sight/fire control system hit zone").gameObject;
                MelonLogger.Msg("T-72M FCS Loaded");

                GameObject gunIR = lftLuna._lateFollowers[0].transform.Find("Luna").gameObject;
                MelonLogger.Msg("T-72M Luna Loaded");
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AAR/LRF hit zone").gameObject;
                MelonLogger.Msg("T-72M LRF Loaded");


                if (tracks_T72M.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T72M.Value)
                {
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T72M.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunAssembly.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T72M.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunIR.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }*/
            yield break;
        }
        public static void Init()
        {
            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Medium);
        }
    }
}
