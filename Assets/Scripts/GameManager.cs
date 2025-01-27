using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float mapTimer = 0f;
    public float mapInterval;
    public float fogInterval;
    public float fogbefore;
    public float fogTimer;
    public GameObject Fog;
    //cnight0, 1csunset, 2deepdusk, 3ebluesky, 4epglor, 5nighmoon,6 overcast, 7space, 8stars;
    public Material[] skyboxMaterials;
    private List<string> PresetList = new List<string>();
    private int usingmap = 4;
    private int lastmap = 4;
    private int lastlastmap = 4;
    private int lastlastlastmap = 4;
    //private Suimono.Core.SuimonoModule moduleObject;
    private Suimono.Core.SuimonoObject oceanObject;
    Pause pause;
    void Awake()
    {
        PresetList.Add("New Preset 17");
        PresetList.Add("New Preset 15");
        PresetList.Add("New Preset 23");
        PresetList.Add("New Preset 22");
        PresetList.Add("New Preset 21");
        PresetList.Add("New Preset 14");
        PresetList.Add("New Preset 16");
        PresetList.Add("Lava Flow");
        PresetList.Add("New Preset 19");
        PlayerPrefs.SetInt("Health", 5);
        PlayerPrefs.SetInt("Ammo", 31);
        PlayerPrefs.SetInt("Boost", 0);
        PlayerPrefs.SetInt("ActiveBoost", 0);
        if (GameObject.Find("SUIMONO_Surface_Ocean") != null)
        {
            oceanObject = GameObject.Find("SUIMONO_Surface_Ocean").gameObject.GetComponent<Suimono.Core.SuimonoObject>() as Suimono.Core.SuimonoObject;
            pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        }
    }
    void LateUpdate()
    {
        if (!pause.pause)
        {
            if (mapTimer >= mapInterval)
            {
                ChangeMap();
                mapTimer = 0f;
            }
            mapTimer += Time.deltaTime;
            if (mapTimer + fogbefore >= mapInterval)
            {
                Fog.SetActive(true);
            }
            if (Fog.activeSelf)
            {
                fogTimer += Time.deltaTime;
            }
            if (fogTimer >= fogInterval)
            {
                Fog.SetActive(false);
                fogTimer = 0f;
            }
        }
    }
    public void ChangeMap()
    {
        int newMap;
        do
        {
            newMap = Random.Range(0, 9);
        } while (newMap == usingmap || newMap == lastmap || newMap == lastlastmap || newMap == lastlastlastmap);

        lastlastlastmap = lastlastmap;
        lastlastmap = lastmap;
        lastmap = usingmap;
        PlayerPrefs.SetFloat("RainIntesity", Random.Range(0.0f,1.0f));

        RenderSettings.skybox = skyboxMaterials[newMap];
        oceanObject.SuimonoSetPreset("Built-In Presets", PresetList[newMap]);
        usingmap = newMap;
        DynamicGI.UpdateEnvironment();
    }
}
