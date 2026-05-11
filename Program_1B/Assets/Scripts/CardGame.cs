using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardGame : MonoBehaviour

{
    [Header("설정")]
    public int cardNum;
    private int maxcard = 14;
    private int mincard = 1;

    [Header("프리팹")]
    public GameObject cardPrefab;
    public Transform cardParent;

    [Header("리소스")]
    public List<Sprite> sprites;
    private List<Card> cards = new List<Card>();
    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;
    void Start()
    {
        StartGame();
    }

    private List<int> GeneratePairNum(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNum = new List<int>();

        for (int i = 0; i < pairCount; i++)
        {
            newCardNum.Add(i);
            newCardNum.Add(i);
        }

        for (int i = newCardNum.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = newCardNum[i];
            newCardNum[i] = newCardNum[rnd];
            newCardNum[rnd] = temp;
        }
        return newCardNum;

    }

    private void StartGame()
    {
        foreach (Card c in cards) { if (c != null) Destroy(c.gameObject); }
        cards.Clear();

        if (cardNum > maxcard) cardNum = maxcard;
        else if (cardNum < mincard) cardNum = mincard;

        List<int> randCardNum = GeneratePairNum(cardNum * 2);

        for (int i = 0; i < cardNum * 2; i++)
        {
            GameObject go = Instantiate(cardPrefab, cardParent);
            Card cardScript = go.GetComponent<Card>();

            int xGrid = i % 10;
            int yGrid = i / 10;

            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(xGrid * 180, yGrid * -160);
            cardScript.SetCardNum(randCardNum[i]);
            cardScript.SetImage(sprites[randCardNum[i]]);
            cards.Add(cardScript);
        }
    }

    private void CheckCard()
    {
        isChecking = true;

        if (firstCard.cardNum == secondCard.cardNum)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;

            firstCard.ChangeColor(Color.ghostWhite);
            secondCard.ChangeColor(Color.ghostWhite);

            firstCard = null;
            secondCard = null;
            isChecking = false;
        }
        else
        {
            Invoke("Hide", 1.0f);
        }
    }
    private void Hide()
    {
        firstCard.Flip(false);
        secondCard.Flip(false);
        isChecking = false;
        firstCard = null;
        secondCard = null;
    }
    public void OnClickCard(Card clickedCard)
    {
        if (isChecking || clickedCard.isMatched || clickedCard == firstCard) return;

        if (firstCard == null)
        {
            firstCard = clickedCard;
            firstCard.Flip(true);
        }
        else
        {
            secondCard = clickedCard;
            secondCard.Flip(true);
            CheckCard();
        }
    }
}