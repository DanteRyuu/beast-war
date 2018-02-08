using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float moveSpeedZ;
    public float moveSpeedX;

    public int hitPoints;
    public int minDamage;
    public int maxDamage;

    private CharacterController characterController;
    private static AsyncOperation loadingInfo;
    public delegate void LoadChange(float value);
    public static event LoadChange OnLoadChange;

    private bool canSave = false;
    private bool canTeleport = false;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        GameData.OnDataInit(SetLocation);
    }
	
    public void SetLocation()
    {
        Debug.Log("setting Location");
        transform.position = new Vector3(GameData.Instance.locationX, transform.position.y, GameData.Instance.locationZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canSave)
            {
                ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
                string message = "Saving...";
                script.SetText(message);

                GameData.Instance.locationX = transform.position.x;
                GameData.Instance.locationZ = transform.position.z;
                GameData.SaveData();
            }
            else
            {
                ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
                string message = "Cannot save unless you're next to a checkpoint";
                script.SetText(message);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (canTeleport)
            {
                LoadSceneAsync(2);
            }
            else
            {
                ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
                string message = "Cannot load scene unless you're next to the exit door";
                script.SetText(message);
            }
        }
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
        if(other.transform.tag.Equals("checkpoint"))
        {
            canSave = true;
            ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
            string message = "Press E to save the game";
            script.SetText(message);
        }
        if(other.transform.tag.Equals("exitDoor"))
        {
            canTeleport = true;
            ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
            string message = "Press L to load the next scene";
            script.SetText(message);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("enemy"))
        {
            GameManager.canAttack = false;
        }
        if (other.transform.tag.Equals("checkpoint"))
        {
            canSave = false;
            ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
            string message = "Checkpoint range left";
            script.SetText(message);
        }
        if (other.transform.tag.Equals("exitDoor"))
        {
            canTeleport = false;
            ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
            string message = "Exit door range left";
            script.SetText(message);
        }
    }

    public static IEnumerator UpdateScene()
    {
        while (!loadingInfo.isDone)
        {
            Debug.Log(loadingInfo.progress);
            OnLoadChange(loadingInfo.progress);
            yield return new WaitForEndOfFrame();
        }

        OnLoadChange(loadingInfo.progress);
    }

    public static void LoadSceneAsync(int sceneIndex)
    {
        loadingInfo = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        GameManager.Get().StartCoroutine(UpdateScene());
    }
}
