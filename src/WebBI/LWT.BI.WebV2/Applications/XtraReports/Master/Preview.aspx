<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="Applications_XtraReports_Master_Preview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input runat="server" id="hdReportID" type="hidden" enableviewstate="true" />
    <script type="text/javascript">
        function reportDesigner_CustomizeMenuActions(s, e) {
            var actions = e.Actions;
            var designInWizard = e.GetById(DevExpress.Designer.Report.ActionId.ReportWizard);
            var newViaWizard = e.GetById(DevExpress.Designer.Report.ActionId.NewReportViaWizard);
            var addDataSource = e.GetById(DevExpress.Designer.Report.ActionId.AddMultiQuerySqlDataSource);
            //if (designInWizard)
            //    designInWizard.visible = false;
            //if (newViaWizard)
            //    newViaWizard.visible = false;
            if (addDataSource)
                addDataSource.visible = false;
        }


        function DisableDataSourceEditing(designer) {
            if (designer.designerModel && designer.designerModel.fieldListActionProviders) {
                var fieldListActionProviders = designer.GetDesignerModel().fieldListActionProviders;
                var parameterActionProvider = fieldListActionProviders.filter(function (item) { return item instanceof DevExpress.Designer.Report.SqlDataSourceEditor })[0];
                fieldListActionProviders.splice(fieldListActionProviders.indexOf(parameterActionProvider), 1);
            }
            //if(reportDesigner.designerModel && reportDesigner.designerModel.fieldListActionProviders) {
            //    var fieldListActionProviders = reportDesigner.designerModel.fieldListActionProviders;
            //    var dataSourceActionsProvider = fieldListActionProviders.filter(function(item) { return item instanceof DevExpress.Designer.Report.SqlDataSourceEditor })[0];
            //    fieldListActionProviders.splice(fieldListActionProviders.indexOf(dataSourceActionsProvider), 1);
            //}

        }

        function ReportDesigner_Init(s, e) {
            DisableDataSourceEditing(s);

            $.each(DevExpress.Designer.Report.controlsFactory.controlsMap, function (_, control) {
                if (control.popularProperties) {
                    removePopularPropertyInfo(control.popularProperties, "dataSource");
                    removePopularPropertyInfo(control.popularProperties, "dataMember");
                    removePopularPropertyInfo(control.popularProperties, "filterString");
                }
            });
        }

        function removePopularPropertyInfo(array, name) {
            var index = array.indexOf(name);
            if (index >= 0) array.splice(index, 1);
        }
    </script>

    <dx:ASPxReportDesigner ID="reportDesigner" runat="server" SettingsWizard-UseMasterDetailWizard="false"
        ClientInstanceName="reportDesigner">

<%--        <ClientSideEvents Init="ReportDesigner_Init" CustomizeMenuActions="reportDesigner_CustomizeMenuActions" />--%>



    </dx:ASPxReportDesigner>

    <%--     <dx:ASPxWebDocumentViewer ID="reportViewer" runat="server">
        </dx:ASPxWebDocumentViewer>--%>
</asp:Content>
