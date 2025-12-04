using UnityEngine;

public class itemIngredient : MonoBehaviour
{


    SpriteRenderer sprite;

    [SerializeField]
    public SpriteRenderer cooking_sprite;
    [SerializeField]
    public string item_Name;
    [SerializeField]
    private int ID;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        sprite = cooking_sprite;
    }


    public string getItemName()
    {
        return item_Name;
    }

    public int getId() {
        return ID;
    }

}

