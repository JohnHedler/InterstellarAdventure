using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
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
        ShotType();        
    }

    private void ShotType()
    {
        GameObject player = GameObject.Find("Player");

        if (player.GetComponent<PlayerController>().weaponType == 1)
        {
            //move object up
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.z > (startPos + shotDistance))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //move object up
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.z > (startPos + shotDistance))
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
