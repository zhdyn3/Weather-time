using System;
using BepInEx;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilla;

namespace GorillaTagModTemplateProject
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.Zhdyn3.gorillatag.weathertime", "Weather Time", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;
        private bool uiOpen = false;
        private bool keyPressed = false;
        private GameObject newOutsideObject;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            if (inRoom)
            {
                // Поиск объекта "NewOutside" в сцене
                newOutsideObject = GameObject.Find("Environment Objects/LocalObjects_Prefab/City/CosmeticsRoomAnchor/NewOutside");
                if (newOutsideObject == null)
                {
                    Logger.LogError("Не удалось найти объект 'NewOutside'. Проверьте путь.");
                }
            }
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

        public void SetNightTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }

        public void SetEveningTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(7);
        }

        public void SetMorningTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }

        public void SetDayTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }

        public void SetRain()
        {
            if (newOutsideObject != null)
            {
                newOutsideObject.SetActive(true); // Показываем объект
            }
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
            }
        }

        public void SetNoRain()
        {
            if (newOutsideObject != null)
            {
                newOutsideObject.SetActive(false); // Скрываем объект
            }
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
            uiOpen = false;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
            uiOpen = false;
        }
    }
}
