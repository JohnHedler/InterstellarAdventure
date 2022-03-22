using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int speed = 5;
    private int attackDelay = 2;
    public GameObject enemyShotPrefab;
    private bool weaponFired = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Attack();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        if (transform.position.z < -16)
        {
            Destroy(gameObject);
        }
    }

    //Check for trigger collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().wasHit = true;
        }
    }

    void Attack()
    {
        if (!weaponFired)
        {
            weaponFired = true;
            Instantiate(enemyShotPrefab, transform.position, enemyShotPrefab.transform.rotation);
            StartCoroutine(WeaponCooldown());
        }
    }

    IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        weaponFired = false;
    }
}
