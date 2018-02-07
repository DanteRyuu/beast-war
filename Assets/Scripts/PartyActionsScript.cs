using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyActionsScript : MonoBehaviour {

    public Button attackButton;
    public Button defendButton;
    public Button tameButton;
    public Button clawButton;

    // Use this for initialization
    void Start () {
        attackButton.onClick.AddListener(onAttack);
        defendButton.onClick.AddListener(onDefend);
        clawButton.onClick.AddListener(onClaw);
    }
	
	void onAttack()
    {

    }

    void onDefend()
    {

    }

    void onClaw()
    {

    }
}
