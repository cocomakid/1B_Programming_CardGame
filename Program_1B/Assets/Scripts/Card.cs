using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI cardText;
    public int cardNum;
    public float rotateSpeed = 10f; 

    private Sprite spriteImage;   
    public Sprite defaultSprite;   

    public bool isFront = false;
    public bool isMatched = false;

    public Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
    public Quaternion originRotation = Quaternion.Euler(0, 0, 0);

    private CardGame cardGame;

    void Start()
    {
        cardGame = FindFirstObjectByType<CardGame>();
    }

    void Update()
    {
        if (isFront)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRotation, rotateSpeed * Time.deltaTime);
        }

        if (transform.rotation.eulerAngles.y < 90 || transform.rotation.eulerAngles.y > 270)
        {
            GetComponent<Image>().sprite = isFront ? spriteImage : defaultSprite;
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
        if (cardText == null) cardText = GetComponentInChildren<TextMeshProUGUI>();

        cardNum = newNum;
        if (cardText != null) cardText.text = cardNum.ToString();
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }

    public void SetImage(Sprite sprite)
    {
        spriteImage = sprite;
        GetComponent<Image>().sprite = defaultSprite;
    }

    public void Flip(bool isFront)
    {
        this.isFront = isFront;
    }
}