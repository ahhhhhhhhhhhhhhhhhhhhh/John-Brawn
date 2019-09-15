using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if level scene is started without coming from the level select screen, this script loads a default level setup for bug testing, etc

public class DefaultLevelLoader : MonoBehaviour {

    public LevelInfo defaultLevel;

    public LevelControl levelControl;

	private void Start ()
    {
        if (levelControl.loadedLevel.backgroundMap == null)
        {
            levelControl.GetComponent<LevelControl>().loadLevel(defaultLevel);
        }
	}
	
}
