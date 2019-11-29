using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using KModkit;
using UnityEngine;

public class TranspositionScript : MonoBehaviour {
	public KMBombInfo Info;
	public KMAudio Audio;
	public KMBombModule Module;
	public KMModSettings ModSettings;
	public KMSelectable[] Key;
	public KMSelectable Submit;
	public TextMesh ScreenText, ScreenText2;
	public AudioClip note1, note2, note3, note4, note5, note6, note7, note8, note9, note10, note11, note12;



	private static int _moduleIdCounter = 1;
	private int _moduleId = 0;
	private bool _isSolved = false, _lightsOn = false;
	int[, ] numberField = new int[4, 20] {
	{4, 2, -4, 1, 0, 3, -4, 3, -1, 0, -3, 2, -1, 3, -2, 1, 3, -4, 4, 3},
	{-1, 3, 4, 2, -3, -2, 0, -3, -1, 3, 1, 1, 0, -1, 3, 4, -2, -1, -1, 0},
	{-2, -1, -3, 0, -4, -2, -3, 0, -4, -1, 2, -4, -3, -3, -4, 0, -2, 1, 2, -4},
	{4, -1, -2, 3, 1, 1, 2, 1, 2, 4, 4, -2, 4, -3, 0, 2, 4, 2, -3, 1}
	};
	private int currentKey = 1;
	private bool pressedSubmit = false;
	// Use this for initialization
	void Start () {
		_moduleId = _moduleIdCounter++;
		Module.OnActivate += Activate;
	}

	void Activate () {
		Init ();
		_lightsOn = true;
	}

	private void Awake () {

		Submit.OnInteract += delegate() {
			pressedSubmit = true;
			answerCheck ();
			return false;	
		};
		for (int i = 0; i < 12; i++)
		{
			int j = i;
			Key[i].OnInteract += delegate ()
			{
				handlePress(j);
				return false;
			};
		}
	}
	private int keyOn = 1;
	private int pressedOne = 0;
	private int pressedTwo = 0;
	private int pressedThree = 0;
	private int pressedFour = 0;
	private int pressedFive = 0;
	private int pressedSix = 0;
	private int correctOne = 0;
	private int correctTwo = 0;
	private int correctThree = 0;
	private int correctFour = 0;
	private int correctFive = 0;
	private int correctSix = 0;

	//when key is pressed//

	public void handlePress(int num) {

		if (!_lightsOn || _isSolved || keyOn > 6) return;

		Debug.LogFormat("[Musical Transposition #{0}] Key {1} pressed!", _moduleId, num + 1);
		if (keyOn == 1) {
			pressedOne = num + 1;
		}
		if (keyOn == 2) {
			pressedTwo = num + 1;
		}
		if (keyOn == 3) {
			pressedThree = num + 1;
		}
		if (keyOn == 4) {
			pressedFour = num + 1;
		}
		if (keyOn == 5) {
			pressedFive = num + 1;
		}
		if (keyOn == 6) {
			pressedSix = num + 1;
		}
		keyOn++;
	}

	//when submit is pressed, checking if input is equal to answer//

