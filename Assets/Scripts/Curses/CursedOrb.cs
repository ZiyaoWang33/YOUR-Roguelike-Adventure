using UnityEngine;

[CreateAssetMenu(fileName = "New Cursed Orb", menuName = "Cursed Orb", order = 3)]
public class CursedOrb : ScriptableObject
{
    [SerializeField] private string _element = null;
    public string element { get { return _element; } }

    [SerializeField] private Curse[] _curses = null;
    public Curse[] curses { get { return _curses; } }

    [SerializeField] private Sprite _image = null;
    public Sprite image { get { return _image; } }
}
