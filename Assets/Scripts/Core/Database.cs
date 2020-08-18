﻿#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour {
    
    [SerializeField] private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] private SoundSystem.SoundGroup[] _soundGroups;
    [SerializeField] private Dictionary<string, Entity> entityPrefabs = new Dictionary<string, Entity>();
    public SoundSystem.SoundGroup[] SoundGroups {
        get {
            return _soundGroups;
        }
        private set {
            _soundGroups = value;
        }
    }


    private static Database _instance;
    public static Database Singleton {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(Database)) as Database;
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }

    void Awake() {
        /* TODO : !!! This call should be moved somewhere else !!! */
        SoundSystem.Awake();
        AutomaticAssignerScript.AssignObjects();
    }

    public void AssingAudioClips(Object[] objs) {
        audioClips = new Dictionary<string, AudioClip>();
        int counter = 0;
        for (int i = 0; i < objs.Length; i++)  {
            audioClips.Add(objs[i].name, (objs[i] as AudioClip));
            counter++;
        }
        //Debug.Log("Succesfully assigned: " + counter + " audio files.");
    }

    public void AssignEntityPrefabs(Object[] objs) {
        entityPrefabs = new Dictionary<string, Entity>();
        int counter = 0;
        for (int i = 0; i < objs.Length; i++) {
            entityPrefabs.Add((objs[i] as Entity).entityName, (objs[i] as Entity));
            counter++;
        }
        //Debug.Log("Succesfully assigned: " + counter + " entities.");
    }

    public AudioClip GetAudioClip(string soundName) {
        AudioClip clip;
        audioClips.TryGetValue(soundName, out clip);
        return clip;
    }

    public Entity GetEntityPrefab(string entityName) {
        entityPrefabs.TryGetValue(entityName, out Entity entity);
        return entity;
    }
}
