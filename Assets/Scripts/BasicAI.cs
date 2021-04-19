using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public IUnityService UnityService;
    private CreatureMove _move;
    private Creature _creature;
    public GameObject Player;

    private void Start()
    {
        _move = gameObject.GetComponent<CreatureMove>();

        _creature = gameObject.GetComponent<Creature>();
        if (UnityService == null)
        {
            UnityService = new UnityService();
        }
    }

    private void Update()
    {
        var xMod = _move.transform.position.x > Player.transform.position.x ? 1 : -1;
        var yMod = _move.transform.position.y > Player.transform.position.y ? 1 : -1;
        var move = new Vector2 (xMod * Player.transform.position.x, yMod * Player.transform.position.y);
        _move.SetVelocity(move);
    }
}
