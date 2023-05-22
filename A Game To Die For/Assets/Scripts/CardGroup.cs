using System.Collections;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    [SerializeField] private Transform[] cards;
    public float percent = 1f;
    public Transform[] cardPrefabList;
    private float distance = 5.5f;
    private float currY;
    private float newY;

    private void Start()
    {
        currY = transform.position.x;
        newY = currY + distance;
        StartCoroutine(nameof(StartingLerp));
    }

    public void SetNewCards(Transform[] cardPrefabs)
    {
        if (cardPrefabs.Length > 3)
        { 
            Debug.LogWarning("Too many elements in cardprefab"); 
            return; 
        }

        for (int i = 0; i < 3; i++)
        {
            Transform newCard = Instantiate(cardPrefabs[i]);
            newCard.transform.parent = transform.GetChild(i);
        }

        cards = cardPrefabs;
    }

    public void DeleteCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (transform.GetChild(i).childCount != 0) 
                Destroy(transform.GetChild(i).GetChild(0).gameObject);
        }
    }

    public void MoveRight()
    {
        StartCoroutine(nameof(LerpGroup), true);
    }

    public void MoveLeft()
    {
        StartCoroutine(nameof(LerpGroup), false);
    }

    public IEnumerator LerpGroup(bool moveRight)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                transform.GetChild(i).GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        float moveSpeed = 1f;

        if (moveRight)
        {
            while (percent <= 1)
            {
                transform.position = new Vector3(Mathf.Lerp(currY, newY, percent), transform.position.y, transform.position.z);
                percent += moveSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            DeleteCards();

            // Replace for random cards
            SetNewCards(cardPrefabList);
        }

        if (!moveRight)
        {
            while (percent > 0)
            {
                transform.position = new Vector3(Mathf.Lerp(currY, newY, percent), transform.position.y, transform.position.z);
                percent += -moveSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                transform.GetChild(i).GetChild(0).gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }
    }

    public IEnumerator StartingLerp()
    {
        float moveSpeed = 1f;

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                transform.GetChild(i).GetChild(0).gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }

        while (percent > 0)
        {
            transform.position = new Vector3(Mathf.Lerp(0f, 5.5f, percent), transform.position.y, transform.position.z);
            percent += -moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        currY = transform.position.x;
        newY = currY + distance;
    }
}
