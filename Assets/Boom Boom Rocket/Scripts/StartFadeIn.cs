using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFadeIn : MonoBehaviour
{

    void Start()
    {
        // white fade-in effect when the game starts
        StartCoroutine(DeactiveFadeInPanel());
    }

    IEnumerator DeactiveFadeInPanel()
    {
        yield return new WaitForSeconds(1f);
        // deactive gameobject after fade-in effect is over 
		gameObject.SetActive(false);

    }
}
