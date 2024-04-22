using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private void Awake()
    {
        
    }

    bool _init = false; 
    public virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }

    private void Update()
    {
        UpdateController();
    }

    public virtual void UpdateController()
    {

    }
}
