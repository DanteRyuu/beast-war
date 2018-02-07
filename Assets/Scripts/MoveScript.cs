using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public float moveSpeedZ;
    public float moveSpeedX;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
       
    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            float facingDirection = characterController.transform.eulerAngles.y;
            Debug.Log("W: " + facingDirection);

            Vector3 direction;
            switch ((int)facingDirection)
            {
                case 0:
                    direction = new Vector3(0, 0, moveSpeedZ);
                    break;
                case 180:
                    direction = new Vector3(0, 0, -moveSpeedZ);
                    break;
                case 270:
                    direction = new Vector3(-moveSpeedX, 0, 0);
                    break;
                default:
                    direction = new Vector3(moveSpeedX, 0, 0);
                    break;
            }

            Debug.Log("dir: " + direction);
            RaycastHit hit;
            if(!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
            else
            {
                Debug.Log("Pos " + characterController.transform.position);
                Debug.Log("Hit " + hit.transform.name);
                Debug.DrawRay(characterController.transform.position, direction, Color.red, 5);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float facingDirection = characterController.transform.eulerAngles.y;
            Debug.Log("S: " + facingDirection);

            Vector3 direction;
            switch ((int)facingDirection)
            {
                case 0:
                    direction = new Vector3(0, 0, -moveSpeedZ);
                    break;
                case 180:
                    direction = new Vector3(0, 0, moveSpeedZ);
                    break;
                case 270:
                    direction = new Vector3(moveSpeedX, 0, 0);
                    break;
                default:
                    direction = new Vector3(-moveSpeedX, 0, 0);
                    break;
            }

            Debug.Log("dir: " + direction);
            RaycastHit hit;
            if (!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
            else
            {
                Debug.Log("Pos " + characterController.transform.position);
                Debug.Log("Hit " + hit.transform.name);
                Debug.DrawRay(characterController.transform.position, direction, Color.red, 5);
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
}

