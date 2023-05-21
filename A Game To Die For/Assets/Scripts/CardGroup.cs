using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform[] cards;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MoveDown()
    {
        anim.SetBool("MoveDown", true);
    }

    public void MoveUp()
    {
        anim.SetBool("MoveDown", false);
    }

    private void ParentCards()
    {
        foreach (Transform g in cards)
        {
            g.parent = gameObject.transform;
        }
    }
}