	public void answerCheck() {
		Submit.AddInteractionPunch ();
		if (!_lightsOn || _isSolved) {
			return;
		}
		if (pressedOne == correctOne || pressedTwo == correctTwo || pressedThree == correctThree || pressedFour == correctFour || pressedFive == correctFive || pressedSix == correctSix) {
			Debug.LogFormat ("[Musical Transposition #{0}] Module passed!", _moduleId);
			Module.HandlePass ();
			ScreenText.text = "";
			ScreenText2.text = "";
			_isSolved = true;
		} else {
			Debug.LogFormat("[Musical Transposition #{0}] Answer incorrect, strike given.", _moduleId);
			Module.HandleStrike();
			Reset();
		}
	}
	void Reset() {
		keyOn = 1;
		pressedOne = 0;
		pressedTwo = 0;
		pressedThree = 0;
		pressedFour = 0;
		pressedFive = 0;
		pressedSix = 0;
	}
	void Init () {
		int LitFrkPresent = 0;
		int UnlitCarPresent = 0;
		int LitIndicatorAmount = 0;
		int UnlitIndicatorAmount = 0;
		int noteOne = Random.Range (1, 13);
		int noteTwo = Random.Range (1, 13);
		int noteThree = Random.Range (1, 13);
		int noteFour = Random.Range (1, 13);
		int noteFive = Random.Range (1, 13);
		int noteSix = Random.Range (1, 13);
		int modifierNum = Random.Range (1, 5);
		string letterOne = "A";
		string letterTwo = "A";
		string letterThree = "A"; 
		string letterFour = "A";
		string letterFive = "A";
		string letterSix = "A";
		int firstNumber = Info.GetSerialNumberNumbers().Last();
		int batteryHolders = Info.GetBatteryHolderCount ();
		IEnumerable[] litIndicators = { Info.GetOnIndicators() };
		LitIndicatorAmount = litIndicators.GetLength (0);
		IEnumerable[] unlitIndicators = { Info.GetOffIndicators() };
		UnlitIndicatorAmount = unlitIndicators.GetLength (0);
		string modifier = "P";
		
	
		//First Note//

		if(noteOne == 1)
		{
			letterOne = "C";
		}
			else if(noteOne == 2)
		{
			letterOne = "C#";
		}
			else if(noteOne == 3)
		{
			letterOne = "D";
		}
			else if(noteOne == 4)
		{
			letterOne = "D#";
		}
			else if(noteOne == 5)
		{
			letterOne = "E";
		}
			else if(noteOne == 6)
		{
			letterOne = "F";
		}
			else if(noteOne == 7)
		{
			letterOne = "F#";
		}
			else if(noteOne == 8)
		{
			letterOne = "G";
		}
			else if(noteOne == 9)
		{
			letterOne = "G#";
		}
			else if(noteOne == 10)
		{
			letterOne = "A";
		}
			else if(noteOne == 11)
		{
			letterOne = "A#";
		}
			else if(noteOne == 12)
		{
			letterOne = "B";
		}

		//Second Note//

		if(noteTwo == 1)
		{
			letterTwo = "C";
		}
		else if(noteTwo == 2)
		{
			letterTwo = "C#";
		}
		else if(noteTwo == 3)
		{
			letterTwo = "D";
		}
		else if(noteTwo == 4)
		{
			letterTwo = "D#";
		}
		else if(noteTwo == 5)
		{
			letterTwo = "E";
		}
		else if(noteTwo == 6)
		{
			letterTwo = "F";
		}
		else if(noteTwo == 7)
		{
			letterTwo = "F#";
		}
		else if(noteTwo == 8)
		{
			letterTwo = "G";
		}
		else if(noteTwo == 9)
		{
			letterTwo = "G#";
		}
		else if(noteTwo == 10)
		{
			letterTwo = "A";
		}
		else if(noteTwo == 11)
		{
			letterTwo = "A#";
		}
		else if(noteTwo == 12)
		{
			letterTwo = "B";
		}

		//Third Note//

		if(noteThree == 1)
		{
			letterThree = "C";
		}
		else if(noteThree == 2)
		{
			letterThree = "C#";
		}
		else if(noteThree == 3)
		{
			letterThree = "D";
		}
		else if(noteThree == 4)
		{
			letterThree = "D#";
		}
		else if(noteThree == 5)
		{
			letterThree = "E";
		}
		else if(noteThree == 6)
		{
			letterThree = "F";
		}
		else if(noteThree == 7)
		{
			letterThree = "F#";
		}
		else if(noteThree == 8)
		{
			letterThree = "G";
		}
		else if(noteThree == 9)
		{
			letterThree = "G#";
		}
		else if(noteThree == 10)
		{
			letterThree = "A";
		}
		else if(noteThree == 11)
		{
			letterThree = "A#";
		}
		else if(noteThree == 12)
		{
			letterThree = "B";
		}

		//Fourth Note//

		if(noteFour == 1)
		{
			letterFour = "C";
		}
		else if(noteFour == 2)
		{
			letterFour = "C#";
		}
		else if(noteFour == 3)
		{
			letterFour = "D";
		}
		else if(noteFour == 4)
		{
			letterFour = "D#";
		}
		else if(noteFour == 5)
		{
			letterFour = "E";
		}
		else if(noteFour == 6)
		{
			letterFour = "F";
		}
		else if(noteFour == 7)
		{
			letterFour = "F#";
		}
		else if(noteFour == 8)
		{
			letterFour = "G";
		}
		else if(noteFour == 9)
		{
			letterFour = "G#";
		}
		else if(noteFour == 10)
		{
			letterFour = "A";
		}
		else if(noteFour == 11)
		{
			letterFour = "A#";
		}
		else if(noteFour == 12)
		{
			letterFour = "B";
		}

		//Fifth Note//

		if(noteFive == 1)
		{
			letterFive = "C";
		}
		else if(noteFive == 2)
		{
			letterFour = "C#";
		}
		else if(noteFive == 3)
		{
			letterFive = "D";
		}
		else if(noteFive == 4)
		{
			letterFive = "D#";
		}
		else if(noteFive == 5)
		{
			letterFive = "E";
		}
		else if(noteFive == 6)
		{
			letterFive = "F";
		}
		else if(noteFive == 7)
		{
			letterFive = "F#";
		}
		else if(noteFive == 8)
		{
			letterFive = "G";
		}
		else if(noteFive == 9)
		{
			letterFive = "G#";
		}
		else if(noteFive == 10)
		{
			letterFive = "A";
		}
		else if(noteFive == 11)
		{
			letterFive = "A#";
		}
		else if(noteFive == 12)
		{
			letterFive = "B";
		}

		//Sixth Note//

		if(noteSix == 1)
		{
			letterSix = "C";
		}
		else if(noteSix == 2)
		{
			letterSix = "C#";
		}
		else if(noteSix == 3)
		{
			letterSix = "D";
		}
		else if(noteSix == 4)
		{
			letterSix = "D#";
		}
		else if(noteSix == 5)
		{
			letterSix = "E";
		}
		else if(noteSix == 6)
		{
			letterSix = "F";
		}
		else if(noteSix == 7)
		{
			letterSix = "F#";
		}
		else if(noteSix == 8)
		{
			letterSix = "G";
		}
		else if(noteSix == 9)
		{
			letterSix = "G#";
		}
		else if(noteSix == 10)
		{
			letterSix = "A";
		}
		else if(noteSix == 11)
		{
			letterSix = "A#";
		}
		else if(noteSix == 12)
		{
			letterSix = "B";
		}

		//The Modifier//

		if(modifierNum == 1) {
			modifier = "P";
		}
		if(modifierNum == 2) {
			modifier = "R";
		}
		if(modifierNum == 3) {
			modifier = "I";
		}
		if(modifierNum == 4) {
			modifier = "RI";
		}
			
		//Calculating the Number//

		Debug.LogFormat("[Musical Transposition #{0}] This module will have 6 notes, being {1}, {2}, {3}, {4}, {5}, {6}, and have a modifier of {7}.", _moduleId, letterOne, letterTwo, letterThree, letterFour, letterFive, letterSix, modifier);
		Debug.LogFormat ("[Musical Transposition #{0}] The last digit of the serial number added to the first number, resulting in {1}.", _moduleId, firstNumber);
		if (Info.IsIndicatorOn(Indicator.FRK)) {
			LitFrkPresent = 1; 
			Debug.LogFormat ("[Musical Transposition #{0}] The bomb has a lit FRK label, so step 1C will be disregarded.", _moduleId);
		};

		if(Info.IsIndicatorOff(Indicator.CAR))
			{ UnlitCarPresent = 1; 
			Debug.LogFormat ("[Musical Transposition #{0}] The bomb has an unlit CAR label, so step 1D will be disregarded.", _moduleId);
		};

		if(Info.GetSerialNumberLetters().Any("AEIOU".Contains)){
			firstNumber += 3;
			Debug.LogFormat ("[Musical Transposition #{0}] The serial number contains a vowel, so 3 was added to the first number, resulting in {1}.", _moduleId, firstNumber);
		};

		firstNumber -= batteryHolders;
		Debug.LogFormat ("[Musical Transposition #{0}] The bomb had {1} battery holders, so {2} was subtracted from the first number; resulting in {3}.", _moduleId, batteryHolders, batteryHolders, firstNumber);


		if(LitIndicatorAmount > 1) {
			if (LitFrkPresent == 0) {
				firstNumber += 2;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb had more than 1 lit indicator, so 2 was added to the first number; resulting in {1}.", _moduleId, firstNumber);
			}
		};

		if(UnlitIndicatorAmount > 1) {
			if (UnlitCarPresent == 0) {
				firstNumber -= 2;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb had more than 1 unlit indicator, so 2 was subtracted from the first number; resulting in {1}.", _moduleId, firstNumber);
			}
		};

		if(Info.GetPortCount(Port.StereoRCA) > 0) {
			firstNumber -= 4;
			Debug.LogFormat ("[Musical Transposition #{0}] The bomb had 1 or more Stereo RCA ports present, so 4 was subtracted from the first number; resulting in {1}.", _moduleId, firstNumber);
		};

		if(Info.GetPortCount(Port.Serial) > 0) {
			firstNumber *= 2;
			Debug.LogFormat ("[Musical Transposition #{0}] The bomb had 1 or more Serial ports present, so the first number was doubled; resulting in {1}.", _moduleId, firstNumber);
		};

		if(firstNumber < 1) {
			for(int k = firstNumber; k < 1; k += 5) {
				firstNumber += 5;
				Debug.LogFormat ("[Musical Transposition #{0}] The first number was negative, so five was added, resulting in {1}.", _moduleId, firstNumber);
			}
		}
		int rowNum = 0;


		if (Info.GetSerialNumberNumbers().Last() %2 == 0) {
			Debug.LogFormat ("[Musical Transposition #{0}] The serial number's last digit was even, so the row number will either be 1 or 2", _moduleId);
			if(Info.GetPortCount(Port.Parallel) > 0) {
				rowNum = 1;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb had 1 or more Parallel ports present, so the row number was set to 1.", _moduleId);
			}
			else 
			{
				rowNum = 2;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb did not have 1 or more Parallel ports present, so the row number was set to 2.", _moduleId);
			}
		}
		if (Info.GetSerialNumberNumbers().Last() %2 == 1) {
			Debug.LogFormat ("[Musical Transposition #{0}] The serial number's last digit was odd, so the row number will either be 3 or 4", _moduleId);
			if(Info.GetPortCount(Port.RJ45) > 0) {
				rowNum = 3;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb had 1 or more RJ-45 ports present, so the row number was set to 3.", _moduleId);
			}
			else 
			{
				rowNum = 4;
				Debug.LogFormat ("[Musical Transposition #{0}] The bomb did not have 1 or more RJ-45 ports present, so the row number was set to 4.", _moduleId);
			}
		}


		//Number is located using table//

		rowNum -= 1;
		firstNumber -= 1;
		int finalNumber = numberField[rowNum, firstNumber];

		//Music is transposed by modifier, then by table number//

		if (modifierNum == 1) {
			correctOne = noteOne + finalNumber;
			correctTwo = noteTwo + finalNumber;
			correctThree = noteThree + finalNumber;
			correctFour = noteFour + finalNumber;
			correctFive = noteFive + finalNumber;
			correctSix = noteSix + finalNumber;
			Debug.LogFormat ("[Musical Transposition #{0}] The modifier was P, so all notes were only transposed by {1} semitones, resulting in sequence {2} {3} {4} {5} {6} {7}.",_moduleId, finalNumber, correctOne, correctTwo, correctThree, correctFour, correctFive, correctSix);
		}
		if (modifierNum == 2) {
			correctOne = noteSix + finalNumber;
			correctTwo = noteFive + finalNumber;
			correctThree = noteFour + finalNumber;
			correctFour = noteThree + finalNumber;
			correctFive = noteTwo + finalNumber;
			correctSix = noteOne + finalNumber;
			Debug.LogFormat ("[Musical Transposition #{0}] The modifier was R, so all notes were reversed and transposed by {1} semitones, resulting in sequence {2} {3} {4} {5} {6} {7}.",_moduleId, finalNumber, correctOne, correctTwo, correctThree, correctFour, correctFive, correctSix);
		}
		if (modifierNum == 3) {
			correctOne = noteOne + finalNumber;
			correctTwo = (noteOne + (-1 * (noteTwo - noteOne))) + finalNumber;
			correctThree = (noteOne + (-1 * (noteThree - noteOne))) + finalNumber;
			correctFour = (noteOne + (-1 * (noteFour - noteOne))) + finalNumber;
			correctFive = (noteOne + (-1 * (noteFive - noteOne))) + finalNumber;
			correctSix = (noteOne + (-1 * (noteSix - noteOne))) + finalNumber;
			Debug.LogFormat ("[Musical Transposition #{0}] The modifier was I, so all difference in semitone from the first note were inverted and transposed by {1} semitones, resulting in sequence {2} {3} {4} {5} {6} {7}.",_moduleId, finalNumber, correctOne, correctTwo, correctThree, correctFour, correctFive, correctSix);
		}
		if (modifierNum == 4) {
			int retroOne, retroTwo, retroThree, retroFour, retroFive, retroSix;
			retroOne = noteOne + finalNumber;
			retroTwo = (noteOne + (-1 * (noteTwo - noteOne))) + finalNumber;
			retroThree = (noteOne + (-1 * (noteThree - noteOne))) + finalNumber;
			retroFour = (noteOne + (-1 * (noteFour - noteOne))) + finalNumber;
			retroFive = (noteOne + (-1 * (noteFive - noteOne))) + finalNumber;
			retroSix = (noteOne + (-1 * (noteSix - noteOne))) + finalNumber;
			correctOne = retroSix;
			correctTwo = retroFive;
			correctThree = retroFour;
			correctFour = retroThree;
			correctFive = retroTwo;
			correctSix = retroOne;
			Debug.LogFormat ("[Musical Transposition #{0}] The modifier was RI, so all difference in semitone from the first note were inverted, then the sequence was reversed and transposed by {1} semitones, resulting in sequence {2} {3} {4} {5} {6} {7}.",_moduleId, finalNumber, correctOne, correctTwo, correctThree, correctFour, correctFive, correctSix);
		}

		//Checks if correct has exceeded value
		for (int l = 0; l < 4; l++) {
			if (correctOne > 12) {
				correctOne -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 1 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctOne);
			}
			if (correctOne < 1) {
				correctOne += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 1 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctOne);
			}

