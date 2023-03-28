using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
     public ParticleSystem deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            MovePlayer.jumps+= 1;
            Destroy(gameObject);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
    }
}
