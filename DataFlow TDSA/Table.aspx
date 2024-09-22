<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="DataFlow_TDSA.Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <link href="css/table.css" rel="stylesheet">

    <!-- Custom styles for this page -->
    <link href="vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <div id="content-wrapper" class="d-flex flex-column">
        <div id="content">
            <div class="container-fluid">
                <div class="card-body">
                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConfirmAdd" EventName="ServerClick" />
                            <asp:AsyncPostBackTrigger ControlID="toggleAtivos" EventName="CheckedChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="col-12 p-0">
                                <div class="col-12 d-flex align-items-center p-0">
                                    <button type="button" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm mt-3 mb-3 btnFormInsereCliente">Adicionar Cliente</button>
                                    <label for="toggleAtivos" class="ml-4 mr-4 mb-0">Mostrar inativos</label>
                                    <div class="form-check form-switch d-flex col-1 align-items-center">
                                        <asp:CheckBox ID="toggleAtivos" ClientIDMode="Static" runat="server" OnCheckedChanged="toggleAtivos_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <aside id="asideInsereCliente">
                                    <section class="col-4 mb-3 d-flex justify-content-between">
                                        <div class="form-floating">
                                            <input id="txbNome" clientidmode="static" type="text" class="form-control" runat="server" placeholder="Nome" />
                                            <label for="txbNome">Nome</label>
                                        </div>
                                        <div class="form-floating">
                                            <input id="datePicker" clientidmode="static" type="date" class="form-control" runat="server" placeholder="" />
                                            <label for="datePicker">Data</label>
                                        </div>
                                        <div class="d-flex justify-content-around align-items-center col-3">
                                            <button id="btnConfirmAdd" clientidmode="static" autopostback="True" type="button" runat="server" onserverclick="InserirCliente_Click" class="btn btn-success btn-circle"><i class="fas fa-check"></i></button>
                                            <a id="btnCancelCliente" class="btn btn-danger btn-circle"><i class="fas fa-times"></i></a>
                                        </div>
                                    </section>
                                </aside>
                            </div>

                            <div class="table-responsive">
                                <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>NOME</th>
                                            <th>DATA NASC.</th>
                                            <th>ATIVO</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <asp:HiddenField ID="hiddenID" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hiddenNome" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hiddenData" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hiddenAtivo" ClientIDMode="Static" runat="server" />
                                    <tbody runat="server" id="tableClientes" clientidmode="static">
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
