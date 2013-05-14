<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TalkWithPictures.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">
    <li class="active"><a href="default.aspx">Home</a></li>
    <li><a href="about.aspx">About</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH2" runat="server">

    <div class="jumbotron">
        <h1>Return An Image</h1>
        <p class="lead">A web API for returning a dynamic image</p>
    </div>

    <hr>


    <h4>A Dinosaur</h4>
    <p>Construct a URL with any keyword you would like to find.</p>
    <blockquote><a href="dinosaur.png" target="_blank">http://rtn.pw/dinosaur.png</a></blockquote>

    <h4>A Dinosaur with LASERS! <small>Thanks Ryan!</small></h4>
    <p>Combine multiple keywords together with an 'underscore'.</p>
    <blockquote><a href="dinosaur_with_lasers.png" target="_blank">http://rtn.pw/dinosaur_with_lasers.png</a></blockquote>

    <h4>A Dinosaur with LASERS! (again)!</h4>
    <p>Append a result number for another match. (up to 50)</p>
    <blockquote><a href="dinosaur_with_lasers.png?1" target="_blank">http://rtn.pw/dinosaur_with_lasers.png?1</a></blockquote>

    <h4>Try it!</h4>
    <p>Enter some keywords!</p>
    <blockquote>
        <form class="form-inline">http://rtn.pw/
            <input type="text" />
            .png</form>
    </blockquote>

    <h4>Coming soon...</h4>
    <p>Specify image size or dimensions</p>
    <p>Animated GIFS!</p>

</asp:Content>
