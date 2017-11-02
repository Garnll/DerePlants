using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    ///Probably All of this is temp

    enum Personality_Type
    {
        tsundere, yandere
    }

    [SerializeField]
    private float toPositionConverter = 100000000000000000000000f;

    private GameObject asignedPlayer;
    public float height;
    private Personality_Type personalityType;

    public int CheckLove(Love_Type phraseLoveType)
    {
        int loveReceived = 0;
        Debug.Log(personalityType);

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

        height = Mathf.Lerp(height, newHeight, Time.deltaTime / 1); //Esta es la animación
        StartCoroutine(GrowAnim(newHeight));
        
    }

    private IEnumerator GrowAnim(float heightToGo)
    {
        float heightReductor = heightToGo / 10;

        if (heightToGo >= height)
        {
            Debug.Log("Altura más pequeña que la nueva");

            while (height != heightToGo)
            {
                yield return new WaitForEndOfFrame();
                height = Mathf.Lerp(height, heightToGo, Time.deltaTime / 1);
                if (height >= (heightToGo - heightReductor))
                {
                    height = heightToGo;
                }

                GrowAnimPosition();
            }
        }
        else
        {
            Debug.Log("Altura más grande que la nueva");
            while (height != heightToGo)
            {
                Debug.Log("Altura a llegar: " + heightToGo);
                Debug.Log("Altura Actual: " + height);

                yield return new WaitForEndOfFrame();
                height = Mathf.Lerp(height, heightToGo, Time.deltaTime / 1);
                if (height <= (heightToGo - heightReductor))
                {
                    height = heightToGo;
                }

                GrowAnimPosition();
            }
        }
    }

    private void GrowAnimPosition()
    {
        float transformHeight = height / toPositionConverter;
        transform.position = new Vector3(transform.position.x, transform.position.y + (transformHeight), transform.position.z);
    }
}
