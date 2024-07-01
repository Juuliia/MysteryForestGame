using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroying = 1f;
    [SerializeField] float timeBeforeIsHit = 0.5f;
    [SerializeField] float damageInterval = 1f;
    [SerializeField] bool isPlayer;
    [SerializeField] public int health = 50;
    [Header("SoundEffect")]
    [SerializeField] AudioClip soundEffectPlayer;
    [SerializeField] [Range(0f, 1f)] float soundVolumePlayer = 1f;
    [SerializeField] AudioClip soundEffectClipEnd;
    [SerializeField] [Range(0f, 1f)] float soundVolumeEnd = 1f;
    [SerializeField] GameObject playAgainButton; // Reference to the Play Again button

    Animator myAnimator;
    EnemyMovement enemyMovement;
    ChasePlayer chasePlayer;
    PlayerMovement playerMovement;
    bool isBeingHit = false;
    Coroutine damageCoroutine;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerMovement = GetComponent<PlayerMovement>();
        chasePlayer = GetComponent<ChasePlayer>();

        LoadHealth();

       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Make loop to keep on taking damage until dead
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null || isBeingHit)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (!isBeingHit)
                {
                    isBeingHit = true;
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(damageDealer));
                }
            }
            else
            {   if(other.gameObject.tag == "Bullet"){
                TakeDamage(damageDealer.GetDamage());
                Die();}
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            isBeingHit = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator ApplyDamageOverTime(DamageDealer damageDealer)
    {
        while (health > 0)
        {
            TakeDamage(damageDealer.GetDamage());
            if (health <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    void TakeDamage(int damage)
    {
        isBeingHit = true;
        health -= damage;
        SaveHealth();
        if (soundEffectPlayer != null)
        {
            AudioSource.PlayClipAtPoint(soundEffectPlayer, Camera.main.transform.position, soundVolumePlayer);
        }
        StartCoroutine(HitAnimationWait());
        Die();
    }

    IEnumerator HitAnimationWait()
    {
        yield return new WaitForSeconds(timeBeforeIsHit);
        myAnimator.SetTrigger("isHit");
        
    }

    void Die()
    {
        if (health <= 0)
        {
            myAnimator.SetBool("isDead", true);
            StartCoroutine(WaitToDestroy());
        }
    }

    IEnumerator WaitToDestroy()
    {
        if (isPlayer)
        {
            playerMovement.isAlive = false;
            if (soundEffectClipEnd != null)
            {
                AudioSource.PlayClipAtPoint(soundEffectClipEnd, Camera.main.transform.position, soundVolumeEnd);
            }
            yield return new WaitForSeconds(timeBeforeDestroying);
            myAnimator.speed = 0f;
            Time.timeScale = 0;

            SceneManager.LoadScene("GameOver");
        }
        else if (!isPlayer)
        {
            yield return new WaitForSeconds(timeBeforeDestroying);
            myAnimator.speed = 0f;
            enemyMovement.isDead = true;
            enemyMovement.IsDeadStopMoving();
            chasePlayer.enabled = false;
        }
    }

    public void RespawnPlayer()
    {
        if (isPlayer)
        {

           // Vector2 startPosition = LevelManager.instance.GetStartPosition();
            // Reset health and position
            ResetHealth();
           // transform.position = startPosition;
            SceneManager.LoadScene("Level1");

            // myAnimator.SetBool("isDead", false);
            // myAnimator.speed = 1f;
            // playerMovement.isAlive = true;
            // Time.timeScale = 1;
            
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void SaveHealth()
    {
        if (isPlayer)
        {
            PlayerPrefs.SetInt("PlayerHealth", health);
            PlayerPrefs.Save();
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        SaveHealth();
        Debug.Log("Health added. Current health: " + health);
    }

    public void LoadHealth()
    {
        if (isPlayer && PlayerPrefs.HasKey("PlayerHealth"))
        {
            health = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            health = 50;
        }
    }

    public void ResetHealth()
    {
        health = 50;
        SaveHealth();
    }
}
