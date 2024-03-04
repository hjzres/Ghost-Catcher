using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	[Header("Ghost Properties")]
	public GhostScriptableObject ghostType;
	[SerializeField] [ReadOnly] private float speed;
	[SerializeField] [ReadOnly] private bool isWalking;
	
	[SerializeField] private Difficulty difficulty;
	private enum Difficulty
	{
		easy,
		medium,
		hard
	}
}
