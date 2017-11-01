using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    ///Probably All of this is temp

    enum Personality_Type
    {
        tsundere, yandere
    }

    private GameObject asignedPlayer;
    public float height;
    private Personality_Type personalityType;

    public int CheckLove(Love_Type phraseLoveType)
    {
        int loveReceived = 0;

        switch (personalityType)
        {
            case Personality_Type.tsundere:
                switch (phraseLoveType)
                {
                    case Love_Type.kind:
                        loveReceived = -1;
                        break;

                    case Love_Type.ambigous:
                        float r = Random.Range(-1, 1);
                        if (r < 0)
                        {
                            loveReceived = -1;
                        }
                        else
                        {
                            loveReceived = 1;
                        }
                        break;

                    case Love_Type.hate:
                        loveReceived = 1;
                        break;
                }

                break;

            case Personality_Type.yandere:
                switch (phraseLoveType)
                {
                    case Love_Type.kind:
                        loveReceived = 1;
                        break;

                    case Love_Type.ambigous:
                        float r = Random.Range(-1, 1);
                        if (r < 0)
                        {
                            loveReceived = -1;
                        }
                        else
                        {
                            loveReceived = 1;
                        }
                        break;

                    case Love_Type.hate:
                        loveReceived = -1;
                        break;
                }
                break;
        }

        return loveReceived;
    }

    public void Grow(float newHeight)
    {
        newHeight = height + newHeight;
        height = Mathf.Lerp(height, newHeight, Time.deltaTime / 1);

        if (height < newHeight)
        {
            StartCoroutine(GrowAnim(newHeight));
        }
    }

    private IEnumerator GrowAnim(float heightToGo)
    {
        float heightReductor = heightToGo / 10;

        while (height < (heightToGo - heightReductor))
        {
            yield return new WaitForEndOfFrame();
            height = Mathf.Lerp(height, heightToGo, Time.deltaTime / 1);
            Debug.Log(height);
        }
    }
}
