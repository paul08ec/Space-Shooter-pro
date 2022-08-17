using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_Up : MonoBehaviour
{
    private GameObject _powerUp;
    [SerializeField]
    private int _speed = 3;
    
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _powerUpSound;
     
   
    void Start()
    {
       
        
    }

   
    void Update()
    {
      
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
      
        if(transform.position.y <= -8 )
        {
            Destroy(this.gameObject);

        }
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
           
            switch(_powerUpID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedBoostActive();
                    break;
                case 2:
                    player.ShieldsActive();
              
                    break;
                default:
                    break;

            }
        }

        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint( _powerUpSound, transform.position);
            Destroy(this.gameObject);
        }
    }
    
}
