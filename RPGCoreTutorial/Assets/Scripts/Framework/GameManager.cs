﻿/*
 * GameManager - Backbone of the game application
 * Contains the most important data used throughout the game (ie: Game Settings)
 * Created by : Allan N. Murillo
 * Last Edited : 2/7/2020
 */

using System;
using ANM.Control;
using UnityEngine;

namespace ANM.Framework
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        private float _deltaTime;
        [SerializeField] private bool displayFps;
        [SerializeField] private bool isMainMenuActive;
        [SerializeField] private bool isGamePaused;
        
        private SaveSettings _saveSettings;
        private PlayerController _player;


        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            DontDestroyOnLoad(gameObject);
            Instance = this;
            
            SaveSettings.SettingsLoadedIni = false;
            _saveSettings = new SaveSettings();
            _saveSettings.Initialize();
            Time.timeScale = 1;
        }
        
        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            if (!displayFps) return;
            var style = new GUIStyle();
            int w = Screen.width, h = Screen.height;
            h *= 2 / 100;
            var rect = new Rect(w - 180, 0, w, h);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            var msecs = _deltaTime * 1000.0f;
            var fps = 1.0f / _deltaTime;
            var text = $"{msecs:0.0} ms ({fps:0.} fps)";
            GUI.Label(rect, text, style);
        }
        
        public bool GetIsMainMenuActive()
        {
            return isMainMenuActive;
        }

        public void SetIsMainMenuActive(bool b)
        {
            isMainMenuActive = b;
        }
        
        public bool GetIsGamePaused()
        {
            return isGamePaused;
        }

        public void SetIsGamePaused(bool b)
        {
            isGamePaused = b;
            Time.timeScale = b ? 0 : 1;
        }

        public SceneTransitionManager GetCustomSceneManager()
        {
            return GetComponentInChildren<SceneTransitionManager>();
        }

        public PlayerController GetPlayerController()
        {
            if (_player == null) _player = FindObjectOfType<PlayerController>();
            return _player;
        }

        public void SaveGameSettings()
        {
            _saveSettings.SaveGameSettings();
        }
        
        public void Reset()
        {
            Time.timeScale = 1;
            isGamePaused = false;
            isMainMenuActive = true;
        }

        private void OnDestroy()
        {
            if (Instance != this) return;
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}
