﻿using Assets.Utils;

public class MonthlyBalanceView : DailyUpdateableView
{
    ColoredValuePositiveOrNegative Text;

    void Start()
    {
        Text = GetComponent<ColoredValuePositiveOrNegative>();
    }

    public override void Render()
    {
        Text.UpdateValue(CompanyEconomyUtils.GetBalanceChange(MyProductEntity, GameContext));
    }
}
