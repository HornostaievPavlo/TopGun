using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UIBehaviorTesting : MonoBehaviour
{
    [UnityTest]
    public IEnumerator TutorialIsShowed()
    {
        GameObject _object = new GameObject("TESTING");

        TutorialController controller = _object.AddComponent<TutorialController>();

        Image _image = _object.AddComponent<Image>();

        controller.controls = _image;

        Button _button = _object.AddComponent<Button>();

        controller.button = _button;

        yield return null;

        Assert.IsTrue(_button.gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator StartTextIsShowed()
    {
        GameObject _object = new GameObject("TESTING");

        StartTextController controller = _object.AddComponent<StartTextController>();

        TextMeshProUGUI _text = _object.AddComponent<TextMeshProUGUI>();

        _text.rectTransform.anchoredPosition = Vector3.zero;

        controller.startText = _text;

        float _speed = 10f;

        controller.startTextSpeed = _speed;

        controller.StartTextMovement();

        yield return null;

        Assert.IsTrue(_text.rectTransform.position != Vector3.zero);
    }
}
