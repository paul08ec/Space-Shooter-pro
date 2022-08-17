using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour


{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _Laser;
    [SerializeField]
    private GameObject _tripleShoot;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    private Spaw_Manager _spawManager;
    [SerializeField]
    private bool _tripleShootActive = false;
    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField]
    private int _score;
    private UI_Manager _uiManeger;
    [SerializeField]
    private AudioClip _laserAudioClip;
    private AudioSource _audioSource;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    void Start()
    {
       
        transform.position = new Vector3(0, -3.5f, 0);
        _spawManager = GameObject.Find("Spawn_Manager").GetComponent<Spaw_Manager>();
        _uiManeger = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }
        if(_uiManeger == null)
        {
            Debug.LogError("The UI manager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourse en the Player is null");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
        }


    }

  
    void Update()
    {

        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();


        }
       

    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
      
        if (transform.position.y >= 2.5f)
        {
            transform.position = new Vector3(transform.position.x, 2.5f, 0);
        }
        else if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }
       
        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }

        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }


    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_tripleShootActive == true)
        {
            Instantiate(_tripleShoot, transform.position, Quaternion.identity);
        }
       
        else
        {
            Instantiate(_Laser, transform.position + new Vector3(0, 1.07f, 0), Quaternion.identity);

        }
       
        _audioSource.Play();


    }
    public void Damage()
    {
      
        if(_shieldActive == true)
        {
            _shieldActive = false;
            _shieldVisualizer.SetActive(false);

            return;
        }
        _lives--;
        
        if(_lives == 2)
        {
            _rightEngine.SetActive(true);

        }
        
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
           
        }
        _uiManeger.UpdateLives(_lives);
        
        if (_lives < 1)
        {
            
            _spawManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

    }
    public void TripleShotActive()
    {
        _tripleShootActive = true;
                //start the power dow coroutine for tripleShot
                StartCoroutine(TripleShotPowerDowRoutine());
    }
        IEnumerator TripleShotPowerDowRoutine()
        {
        yield return new WaitForSeconds(5.0f);
        _tripleShootActive = false;

        }
    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _speed /= _speedMultiplier;


    }
    public void ShieldsActive()
    {
       
        _shieldActive = true;
        _shieldVisualizer.SetActive(true);

    }
   
    public void AddScore(int points)
    {
        _score += points;
        _uiManeger.UpdateScore(_score);

    }

}
