using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

	// Probability of returning to Weather 0 (Neutral):
	const float WEATHER0_P = 0.45f;

	// This is how many times a Weather can be repeated consecutively:
	const int MAX_TIMES_PER_WEATHER = 3;

	// Maximum times the neutral weather can appear to a player in a session:
	const int MAX_TIMES_WEATHER0 = 5;

	public Weather[] weathers;

	enum WeatherName				{ Spring, Summer, Autumn, Winter};
	int[] magnitudes = new int[]	{ 0,	  4,	  3,	  5		};

	string[] weatherNames;
	float[,] probabilityMatrix;
	float[,] acumulativeDist;

	// This array contais how many times each weather have been repeated consecutively:
	int[] turnsWeatherBeenRepeated;

	// This array contains
	// i: how many times each weather appeared during a session for
	// j: each player
	int[,] timesByWeather;

	public Weather currentWeather;

	int currentPlayer;

	void Start () {

		fillWeathersArray();
		fillProbabilityMatrix();
		fillAcumulativeDist();

		Turn_Manager.OnTurnSystemStarted += startWeather;
		Turn_Manager.OnTurnChanged += switchWeather;
		Turn_Manager.OnTurnSystemFinished += printTimesByWeather;
		
	}

	void startWeather() {
		currentPlayer = 0;
		setWeatherAs(weathers[0]);
	}

	void setWeatherAs(Weather w) {
		Weather lastWeather = currentWeather;
		currentWeather = w;
		timesByWeather[currentPlayer, w.id]++;
		setTurnsRepeated(lastWeather);

		//Debug.Log("----> Weather for player " + (currentPlayer + 1) + " is now " + currentWeather.name 
		//	+ " by " + timesByWeather[currentPlayer, w.id] + " time" + " and has been repeated " 
		//	+ turnsWeatherBeenRepeated[w.id] + " times.");		
	}

	// Switchs the Weather depending on various conditions.
	void switchWeather(int turn) {

		if (turn % 2 == 0) {
			currentPlayer = 1;
		} else {
			currentPlayer = 0;
		}

		if (turnsWeatherBeenRepeated[currentWeather.id] > MAX_TIMES_PER_WEATHER) {
			setWeatherAs(weathers[getAnotherIndex()]);
		} else {
			changeWeatherUsingMatrix();
		}
		
		
		//Debug.Log(turnsWeatherBeenRepeated[currentWeather.id]); //How many times have been this Weather being consecutively.
	}

	// Change the currentWeather using a random float and the Acumulative Distribution matrix.
	void changeWeatherUsingMatrix() {
		float r = Random.Range(0.0f, 1.0f);
		for (int i = 0; i < weathers.Length; i++) {
			try {
				if (r < acumulativeDist[currentWeather.id, i]) {
					setWeatherAs(weathers[i]);
					break;
				}
			}
			catch { }
		}
	}

	// Returns a String array with only the names of the enumeration WeatherName.
	string[] getWeatherNames() {
		string[] weatherNames = System.Enum.GetNames(typeof(WeatherName));
		return weatherNames;
	}

	// Defines the most basic way of probability to a J weather from an I weather using only the WEATHER0_P const.
	float getProbabilityOf(int i, int j) {
		if (j == 0) {
			return WEATHER0_P;
		}
		else {
			return (1 - WEATHER0_P) / (weathers.Length - 1);
		}
	}

	// Fills the Array of Weathers depending on the enum WeatherName and their respective values on magnitudes.
	void fillWeathersArray() {
		weatherNames = getWeatherNames();
		//Debug.Log("There are " + weatherNames.Length + " weathers.");
		weathers = new Weather[weatherNames.Length];
		for (int i = 0; i < weatherNames.Length; i++) {
			weathers[i] = new Weather(i, magnitudes[i], weatherNames[i]);
			//Debug.Log("Created " + weathers[i].name + " weather with id " + weathers[i].id + " and magnitude " + weathers[i].magnitude + ".");
		}

		timesByWeather = new int[2, weathers.Length];
		turnsWeatherBeenRepeated = new int[weathers.Length];
	}

	// Fills the theoretical matrix depending on WEATHER0_P, that is the probability of having the 0 Weather.
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

	// Fills the matrix that will be used by the Random number to set next weather.
	void fillAcumulativeDist() {
		acumulativeDist = new float[probabilityMatrix.Length, probabilityMatrix.Length];
		for (int i = 0; i<weathers.Length; i++) {
			float sum = 0;
			for (int j=0; j<weathers.Length; j++) {
				sum += probabilityMatrix[i, j];
				acumulativeDist[i, j] = sum;
				if (j > 0) {
					//Debug.Log("If " + acumulativeDist[i, j - 1] + " < r < " + acumulativeDist[i, j]
					//	+ " then it will be " + weathers[j].name + ".");
				}
			}
		}
	}

	// Prints how many times a weather appeared during a session.
	void printTimesByWeather() { //Remove Later.
		string s = "";
		for (int i=0; i<weathers.Length; i++) {
			s += weathers[i].name + " appeared " + timesByWeather[0,i] + " times for  player 1." + System.Environment.NewLine;
			s += weathers[i].name + " appeared " + timesByWeather[1, i] + " times for  player 2." + System.Environment.NewLine;
		}
		Debug.Log(s);
	}

	// To determine how many times a weather has been repeated IN A ROW.
	void setTurnsRepeated(Weather lastWeather) {
		if (currentWeather == lastWeather) {
			turnsWeatherBeenRepeated[currentWeather.id]++;
		}
		else {
			turnsWeatherBeenRepeated[currentWeather.id] = 0;
		}
	}

	// This must return the index/id of the less frequent weather during the session.
	// Now it doesn't. It just returns another index.
	int getAnotherIndex() {
		//int index = turnsWeatherBeenRepeated.Min();
		if (currentWeather == weathers[0]) {
			return 2;
		} else {
			if (timesByWeather[currentPlayer, 0] > MAX_TIMES_WEATHER0) {
				int r = Random.Range(1, weathers.Length - 1);
				return r;
			}
			else {
				return 0;
			}
		}
	}

	void forceAnotherWeather() {
		if (currentWeather == weathers[0]) {
			int r = Random.Range(1, weathers.Length - 1);
			setWeatherAs(weathers[r]);
		}
		else {
			setWeatherAs(weathers[0]);
		}

	}
}
