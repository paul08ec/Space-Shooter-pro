using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _LaserPrerfab;
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _FireRate = 3.0f;
    private float _CanFire = -1.0f;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
            Debug.LogError("The player is null");
        }
     
        _animator = gameObject.GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("the Animator is null");
        }
        
    }

    
    void Update()
    {
        CalculateMovemnt(); 
        if (Time.time > _CanFire)
        {
            _FireRate = Random.Range(3.0f, 7.0f);
            _CanFire = Time.time + _FireRate;
           GameObject enemyLaser =  Instantiate(_LaserPrerfab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssingEnemyLaser();
            }

        }
        
        
    }
    void CalculateMovemnt()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -7)
        {
            float ramdonX = Random.Range(-8f, 8f);
            transform.position = new Vector3(ramdonX, 8.0f, 0);

        }

    }
    private void OnTriggerEnter2D (Collider2D other)

    { 

        if (other.tag == "Player")
        {
            Player player = other.transform. GetComponent<Player>();
           if(player != null)
           {
                player.Damage();
           }
            
            _animator.SetTrigger("OnEnemyDeath");
            
            _speed = 0.5f;
            _audioSource.Play();
            
            Destroy(this.gameObject, 2.6f);
        }
        
       
        if(other.tag == "laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);

            }
            
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 1f;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }

    }



}
