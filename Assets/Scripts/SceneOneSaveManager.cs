using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneSaveManager : MonoBehaviour
{
    public GameObject monsterOne;

    public GameObject monsterTwo;

    public GameObject monsterThree;

    public GameObject player;

    public GameObject[] doors;

    public GameObject key;

    public int[] SceneOneKeyIndexes;

    private IEnumerator coroutine;

    private void Start()
    {
        SceneOneKeyIndexes = new int[] { 2, 5, 7, 9, 13 };
        coroutine = DelayedStart(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < doors.Length; i++) { doors[i].GetComponentInChildren<InteractableDoor>().doorIndex = i; }
        if (PlayerPrefs.HasKey("SceneOne")) { Load(); }
        else { NewGame(); Load(); } 
    }

    public void Save()
    {
        SceneOneSaveData save = new SceneOneSaveData(BuildDoorArray(),player.GetComponent<CharacterControllerAddition>().keyRing);
        string mySavedCollection = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("SceneOne", mySavedCollection);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("SceneOne"))
        {
            string myLoadedCollection = PlayerPrefs.GetString("SceneOne");
            SceneOneSaveData loadedSave = JsonUtility.FromJson<SceneOneSaveData>(myLoadedCollection);
            player.GetComponent<CharacterControllerAddition>().keyRing = loadedSave.keyRing;
            for (int i = 0; i < doors.Length; i++)
            {
                if (loadedSave.unlocked[i])
                {
                    doors[i].GetComponentInChildren<Lock>().UnlockDoor();
                    doors[i].GetComponentInChildren<Lock>().RemoveBarricade();
                }
                doors[i].GetComponentInChildren<Lock>().RendColor();
            }
            for (int i = 0; i < SceneOneKeyIndexes.Length; i++)
            {
                bool spawn = true;
                for (int j = 0; j < loadedSave.keyRing.Count; j++)
                {
                    if (SceneOneKeyIndexes[i] == loadedSave.keyRing[j]) spawn = false;
                }
                if (spawn) SpawnSceneOneKeys(i);
            }
            for (int i = 0; i < loadedSave.keyRing.Count; i++)
            {
                if (loadedSave.keyRing[i] == 7) monsterOne.GetComponent<LightFreezeAI>().triggered = true;
                if (loadedSave.keyRing[i] == 9) monsterTwo.GetComponent<LightFreezeAI>().triggered = true;
                if (loadedSave.keyRing[i] == 2) monsterThree.GetComponent<LightFreezeAI>().triggered = true;
            }
        }
    }

    public void SpawnSceneOneKeys(int i)
    {
        
        switch (i)
        {
            case 0: SpawnKey(new Vector3(-30f,.32f,105f),2); break;
            case 1: SpawnKey(new Vector3(2f, .32f, 75f),5); break;
            case 2: SpawnKey(new Vector3(-30f, .32f, 75f),7); break;
            case 3: SpawnKey(new Vector3(-25f, .32f, 121f),9); break;
            case 4: SpawnKey(new Vector3(-30f, .32f, 152f),13); break;
        }
    }

    public void SpawnKey(Vector3 spawnPos, int index){
        GameObject tempKey;
        tempKey = Instantiate(key,spawnPos ,Quaternion.identity); 
        tempKey.GetComponent<KeyData>().keyIndex = index; 
        tempKey.GetComponent<KeyData>().RendColor();
    }

    public bool[] BuildDoorArray()
    {
        bool[] UnlockedDoors = new bool[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            UnlockedDoors[i] = doors[i].GetComponentInChildren<InteractableDoor>().Unlocked();
        }
        return UnlockedDoors;
    }

    public void NewGame()
    {
        SceneOneSaveData newGame = new SceneOneSaveData();
        string myNewGame = JsonUtility.ToJson(newGame);
        PlayerPrefs.SetString("SceneOne", myNewGame);
    }
    
}
[System.Serializable]
public class SceneOneSaveData
{
    public bool[] unlocked;

    public List<int> keyRing;

    public SceneOneSaveData()
    {
        unlocked = new bool[] { true, true , false, true, true, false, false, false, true, false, true, false, true, false };
        keyRing = new List<int>();
    }

    public SceneOneSaveData(bool[] doorSave,List<int> keySave)
    {
        unlocked = doorSave;
        keyRing = keySave;
    }
}
