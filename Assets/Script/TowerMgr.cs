using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMgr : MonoBehaviour
{
    public Slider healthBar;

    public int Health, damage;
    public float invertedForce;

    public AudioSource _audioSource;
    public AudioClip damaged;

    public GameMgr m_gameMgr;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Health -= damage;
            Vector3 tempVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
            if(tempVelocity.x < 0)
                tempVelocity.x = invertedForce;
            else
                tempVelocity.x = -invertedForce;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-tempVelocity);
            
            _audioSource.clip = damaged;
            _audioSource.Play();

            healthBar.value = Health;

            if (Health <= 0)
                m_gameMgr.GameOver();

            m_gameMgr.updateUI();
        }
    }
}
