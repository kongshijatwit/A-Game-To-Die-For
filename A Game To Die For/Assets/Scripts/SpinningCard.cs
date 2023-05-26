using UnityEngine;

public class SpinningCard : MonoBehaviour
{
    private const float ROTATION_SPEED = 10f;
    private const float COOLDOWN = 1f;

    private MeshRenderer[] mr;
    private bool swapped = false;
    private int index = 0;

    private void Start()
    {
        mr = new MeshRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            mr[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime, Space.World);
        if (Mathf.RoundToInt(transform.rotation.eulerAngles.y) == 140 && !swapped) 
        {
            mr[index].enabled = false;
            mr[LoopChildren()].enabled = true;
            swapped = true;
            Invoke(nameof(SwapCooldown), COOLDOWN);
        }
    }

    private int LoopChildren() => index + 1 > transform.childCount - 1 ? 0 : ++index;

    private void SwapCooldown() => swapped = false;
}
