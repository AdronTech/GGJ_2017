using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Building_Flag : AbstractBaseBuilding {

    public string nextSceneName;

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = false;
        bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    public new void DestroyBuilding()
    {
        // Todo GameOver
        Destroy(gameObject);
    }
}
