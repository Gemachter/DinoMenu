using MelonLoader;
using HarmonyLib;
using UnityEngine;
using System;
using DinoMenu;
[assembly: MelonInfo(typeof(Mod), "Dino Menu", "0.1.0", "Gemachter")]

namespace DinoMenu
{
    public class Mod : MelonMod
    {
        public static PlayerMovement pControl;
        public static ZoneMove zone;
        public static float runSpeed = 0f;
        public static bool infinityJump = false;
        public override void OnUpdate() {
            if (infinityJump) pControl.m_Grounded = true;
        }
        public override void OnGUI()
        {
            if (pControl != null)
            {
                GUI.Window(0, new Rect(130, 20, 200, 300), (GUI.WindowFunction)MainWindow, "Dino Menu 0.1.0");
            }
        }
        public static void MainWindow(int windowID)
        {
            GUILayout.Label((Il2CppSystem.String)"Speed hack", null);
            runSpeed = pControl.runSpeed = GUILayout.HorizontalSlider(pControl.runSpeed, 1f, 200f, null);
            infinityJump = GUILayout.Toggle(infinityJump, (Il2CppSystem.String)"Infinity jump", null);
            zone.DontMove = GUILayout.Toggle(zone.DontMove, (Il2CppSystem.String)"Stop death zone", null);
        }
    }
    [HarmonyPatch(typeof(PlayerMovement), "Start", new Type[] { })]
    public class SpeedPatch
    {
        private static void Postfix(PlayerMovement __instance)
        {
            if (Mod.runSpeed != 0f) __instance.runSpeed = Mod.runSpeed;
            Mod.pControl = __instance;
        }
    }
    [HarmonyPatch(typeof(ZoneMove), "Start", new Type[] { })]
    public class ZonePatch
    {
        private static void Prefix(ZoneMove __instance)
        {
            Mod.zone = __instance;
        }
    }
}
