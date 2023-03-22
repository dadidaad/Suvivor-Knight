using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject chooseCharacterView;
    [HideInInspector]
    public bool isSetup = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetupPlayer(PlayerScriptableObject playerScriptableObject)
    {
        GameObject characterSprite = Instantiate(playerScriptableObject.Character);
        characterSprite.transform.SetParent(player.transform, false);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerAnimator = characterSprite.GetComponent<PlayerAnimator>();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.ChooseCharacter(playerScriptableObject);
        isSetup = true;
    }
}
