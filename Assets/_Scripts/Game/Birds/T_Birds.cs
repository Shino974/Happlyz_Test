using System;
using UnityEngine;

public class T_Birds : MonoBehaviour
{
    // Public Variables
    public float launchForce;
    public Rigidbody2D rb;
    
    // SCore reference
    private T_ScoreHandler _scoreHandler;
    
    // Sounds
    [SerializeField] private AudioClip launchSound;
    [SerializeField] private AudioClip collisionSound;
    private AudioSource _audioSource;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _scoreHandler = FindObjectOfType<T_ScoreHandler>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.1f;
        ApplyAudioSettings();
    }

    // Launch the bird
    public virtual void Launch(Vector2 vl)
    {
        if (launchSound != null)
            _audioSource.PlayOneShot(launchSound);

        rb.velocity = vl;
    }

    private void Update()
    {
        ApplyAudioSettings();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Props"))
        {
            _scoreHandler.IncrementScore();
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject, 2f);

            if (collisionSound != null)
                _audioSource.PlayOneShot(collisionSound);
        }
    }
    
    // Apply audio settings
    private void ApplyAudioSettings()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        _audioSource.mute = isMuted;
        if (isMuted)
            _audioSource.volume = 0;
        else
            _audioSource.volume = 1;
    }
}