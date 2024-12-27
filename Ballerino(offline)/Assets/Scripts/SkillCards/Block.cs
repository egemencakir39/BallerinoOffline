using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlashStart(float time)
    {
        StartCoroutine(SelfDestroyTimer(time));
        StartCoroutine(FalshEffect());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Destroy(gameObject);
        }
    }


    IEnumerator SelfDestroyTimer(float time)
    {

        yield return new WaitForSeconds(time);
        Debug.Log("destroy");
        Destroy(gameObject);
        
    }
    
    IEnumerator FalshEffect()
    {
        yield return new WaitForSeconds(0);

        for (int i = 0; i < 12; i++)
        {
            spriteRenderer.DOFade(0, 0f);
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.DOFade(1, 0f);
            yield return new WaitForSeconds(0.5f);

        }
    }
}
