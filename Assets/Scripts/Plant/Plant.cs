using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	///Probably All of this is temp
	///

	[SerializeField]
	private int id;

    enum Personality_Type
    {
        tsundere, yandere
    }

    public delegate void AnimationOcurrences();
    public static event AnimationOcurrences OnPlantMovementEnded;
    public static event AnimationOcurrences OnPlantMovementStarted;

	public Seed seed;
	private Plant_Creator creator;
	private GameObject face;
	private GameObject head;
	private GameObject stem;

    [SerializeField]
    private float toPositionConverter = 100f;

    private GameObject asignedPlayer;
    public float height;

    private bool stop;

    private float animHeight;
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

	private void Start() {
		GameObject creatorObject = GameObject.FindGameObjectWithTag("PlantCreator");
		creator = creatorObject.GetComponent<Plant_Creator>();

		seed = creator.newRandomPlant();

		face = GameObject.FindGameObjectWithTag("Face" + id);
		head = GameObject.FindGameObjectWithTag("Head" + id);
		stem = GameObject.FindGameObjectWithTag("Stem" + id);

		head.GetComponent<SpriteRenderer>().sprite = seed.head;
		stem.GetComponent<SpriteRenderer>().sprite = seed.stem;
		
	}




	/// A partir de aqui esta todo lo necesario para animar la planta

	public void Grow(float newHeight)
    {
        stop = false;
        animHeight = newHeight;

        newHeight = height + newHeight;

		height = newHeight;

        StartCoroutine("GrowAnimPosition");
        
    }

    private void GrowAnim(float heightToGo)
    {
        float heightReductor = heightToGo / 10;
        
        height = heightToGo;
        
    }

    private IEnumerator GrowAnimPosition()
    {
        if (OnPlantMovementStarted != null)
        {
            OnPlantMovementStarted(); //Por si se necesita
        }
        StartCoroutine("Wait");

        while (stop == false)
        {
            yield return new WaitForEndOfFrame();

            float transformHeight = animHeight / toPositionConverter;
            transform.position = new Vector3(transform.position.x, (transform.position.y + (transformHeight)), transform.position.z);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        stop = true;
        if (OnPlantMovementEnded != null)
        {
            OnPlantMovementEnded(); //Se planea enviar a Turn Manager (switchTurn())
        }
    }
}
