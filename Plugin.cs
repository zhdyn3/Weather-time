using System;
using BepInEx;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilla;

namespace GorillaTagModTemplateProject
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.Zhdyn3.gorillatag.weathertime", "Weather Time", "1.0.0")] // Используйте явно указанный GUID, имя и версию
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        private bool uiOpen = false;
        private bool keyPressed = false;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            // Если вы не используете Harmony, удалите следующие строки
            // HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            // Если вы не используете Harmony, удалите следующие строки
            // HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            // Инициализация после загрузки игры
        }

        void Update()
        {
            if (inRoom)
            {
                if (Keyboard.current.tabKey.isPressed)
                {
                    if (!keyPressed)
                    {
                        uiOpen = !uiOpen;
                    }
                    keyPressed = true;
                }
                else
                {
                    keyPressed = false;
                }
            }
        }

        void OnGUI()
        {
            if (uiOpen && inRoom)
            {
                float buttonWidth = 180f;
                float buttonHeight = 30f;
                float buttonSpacing = 40f;
                float buttonX = 30f;
                float buttonY = Screen.height - 210f;

                if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Night"))
                {
                    SetNightTime();
                }
                if (GUI.Button(new Rect(buttonX, buttonY + buttonSpacing, buttonWidth, buttonHeight), "Evening"))
                {
                    SetEveningTime();
                }
                if (GUI.Button(new Rect(buttonX, buttonY + 2 * buttonSpacing, buttonWidth, buttonHeight), "Morning"))
                {
                    SetMorningTime();
                }
                if (GUI.Button(new Rect(buttonX, buttonY + 3 * buttonSpacing, buttonWidth, buttonHeight), "Day"))
                {
                    SetDayTime();
                }

                float weatherButtonX = Screen.width - buttonWidth - 30f;
                if (GUI.Button(new Rect(weatherButtonX, buttonY, buttonWidth, buttonHeight), "Rain"))
                {
                    SetRain();
                }
                if (GUI.Button(new Rect(weatherButtonX, buttonY + buttonSpacing, buttonWidth, buttonHeight), "Clear"))
                {
                    SetNoRain();
                }
            }
        }

        public static void SetNightTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }

        public static void SetEveningTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(7);
        }

        public static void SetMorningTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }

        public static void SetDayTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }

        public static void SetRain()
        {
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
            }
        }

        public static void SetNoRain()
        {
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}

