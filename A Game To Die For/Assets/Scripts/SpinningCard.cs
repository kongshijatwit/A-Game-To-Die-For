using System.Collections;
using UnityEngine;

public class SpinningCard : MonoBehaviour
{
    private MeshRenderer[] mr;
    private float rotationSpeed = 10f;
    private bool swapped = false;
    private float cooldown = 3f;
    private int index = 0;

    private void Start()
    {
        mr = new MeshRenderer[3];
        for (int i = 0; i < transform.childCount; i++)
        {
            mr[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime, Space.World);
        if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 140 && !swapped) 
        {
            mr[index].enabled = false;
            mr[LoopChildren()].enabled = true;
            swapped = true;
            StartCoroutine(nameof(SwapCooldown));
        }
    }

    private int LoopChildren()
    {
        if (index + 1 > transform.childCount - 1) index = 0;
        else index++;
        return index;
    }

    private IEnumerator SwapCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        swapped = false;
    }
}
