using UnityEngine;

public class WoodBranchWhip : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    private float time = 0;
    private bool segmentsInitialized = false;

    private GameObject origin = null;
    private float rotationSpeed = 0;
    private float lifeTime = 0;
    private float sizeMultiplier = 1f;
    private bool healing = false;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<SpriteRenderer>().enabled = false;
            child.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    public void SetStats(GameObject _origin, float _rotationSpeed, float _lifeTime, float _sizeMultiplier = 1f, bool _healing = false)
    {
        origin = _origin;
        rotationSpeed = _rotationSpeed;
        lifeTime = _lifeTime;
        sizeMultiplier = _sizeMultiplier;
        healing = _healing;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (!segmentsInitialized)
        {
            InitializeSegments();
        }

        // Whip motion is pretty basic
        // Could be changed so that each segment moves independently to be more whip-like
        // May not be worth the effort to do ^
        transform.Rotate(new Vector3(0, 0, -rotationSpeed));       

        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSegments()
    {    
        transform.localScale = new Vector3(transform.localScale.x * sizeMultiplier, transform.localScale.y * sizeMultiplier, transform.localScale.z);

        foreach (Transform child in transform)
        {
            WoodBranch branch = child.gameObject.GetComponent<WoodBranch>();
            branch.SetStats(origin, healing, true);
            child.GetComponentInChildren<SpriteRenderer>().enabled = true;
            child.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        segmentsInitialized = true;
    }
}
