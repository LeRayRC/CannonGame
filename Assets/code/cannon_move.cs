using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon_move : MonoBehaviour
{
    Vector3 inputVec_ = new Vector3();
    public float deltaRot_ = 2.0f;
    float currentRotationY;
    float limitedRotationY;
    // Start is called before the first frame update
    private Transform tr_ = null;
    public Transform trBall_ = null;
    private Rigidbody rb_ = null;
    public Rigidbody rbBall_ = null;
    public SphereCollider scBall_ = null;
    public float force_ = 10f;
    private float cannonHeight_;
    void Start()
    {
        tr_ = GetComponent<Transform>();
        rb_ = GetComponent<Rigidbody>();
        cannonHeight_ = GetComponent<CapsuleCollider>().height;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec_.x = 0;
        inputVec_.y = Input.GetAxis("Horizontal");
        inputVec_.z = 0;
        if(inputVec_.magnitude != 0){
            tr_.Rotate(deltaRot_ * inputVec_,Space.World);
        }

        // if(tr_.eulerAngles.y > 30.0f && tr_.eulerAngles.y <= 32.0f){
        //     tr_.Rotate(new Vector3(0.0f,-2.0f,0.0f),Space.World);
        // }else if(tr_.eulerAngles.y < 330.0f && tr_.eulerAngles.y >= 328.0f){
        //     tr_.Rotate(new Vector3(0.0f,2.0f,0.0f),Space.World);
        // }
        //print(tr_.eulerAngles.y);
        currentRotationY = tr_.eulerAngles.y;
        if(currentRotationY > 180f){
            currentRotationY -= 360f;
        }
        limitedRotationY = Mathf.Clamp(currentRotationY,-30f,30f);
        tr_.eulerAngles = new Vector3(tr_.eulerAngles.x,limitedRotationY,transform.eulerAngles.z);
        //print(tr_.up);
        if(Input.GetKeyDown(KeyCode.Space)){
            rbBall_.AddForce(tr_.up * force_,ForceMode.Impulse);
            rbBall_.useGravity = true;
            trBall_.SetParent(null);
            scBall_.isTrigger = false;
        }
        if(Input.GetKeyDown(KeyCode.F1)){
            print(tr_.position);
            //+ (tr_.up * cannonHeight_ * 0.5f)
            //trBall_.Translate(tr_.position  ,Space.World);
            trBall_.position = tr_.position + (tr_.up * cannonHeight_ * 0.5f);
            trBall_.SetParent(tr_);
            rbBall_.useGravity = false;
            rbBall_.velocity = new Vector3(0f,0f,0f);
            scBall_.isTrigger = true;
        }
    }
}
