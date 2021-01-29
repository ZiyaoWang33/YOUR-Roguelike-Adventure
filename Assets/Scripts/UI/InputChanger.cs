using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class InputChanger : MonoBehaviour, IMenuButton
{
    [SerializeField] private Button button = null;
    [SerializeField] private TextMeshProUGUI text = null;

    private KeyCode newKey = KeyCode.None;
    private bool listening = false;

    public void OnClick()
    {
        button.interactable = false;
        listening = true;
        StartCoroutine(WaitForKey());
    }

    private void OnGUI()
    {
        if (listening)
        {
            if (Event.current.isKey)
            {
                newKey = Event.current.keyCode;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                newKey = KeyCode.Mouse0;
            }
        }
    }

    private IEnumerator WaitForKey()
    {
        yield return new WaitWhile(() => newKey == KeyCode.None);
        listening = false;
        InputManager.Instance.ChangeBinding(gameObject.name, newKey, this);
        text.text = newKey.ToString();
        button.interactable = true;
        newKey = KeyCode.None;
    }
}
