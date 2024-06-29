using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class T80
    {
        static MelonPreferences_Entry<bool> tracks_T80B, powertrain_T80B, gun_T80B, optics_T80B;
        static GameObject autoLoader1, autoLoader2, turretDrive1, turretDrive2, turretDrive3, gunOptics1, gunOptics2, gunLRF;

        public static void Config(MelonPreferences_Category cfg)
        {
            tracks_T80B = cfg.CreateEntry<bool>("T-80B Tracks", true);
            gun_T80B = cfg.CreateEntry<bool>("T-80B Gun System", false);
            optics_T80B = cfg.CreateEntry<bool>("T-80B Optics", false);
            powertrain_T80B = cfg.CreateEntry<bool>("T-80B Powertrain", false);
        }

        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-80B") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();


                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("T80B_rig/HULL/TURRET/gun/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("T80B_rig/HULL/TURRET/").GetComponent<LateFollowTarget>();

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Tracks_Left_002/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Tracks_Left_001/").gameObject;

                GameObject radiator1 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Radiator_EngineOil_001").gameObject;
                GameObject radiator2 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Radiator_TransmissionOil_001").gameObject;
                GameObject gearbox1 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Gearbox_001").gameObject;
                GameObject gearbox2 = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Gearbox_002").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("T80B_AAR_Hull/Engine_GTD-1000TF").gameObject;

                GameObject gunIR = lftTurret._lateFollowers[1].transform.Find("Luna/").gameObject;

                GameObject gunBarrel = lftGun._lateFollowers[1].transform.Find("Gun_2A46-2/").gameObject;
                GameObject gunCoax = lftGun._lateFollowers[1].transform.Find("MG_Coax_PKT_7.62mm/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[1].transform.Find("Gun_Breach_Gun_Breach_BitsThatRecoil/").gameObject;

                try
                {
                    autoLoader1 = lftTurret._lateFollowers[0].transform.Find("Autoloader_MZ/").gameObject;
                    autoLoader2 = lftTurret._lateFollowers[0].transform.Find("Carousel_ElectricDrive(Can be manually cranked if failed)/").gameObject;
                    turretDrive1 = lftTurret._lateFollowers[0].transform.Find("Gun_Elevation_HydroelectricPump(part of 2E26M stabilizer)/").gameObject;
                    turretDrive2 = lftTurret._lateFollowers[0].transform.Find("Gun_Elevation_ManualDrive/").gameObject;
                    turretDrive3 = lftTurret._lateFollowers[0].transform.Find("Turret_ManualDrive/").gameObject;

                    gunOptics1 = lftTurret._lateFollowers[0].transform.Find("Gunner_Sight_1G42/").gameObject;
                    gunOptics2 = lftTurret._lateFollowers[0].transform.Find("Gunner_Sight_TPN‑3‑49/").gameObject;

                    gunLRF = lftTurret._lateFollowers[2].transform.Find("LRF hit zone/").gameObject;
                }

                catch
                {
                    autoLoader1 = lftTurret._lateFollowers[2].transform.Find("Autoloader_MZ/").gameObject;
                    autoLoader2 = lftTurret._lateFollowers[2].transform.Find("Carousel_ElectricDrive(Can be manually cranked if failed)/").gameObject;
                    turretDrive1 = lftTurret._lateFollowers[2].transform.Find("Gun_Elevation_HydroelectricPump(part of 2E26M stabilizer)/").gameObject;
                    turretDrive2 = lftTurret._lateFollowers[2].transform.Find("Gun_Elevation_ManualDrive/").gameObject;
                    turretDrive3 = lftTurret._lateFollowers[2].transform.Find("Turret_ManualDrive/").gameObject;

                    gunOptics1 = lftTurret._lateFollowers[2].transform.Find("Gunner_Sight_1G42/").gameObject;
                    gunOptics2 = lftTurret._lateFollowers[2].transform.Find("Gunner_Sight_TPN‑3‑49/").gameObject;

                    gunLRF = lftTurret._lateFollowers[0].transform.Find("LRF hit zone/").gameObject;
                }

                if (tracks_T80B.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T80B.Value)
                {
                    Component.Destroy(radiator1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(radiator2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T80B.Value)
                {
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(autoLoader2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive3.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T80B.Value)
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
