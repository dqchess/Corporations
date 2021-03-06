﻿using Assets.Core;

public class CreateAppPopupButton : PopupButtonController<PopupMessageDoYouWantToCreateApp>
{
    public override void Execute()
    {
        NicheType nicheType = Popup.NicheType;

        var id = Companies.CreateProductAndAttachItToGroup(Q, nicheType, MyCompany);

        NotificationUtils.ClosePopup(Q);
        NotificationUtils.AddPopup(Q, new PopupMessageCreateApp(id));
    }

    public override string GetButtonName() => "YES";
}
