﻿using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);

        string name = "New Company " + CompanyUtils.GenerateCompanyId(GameContext);

        var c = CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);

        var companyID = c.company.Id;

        if (MyProductEntity == null)
        {
            SetMyselfAsCEO(companyID);
        }
        else if (MyGroupEntity != null)
        {
            AttachToGroup(companyID);

            name = MyGroupEntity.company.Name + " " + EnumUtils.GetFormattedNicheName(nicheType);

            CompanyUtils.Rename(GameContext, companyID, name);
        }
    }

    void AttachToGroup(int companyID)
    {
        CompanyUtils.AttachToGroup(GameContext, MyGroupEntity.company.Id, companyID);
    }

    void SetMyselfAsCEO(int companyID)
    {
        CompanyUtils.BecomeCEO(GameContext, companyID);
    }
}
