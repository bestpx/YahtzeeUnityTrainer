using System.Collections;
using System.Collections.Generic;
using CommonUtil;
using UnityEngine;
using Yahtzee;
using ILogger = CommonUtil.ILogger;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LogLevel _level = LogLevel.Info;
    private ILogger _logger;
    
    private void Start()
    {
        _logger = new CommonUtil.Logger(_level);
        
        
    }
}
