using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    [SerializeField] private Transform[] cards;
    public float percent = 1f;
    public Transform cardPrefab;

    private void Start()
    {
        ParentCards();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(nameof(LerpGroup), true);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(nameof(LerpGroup), false);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            ResetInit();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SetNewCards(cardPrefab, 0);
        }
    }

    public void ParentCards()
    {
        for (int i = 0; i < 3; i++)
        {
            cards[i].transform.parent = gameObject.transform.GetChild(i);
        }
    }

    public void SetNewCards(Transform cardPrefab, int position)
    {
        if (position > 2) 
        { 
            Debug.LogWarning("Position can only be from 0 to 2"); 
            return; 
        }
        cards[position] = cardPrefab;
        Transform g = Instantiate(cardPrefab);
        g.transform.parent = cards[position];
    }

    public void ResetInit()
    {
        for (int i = 0; i < 3; i++)
        {
            if (transform.GetChild(i).childCount != 0) 
                Destroy(transform.GetChild(i).GetChild(0).gameObject);
        }
    }

    public IEnumerator LerpGroup(bool moveDown)
    {
        float moveSpeed = 1f;
        
        while (moveDown && percent > 0)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-2f, 0.5f, percent), transform.position.z);
            percent += -moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (!moveDown && percent <= 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(-2f, 0.5f, percent), transform.position.z);
            percent += moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Mathf.Round(percent);
        yield return new WaitForEndOfFrame();
    }
}
