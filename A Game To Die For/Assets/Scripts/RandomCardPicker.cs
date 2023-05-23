using UnityEngine;

public class RandomCardPicker
{
    private int BASECARD_CHANCE = 93;
    private int LIFECARD_CHANCE = 5;
    //private int SCYTHECARD_CHANCE = 2;

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

    //Picks card based on calculations
    public Transform PickOne()
    {
        Transform result = null;
        int randNum = ((int)Time.time * Random.Range(0, 100)) % 101;
        Debug.Log(randNum);

        if(randNum <= BASECARD_CHANCE) {
            int percent = Random.Range(0, allCardPrefabs.Length-2);
            result = allCardPrefabs[percent];
        } else if(randNum <= (BASECARD_CHANCE + LIFECARD_CHANCE)) {
            if(GameManager.instance.GetPlayerHealth() <= 0.5) {
                result = allCardPrefabs[allCardPrefabs.Length-2];
            } else {
                int percent = Random.Range(0, allCardPrefabs.Length-2);
                result = allCardPrefabs[percent];
            }
        } else {
            result = allCardPrefabs[allCardPrefabs.Length-1];
        }
        return result;
    }
}
