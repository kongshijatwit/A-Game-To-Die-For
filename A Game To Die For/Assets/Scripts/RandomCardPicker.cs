using UnityEngine;

public class RandomCardPicker
{
    private int BASECARD_CHANCE = 85;

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
        int randNum = ((int)Time.time * Random.Range(0, 100)) % 101;
        Debug.Log(randNum);

        if(randNum <= BASECARD_CHANCE) {
            int percent = Random.Range(0, allCardPrefabs.Length-1);
            result = allCardPrefabs[percent];
        } else {
            result = allCardPrefabs[allCardPrefabs.Length-1];
        }
        return result;
    }
}
