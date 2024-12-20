﻿/*
 * Copyright (c) 2024 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using CarterGames.Assets.SaveManager.Serializiation;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Assets.SaveManager.Editor
{
    /// <summary>
    /// Draws a list of all the save profiles the user currently has stored on the profiles tab of the save editor.
    /// </summary>
    public static class SaveProfileListGUI
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        // General text.
        private const string DropDownLabel = "Profile Viewer";
        private const string LoadButtonLabel = "Load Profile";
        private const string EditButtonLabel = "View File";
        private const string DeleteButtonLabel = "-";
        
        // No profiles help box
        private const string NoProfilesHelpBoxContent = "No profiles created to display.";
        
        // Load profile dialogue text.
        private const string LoadTitle = "Load Profile Warning";
        private const string LoadBody = "Are you sure you want to load this profile, any data currently in the save will no be recoverable.";
        private const string LoadYes = "Load Profile";
        private const string LoadNo = "Cancel";
        
        // Delete profile dialogue text.
        private const string DeleteTitle = "Delete Profile?";
        private const string DeleteBody = "Are you sure you want to delete this profile, The profile will not be recoverable.";
        private const string DeleteYes = "Delete Profile";
        private const string DeleteNo = "Cancel";

        // Fields
        private const string ProfileNameReplaceString = "- Save Data";
        
        // Caches
        private static Vector2 scrollViewRect;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Draw Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static void DrawDisplay()
        {
            // Draws the dropdown for this GUI.
            PerUserSettings.SaveEditorProfileViewer =
                EditorGUILayout.Foldout(PerUserSettings.SaveEditorProfileViewer, DropDownLabel);
            
            
            // Stop if the dropdown is not opened.
            if (!PerUserSettings.SaveEditorProfileViewer) return;
            if (UtilEditor.SaveProfiles.Data == null) return;
            
            
            // Just draws a help box if there are no profiles.
            if (UtilEditor.SaveProfiles.Data.Count <= 0)
            {
                EditorGUILayout.HelpBox(NoProfilesHelpBoxContent, MessageType.Info);
                return;
            }

            
            EditorGUILayout.Space(5f);

            scrollViewRect = EditorGUILayout.BeginScrollView(scrollViewRect);
            
            // Draws all the save profiles in the project.
            foreach (var profile in UtilEditor.SaveProfiles.Data.ToArray())
            {
                DrawProfile(profile);
                EditorGUILayout.Space(0f);  // Funnily this works to draw the right size xD
            }
            
            EditorGUILayout.EndScrollView();
        }


        private static void DrawProfile(TextAsset data)
        {
            // Stops if the data is null to avoid errors.
            if (data == null) return;
                   
                
            // Draws the label for the profile.
            EditorGUILayout.BeginHorizontal("HelpBox");
            EditorGUILayout.LabelField(data.name.Replace(ProfileNameReplaceString, string.Empty));
                
                
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            
            // Draws the load profile button.
            if (GUILayout.Button(LoadButtonLabel, GUILayout.Width(115)))
            {
                OnLoadProfilePressed(data);
            }
                
                
            GUILayout.Space(2.5f);
                
                
            // Draws the edit profile button.
            if (GUILayout.Button(EditButtonLabel, GUILayout.Width(100)))
            {
                OnEditProfilePressed(data);
            }
                
                
            GUILayout.Space(2.5f);
                
            
            GUI.backgroundColor = UtilEditor.Red;
                
            // Draws the delete profile button.
            if (GUILayout.Button(DeleteButtonLabel, GUILayout.Width(25)))
            {
                if (EditorUtility.DisplayDialog(DeleteTitle, DeleteBody, DeleteYes, DeleteNo))
                {
                    OnDeleteProfilePressed(data); 
                }
            }
                
            GUI.backgroundColor = Color.white;
            
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Runs the logic for the user pressing the load profile button.
        /// </summary>
        /// <param name="data">The data to load.</param>
        private static void OnLoadProfilePressed(TextAsset data)
        {
            if (!EditorUtility.DisplayDialog(LoadTitle, LoadBody, LoadYes, LoadNo)) return;
            LoadSaveProfile(data);
        }


        /// <summary>
        /// Runs the logic for the user pressing the edit profile button.
        /// </summary>
        /// <param name="data">The object to select.</param>
        private static void OnEditProfilePressed(Object data)
        {
            EditorGUIUtility.PingObject(data);
        }


        /// <summary>
        /// Deletes the profile when called.
        /// </summary>
        /// <param name="data">The data to delete.</param>
        private static void OnDeleteProfilePressed(TextAsset data)
        {
            UtilEditor.SaveProfiles.RemoveProfile(data);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(data));
        }
        

        /// <summary>
        /// Loads the save data entered when called.
        /// </summary>
        /// <param name="data">The data to load.</param>
        private static void LoadSaveProfile(TextAsset data)
        {
            // Null Data
            if (data == null)
            {
                SaveManagerLogger.Log("No data found in the profile, so nothing was loaded.");
                return;
            }
            

            try
            {
                SerializableDictionary<string, SerializableDictionary<string, string>> jsonData;
                jsonData = JsonUtility.FromJson<SerializableDictionary<string, SerializableDictionary<string, string>>> (data.text);

                SaveManager.Load(jsonData);
                SaveManager.Save();
            }
            catch (Exception e)
            {
                SaveManagerLogger.LogError($"Failed to read to {UtilEditor.AssetGlobalRuntimeSettings.SavePath} with the exception: {e}");
                return;
            }


            foreach (var saveObject in UtilEditor.SaveData.Data)
            {
                EditorUtility.SetDirty(saveObject);
            }

            // Updates the data in the editor & in the actual save.
            EditorUtility.SetDirty(UtilEditor.AssetGlobalRuntimeSettings);
            AssetDatabase.SaveAssets();
            SaveManager.Save();
        }
    }
}