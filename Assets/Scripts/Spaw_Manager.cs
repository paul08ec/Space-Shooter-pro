using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

   
    public void StartSpawning()
    {
        StartCoroutine(SpawEnemyRutine());
        StartCoroutine(SpawPowerUpRutine());

    }

    void Update()
    {
        
    }
   
    IEnumerator SpawEnemyRutine()
    {
        yield return new WaitForSeconds(2.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8, 0);            
            GameObject _newEnemy =   Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
     
    }

    IEnumerator SpawPowerUpRutine()
    {
        yield return new WaitForSeconds(2.0f);
       
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8, 0);
            int randomPowerups = Random.Range(0, 3);
            GameObject _newSpeedPowerUp = Instantiate(_powerUps[randomPowerups], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 10));


        }



    }
    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
    }
}
