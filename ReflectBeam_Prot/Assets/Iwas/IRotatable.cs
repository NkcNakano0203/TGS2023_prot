using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface IRotatable
{
    void RightRotate(bool isLeftRotate, bool isRightRotate);
    void  LeftRotate(bool isLeftRotate, bool isRightRotate);
}