using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleLogo : MonoBehaviour
{
    public void LoadScene() => SceneManager.LoadScene("Play");
}
