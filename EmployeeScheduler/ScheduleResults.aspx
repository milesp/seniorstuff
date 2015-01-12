<%@ Page Title="" Language="C#" MasterPageFile="~/ESMasterPage.Master" AutoEventWireup="true" CodeBehind="ScheduleResults.aspx.cs" Inherits="EmployeeScheduler.ScheduleResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DropDownList ID="ddlDisplayOptions" runat="server">
        <asp:ListItem Text="Version1" Value="Version1" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Version2" Value="Version2"></asp:ListItem>
        <asp:ListItem Text="AllData" Value="AllData"></asp:ListItem>
        <asp:ListItem Text="InputData" Value="InputData"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="bChangeView" runat="server" OnClick="bChangeView_Click" Text="Change View" />
    <br />
    <br />
    <div>
        <asp:Repeater ID="rInputData" runat="server">
            <HeaderTemplate>
                <table border="1" cellpadding="3">
                    <tr>
                        <td align="right">Day</td>
                        <td align="right">Shift</td>
                        <td align="right">Employees per Shift</td>
                        <td align="right">Experience Minimum Per Shift</td>
                        <td align="right">Experience Maximum</td>
                        <td align="right">Wage Avg Per Shift</td>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate></table></FooterTemplate>
            <ItemTemplate>
                <asp:Repeater ID="Repeater4" runat="server" DataSource='<%# Eval("shiftsInDay") %>'>
                    <ItemTemplate>
                        <tr>
                            <td align="right">
                                <%# Eval("DayName") %>
                            </td>
                            <td align="right">
                                <%# Eval("ShiftName") %>
                            </td>
                            <td align="right">
                                <%# Eval("EmployeeTotal") %>
                            </td>
                            <td align="right">
                                <%# Eval("ExpMin") %>
                            </td>
                            <td align="right">
                                <%# Eval("ExpMax") %>
                            </td>
                            <td align="right">
                                <%# Eval("WageAvg") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div>
        <asp:Repeater ID="rEmployerOutput" runat="server">
            <HeaderTemplate>
                <table border="1" cellpadding="3">
                    <tr>
                        <th>Day</th>
                        <th>Shift</th>
                        <th>Employees Scheduled to the Shift</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate></table></FooterTemplate>
            <ItemTemplate>
                <asp:Repeater ID="Repeater6" runat="server" DataSource='<%# Eval("shiftsInDay") %>'>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("DayName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Repeater ID="Repeater7" runat="server" DataSource='<%# Eval("ScheduledEmployees") %>'>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("Name") %>' Font-Size="14pt"></asp:Label>,
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Repeater ID="rEmployeeOutput" runat="server">
            <HeaderTemplate>
                <table border="1" cellpadding="3">
                    <tr>
                        <th>Name</th>
                        <th>Employee Number</th>
                        <th>Scheduled Shifts</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate></table></FooterTemplate>

            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("EmployeeNumber") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Repeater ID="Repeater7" runat="server" DataSource='<%# Eval("AssignedShiftsStack") %>'>
                            <ItemTemplate>
                                (<asp:Label ID="Label11" runat="server" Text='<%# Eval("Day") %>' Font-Size="14pt"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ShiftNumber") %>' Font-Size="14pt"></asp:Label>)
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
