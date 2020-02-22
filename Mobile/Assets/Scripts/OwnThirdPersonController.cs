using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OwnThirdPersonController : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public FixedTouchField Touchfield;

   

    protected Actions Actions;
    protected PlayerController PlayerController;
    protected Rigidbody Rigidbody;
    protected ParticleSystem ShootParticle;

    protected float CameraAngleY;
    protected float CameraAnglesSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    protected float Cooldown;

    // Start is called before the first frame update
    void Start()
    {
        Actions = GetComponent<Actions>();
        ShootParticle = GetComponentInChildren<ParticleSystem>();
        PlayerController = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        var input = new Vector3(LeftJoystick.Horizontal, 0, LeftJoystick.Vertical);

        var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * 5f;

        Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
        transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.0001f, Vector3.up) + 180, Vector3.up);

        CameraAngleY += Touchfield.TouchDist.x * CameraAnglesSpeed;
        CameraPosY = Mathf.Clamp(CameraPosY - Touchfield.TouchDist.y * CameraPosSpeed,0,5f); // 총 구 위치 조정

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, 4); // 
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up); // 캐릭터 방향에 따라 회전

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        Cooldown -= Time.deltaTime;
        if (Button.Pressed)
        {
            //shooting
            PlayerController.SetArsenal("Rifle");
            Actions.Attack();

            if(Cooldown <= 0f)
            {
                Cooldown = 0.3f;
                ShootParticle.Play();

                RaycastHit hitinfo;
                if(Physics.Raycast(ray, out hitinfo))
                {
                    var other = hitinfo.collider.GetComponent<Shootable>();
                    if(other !=null)
                    {
                        other.GetComponent<Rigidbody>().AddForceAtPosition((hitinfo.point - ShootParticle.transform.position).normalized * 500f, hitinfo.point);
                    }
                }
            }

        }

        else
        {

        }


        if (Rigidbody.velocity.magnitude > 3f)
            Actions.Run();
        else if (Rigidbody.velocity.magnitude > 0.5f)
            Actions.Walk();
        else
            Actions.Stay();
    }
   
}
