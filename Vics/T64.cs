using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class T64
    {
        static MelonPreferences_Entry<bool> tracks_T64A, powertrain_T64A, gun_T64A, optics_T64A;
        static MelonPreferences_Entry<bool> tracks_T64B, powertrain_T64B, gun_T64B, optics_T64B;

        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_T64A = cfg.CreateEntry<bool>("T-64A Tracks", true);
            optics_T64A = cfg.CreateEntry<bool>("T-64A Optics", false);
            gun_T64A = cfg.CreateEntry<bool>("T-64A Gun System", false);
            powertrain_T64A = cfg.CreateEntry<bool>("T-64A Powertrain", false);

            tracks_T64B = cfg.CreateEntry<bool>("T-64B Tracks", true);
            optics_T64B = cfg.CreateEntry<bool>("T-64B Optics", false);
            powertrain_T64B = cfg.CreateEntry<bool>("T-64B Powertrain", false);
            gun_T64B = cfg.CreateEntry<bool>("T-64B Gun System", false);
        }

        public static IEnumerator Convert(GameState _)
        {                 
            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-64A") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("---T64A_MESH---/HULL/TURRET/Main gun/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("---T64A_MESH---/HULL/TURRET/").GetComponent<LateFollowTarget>(); ;
                var lftLuna = vic.transform.Find("---T64A_MESH---/HULL/TURRET/luna_elbow_3/").GetComponent<LateFollowTarget>();

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/track_R/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/track_L/").gameObject;

                GameObject radiator = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Radiator_Coolant_001").gameObject;
                GameObject gearbox1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Gearbox_001").gameObject;
                GameObject gearbox2 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Gearbox_002").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Engine_5TDF").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("AAR/Gun_2A46M-1/").gameObject;
                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("AAR/MG_Coax_PKT_7.62mm/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("AAR/Gun_Breech_Gun_Breech_BitsThatRecoil/").gameObject;

                GameObject autoLoader1 = lftTurret._lateFollowers[0].transform.Find("AAR/Autoloader_6ETs10M/").gameObject;
                GameObject autoLoader2 = lftTurret._lateFollowers[0].transform.Find("AAR/Carousel_ElectricDrive(Can be manually cranked if failed)/").gameObject;
                GameObject turretDrive1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Turret_TraverseMotor_Hull mounted parts of 2E28M stabilizer").gameObject;
                GameObject turretDrive2 = lftTurret._lateFollowers[0].transform.Find("AAR/Gun_Elevation_ManualBackUp/").gameObject;
                GameObject turretDrive3 = lftTurret._lateFollowers[0].transform.Find("AAR/Turret_Traverse_ManualDrive/").gameObject;
                GameObject turretDrive4 = lftTurret._lateFollowers[0].transform.Find("AAR/Commander_Cupola_Drive/").gameObject;

                GameObject gunOptics1 = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner_Sight_TPD-2-49/").gameObject;
                GameObject gunOptics2 = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner_Sight_TPD-2-49_CoincidenceRangeFinder/").gameObject;
                GameObject gunOptics3 = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner_Sight_TPN‑1‑49‑23/").gameObject;

                GameObject gunIR = lftLuna._lateFollowers[0].transform.Find("Luna/").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AAR/LRF hit zone/").gameObject;


                if (tracks_T64A.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T64A.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T64A.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive3.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive4.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T64A.Value)
                {
                    Component.Destroy(gunOptics1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunOptics2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunOptics3.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunIR.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunLRF.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

            }


            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-64B") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("T64B_rig/HULL/TURRET/Main gun/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("T64B_rig/HULL/TURRET/").GetComponent<LateFollowTarget>(); ;
                var lftLuna = vic.transform.Find("T64B_rig/HULL/TURRET/luna_elbow_3/").GetComponent<LateFollowTarget>();
                
                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/track_R/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/track_L/").gameObject;

                GameObject radiator = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Radiator_Coolant_001").gameObject;
                GameObject gearbox1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Gearbox_001").gameObject;
                GameObject gearbox2 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Gearbox_002").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Engine_5TDF").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("AAR/Gun_2A46M-1/").gameObject;
                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("AAR/MG_Coax_PKT_7.62mm/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("AAR/Gun_Breech_Gun_Breech_BitsThatRecoil/").gameObject;

                GameObject autoLoader1 = lftTurret._lateFollowers[0].transform.Find("AAR/Autoloader_6ETs10M/").gameObject;
                GameObject autoLoader2 = lftTurret._lateFollowers[0].transform.Find("AAR/Carousel_ElectricDrive(Can be manually cranked if failed)/").gameObject;
                GameObject turretDrive1 = lftHull._lateFollowers[0].transform.Find("AAR/T64_AAR_hull/LP_Hull/TurretRingNotSureAboutTheCrossection/Turret_TraverseMotor_Hull mounted parts of 2E28M stabilizer").gameObject;
                GameObject turretDrive2 = lftTurret._lateFollowers[0].transform.Find("AAR/Gun_Elevation_ManualBackUp/").gameObject;
                GameObject turretDrive3 = lftTurret._lateFollowers[0].transform.Find("AAR/Turret_Traverse_ManualDrive/").gameObject;
                GameObject turretDrive4 = lftTurret._lateFollowers[0].transform.Find("AAR/Commander_Cupola_Drive/").gameObject;

                GameObject gunOptics1 = lftTurret._lateFollowers[0].transform.Find("Imports from T80B/Gunner_Sight_1G42/").gameObject;
                GameObject gunOptics2 = lftTurret._lateFollowers[0].transform.Find("AAR/Gunner_Sight_TPN‑1‑49‑23/").gameObject;

                GameObject gunIR = lftLuna._lateFollowers[0].transform.Find("Luna/").gameObject;
                GameObject gunLRF = lftTurret._lateFollowers[0].transform.Find("AAR/LRF hit zone/").gameObject;


                if (tracks_T64B.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T64B.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T64B.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive3.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive4.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T64B.Value)
                {
                    Component.Destroy(gunOptics1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunOptics2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunIR.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
