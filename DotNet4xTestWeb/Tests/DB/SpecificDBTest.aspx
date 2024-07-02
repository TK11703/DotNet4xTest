<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpecificDBTest.aspx.cs" Inherits="DotNet4xTestWeb.Tests.DB.SpecificDBTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %> - Specific DB Test
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    Specific DB
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item"><a href="/default.aspx">Home</a></li>
    <li class="breadcrumb-item"><a href="/Tests/DB/default.aspx">DB Testing Options</a></li>
    <li class="breadcrumb-item active" aria-current="page">Specific DB Test</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">A command from the selectable list can be executed against the specified database on the SQL resource, but the connection to the database is made based on the identity selection. Select a command type & identity option and then click execute. The result of the selections will be output in the space below.</p>

    <div class="row mb-3">
        <div class="col-4">
            <h5>Choose An Command</h5>
        </div>
        <div class="col-8">
            <div class="form-check">
                <input class="form-check-input" type="radio" name="CommandType" id="GetDate">
                <label class="form-check-label" for="GetDate">
                    select getdate()
                </label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="CommandType" id="GetAllUsers_CMD">
                <label class="form-check-label" for="GetAllUsers_CMD">
                    select * from customer (DB Project Required)
                </label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="CommandType" id="GetAllUsers_SP">
                <label class="form-check-label" for="GetAllUsers_SP">
                    exec spCustomer_GetAll (DB Project Required)
                </label>
            </div>
            <asp:HiddenField ID="CommandSelection" runat="server" Value="" />
        </div>
    </div>
    <div class="row mb-3">
        <div class="col-4">
            <h5>Choose An Identity</h5>
        </div>
        <div class="col-8">
            <div class="form-check">
                <input class="form-check-input" type="radio" name="IdentityType" id="WebIdentity">
                <label class="form-check-label" for="WebIdentity">
                    Website Identity (Managed Identity | Pool Identity)
                </label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="IdentityType" id="UserIdentity">
                <label class="form-check-label" for="UserIdentity">
                    Authenticated User Identity
                </label>
            </div>
            <asp:HiddenField ID="IdentitySelection" runat="server" Value="" />
            <asp:Button ID="SubmitSelectionButton" runat="server" Text="Execute" CssClass="btn btn-primary mt-2 mb-2" OnClick="SubmitSelectionButton_Click" />
        </div>
    </div>
    <div id="ResultsView" runat="server" visible="false">
        <div class="row mb-3">
            <asp:Label ID="executedAsLabel" runat="server" Text="Executed As:" CssClass="col-sm-2 col-form-label"></asp:Label>
            <div class="col-sm-10">
                <asp:TextBox ID="executedAs" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="alert alert-warning" role="alert">
                <h5>The result of the test are displayed below:</h5>
                <asp:Literal ID="dbResult" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <br />
    <div>
        <asp:Label ID="serverVariableResultsLabel" runat="server" Text="Server Variables:"></asp:Label>
        <asp:Literal ID="serverVariableResults" runat="server"></asp:Literal>
        <a href="ServerVariables.aspx" title="Click to view all server variable information.">View all server variables...</a>
    </div>
    <br />
    <div>
        <a class="btn btn-secondary d-inline-flex align-items-center" href="default.aspx" title="Go back to the available tests.">
            <svg height="16" width="16" fill="currentColor" class="me-1">
                <use href="#arrow-left-circle"></use>
            </svg>
            <span>Back to Tests</span>
        </a>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("input[type=radio][name='CommandType']").change(function () {
                var selected = $(this).attr('id');
                $('#PageBodyContent_CommandSelection').val(selected);
            });
            $("input[type=radio][name='IdentityType']").change(function () {
                var selected = $(this).attr('id');
                $('#PageBodyContent_IdentitySelection').val(selected);
            });
            CheckIfValuePresent($('#PageBodyContent_CommandSelection'), $("input[type=radio][name='CommandType']"));
            CheckIfValuePresent($('#PageBodyContent_IdentitySelection'), $("input[type=radio][name='IdentityType']"));
        });

        function CheckIfValuePresent(hiddenField, radioButtons) {
            if (hiddenField.val() !== '') {
                $(radioButtons).each(function () {
                    if ($(this).attr('id') == hiddenField.val()) {
                        $(this).prop("checked", true);
                    }
                });
            }
        }
    </script>
</asp:Content>
