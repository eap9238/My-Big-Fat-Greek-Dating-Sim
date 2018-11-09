using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CorrectSave : MonoBehaviour {
    //private GameObject save;
    private Fungus.SaveMenu saveMenu;

    //public string key;

	// Use this for initialization
	void Start () {
        //save = GameObject.Find("/DontDestroyOnLoad/SaveMenu");
        saveMenu = gameObject.GetComponent<SaveMenu>();
    }

    public void SetSaveFile (string key)
    {
        //Debug.Log("Previous Save File " + saveMenu.saveDataKey);
        saveMenu.saveDataKey = key;
        //Debug.Log("Loaded Save File " + key);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
