using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputPanel : Panel
{
   [SerializeField] private TMP_InputField inputField;
   
   public void RebuildButton()
   {
      GridManager.Instance.SetSize(Int32.Parse(inputField.text));
   }
}
