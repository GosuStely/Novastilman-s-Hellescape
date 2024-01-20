using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HellSceneTransition : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
