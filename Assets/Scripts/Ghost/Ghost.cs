using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Difficulty difficulty;
	private enum Difficulty
	{
		easy,
		medium,
		hard
	}
}
