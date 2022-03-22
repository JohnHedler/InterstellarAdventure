using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI livesText;
    public GameObject weaponImg;
    public Texture weaponOne;
    public Texture weaponTwo;
    private RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIStageLives();
        UIWeaponSelected();
    }

    //Display current stage and available lives
    void UIStageLives()
    {
        IAGameManager gameManager = GameObject.Find("Game Manager").GetComponent<IAGameManager>();
        stageText.GetComponent<TextMeshProUGUI>().text = $"Stage: {gameManager.stageNumber}";
        livesText.GetComponent<TextMeshProUGUI>().text = $"Lives: {gameManager.playerLives}";
    }

    //Display currently selected weapon
    void UIWeaponSelected()
    {
        image = weaponImg.GetComponent<RawImage>();
        GameObject player = GameObject.Find("Player");

        if(player.GetComponent<PlayerController>().weaponType == 1)
        {
            image.texture = weaponOne;
        }
        else
        {
            image.texture = weaponTwo;
        }
        
    }
}
