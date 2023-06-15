using System;
using jQueryApi;
using W6.ClickMobile.Web.ObjectModel;
using W6.ClickMobile.Web.AddIns.AddInsInterfaces;
using W6.ClickMobile.Web.AddIns.ClientServices;
using System.Html;

namespace Client.Buttons
{
    public class GoToAssignment : IW6CMInlineWidget
    {
        // global click mobile controller service
        private static IW6CMControllerServices ControllerServices;

        public Element InitInlineWidget(IW6CMControllerServices objControllerServices, W6CMObject item, int displayMode, string filterName)
        {
            // assign controller services object from the controller that's getting passed into the inline widget when click task collection is launched in CM.
            ControllerServices = objControllerServices;
            
            #region html for task collection inline button
            const string DivClass = "W6CMPanel W6CMPropPanelButton";
            const string TableStyle = "width: 100%;";
            const string TableDataWidth = "100%";
            const string TableDataStyle = "vertical-align:top;";
            string ButtonID = "btn" + item.Key;
            string OnClick = "Client.Buttons.GoToAssignment.openAssigment(" + item.Key + ")";
            const string ButtonClass = "button1 darkBlueButton highlightEnable";
            const string ButtonStyle = "width:100px;height:30px;border-radius:8px;";
            const string ButtonText = "Open";
            
            const string HTML = "<div><div class='{0}'><table style='{1}'><tbody><tr><td width='{2}' style='{3}'><div objname='ButtonPanel'><button id={4} onClick={5} objname='ButtonControl' class='{6}' style='{7}'>{8}</button></div></td></tr></tbody></table></div></div>";
            string htmlContent = String.Format(HTML, DivClass, TableStyle, TableDataWidth, TableDataStyle, ButtonID, OnClick, ButtonClass, ButtonStyle, ButtonText);
            #endregion
            
            jQueryObject e = jQuery.FromHtml(htmlContent);
            
            return e.GetElement(0);
        }
        public void SetInlineWidgetSettings(string strInlineWidgetSettings)
        {
            return;
        }

        public static void OpenAssigment(int taskKey)
        {
            Script.Literal("window.event.stopPropagation()");
            ControllerServices.GetAssignmentByTaskKey(taskKey, delegate (W6.ClickMobile.Web.ObjectModel.W6CMAssignment assignment)
            {
                try
                {
                    ControllerServices.OpenFormByKey(0, assignment.Key, false, successCallback);
                }
                catch (Exception ex)
                {
                    ControllerServices.ErrorLog(ex);
                }

            });
        }

        private static void successCallback(OpenFormStatus eOpenFormSuccess)
        {
            return;
        }
    }
}
