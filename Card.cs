using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string suit;
    private string rank;
    private bool faceUp = false;

    // This method should have the correct parameter name 'inRank' instead of 'in Rank'
    public void SetSuitAndRank(string inSuit, string inRank)
    {
        suit = inSuit;
        rank = inRank;

        // Set the graphics for this suit & rank
        string path = "Free_Playing_Cards/PlayingCards_" + rank + suit;

        // Assuming you are trying to load a mesh, not a Mesh type resource
        GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>(path);

        // Add a collider component for mouse click detection
        gameObject.AddComponent<MeshCollider>();
    }

    public bool Matches(Card otherCard)
    {
        return (rank == otherCard.rank) && (suit == otherCard.suit);
    }

    public void Flip()
    {
        faceUp = !faceUp;

        // Rotate the card to make it face up or face down
        if (faceUp)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (!faceUp)
                    {
                        // Assuming MemoryGame is a class where you handle card selection
                        MemoryGame.instance.Select(this);
                    }
                }
            }
        }
    // Check if Update method is being called
    Debug.Log("Update method called");

    if (Input.GetMouseButtonDown(0))
    {
        // Check if mouse button click is detected
        Debug.Log("Mouse button clicked");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if a hit is detected
            Debug.Log("Hit detected");

            if (hit.transform == transform)
            {
                if (!faceUp)
                {
                    // Assuming MemoryGame is a class where you handle card selection
                    MemoryGame.instance.Select(this);
                }
                else
                {
                    // Card is already face up
                    Debug.Log("Card is face up");
                }
            }
            else
            {
                // The hit is not on this card
                Debug.Log("Hit is not on this card");
            }
        }
        else
        {
            // No hit detected
            Debug.Log("No hit detected");
        }
    }
}
}
   

