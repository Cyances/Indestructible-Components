using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class PT76
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_PT76, powertrain_PT76, gun_PT76, optics_PT76;

        public static void Config(MelonPreferences_Category cfg)
        {

            tracks_PT76 = cfg.CreateEntry<bool>("PT-76 Tracks", true);
            optics_PT76 = cfg.CreateEntry<bool>("PT-76 Optics", false);
            gun_PT76 = cfg.CreateEntry<bool>("PT-76 Gun System", false);
            powertrain_PT76 = cfg.CreateEntry<bool>("PT-76 Powertrain", false);
        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "PT-76B") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("PT76_rig/HULL/TURRET/GUN/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("PT76_rig/HULL/TURRET/").GetComponent<LateFollowTarget>(); ;

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("Armor/l_track002 (1)/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("Armor/l_track003 (1)/").gameObject;

                GameObject radiator = lftHull._lateFollowers[0].transform.Find("AAR/radiator/").gameObject;
                GameObject gearbox = lftHull._lateFollowers[0].transform.Find("AAR/transmission/").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("AAR/motor").gameObject;

                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("AAR/PKT --R--/").gameObject;
                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("Armor/gun tube/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("AAR/gun breech/").gameObject;

                GameObject turretDrive1 = lftTurret._lateFollowers[0].transform.Find("AAR/electric traverse/").gameObject;
                GameObject turretDrive2 = lftTurret._lateFollowers[0].transform.Find("AAR/stabilizer/").gameObject;
                GameObject gunControls = lftTurret._lateFollowers[0].transform.Find("AAR/gunner's controls/").gameObject;

                GameObject gunIR = lftTurret._lateFollowers[1].transform.Find("turret lamp/").gameObject;

                if (tracks_PT76.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_PT76.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_PT76.Value)
                {
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunControls.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_PT76.Value)
                {
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
