using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO.Enumeration;
using System.Collections.Generic;
using System;
public class GraphicsConfigManager : MonoBehaviour
{
    public string configFilePath;
    public string MyGames;
    public string Deltaink;
    public string Chel;
    private int qualityLevel = 2;
    private bool fullscreen = true;
    private int resolutionIndex = 0;
    private bool vSync = true;
    private int antiAliasing = 1;
    private int shadowQuality = 2;
    private int textureQuality = 1; 
    private float fieldOfView = 60f;
    private bool ambientOcclusion = true;
    private bool bloom = false;
    private bool motionBlur = false;
    private bool depthOfField = false;
    private bool postProcessingEffects = true;
    private int anisotropicFiltering = 1;

    void Start()
    {
        Directory.CreateDirectory(MyGames);
        Directory.CreateDirectory(Deltaink);
        Directory.CreateDirectory(Chel);
        LoadConfig();
        ApplySettings();
    }

    public void LoadConfig()
    {
        if (File.Exists(configFilePath))
        {
            string[] lines = File.ReadAllLines(configFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    switch (key)
                    {
                        case "qualityLevel":
                            int.TryParse(value, out qualityLevel);
                            break;
                        case "fullscreen":
                            bool.TryParse(value, out fullscreen);
                            break;
                        case "resolutionIndex":
                            int.TryParse(value, out resolutionIndex);
                            break;
                        case "vSync":
                            bool.TryParse(value, out vSync);
                            break;
                        case "antiAliasing":
                            int.TryParse(value, out antiAliasing);
                            break;
                        case "shadowQuality":
                            int.TryParse(value, out shadowQuality);
                            break;
                        case "textureQuality":
                            int.TryParse(value, out textureQuality);
                            break;
                        case "fieldOfView":
                            float.TryParse(value, out fieldOfView);
                            break;
                        case "ambientOcclusion":
                            bool.TryParse(value, out ambientOcclusion);
                            break;
                        case "bloom":
                            bool.TryParse(value, out bloom);
                            break;
                        case "motionBlur":
                            bool.TryParse(value, out motionBlur);
                            break;
                        case "depthOfField":
                            bool.TryParse(value, out depthOfField);
                            break;
                        case "postProcessingEffects":
                            bool.TryParse(value, out postProcessingEffects);
                            break;
                        case "anisotropicFiltering":
                            int.TryParse(value, out anisotropicFiltering);
                            break;
                    }
                }
                            }
        }
        else
        {
            SaveConfig();
        }
    }

    public void SaveConfig()
    {
        File.WriteAllLines(configFilePath, new string[] 
        { 
            $"qualityLevel = {qualityLevel}", 
            $"fullscreen = {fullscreen}", 
            $"resolutionIndex = {resolutionIndex}",
            $"vSync = {vSync}",
            $"antiAliasing = {antiAliasing}",
            $"shadowQuality = {shadowQuality}",
            $"textureQuality = {textureQuality}",
            $"fieldOfView = {fieldOfView}",
            $"ambientOcclusion = {ambientOcclusion}",
            $"bloom = {bloom}",
            $"motionBlur = {motionBlur}",
            $"depthOfField = {depthOfField}",
            $"postProcessingEffects = {postProcessingEffects}",
            $"anisotropicFiltering = {anisotropicFiltering}"
        });
    }

    public void ApplySettings()
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        Screen.fullScreen = fullscreen;

        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, fullscreen);
        }

        QualitySettings.vSyncCount = vSync ? 1 : 0;
        QualitySettings.antiAliasing = antiAliasing;
        QualitySettings.shadows = (ShadowQuality)shadowQuality;
        QualitySettings.globalTextureMipmapLimit = textureQuality;

        Camera.main.fieldOfView = fieldOfView;

        var postProcessingVolume = Camera.main.GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        if (postProcessingVolume != null)
        {
            postProcessingVolume.enabled = postProcessingEffects;

            var aoLayer = postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.AmbientOcclusion>();
            if (aoLayer != null) aoLayer.active = ambientOcclusion;

            var bloomLayer = postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();
            if (bloomLayer != null) bloomLayer.active = bloom;

            var motionBlurLayer = postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.MotionBlur>();
            if (motionBlurLayer != null) motionBlurLayer.active = motionBlur;

            var dofLayer = postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>();
            if (dofLayer != null) dofLayer.active = depthOfField;
        }

        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)anisotropicFiltering;
    }

    public void ChangeSettings(int newQualityLevel, bool newFullscreen, int newResolutionIndex,
                               bool newVSync, int newAntiAliasing, int newShadowQuality,
                               int newTextureQuality, float newFieldOfView,
                               bool newAmbientOcclusion, bool newBloom,
                               bool newMotionBlur, bool newDepthOfField,
                               bool newPostProcessingEffects, int newAnisotropicFiltering)
    {
        qualityLevel = newQualityLevel;
        fullscreen = newFullscreen;
        resolutionIndex = newResolutionIndex;
        vSync = newVSync;
        antiAliasing = newAntiAliasing;
        shadowQuality = newShadowQuality;
        textureQuality = newTextureQuality;
        fieldOfView = newFieldOfView;
        ambientOcclusion = newAmbientOcclusion;
        bloom = newBloom;
        motionBlur = newMotionBlur;
                depthOfField = newDepthOfField;
        postProcessingEffects = newPostProcessingEffects;
        anisotropicFiltering = newAnisotropicFiltering;

        SaveConfig();
        ApplySettings();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) // Нажмите пробел для изменения настроек
        //{
        //    ChangeSettings(Random.Range(0, QualitySettings.names.Length), 
        //                   !fullscreen, 
        //                   Random.Range(0, Screen.resolutions.Length),
        //                   !vSync,
         //                  Random.Range(0, 4), 
        //                   Random.Range(0, 3), 
         //                  Random.Range(0, 3), 
         //                  Random.Range(60f, 90f), 
        //                   Random.value > 0.5f,
        //                   Random.value > 0.5f,
         //                  Random.value > 0.5f,
        //                   Random.value > 0.5f,
         //                  Random.value > 0.5f,
         //                  Random.Range(0, 3)); // Предполагаем, что 0-2 — допустимые значения для Anisotropic Filtering
        //                   
        //    Debug.Log($"Настройки сохранены: Quality Level: {qualityLevel}, Fullscreen: {fullscreen}, Resolution Index: {resolutionIndex}, V-Sync: {vSync}, Anti-Aliasing: {antiAliasing}, Shadow Quality: {shadowQuality}, Texture Quality: {textureQuality}, Field of View: {fieldOfView}, Ambient Occlusion: {ambientOcclusion}, Bloom: {bloom}, Motion Blur: {motionBlur}, Depth of Field: {depthOfField}, Post Processing Effects: {postProcessingEffects}, Anisotropic Filtering: {anisotropicFiltering}");
        //}
    }
}