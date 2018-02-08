using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private int hitPoints;
    private int reward;
    private int damage;
    private bool canAttack;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        hitPoints = 30;
        reward = 5;
        canAttack = false;
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        TextMesh ThreeDtext = GetComponentInChildren<TextMesh>();
        ThreeDtext.text = "HP: " + hitPoints;
    }

    public void ReduceHealth(int damage)
    {
        if (damage <= hitPoints)
            hitPoints -= damage;
        else
        {
            canAttack = false;
            hitPoints = 0;

            if (GetComponentInChildren<Animation>().isPlaying)
                GetComponentInChildren<Animation>().Stop();

            GetComponentInChildren<Animation>().Play("die");

            GameManager.Get().StartCoroutine(Die(GetComponent<Animation>().GetClip("die").length + 2));
        }
    }

    private IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);

        ShowMessagesScript script = GameObject.Find("GUI").GetComponentInChildren<ShowMessagesScript>();
        string message = "You have killed " + transform.name + ".";
        script.SetText(message);

        PlayerResources.CollectCoins(reward);

        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        foreach (BoxCollider bc in GetComponentsInChildren<BoxCollider>()) {
            bc.enabled = false;
        }

        GetComponentInChildren<Animation>().enabled = false;

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.transform;
        canAttack = true;
        GameManager.Get().StartCoroutine(AttackPlayer());
    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(3);
        while (canAttack)
        {
            PlayerResources.ReduceHealth(damage);
            if(PlayerResources.hitPoints == 0)
            {
                canAttack = false;
                SceneManager.LoadScene("MenuScene");
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
    }
}
