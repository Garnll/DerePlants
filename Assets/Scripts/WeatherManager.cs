using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

	const float WEATHER0_P = 0.4f;

	Weather[] weathers;
	enum WeatherName				{ Spring, Summer, Autumn, Winter};
	int[] magnitudes = new int[]	{ 0,	  5,	  3,	  6		};

	float[,] probabilityMatrix;
	float[,] acumulativeDist;

	public Weather currentWeather;

	void Start () {
		fillWeathersArray();
		fillProbabilityMatrix();
		fillAcumulativeDist();
		currentWeather = weathers[0];
	}

	void switchWeather() {
		float r = Random.Range(0, 1);
	}

	string[] getWeatherNames() {
		string[] weatherNames = System.Enum.GetNames(typeof(WeatherName));
		return weatherNames;
	}

	float getProbabilityOf(int i, int j) {
		if (j == 0) {
			return WEATHER0_P;
		}
		else {
			return (1 - WEATHER0_P) / (weathers.Length - 1);
		}
	}

	void fillWeathersArray() {
		string[] weatherNames = getWeatherNames();
		//Debug.Log("There are " + weatherNames.Length + " weathers.");
		weathers = new Weather[weatherNames.Length];
		for (int i = 0; i < weatherNames.Length; i++) {
			weathers[i] = new Weather(i, magnitudes[i], weatherNames[i]);
			Debug.Log("Created " + weathers[i].name + " weather with id " + weathers[i].id + " and magnitude " + weathers[i].magnitude + ".");
		}
	}

	void fillProbabilityMatrix() {
		probabilityMatrix = new float[weathers.Length, weathers.Length];
		for (int i = 0; i < weathers.Length; i++) {
			for (int j = 0; j < weathers.Length; j++) {
				probabilityMatrix[i, j] = getProbabilityOf(i, j);
				//Debug.Log("The probability to get to " + weathers[j].name + " from " 
				//	+ weathers[i].name + " is " + probabilityMatrix[i, j] + ".");
			}
		}
	}

	void fillAcumulativeDist() {
		acumulativeDist = new float[probabilityMatrix.Length, probabilityMatrix.Length];
		for (int i = 0; i<weathers.Length; i++) {
			float sum = 0;
			for (int j=0; j<weathers.Length; j++) {
				Debug.Log(i + "," + j);
				sum += probabilityMatrix[i, j];
				acumulativeDist[i, j] = sum;
				if (j > 0) {
					Debug.Log("If " + acumulativeDist[i, j - 1] + " < r < " + acumulativeDist[i, j]
						+ " then it will be " + weathers[j].name + ".");
				}
			}
		}
	}
}
