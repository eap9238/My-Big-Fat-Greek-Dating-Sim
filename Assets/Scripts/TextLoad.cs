using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextLoad : MonoBehaviour
{
    //nic was here
	private static Text text;
	private static Button button;

    public string fileName;
    private string dataPath;
    private string printData;

    void Start()
    {
		text = GetComponentInChildren<Text> ();
		button = GetComponent<Button> ();

        dataPath = Path.Combine(Application.persistentDataPath, fileName);
    }

    void Update()
	{
		
    }

	public void LoadText() {
		printData = LoadJson(dataPath);

		text.text = printData;
	}

    static string LoadJson(string path)
    {
		try {
			using (StreamReader streamReader = new StreamReader(path))
			{
				string jsonString = streamReader.ReadToEnd();

				JsonObject formattedString = JsonUtility.FromJson<JsonObject>(jsonString);

				//Debug.Log(jsonString);
				//Debug.Log(formattedString.SavePoints[(formattedString.SavePoints.Count) - 1]);
				//Debug.Log(formattedString.Length);

				return JSONDescription(formattedString.SavePoints[(formattedString.SavePoints.Count) - 1]);
			}
		}
		catch {
			button.interactable = false;

			return ("No Save File");
		}
    }

    public static string JSONDescription(string saveDataJSON)
    {
        var savePointData = JsonUtility.FromJson<SavePointData>(saveDataJSON);

        //Debug.Log(savePointData);

        SaveObject playerData = new SaveObject();

        playerData = JsonUtility.FromJson<SaveObject>(savePointData.SaveDataItems[0].Data);

        //Debug.Log(savePointData.SaveDataItems[0].Data);
        //Debug.Log(playerData.FlowchartName);
        
        string temp = "";

        if (playerData.BoolVars[0].Value)
        {
            temp += playerData.StringVars[0].Value + " (F)" + '\n';
        }
        else
        {
            temp += playerData.StringVars[0].Value + " (M)" + '\n';
        }

        temp += savePointData.SavePointKey + '\n';
        temp += playerData.IntVars[1].Value.ToString("D2") + ":" + playerData.IntVars[0].Value.ToString("D2") + '\n';
        temp += "Week: " + playerData.IntVars[3].Value + " Day: " + playerData.IntVars[2].Value + '\n';

        return temp;
    }

    [System.Serializable]
    public class JsonObject
    {
        [SerializeField] protected int version;
        [SerializeField] protected List<string> savePoints;

        #region Public methods

        public int Version { get { return version; } set { version = value; } }
        public List<string> SavePoints { get { return savePoints; } set { savePoints = value; } }
        
        #endregion
    }

    [System.Serializable]
    public class SaveObject
    {
        [SerializeField] protected string flowchartName;
        [SerializeField] protected List<StringVar> stringVars = new List<StringVar>();
        [SerializeField] protected List<IntVar> intVars = new List<IntVar>();
        [SerializeField] protected List<FloatVar> floatVars = new List<FloatVar>();
        [SerializeField] protected List<BoolVar> boolVars = new List<BoolVar>();

        #region Public methods

        /// <summary>
        /// Gets or sets the name of the encoded Flowchart.
        /// </summary>
        public string FlowchartName { get { return flowchartName; } set { flowchartName = value; } }

        /// <summary>
        /// Gets or sets the list of encoded string variables.
        /// </summary>
        public List<StringVar> StringVars { get { return stringVars; } set { stringVars = value; } }

        /// <summary>
        /// Gets or sets the list of encoded integer variables.
        /// </summary>
        public List<IntVar> IntVars { get { return intVars; } set { intVars = value; } }

        /// <summary>
        /// Gets or sets the list of encoded float variables.
        /// </summary>
        public List<FloatVar> FloatVars { get { return floatVars; } set { floatVars = value; } }

        /// <summary>
        /// Gets or sets the list of encoded boolean variables.
        /// </summary>
        public List<BoolVar> BoolVars { get { return boolVars; } set { boolVars = value; } }
       
        #endregion
    }


    /// <summary>
    /// Serializable container for an string variable.
    /// </summary>
    [System.Serializable]
    public class StringVar
    {
        [SerializeField] protected string key;
        [SerializeField] protected string value;

        #region Public methods

        public string Key { get { return key; } set { key = value; } }
        public string Value { get { return value; } set { this.value = value; } }

        #endregion
    }

    /// <summary>
    /// Serializable container for an integer variable.
    /// </summary>
    [System.Serializable]
    public class IntVar
    {
        [SerializeField] protected string key;
        [SerializeField] protected int value;

        #region Public methods

        public string Key { get { return key; } set { key = value; } }
        public int Value { get { return value; } set { this.value = value; } }

        #endregion
    }

    /// <summary>
    /// Serializable container for a float variable.
    /// </summary>
    [System.Serializable]
    public class FloatVar
    {
        [SerializeField] protected string key;
        [SerializeField] protected float value;

        #region Public methods

        public string Key { get { return key; } set { key = value; } }
        public float Value { get { return value; } set { this.value = value; } }

        #endregion
    }

    /// <summary>
    /// Serializable container for a boolean variable.
    /// </summary>
    [System.Serializable]
    public class BoolVar
    {
        [SerializeField] protected string key;
        [SerializeField] protected bool value;

        #region Public methods

        public string Key { get { return key; } set { key = value; } }
        public bool Value { get { return value; } set { this.value = value; } }

        #endregion
    }

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

        #endregion
    }

    /// <summary>
    /// A container for a single unity of saved data.
    /// The data and its associated type are stored as string properties.
    /// The data would typically be a JSON string representing a saved object.
    /// </summary>
    [System.Serializable]
    public class SaveDataItem
    {
        [SerializeField] protected string dataType = "";
        [SerializeField] protected string data = "";

        #region Public methods

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        public virtual string DataType { get { return dataType; } }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public virtual string Data { get { return data; } }

        /// <summary>
        /// Factory method to create a new SaveDataItem.
        /// </summary>
        public static SaveDataItem Create(string dataType, string data)
        {
            var item = new SaveDataItem();
            item.dataType = dataType;
            item.data = data;

            return item;
        }

        #endregion
    }
}
