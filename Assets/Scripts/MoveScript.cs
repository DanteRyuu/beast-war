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
            
            RaycastHit hit;
            if(!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
            else
            {
                if(hit.transform.tag.Equals("enemy"))
                {
                    Debug.Log("enemy in range");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float facingDirection = characterController.transform.eulerAngles.y;

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
            
            RaycastHit hit;
            if (!Physics.Raycast(characterController.transform.position, direction, out hit, Mathf.Abs(direction.x + direction.z)))
            {
                characterController.Move(direction);
            }
            else
            {}
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

