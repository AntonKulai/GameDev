using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rocket : MonoBehaviour {



    [Header(" Rocket ")]
    [SerializeField] private GameObject shieldObj;
    [SerializeField] private GameObject shieldFadeOutObj;
    [SerializeField] private ParticleSystem engineParticleSystem;
    
    [Header(" Effect ")]
    [SerializeField] private GameObject ItemEffectPrefab;
    [SerializeField] private GameObject DeadEffectPrefab;
    [SerializeField] private GameObject pfPowerJumpParticle;

    [Header(" Settings ")]
    [SerializeField] private float rotationSpeed = 80;
    [SerializeField] private int angleLimit = 120;
    [SerializeField] private float normalJumpPower = 30f;
    [SerializeField] private float specialJumpPower = 2f;

    [Header(" Actions ")]
    public static Action onRocketJumped;
    public static Action onRocketPowerJumped;
    public static Action onRocketExploded;


    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    private bool isWaitingToStart = true;
    private bool isPowerJumping = false;
    private bool isExploded = false;

    private float angle = 0;



    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
    }


    void Update()
    {
        if (isExploded) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitingToStart)
            {
                GamePlayManager.Instance.GameStart();
                isWaitingToStart = false;
                rb2D.isKinematic = false;
            }

            Jump(angle, normalJumpPower);
        }

        if (!isWaitingToStart && !isPowerJumping)
        {
            RotateRocket();
        }
        
        DecelerateRocket();
        
        Vector3 viewPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        if (viewPos.y < 0)
        {
            Explode();
        }

    }


    private void RotateRocket()
    {
        angle += rotationSpeed * Time.deltaTime;
        
        if (angle > angleLimit)
        {
            angle = angleLimit;
            rotationSpeed *= -1;
        }
        if (angle < -angleLimit)
        {
            angle = -angleLimit;
            rotationSpeed *= -1;
        }
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    private void Jump(float angle, float power)
    {
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
        rb2D.velocity = direction * power;

        engineParticleSystem.Play();
        
        onRocketJumped?.Invoke();
    }


    private void DecelerateRocket()
    {
        float decelerateValue = 5;
        
        if (rb2D.velocity.magnitude > 0.03f)
            rb2D.velocity -= rb2D.velocity * (Time.smoothDeltaTime * decelerateValue);
        else
            rb2D.velocity = Vector2.zero;
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Explode();
        }
        if (other.gameObject.tag == "Item")
        {
            StartCoroutine(PowerJump());
            
            Destroy(Instantiate(ItemEffectPrefab, transform.position, Quaternion.identity), 1.0f);
            
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    
    void Explode()
    {
        isExploded = true;
        
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb2D.velocity = Vector2.zero;
        rb2D.isKinematic = true;

        Destroy(Instantiate(DeadEffectPrefab, transform.position, Quaternion.identity), 1.5f);
        
        GamePlayManager.Instance.GameOver();
        
        onRocketExploded?.Invoke();
    }


    IEnumerator PowerJump()
    {
        if (isExploded) yield break;
        
        onRocketPowerJumped?.Invoke();

        isPowerJumping = true;
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        angle = 0;
        boxCollider2D.enabled = false;
        
        Jump(0, specialJumpPower);
        
        Instantiate(pfPowerJumpParticle, transform.position, Quaternion.identity);
        
        shieldObj.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);
        
        isPowerJumping = false;
        
        yield return new WaitForSecondsRealtime(1.0f);
        
        boxCollider2D.enabled = true;
        
        shieldObj.SetActive(false);
        shieldFadeOutObj.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        shieldFadeOutObj.SetActive(false);
        
    }
}