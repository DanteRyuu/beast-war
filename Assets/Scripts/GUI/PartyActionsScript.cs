using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyActionsScript : MonoBehaviour {

    public Button attackButton;
    public Button defendButton;
    public Button tameButton;
    public Button clawButton;
    private PlayerScript playerScript;

    // Use this for initialization
    void Start () {
        attackButton.onClick.AddListener(onAttack);
        defendButton.onClick.AddListener(onDefend);
        clawButton.onClick.AddListener(onClaw);
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
	
	void onAttack()
    {
        SoundManager.PlaySFX(GetComponentInChildren<AudioSource>());
        if(GameManager.canAttack)
            playerScript.attack();
    }

    void onDefend()
    {

    }

    void onClaw()
    {

    }
}
