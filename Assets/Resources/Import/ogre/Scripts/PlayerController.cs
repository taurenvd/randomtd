using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public SkinnedMeshRenderer Weapon;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float movingX = Input.GetAxis("Horizontal");
        float movingZ = Input.GetAxis("Vertical");

        transform.Translate(movingX * Speed * Time.deltaTime, 0,movingZ * Speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime*RotationSpeed);

        if (movingX!=0||movingZ!=0)
            _animator.SetBool("Walking", true);
        else
            _animator.SetBool("Walking", false);

        if(Input.GetMouseButtonDown(0))
            _animator.SetBool("Attack", true);
        else if(Input.GetMouseButtonUp(0))
            _animator.SetBool("Attack", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
           Weapon.sharedMesh=other.gameObject.GetComponent<MeshFilter>().mesh;
            Destroy(other.gameObject);
        }
    }
   
}
