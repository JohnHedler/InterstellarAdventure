using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    private float speed = 20;
    private float startPos;
    private float shotDistance = 20;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //move object up
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        if(transform.position.z < (startPos - shotDistance))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().wasHit = true;
            Destroy(gameObject);
        }
    }
}
