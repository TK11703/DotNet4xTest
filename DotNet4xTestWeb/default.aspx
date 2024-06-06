<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DotNet4xTestWeb._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.siteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.siteTitle %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This application will assist engineers and developers in testing a new site space. It should test the processing capability and database connectivity of the site. It will assist in confirming or identifying configuration issues.</p>
    <div class="row text-center">
        <div class="col">
            <h4>Test Master DB?</h4>
        </div>
        <div class="col">
            <h4>Test Specific DB?</h4>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <p>Select this option to test the connectivity between the site and the SQL Master DB.</p>
        </div>
        <div class="col">
            <p>Select this option to test the connectivity between the site and the configured SQL DB.</p>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <p class="text-center"><a class="btn btn-success" href="MasterDBTest.aspx">Test</a></p>
        </div>
        <div class="col">
            <p class="text-center"><a class="btn btn-success" href="SpecificDBTest.aspx">Test</a></p>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="span12">
            <p class="lead">Would you like to view the server variables?</p>
            <p>Select the following link to view the server variables associated with the requests to the web server.</p>
            <p><a href="ServerVariables.aspx" title="Click to view the server variables.">View all server variables...</a></p>
        </div>
    </div>

</asp:Content>
