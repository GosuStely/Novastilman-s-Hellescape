using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("HellCutScene", LoadSceneMode.Single);
    }
}
