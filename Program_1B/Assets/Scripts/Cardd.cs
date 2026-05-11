using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cardd : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI cardText;
    public Image cardImage; 
    public Sprite defaultSprite;

    [Header("상태")]
    public int cardNum;
    public bool isFront = false;
    public bool isMatched = false;
    public float rotateSpeed = 10f;

    private Sprite frontSprite;
    private Game cardGame;

    void Awake()
    {
        cardGame = Object.FindFirstObjectByType<Game>();
        if (cardImage == null) cardImage = GetComponent<Image>();
    }

    void Update()
    {
        Quaternion targetRotation = isFront ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        float angle = transform.rotation.eulerAngles.y;
        if (angle < 90 || angle > 270)
        {
            cardImage.sprite = isFront ? frontSprite : defaultSprite;
            if (cardText != null) cardText.enabled = isFront;
        }
        else
        {
            cardImage.sprite = defaultSprite;
            if (cardText != null) cardText.enabled = false;
        }
    }

    public void ClickCard()
    {
        cardGame.OnClickCard(this);
    }

    public void SetCardNum(int newNum)
    {
        cardNum = newNum;
        if (cardText != null) cardText.text = cardNum.ToString();
    }

    public void SetImage(Sprite sprite)
    {
        frontSprite = sprite;
        cardImage.sprite = defaultSprite;
    }

    public void Flip(bool front) => isFront = front;

    public void ChangeColor(Color color) => cardImage.color = color;
}