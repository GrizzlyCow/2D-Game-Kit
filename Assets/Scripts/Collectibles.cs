using UnityEngine;

public class Collectibles : MonoBehaviour
{

    enum ItemType { Coin, Health, Ammo, InventoryItem }
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;

    void Start()
    {

    }


    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += 1;
            }
            else if (itemType == ItemType.Health)
            {
                if (NewPlayer.Instance.health < 100)
                {
                    NewPlayer.Instance.health += 1;
                }
            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if (itemType == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            else
            {

            }

            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }

}
