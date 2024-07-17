using BepInEx;
using UnityEngine;
using UnityEngine.InputSystem;

[BepInPlugin("com.Zhdyn3.gorillatag.weathertime", "Weather Time", "1.0.0")]
public class TimeAndWeatherMod : BaseUnityPlugin
{
    private bool uiOpen = false;
    private bool keyPressed = false;

    private void Update()
    {
        // Показывать/скрывать UI по нажатию Tab
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

    void OnGUI()
    {
        if (uiOpen)
        {
            // Кнопки времени
            float buttonWidth = 180f;
            float buttonHeight = 30f;
            float buttonSpacing = 40f;
            float buttonX = 30f;
            float buttonY = Screen.height - 210f;

            if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Ночь"))
            {
                SetNightTime();
            }
            if (GUI.Button(new Rect(buttonX, buttonY + buttonSpacing, buttonWidth, buttonHeight), "Вечер"))
            {
                SetEveningTime();
            }
            if (GUI.Button(new Rect(buttonX, buttonY + 2 * buttonSpacing, buttonWidth, buttonHeight), "Утро"))
            {
                SetMorningTime();
            }
            if (GUI.Button(new Rect(buttonX, buttonY + 3 * buttonSpacing, buttonWidth, buttonHeight), "День"))
            {
                SetDayTime();
            }

            // Кнопки погоды
            float weatherButtonX = Screen.width - buttonWidth - 30f; // Позиция X для кнопок погоды
            if (GUI.Button(new Rect(weatherButtonX, buttonY, buttonWidth, buttonHeight), "Дождь"))
            {
                SetRain();
            }
            if (GUI.Button(new Rect(weatherButtonX, buttonY + buttonSpacing, buttonWidth, buttonHeight), "Ясная"))
            {
                SetNoRain();
            }
        }
    }

    // Функции смены времени
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

    // Функции смены погоды
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
}
