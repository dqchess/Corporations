﻿using UnityEngine.UI;

public class ProductLevelView : View
{
    Text Level;

    private void Start()
    {
        Level = GetComponent<Text>();
    }

    void Render()
    {
        AnimateIfValueChanged(Level, myProduct.ProductLevel + "");
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}
