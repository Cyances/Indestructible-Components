using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class T62
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_T62, powertrain_T62, gun_T62, optics_T62;

        public static void Config(MelonPreferences_Category cfg)
        {

            tracks_T62 = cfg.CreateEntry<bool>("T-62 Tracks", true);
            optics_T62 = cfg.CreateEntry<bool>("T-62 Optics", false);
            gun_T62 = cfg.CreateEntry<bool>("T-62 Gun System", false);
            powertrain_T62 = cfg.CreateEntry<bool>("T-62 Powertrain", false);

        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-62") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("---T62_rig---/HULL/TURRET/GUN/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("---T62_rig---/HULL/TURRET/").GetComponent<LateFollowTarget>(); ;

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("T62_hull_armour/track_L_armour/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("T62_hull_armour/track_R_armour/").gameObject;

                GameObject radiator = lftHull._lateFollowers[0].transform.Find("T62_hull_armour/Radiator/").gameObject;
                GameObject gearbox = lftHull._lateFollowers[0].transform.Find("Transmission.001").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("Engine").gameObject;

                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("PKT/").gameObject;
                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("barrel/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("gun breech/").gameObject;

                GameObject turretDrive1 = lftTurret._lateFollowers[0].transform.Find("traversing gear/").gameObject;
                GameObject turretDrive2 = lftGun._lateFollowers[0].transform.Find("manual elevation/").gameObject;

                GameObject gunOptics = lftGun._lateFollowers[0].transform.Find("Optic/").gameObject;
                GameObject gunIR = lftTurret._lateFollowers[0].transform.Find("T62_turret_armour/turret casting/luna/").gameObject;

                if (tracks_T62.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T62.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T62.Value)
                {
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T62.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunIR.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
