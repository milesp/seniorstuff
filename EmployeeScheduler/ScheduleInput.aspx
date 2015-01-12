<%@ Page Title="" Language="C#" MasterPageFile="~/ESMasterPage.Master" AutoEventWireup="true" CodeBehind="ScheduleInput.aspx.cs" Inherits="EmployeeScheduler.ScheduleInput" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div>
            <asp:Button ID="Button1" runat="server" Text="Shift Creator" OnClick="Button1_Click" />
            <asp:TextBox ID="shiftsPerDay" runat="server" placeholder="Shifts per day"></asp:TextBox>
            <asp:TextBox ID="numOfDays" runat="server" placeholder="Days per work period"></asp:TextBox>

        </div>
        <div>


            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>Day number </td>
                            <td>Shift number:</td>
                            <td>Shifts per day:</td>
                            <td>Employees per Shift</td>
                            <td>Experience Average</td>
                            <td>Experience Minimum</td>
                            <td>Experience Maximum</td>
                            <td>Wage Average</td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate></table></FooterTemplate>
                <ItemTemplate>
                    <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("shiftsInDay") %>'>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Day") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ShiftNumber") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox6"
                                        Text='<%# Eval("ShiftsPerDay") %>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox3"
                                        Text='<%# Eval("EmployeeTotal") %>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox4"
                                        Text='<%# Eval("ExpAvg") %>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox1"
                                        Text='<%# Eval("ExpMin") %>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox2"
                                        Text='<%# Eval("ExpMax") %>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox5"
                                        Text='<%# Eval("WageAvg") %>' />
                                </td>

                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>

            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Submit Work Week Constraints" Visible="False" />
            <br />

        </div>
        <div>
            <table>
                <tr>
                    <td>Name</td>
                    <td>Social Security Number</td>
                    <td>Wage</td>
                    <td>Experience</td>
                    <td>Minimum Shifts</td>
                    <td>Maximum Shifts</td>
                </tr>

                <tr>

                    <td>
                        <asp:TextBox ID="EmpName" runat="server"></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="EmpSSN" runat="server"></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="EmpWage" runat="server"></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="EmpExp" runat="server"></asp:TextBox>
                    </td>

                    <td>
                        <asp:TextBox ID="EmpShiftMin" runat="server"></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="EmpShiftMax" runat="server"></asp:TextBox></td>

                </tr>
            </table>
        </div>
        <p>
            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        </p>
        <p>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Display Output" />
        </p>
</asp:Content>
