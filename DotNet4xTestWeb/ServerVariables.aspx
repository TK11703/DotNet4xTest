<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServerVariables.aspx.cs" Inherits="DotNet4xTestWeb.ServerVariables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.siteTitle %> - Server Variables
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    Server Variables
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">Listed below you will find all the server variables, which provide information about the server, the client's connections, and the current HTTP request.</p>
    <asp:Repeater ID="ServerVariableDetails" runat="server" OnItemDataBound="ServerVariableDetails_OnItemDataBound">
        <ItemTemplate>
            <div class="card mb-2">
                <h4 class="card-header">
                    <asp:Label ID="VariableIDLabel" CssClass="bold" runat="server" Text='<%# Container.DataItem.ToString() %>'></asp:Label></h4>
                <div class="card-body">
                    <div class="wordwrap">
                        <asp:Label ID="VariableValueLabel" runat="server" Text='<%# Request.ServerVariables[Container.DataItem.ToString()] %>'></asp:Label>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <div class="mb-3">
        <a class="btn btn-secondary d-inline-flex align-items-center" href="default.aspx" title="Go back to the available tests.">
            <svg height="16" width="16" fill="currentColor" class="me-1">
                <use href="#arrow-left-circle"></use>
            </svg>
            <span>Back to Tests</span>
        </a>
    </div>
</asp:Content>
