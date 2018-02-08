﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveSpeedZ;
    public float moveSpeedX;

    public int hitPoints;
    public int minDamage;
    public int maxDamage;

    private CharacterController characterController;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 GetDirection(float facingDirection, float forward)
    {
        Vector3 direction;
        switch ((int)facingDirection)
        {
            case 0:
                direction = new Vector3(0, 0, forward * moveSpeedZ);
                break;
            case 180:
                direction = new Vector3(0, 0, forward * -moveSpeedZ);
                break;
            case 270:
                direction = new Vector3(forward * -moveSpeedX, 0, 0);
                break;
            default:
                direction = new Vector3(forward * moveSpeedX, 0, 0);
                break;
        }

        return direction;
    }

    public void attack()
    {
        float facingDirection = characterController.transform.eulerAngles.y;

        Vector3 direction = GetDirection(facingDirection, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Max(moveSpeedX, moveSpeedZ)))
        {
            if(hit.transform.tag.Equals("enemy"))
            {
                int damage = Random.Range(minDamage, maxDamage);

                ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
                string message = "You hit " + hit.transform.name + " for " + damage.ToString() + " damage.";
                script.SetText(message);

                EnemyScript enemyScript = hit.transform.GetComponent<EnemyScript>();
                enemyScript.ReduceHealth(damage);

                ParticleSystem bloodEffect = GetComponentInChildren<ParticleSystem>();
                if (!bloodEffect.isPlaying)
                    bloodEffect.Play();
            }
        }
    }

    void FixedUpdate()
    {
        //move forward/back
        if (Input.GetKeyDown(KeyCode.W))
        {
            float facingDirection = characterController.transform.eulerAngles.y;

            Vector3 direction = GetDirection(facingDirection, 1.0f);

            RaycastHit hit;
            if (!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float facingDirection = characterController.transform.eulerAngles.y;

            Vector3 direction = GetDirection(facingDirection, -1.0f);

            RaycastHit hit;
            if (!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
        }

        //camera rotation on Y axis
        if (Input.GetKeyDown(KeyCode.D))
        {
            characterController.transform.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            characterController.transform.Rotate(0, -90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("enemy"))
        {
            GameManager.canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("enemy"))
        {
            GameManager.canAttack = false;
        }
    }
}
