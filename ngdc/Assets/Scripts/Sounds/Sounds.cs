using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds instance;
    public Dictionary<string, List<AudioClip>> audioDict = new Dictionary<string, List<AudioClip>>();
    public List<AudioClip> BGM = new List<AudioClip>();
    public List<AudioClip> Corridor = new List<AudioClip>();
    public List<AudioClip> Player = new List<AudioClip>();
    public List<AudioClip> Cutscenes = new List<AudioClip>();
    public List<AudioClip> SFX = new List<AudioClip>();
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }        
        audioDict.Add("Corridor", Corridor);
        audioDict.Add("BGM", BGM);
        audioDict.Add("Player", Player);
        audioDict.Add("SFX", SFX);
        audioDict.Add("Cutscenes", Cutscenes);
    }
}
