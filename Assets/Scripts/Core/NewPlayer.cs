using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float attackDuration; //How long is the attack box active when attacking?
    public int attackPower = 25;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float maxSpeed = 1f;




    [Header("Inventory")]
    public int ammo;
    public int coinsCollected;
    public int health = 100;
    private int maxHealth = 100;



    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attackBox;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); //Dictionary storing all item strings and values
    private Vector2 healthBarOrigSize;
    public Sprite inventoryItemBlank; //The default inventory item slot sprite
    public Sprite keySprite; //The key inventory item
    public Sprite keyGemSprite; //The gem key inventory item





    //Singlton instantation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindAnyObjectByType<NewPlayer>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.name = "New Player";

        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();

        transform.position = GameObject.Find("Spawn Location").transform.position;
    }


    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        //flip the players local scale.x if the move speed is greater than .01 or less than -.01
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //if we press "Fire1", set the attackBox to active, otherwise set to false
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack()); 
        }

        //Check is player health <= 0
        if (health <= 0)
        {
            Die();
        }

        //Set each animator float, bool, and trigger so it knows which animation to play
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    //UpdateUI
    public void UpdateUI()
    {
        //If the healthBarOrigSize has not been set yet, match it to the healthBar rectTransform size!
        if (healthBarOrigSize == Vector2.zero) healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        GameManager.Instance.coinsText.text = coinsCollected.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);

    }

    //Die
    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }


    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }


}
