using System;
using System.Collections;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    private const float MOVE_SPEED = 1f;
    private const float HAND_POS = 0f;
    private const float BOX_POS = 5.5f;

    [SerializeField] Transform[] allCardsToBePulled;
    RandomCardPicker randomCardPicker;

    private void Start()
    {
        randomCardPicker = new RandomCardPicker(allCardsToBePulled);
        StartCoroutine(nameof(StartGame));
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4f);
        MoveLeft();
    }

    public void MoveRight()
    {
        StartCoroutine(MoveCardsTo(BOX_POS, true));
    }

    public void MoveLeft()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Triple Draw", GetComponent<Transform>().position);
        StartCoroutine(MoveCardsTo(HAND_POS, false));
    }

    /// <summary>
    /// Moves cards from current position to new position by lerping
    /// </summary>
    /// <param name="newPos">The position that the card group will move to</param>
    /// <param name="redraw">Whether or not to reset the cards</param>
    private IEnumerator MoveCardsTo(float newPos, bool redraw)
    {
        float percent = 0;
        float currPos = transform.position.x;

        // Make cards unclickable while moving
        ChangeChildLayer("Default");

        while (percent <= 1)
        {
            // Moving group using Mathf.Lerp
            transform.position = new Vector3(Mathf.Lerp(currPos, newPos, percent), transform.position.y, transform.position.z);
            percent += MOVE_SPEED * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Round off any long floats - more consistency
        transform.position = new Vector3((float)Math.Round(transform.position.x, 2), transform.position.y, transform.position.z);

        // Only redraw when the cards are in the box
        if (redraw)
        {
            DeleteCards();
            SetNewCards(randomCardPicker.PickThree());
        }

        // Make cards clickable again
        ChangeChildLayer("Interactable");
    }

    /// <summary>
    /// Change the layer of every child in this GameObject
    /// </summary>
    /// <param name="layerName">The name of the layer to change to (case sensitive)</param>
    private void ChangeChildLayer(string layerName)
    {
        foreach (Transform child in transform)
        {
            if (child.transform.childCount > 0)
                child.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

#region Can put these in a CardManager.cs Script
    public void SetNewCards(Transform[] cardPrefabs)
    {
        for (int i = 0; i < 3; i++)
        {
            Transform newCard = Instantiate(cardPrefabs[i]);
            newCard.transform.parent = transform.GetChild(i);
        }
    }

    public void DeleteCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (transform.GetChild(i).childCount != 0) 
                Destroy(transform.GetChild(i).GetChild(0).gameObject);
        }
    }
#endregion
}
