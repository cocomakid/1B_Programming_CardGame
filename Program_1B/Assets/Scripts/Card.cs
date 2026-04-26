using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public TextMeshProUGUI card;
    public int cardNum;
    public float rotateSpeed;

    private Sprite mySpriteImage;
    public Sprite defaultSprite;

    public bool isFront = false;

    public bool isMatched = false;


    public Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
    public Quaternion originRotation = Quaternion.Euler(0, 0, 0);

    public CardGame cardGame;

    void Update()
    {
        if(isFront)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, rotateSpeed * Time.deltaTime);
            GetComponent<Image>().sprite = mySpriteImage;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRotation, rotateSpeed * Time.deltaTime);
            GetComponent<Image>().sprite = defaultSprite;
        }
    }

    public void ClickCard()
    {
        if (!isMatched)
        {
            cardGame.OnClickCard(this);

        }
    }

    public void SetCardNum(int newNum)
    {
        card = GetComponentInChildren<TextMeshProUGUI>();
        cardNum = newNum;

        card.text = cardNum.ToString();
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }

    public void SetImage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
        mySpriteImage = sprite;
    }

    public void Flip(bool isFront)
    {
        this.isFront = isFront;
    }
}
