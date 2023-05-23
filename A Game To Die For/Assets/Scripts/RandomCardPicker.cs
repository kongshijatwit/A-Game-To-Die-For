using UnityEngine;

public class RandomCardPicker
{
    private Transform[] allCardPrefabs;

    public RandomCardPicker(Transform[] allCardPrefabs)
    {
        this.allCardPrefabs = allCardPrefabs;
    }

    public Transform[] PickThree()
    {
        Transform[] result = new Transform[3];

        for (int i = 0; i < 3; i++)
        {
            result[i] = PickOne();
        }

        return result;
    }

    public Transform PickOne()
    {
        Transform result = null;
        int percent = Random.Range(0, allCardPrefabs.Length);
        result = allCardPrefabs[percent];
        return result;
    }
}
