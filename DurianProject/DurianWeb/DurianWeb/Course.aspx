<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SiteDefault.master"
    AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="DurianWeb.Course" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="UserControl/CourseOverview.ascx" tagname="CourseOverview" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="course-container">
        <div class="course-box player-wide">
        <div class="course-info course-defaults">
        <h1>
            <asp:Literal ID="lblCourseName" runat="server"></asp:Literal></h1>
        </div>
            <div class="player-box">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/17-10-2557 22-49-16.png" />
            </div>
            <div class="clear" style="height: 10px">
            </div>
            <div class="course-nav">
                <asp:Repeater ID="rpCourseTopic" runat="server" DataSourceID="dsCourseTopic">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HiddenField ID="hdnCourseTopicId" runat="server" Value='<%# Eval("CourseTopicId") %>' />
                            <asp:Label ID="lblCourseTopic" runat="server" Text='<%# Bind("CourseTopic") %>' Font-Bold="true"></asp:Label>
                            <asp:SqlDataSource ID="dsCourseDetail" runat="server" ConnectionString="<%$ ConnectionStrings:durianDbConnectionString %>"
                                SelectCommand="SELECT * FROM [CourseDetail] WHERE ([CourseTopicId] = @CourseTopicId)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hdnCourseTopicId" Name="CourseTopicId" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>                                
                            </asp:SqlDataSource>
                            <asp:Repeater ID="rpCourseDetail" runat="server" DataSourceID="dsCourseDetail">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="lnkCourseDetail" runat="server" Text='<%# Eval("CourseDetail") %>'/>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul></FooterTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul></FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="dsCourseTopic" runat="server" ConnectionString="<%$ ConnectionStrings:durianDbConnectionString %>"
                    SelectCommand="SELECT [CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder] FROM [CourseTopic] WHERE ([CourseId] = @CourseId) ORDER BY [CourseTopicOrder]">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="1" Name="CourseId" QueryStringField="CourseId"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
            <div class="course-extras">
                <asp:TabContainer ID="TabContainer1" runat="server" CssClass="yui" Width="100%">
                    <asp:TabPanel runat="server" HeaderText="Course details" ID="TabPanel1">
                        <ContentTemplate>
                            
                            <uc1:CourseOverview ID="CourseOverview1" runat="server" />
                            
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Transcript" ID="TabPanel2">
                        <ContentTemplate>
                            s
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="FAQs" ID="TabPanel3">
                        <ContentTemplate>
                            s
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
            <div class="course-suggestions course-defaults">
            </div>
        </div>
    </div>
</asp:Content>