			if (correctTwo > 12) {
				correctTwo -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 2 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctTwo);
			}
			if (correctTwo < 1) {
				correctTwo += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 2 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctTwo);
			}

			if (correctThree > 12) {
				correctThree -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 3 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctThree);
			}
			if (correctThree < 1) {
				correctThree += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 3 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctThree);
			}

			if (correctFour > 12) {
				correctFour -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 4 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctFour);
			}
			if (correctFour < 1) {
				correctFour += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 4 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctFour);
			}

			if (correctFive > 12) {
				correctFive -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 5 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctFive);
			}
			if (correctFive < 1) {
				correctFive += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 5 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctFive);
			}

			if (correctSix > 12) {
				correctSix -= 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 6 exceeded max value (12), so it was looped back around to {1}.", _moduleId, correctSix);
			}
			if (correctSix < 1) {
				correctSix += 12;
				Debug.LogFormat ("[Musical Transposition #{0}] Number 6 exceeded min value (0), so it was looped back around to {1}.", _moduleId, correctSix);
			}
		}

		Debug.LogFormat ("[Musical Transposition #{0}] After adjustment, the final correct answer is {1} {2} {3} {4} {5} {6}.",_moduleId, correctOne, correctTwo, correctThree, correctFour, correctFive, correctSix);
		//Stage Generation//

		ScreenText.text = letterOne + " " + letterTwo + " " + letterThree + " " + letterFour + " " + letterFive + " " + letterSix;
		ScreenText2.text = modifier;
		}

	// Update is called once per frame
}
