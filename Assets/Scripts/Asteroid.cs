using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 5;
    [SerializeField]
    private GameObject _explotionPrefab;
    private Spaw_Manager _spawnManager;
    

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spaw_Manager>();
       
    }

    void Update()
    {
       
        transform.Rotate(0, 0, -9 * _rotationSpeed * Time.deltaTime);    
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explotionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);  

        }
       
    }

}
