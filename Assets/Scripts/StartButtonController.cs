using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(sahneDegis);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sahneDegis(){
        Application.LoadLevel("SampleScene");
    }
}
