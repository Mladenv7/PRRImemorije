using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
 


    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject cardBack;
    private int _id;

    public int Id
    {
        get { return _id;  }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseDown()
    {
        if (cardBack.activeSelf && sceneController.canReveal)
        {
            cardBack.SetActive(false);
            sceneController.FlippedCard(this);
        }
    }


    public void FlipDown()
    {
        cardBack.SetActive(true);
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
