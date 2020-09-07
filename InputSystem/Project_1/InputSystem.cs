using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    float _x;
    InputMapper _inputMapper;
    // Start is called before the first frame update
    void Start()
    {
        _inputMapper = GetComponent<InputMapper>();
        _x=Time.deltaTime * 5;
        _inputMapper.BindHoldEvent(InputMapper.LEFT, MoveLeft);
        _inputMapper.BindHoldEvent(InputMapper.RIGHT, MoveRight);
        _inputMapper.BindHoldEvent(InputMapper.UP, MoveUp);
        _inputMapper.BindHoldEvent(InputMapper.DOWN, MoveDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void MoveLeft()
    {
        transform.Translate(Vector3.left*_x,Space.Self);
    }
    void MoveRight()
    {
        transform.Translate(Vector3.right*_x,Space.Self);
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up*_x,Space.Self);
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down*_x,Space.Self);
    }
    
}
