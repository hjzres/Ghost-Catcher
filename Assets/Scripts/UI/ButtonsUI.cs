using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class ButtonsUI : MonoBehaviour
	{
		public void ChangeScene(int scene)
		{
			SceneManager.LoadScene(scene);
		}
		public void ChangeScene(string scene)
		{
			SceneManager.LoadScene(scene);
		}
	}
}