using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class TullyMonster67 : MonoBehaviour
{
    [SerializeField] Flying flying;
    [SerializeField] Skeleton skeleton;
    [SerializeField] Lizard lizard;
    [SerializeField] List<GameObject> platforms;
    public AudioSource Music;
    public static GameObject original;

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
        if(gameObject != original) 
           {
                Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //public void FlyingSelect()
    //{

    //    skeleton.GetComponent<Skeleton>().Deselect();
    //    lizard.GetComponent<Lizard>().Deselect();
    //}
    //public void SkeletonSelect()
    //{

    //    flying.GetComponent<Flying>().Deselect();
    //    lizard.GetComponent<Lizard>().Deselect();
    //}
    //public void LizardSelect()
    //{
    //    flying.GetComponent<Flying>().Deselect();
    //    skeleton.GetComponent<Skeleton>().Deselect();
    //}
    //public void PlatformSelect()
    //{
    //    DeselectAll();
    //}
    public void DeselectAll()
    {
        flying.GetComponent<Flying>().Deselect();
        skeleton.GetComponent<Skeleton>().Deselect();
        lizard.GetComponent<Lizard>().Deselect();
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].GetComponent<Platform>().Deselect();
        }
    }
    public void ChangeMusicVolume(float volume)
    {
        Music.volume = volume;
    }
    public void ChangeSFVolume(float volume)
    {
        Music.volume = volume;
    }
    public void ChangeMainVolume(float volume)
    {
        Music.volume = Music.volume * volume;
    }

}
