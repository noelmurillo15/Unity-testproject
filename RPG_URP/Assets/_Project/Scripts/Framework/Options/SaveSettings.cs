﻿/*
 * SaveSettings - Save/Loads game settings (audio, video) to/from a JSON file
 * Created by : Allan N. Murillo
 * Last Edited : 1/14/2021
 */

using System.IO;
using UnityEngine;

namespace ANM.Framework.Options
{
    [System.Serializable]
    public class SaveSettings
    {
        private static string _jsonString;
        private static string _fileName = "/GameSettings.json";

        public float masterVolume;
        public float effectVolume;
        public float backgroundVolume;
        public int currentQualityLevel;
        public int msaa;
        public float renderDist;
        public float shadowDist;
        public int textureLimit;
        public int anisotropicFilteringLevel;
        public int shadowCascade;
        public bool displayFps;
        public bool fullscreen;

        internal static float masterVolumeIni;
        internal static float effectVolumeIni;
        internal static float backgroundVolumeIni;
        internal static int currentQualityLevelIni;
        internal static int msaaIni;
        internal static float renderDistIni;
        internal static float shadowDistIni;
        internal static int textureLimitIni;
        internal static int anisotropicFilteringLevelIni;
        internal static int shadowCascadeIni;
        internal static bool displayFpsIni;
        internal static bool fullScreenIni;
        internal static bool settingsLoadedIni;


        private static object CreateJsonObj(string jsonString)
        {
            return JsonUtility.FromJson<SaveSettings>(jsonString);
        }

        public bool LoadGameSettings()
        {
            var filePath = Application.persistentDataPath + _fileName;
            if (!VerifyDirectory(filePath)) return false;
            OverwriteGameSettings(File.ReadAllText(filePath));
            return true;
        }

        public void SaveGameSettings()
        {
            var filePath = Application.persistentDataPath + _fileName;
            if (VerifyDirectory(filePath))
            {
                File.Delete(filePath);
            }

            masterVolume = masterVolumeIni;
            effectVolume = effectVolumeIni;
            backgroundVolume = backgroundVolumeIni;
            renderDist = renderDistIni;
            shadowDist = shadowDistIni;
            msaa = msaaIni;
            textureLimit = textureLimitIni;
            currentQualityLevel = currentQualityLevelIni;
            shadowCascade = shadowCascadeIni;
            anisotropicFilteringLevel = anisotropicFilteringLevelIni;
            displayFps = displayFpsIni;
            fullscreen = fullScreenIni;

            _jsonString = JsonUtility.ToJson(this);
            File.WriteAllText(filePath, _jsonString);
        }

        private void OverwriteGameSettings(string jsonString)
        {
            var jsonObj = (SaveSettings) CreateJsonObj(jsonString);
            masterVolumeIni = jsonObj.masterVolume;
            effectVolumeIni = jsonObj.effectVolume;
            backgroundVolumeIni = jsonObj.backgroundVolume;
            renderDistIni = jsonObj.renderDist;
            shadowDistIni = jsonObj.shadowDist;
            msaaIni = jsonObj.msaa;
            textureLimitIni = jsonObj.textureLimit;
            currentQualityLevelIni = jsonObj.currentQualityLevel;
            shadowCascadeIni = jsonObj.shadowCascade;
            anisotropicFilteringLevelIni = jsonObj.anisotropicFilteringLevel;
            displayFpsIni = jsonObj.displayFps;
            fullScreenIni = jsonObj.fullscreen;
            settingsLoadedIni = true;
        }

        public static void DefaultSettings()
        {
            masterVolumeIni = 0.8f;
            effectVolumeIni = 0.8f;
            backgroundVolumeIni = 0.8f;
            currentQualityLevelIni = 2;
            msaaIni = 2;
            anisotropicFilteringLevelIni = 1;
            renderDistIni = 1000.0f;
            shadowDistIni = 150;
            shadowCascadeIni = 3;
            textureLimitIni = 0;
            displayFpsIni = true;
            fullScreenIni = false;
            settingsLoadedIni = true;
        }

        private bool VerifyDirectory(string filePath) => File.Exists(filePath);

        public void Initialize() => settingsLoadedIni = LoadGameSettings();
    }
}