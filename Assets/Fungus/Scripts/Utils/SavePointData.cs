// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

#if UNITY_5_3_OR_NEWER

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Fungus
{
    /// <summary>
    /// Serializable container for a Save Point's data. 
    /// All data is stored as strings, and the only concrete game class it depends on is the SaveData component.
    /// </summary>
    [System.Serializable]
    public class SavePointData
    {
        [SerializeField] protected string savePointKey;

        [SerializeField] protected string savePointDescription;

        [SerializeField] protected string sceneName;

        [SerializeField] protected List<SaveDataItem> saveDataItems = new List<SaveDataItem>();

        protected static SavePointData Create(string _savePointKey, string _savePointDescription, string _sceneName)
        {
            var savePointData = new SavePointData();

            savePointData.savePointKey = _savePointKey;
            savePointData.savePointDescription = _savePointDescription;
            savePointData.sceneName = _sceneName;

            return savePointData;
        }

        #region Public methods

        /// <summary>
        /// Gets or sets the unique key for the Save Point.
        /// </summary>
        public string SavePointKey { get { return savePointKey; } set { savePointKey = value; } }

        /// <summary>
        /// Gets or sets the description for the Save Point.
        /// </summary>
        public string SavePointDescription { get { return savePointDescription; } set { savePointDescription = value; } }

        /// <summary>
        /// Gets or sets the scene name associated with the Save Point.
        /// </summary>
        public string SceneName { get { return sceneName; } set { sceneName = value; } }

        /// <summary>
        /// Gets the list of save data items.
        /// </summary>
        /// <value>The save data items.</value>
        public List<SaveDataItem> SaveDataItems { get { return saveDataItems; } }

		/// <summary>
        /// Encodes a new Save Point to data and converts it to JSON text format.
        /// </summary>
        public static string Encode(string _savePointKey, string _savePointDescription, string _sceneName)
		{
            var savePointData = Create(_savePointKey, _savePointDescription, _sceneName);

            // Look for a SaveData component in the scene to populate the save data items.
            var saveData = GameObject.FindObjectOfType<SaveData>();
            if (saveData != null)
            {
                saveData.Encode(savePointData.SaveDataItems);
            }

            return JsonUtility.ToJson(savePointData, true);
        }

        /// <summary>
        /// Decodes a Save Point from JSON text format and loads it.
        /// </summary>
        public static void Decode(string saveDataJSON)
        {
            var savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);

			JSONDescription(saveDataJSON);

            UnityAction<Scene, LoadSceneMode> onSceneLoadedAction = null;

            onSceneLoadedAction = (scene, mode) =>  {
                // Additive scene loads and non-matching scene loads could happen if the client is using the
                // SceneManager directly. We just ignore these events and hope they know what they're doing!
                if (mode == LoadSceneMode.Additive ||
                    scene.name != savePointData.SceneName)
                {
                    return;
                }

                SceneManager.sceneLoaded -= onSceneLoadedAction;

                // Look for a SaveData component in the scene to process the save data items.
                var saveData = GameObject.FindObjectOfType<SaveData>();
                if (saveData != null)
                {
                    saveData.Decode(savePointData.SaveDataItems);
                }

                SaveManagerSignals.DoSavePointLoaded(savePointData.savePointKey);
            };
                
            SceneManager.sceneLoaded += onSceneLoadedAction;
            SceneManager.LoadScene(savePointData.SceneName);
        }

		public struct StringVar
		{
			public string key { get; set; }
			public string value { get; set; }
		}

		public struct IntVar
		{
			public string key { get; set; }
			public int value { get; set; }
		}

		public struct BoolVar
		{
			public string key { get; set; }
			public bool value { get; set; }
		}

		public struct RootObject
		{
			public string flowchartName { get; set; }
			public List<StringVar> stringVars { get; set; }
			public List<IntVar> intVars { get; set; }
			public List<object> floatVars { get; set; }
			public List<BoolVar> boolVars { get; set; }
			
			public static RootObject CreateFromJSON(string jsonString)
			{
				return JsonUtility.FromJson<RootObject>(jsonString);
			}
		}

		public static string JSONDescription (string saveDataJSON) 
		{
			var savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);

			Debug.Log(savePointData);

			string data = savePointData.SaveDataItems[0].Data;

			Debug.Log(data);

			RootObject playerData = RootObject.CreateFromJSON(data);

Debug.Log("SavePointData: " + playerData);
			Debug.Log(playerData.flowchartName);
			Debug.Log(playerData.stringVars);
			Debug.Log(playerData.intVars);
			Debug.Log(playerData.boolVars);
			return data;
		}

		/*
		/// <summary>
		/// Decodes a Save Point from JSON text format and loads it.
		/// </summary>
		public static string StringDecode(string saveDataJSON)
		{
			var savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);

			UnityAction<Scene, LoadSceneMode> onSceneLoadedAction = null;

			onSceneLoadedAction = (scene, mode) =>  {
				// Additive scene loads and non-matching scene loads could happen if the client is using the
				// SceneManager directly. We just ignore these events and hope they know what they're doing!
				if (mode == LoadSceneMode.Additive ||
				scene.name != savePointData.SceneName)
				{
					return;
				}

				SceneManager.sceneLoaded -= onSceneLoadedAction;

				// Look for a SaveData component in the scene to process the save data items.
				var saveData = GameObject.FindObjectOfType<SaveData>();
				if (saveData != null)
				{
					saveData.Decode(savePointData.SaveDataItems);
				}

				SaveManagerSignals.DoSavePointLoaded(savePointData.savePointKey);
			};

			SceneManager.sceneLoaded += onSceneLoadedAction;
			SceneManager.LoadScene(savePointData.SceneName);
		}   
		*/

        #endregion
    }
}

#endif