using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;

namespace Client
{
    public class Main : BaseScript
    {
        static Random r = new Random();
        public Main()
        {
            API.RegisterCommand("Bodyguard", new Action(TestCommand), false);
        }

        private async static void TestCommand()
        {
            Ped player = Game.Player.Character;
            Screen.ShowNotification("Bodyguard spawned");
            API.RequestModel((uint)PedHash.Armoured01SMM);
            while (!API.HasModelLoaded((uint)PedHash.Armoured01SMM))
            {
                Debug.WriteLine("Waiting for model to load");
                await BaseScript.Delay(100);
            }
            Ped bodyguard = await World.CreatePed(PedHash.Armoured01SMM, player.Position + (player.ForwardVector * 2));
            bodyguard.Task.LookAt(player);
            API.SetPedAsGroupMember(bodyguard.Handle, API.GetPedGroupIndex(player.Handle));
            API.SetPedCombatAbility(bodyguard.Handle, 2);
            API.GiveWeaponToPed(bodyguard.Handle, (uint)GetRandomWeapon(), 500, false, true);
            API.SetPedArmour(bodyguard.Handle, 100);
            bodyguard.PlayAmbientSpeech("GENERIC_HI");
           
        }

        private static WeaponHash GetRandomWeapon() 
        {
            switch (r.Next(0,5))
            {
                case 0:
                    return WeaponHash.AssaultSMG;
                case 1:
                    return WeaponHash.Railgun;
                case 2:
                    return WeaponHash.RPG;
                case 3:
                    return WeaponHash.RevolverMk2;
                case 4:
                    return WeaponHash.SniperRifle;
                default:
                    return WeaponHash.Pistol;
            }
        }
    }
}
