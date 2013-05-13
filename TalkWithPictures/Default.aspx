<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TalkWithPictures.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">
                <li class="active"><a href="default.aspx">Home</a></li>
                <li><a href="about.aspx">About</a></li>
                <li><a href="#">Contact</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH2" runat="server">

      <div class="jumbotron">
        <h1>Return An Image</h1>
        <p class="lead">A web API for returning a dyanmic image</p>
      </div>

      <hr>

      <div class="row-fluid marketing">    
        <div class="span6">
          <h4>A Dinosaur</h4>
          <p>Construct a URL with any keyword you would like to find.</p>
          <blockquote><a href="dinosaur.png">http://rtn.pw/dinosaur.png</a></blockquote>

          <h4>A Dinosaur with LASERS!</h4>
          <p>Combine multiple keywords together with an 'underscore'.</p>
          <blockquote><a href="dinosaur_with_lasers.png">http://rtn.pw/dinosaur_with_lasers.png</a></blockquote>

          <h4>A Dinosaur with LASERS! (again)!</h4>
          <p>Append a result number for another match. (up to 50)</p>
          <blockquote><a href="dinosaur_with_lasers.png?1">http://rtn.pw/dinosaur_with_lasers.png?1</a></blockquote>

        </div>

        <div class="span6">
          <form class="form-inline">
            <h4>Try it!</h4>
            <p>Enter a keyword!</p>
            <blockquote>http://rtn.pw/ <input type="text" size="10" /> .png</blockquote>
          </form>


        </div>
      </div>

</asp:Content>
