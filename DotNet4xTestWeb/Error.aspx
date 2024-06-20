<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DotNet4xTestWeb.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.siteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.siteTitle %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This page has been shown to you, because an error occurred in the typical processing of the last request. Please see the details on this page for more information.</p>
    <div class="row">
        <div class="col">
            <asp:repeater runat="server" id="AlertRepeater">
                <ItemTemplate>
                    <div class="alert alert-danger" role="alert">
                        <p class="mb-3"><%# Eval("Message") %></p>
                        <asp:Literal runat="server" Text='<%# Eval("Debug") %>' />
                    </div>
                </ItemTemplate>
            </asp:repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
