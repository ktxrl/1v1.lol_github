using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class TullyMonster67 : MonoBehaviour
{
    [SerializeField] Flying flying;
    [SerializeField] Skeleton skeleton;
    [SerializeField] Lizard lizard;
    [SerializeField] List<GameObject> platforms;
    [SerializeField] Camera cameraP2;
    public AudioSource Music;
    public static GameObject original;
    bool controlling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Awake()
    {
        // Check if instance already exists
        if (original == null)
        {
            original = gameObject;
        }
        if (gameObject != original)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void DeselectAll()
    {
        if (skeleton != null) skeleton.GetComponent<Skeleton>().Deselect();
        if (flying != null) flying.GetComponent<Flying>().Deselect();
        if (lizard != null) lizard.GetComponent<Lizard>().Deselect();
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i] != null) platforms[i].GetComponent<Platform>().Deselect();
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        Music.volume = volume;
    }
    public void ChangeSEVolume(float volume)
    {
        Music.volume = volume;
    }
    public void ChangeMainVolume(float volume)
    {
        AudioListener.volume = volume;

    }
}
