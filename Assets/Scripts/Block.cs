using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //  Cached Reference
    private Level level;
    private GameSession gameStatus;

    // State Variables
    [SerializeField] int timesHit; // Serialized for debug

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        } else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Block Sprite is missing from array [" + gameObject.name + "]");
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.DestroyBlock();
        gameStatus.AddToScore();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}

