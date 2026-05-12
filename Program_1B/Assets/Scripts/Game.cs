using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Game : MonoBehaviour
{
    [Header("설정")]
    public int cardNum = 10;
    private const int MAX_CARD = 14;

    [Header("프리팹 & 부모")]
    public GameObject cardPrefab;
    public Transform cardParent;

    [Header("리소스")]
    public List<Sprite> sprites;

    private List<Cardd> cards = new List<Cardd>();
    private Cardd firstCard = null;
    private Cardd secondCard = null;
    private bool isChecking = false;

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        SoundManager.instance.PlaySoundBGM();

        foreach (Cardd c in cards) { if (c != null) Destroy(c.gameObject); }
        cards.Clear();

        int maxAvailable = sprites.Count;

        if (cardNum > maxAvailable)
        { 
            cardNum = maxAvailable;
        }
        else if (cardNum < 1) cardNum = 1;

        List<int> randCardNum = GeneratePairNum(cardNum * 2);

        for (int i = 0; i < cardNum * 2; i++)
        {
            GameObject go = Instantiate(cardPrefab, cardParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;

            Cardd cardScript = go.GetComponent<Cardd>();
            cardScript.SetCardNum(randCardNum[i]);
            cardScript.SetImage(sprites[randCardNum[i]]);

            cards.Add(cardScript);
        }
    }

    private List<int> GeneratePairNum(int totalCount)
    {
        List<int> list = new List<int>();
        int pairCount = totalCount / 2;

        for (int i = 0; i < pairCount; i++)
        {
            list.Add(i);
            list.Add(i);
        }
    
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }

    public void OnClickCard(Cardd clickedCard)
    {
        if (isChecking || clickedCard.isMatched || clickedCard == firstCard) return;

        clickedCard.Flip(true);

        if (firstCard == null)
        {
            firstCard = clickedCard;
            SoundManager.instance.PlaySound();
        }
        else
        {
            secondCard = clickedCard;
            SoundManager.instance.PlaySound();
            StartCoroutine(CheckCardRoutine());
        }
    }

    private IEnumerator CheckCardRoutine()
    {
        isChecking = true;

        if (firstCard.cardNum == secondCard.cardNum)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;
            firstCard.ChangeColor(new Color(0.8f, 0.8f, 0.8f));
            secondCard.ChangeColor(new Color(0.8f, 0.8f, 0.8f));

            firstCard = null;
            secondCard = null;
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
            firstCard.Flip(false);
            secondCard.Flip(false);
            firstCard = null;
            secondCard = null;
        }

        isChecking = false;
    }
}