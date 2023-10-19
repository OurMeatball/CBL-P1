using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    string[] kCardSuits = new string[] { "Club", "Diamond", "Spades", "Heart" };
    string[] kCardRanks = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    static public MemoryGame instance;
    private Card[] cards;
    private Card selectOne;
    private Card selectTwo;
    private double selectTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // Get all cards on GameBoard
        cards = transform.GetComponentsInChildren<Card>();

        // Deal random cards, in pairs
        int n = 0;
        Shuffle(cards);
        for (int m = 0; m < cards.Length / 2; ++m)
        {
            // choose a random suit & rank
            string suit = GetRandomFromArray(kCardSuits);
            string rank = GetRandomFromArray(kCardRanks);
            // assign it to two cards
            cards[n++].SetSuitAndRank(suit, rank);
            cards[n++].SetSuitAndRank(suit, rank);
        }
    }

    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = (int)Mathf.Floor(Random.value * (n--));
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    private T GetRandomFromArray<T>(T[] array)
    {
        return array[(int)Mathf.Floor(Random.value * array.Length)];
    }

    public void Select(Card card)
    {
        // If we don't already have two selected cards)
        if (selectTwo == null)
        {
            // flip card
            card.Flip();
            // save card in selectOne or selectTwo
            if (selectOne == null)
            {
                selectOne = card;
            }
            else
            {
                selectTwo = card;
                selectTime = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for match or mismatch
        if (selectTwo != null)
        {
            // wait one second so the user can see the card
            if (Time.time > selectTime + 1.0)
            {
                CheckMatch();
            }
        }
    }

    private void CheckMatch()
    {
        if (this.selectOne.Matches(this.selectTwo))
        {
            // Remove cards from the board
            this.selectOne.Hide();
            this.selectTwo.Hide();
        }
        else
        {
            // Return cards to face down
            this.selectOne.Flip();
            this.selectTwo.Flip();
        }

        // Clear selection
        this.selectOne = null;
        this.selectTwo = null;
    }
}

	

