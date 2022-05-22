using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionMenu : MonoBehaviour{
    public GameObject interactionMenu;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        interactionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.nearDoor){
            interactionMenu.SetActive(true);
            text.text = "'E' to start run!";
        }
        else if (Player.nearUpStation){
            interactionMenu.SetActive(true);
            text.text = "'E' for shop!";
        }
        else if (Player.nearTeleporter){
            interactionMenu.SetActive(true);
            if (Player.instance.gold * .05f > 50){
                text.text = "'T' to teleport! ("+(int)Player.instance.gold * .05f+"g)";
            }
            else{
                text.text = "'T' to teleport! (50g)";
            }
        }
        else{
            interactionMenu.SetActive(false);
        }
    }
}
