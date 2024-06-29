using System.Collections;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Equipment;
using MelonLoader;
using UnityEngine;

namespace IndestructibleComponents
{
    public static class T55
    {
        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> tracks_T55, powertrain_T55, gun_T55, optics_T55;

        public static void Config(MelonPreferences_Category cfg)
        {

            tracks_T55 = cfg.CreateEntry<bool>("T-55 Tracks", true);
            optics_T55 = cfg.CreateEntry<bool>("T-55 Optics", false);
            gun_T55 = cfg.CreateEntry<bool>("T-55 Gun System", false);
            powertrain_T55 = cfg.CreateEntry<bool>("T-55 Powertrain", false);

        }
        public static IEnumerator Convert(GameState _)
        {

            foreach (GameObject vic_go in IndestructibleComponentsMod.vic_gos)
            {
                Vehicle vic = vic_go.GetComponent<Vehicle>();

                if (vic == null) continue;
                if (vic.GetComponent<Util.AlreadyConvertedIndesComp>() != null) continue;
                if (vic.FriendlyName != "T-55A") continue;
                vic.gameObject.AddComponent<Util.AlreadyConvertedIndesComp>();

                var lftHull = vic.GetComponent<LateFollowTarget>();
                var lftGun = vic.transform.Find("T55A_skeleton/HULL/Turret/GUN/").GetComponent<LateFollowTarget>();
                var lftTurret = vic.transform.Find("T55A_skeleton/HULL/Turret/").GetComponent<LateFollowTarget>(); ;

                GameObject tracks1 = lftHull._lateFollowers[0].transform.Find("model/tracks_R.001/").gameObject;
                GameObject tracks2 = lftHull._lateFollowers[0].transform.Find("model/tracks_R.002/").gameObject;

                GameObject radiator = lftHull._lateFollowers[0].transform.Find("model/Radiator/").gameObject;
                GameObject gearbox = lftHull._lateFollowers[0].transform.Find("model/Transmission.001/").gameObject;
                GameObject engine = lftHull._lateFollowers[0].transform.Find("model/Engine").gameObject;

                GameObject gunCoax = lftGun._lateFollowers[0].transform.Find("PKT/").gameObject;
                GameObject gunBarrel = lftGun._lateFollowers[0].transform.Find("gun barrel/").gameObject;
                GameObject gunBreech = lftGun._lateFollowers[0].transform.Find("Breech block/").gameObject;

                GameObject turretDrive1 = lftTurret._lateFollowers[0].transform.Find("model/turret drive control box/").gameObject;
                GameObject turretDrive2 = lftTurret._lateFollowers[0].transform.Find("model/Turret Drive/").gameObject;
                GameObject turretDrive3 = lftGun._lateFollowers[0].transform.Find("vertical drive/").gameObject;

                GameObject gunOptics = lftTurret.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("model/gunner sight/").gameObject;

                if (tracks_T55.Value)
                {
                    Component.Destroy(tracks1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(tracks2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (powertrain_T55.Value)
                {
                    Component.Destroy(radiator.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gearbox.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(engine.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (gun_T55.Value)
                {
                    Component.Destroy(gunCoax.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBarrel.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(gunBreech.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive1.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive2.GetComponent<GHPC.Equipment.DestructibleComponent>());
                    Component.Destroy(turretDrive3.GetComponent<GHPC.Equipment.DestructibleComponent>());
                }

                if (optics_T55.Value)
                {
                    Component.Destroy(gunOptics.GetComponent<GHPC.Equipment.DestructibleComponent>());
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
