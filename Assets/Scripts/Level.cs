using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int totalBlocks; //Serialized for debug.

    //Cached reference
    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void DestroyBlock()
    {
        totalBlocks--;
        if (totalBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    public void CountBlocks()
    {
        totalBlocks++;
    }
}
