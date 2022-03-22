using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int weaponType = 1;
    private float weaponCooldown = 0.5f;
    private float speed = 10.0f;
    private float zBound = 7.5f;
    private float xBound = 17.0f;
    private Vector3 startPos;
    private Rigidbody playerRb;
    public GameObject shotPrefab;
    public GameObject shot2Prefab;

    private bool weaponFired = false;
    public bool wasHit = false;
    private bool weaponSwapped = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasHit)
        {
            PlayerDie();
        }

        MovePlayer();
        ConstrainPlayerMovement();
        SwapWeapons();
        FireWeapons();
    }

    //Moves player with arrow / AWSD keys
    void MovePlayer()
    {
        //assign input controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //move ship around
        playerRb.transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        playerRb.transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }

    //Check for trigger collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            wasHit = true;
        }else if (other.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("PowerUp obtained.");
            Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("1Up"))
        {
            Debug.Log("1Up obtained.");

            IAGameManager gameManager = GameObject.Find("Game Manager").GetComponent<IAGameManager>();
            gameManager.playerLives++;

            Destroy(other.gameObject);
        }
    }

    //Prevents player from moving outside game boundaries
    void ConstrainPlayerMovement()
    {
        //prevent movement beyond top/bottom edges
        if (playerRb.transform.position.z < -zBound)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, -zBound);
        }
        else if (playerRb.transform.position.z > zBound)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, zBound);
        }

        //prevent movement beyond left/right edges
        if (playerRb.transform.position.x < -xBound)
        {
            playerRb.transform.position = new Vector3(-xBound, playerRb.transform.position.y, playerRb.transform.position.z);
        }
        else if (playerRb.transform.position.x > xBound)
        {
            playerRb.transform.position = new Vector3(xBound, playerRb.transform.position.y, playerRb.transform.position.z);
        }
    }

    //Allows player to shoot weapons
    void FireWeapons()
    {
        //Fire weapon
        if (Input.GetKey(KeyCode.Space))
        {
            if (!weaponFired)
            {
                weaponFired = true;

                if(weaponType == 1)
                {
                    Instantiate(shotPrefab, transform.position, shotPrefab.transform.rotation);
                }
                else
                {
                    Instantiate(shot2Prefab, transform.position - new Vector3(0.3f, 0, 0), shot2Prefab.transform.rotation);
                }

                StartCoroutine(WeaponCooldown());
            }
            
        }
    }

    //Swap weapons
    void SwapWeapons()
    {
        //Fire weapon
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (!weaponSwapped)
            {
                weaponSwapped = true;

                if (weaponType == 1)
                {
                    weaponType = 2;
                    Debug.Log("Weapon 2 Selected");
                    StartCoroutine(WeaponSwapCooldown());
                }
                else
                {
                    weaponType = 1;
                    Debug.Log("Weapon 1 Selected");
                    StartCoroutine(WeaponSwapCooldown());
                }
            }
        }
    }

    //Player death function
    void PlayerDie()
    {
        IAGameManager gameManager = GameObject.Find("Game Manager").GetComponent<IAGameManager>();
        gameManager.playerLives--;
        weaponType = 1;
        transform.position = startPos;
        wasHit = false;

    }

    //Coroutine to delay weapon fire
    IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(weaponCooldown);
        weaponFired = false;
    }

    IEnumerator WeaponSwapCooldown()
    {
        yield return new WaitForSeconds(weaponCooldown);
        weaponSwapped = false;
    }
}
