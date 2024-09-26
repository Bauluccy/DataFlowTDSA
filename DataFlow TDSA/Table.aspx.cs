using DataFlow_TDSA.App_Code.BAS;
using DataFlow_TDSA.App_Code.Dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DataFlow_TDSA
{
    public partial class Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaClientes();

            }
            else
            {
                CarregaClientes();
            }
                
        }

        protected void InserirCliente_Click(object sender, EventArgs e)
        {
            DaoCliente daoCliente = new DaoCliente();
            if (!String.IsNullOrEmpty(txbNome.Value) && !String.IsNullOrEmpty(datePicker.Value))
            {
                daoCliente.InsereCliente(txbNome.Value, DateTime.Parse(datePicker.Value));

                CarregaClientes();
            }
        }

        protected void DeletarCliente_Click(object sender, EventArgs e)
        {
            string id = hiddenID.Value;
            Cliente cliente = new Cliente();
            cliente.DeletarCliente(int.Parse(id));
            CarregaClientes();
        }

        protected void EditarCliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            bool ativo = bool.Parse(hiddenAtivo.Value) == false ? false : true;
            cliente.EditarCliente(int.Parse(hiddenID.Value), hiddenNome.Value, DateTime.Parse(hiddenData.Value), ativo);
            CarregaClientes();
        }

        private List<Cliente> CarregaClientes()
        {
            tableClientes.Controls.Clear();
            Cliente controllerCliente = new Cliente();
            List<Cliente> listaClientes = controllerCliente.CarregarClientes();

            foreach (var cliente in listaClientes)
            {
                if (cliente.CLI_ATIVO || (toggleAtivos.Checked && !cliente.CLI_ATIVO))
                {
                    HtmlGenericControl tr = new HtmlGenericControl("tr");
                    for (int i = 0; i < 5; i++)
                    {
                        HtmlGenericControl td = new HtmlGenericControl("td");

                        switch (i)
                        {
                            case 0:
                                td.InnerText = cliente.CLI_ID.ToString();
                                td.Attributes["class"] = "textID";
                                break;

                            case 1:
                                td.InnerText = cliente.CLI_NOME;
                                break;

                            case 2:
                                td.InnerText = cliente.CLI_DATANASCIMENTO.ToString("yyyy-MM-dd");
                                break;

                            case 3:
                                HtmlGenericControl div = new HtmlGenericControl("div");
                                div.Attributes["class"] = "form-check form-switch d-flex justify-content-center";

                                HtmlInputCheckBox check = new HtmlInputCheckBox();
                                check.Attributes["class"] = "form-check-input";
                                check.Attributes["disabled"] = "true";
                                check.Attributes["checked"] = cliente.CLI_ATIVO ? "checked" : null;

                                div.Controls.Add(check);
                                td.Controls.Add(div);
                                break;

                            case 4:
                                HtmlGenericControl divBtns = new HtmlGenericControl("div");
                                divBtns.Attributes["class"] = "col-3 d-flex justify-content-around";

                                CriarBotoes(divBtns, cliente);
                                td.Controls.Add(divBtns);
                                break;
                        }

                        tr.Controls.Add(td);
                    }

                    tableClientes.Controls.Add(tr);
                }
            }
        }

        private void CriarBotoes(HtmlGenericControl divBtns, Cliente cliente)
        {
            LinkButton btnEditar = new LinkButton
            {
                ClientIDMode = ClientIDMode.Static,
                ID = $"btnEditar{cliente.CLI_ID}",
                Text = "Editar"
            };
            btnEditar.Attributes["onclick"] = "event.preventDefault();";
            btnEditar.Attributes["class"] = "btn btn-warning btn-circle btnSelectRow btnEditar";
            btnEditar.Controls.Add(new HtmlGenericControl("i") { Attributes = { ["class"] = "fas fa-edit" } });

            LinkButton btnDeletar = new LinkButton
            {
                ClientIDMode = ClientIDMode.Static,
                ID = $"btnDeletar{cliente.CLI_ID}",
                Text = "Deletar",
                Attributes = { ["class"] = "btn btn-danger btn-circle btnSelectRow btnDeletar" }
            };
            btnDeletar.Click += DeletarCliente_Click;
            btnDeletar.Controls.Add(new HtmlGenericControl("i") { Attributes = { ["class"] = "fas fa-trash" } });

            LinkButton btnConfirmar = new LinkButton
            {
                ClientIDMode = ClientIDMode.Static,
                ID = $"btnConfirmar{cliente.CLI_ID}",
                Text = "Confirmar",
                Attributes = { ["class"] = "btn btn-success btn-circle btnSelectRow btnConfirmar d-none" }
            };

            btnConfirmar.Click += EditarCliente_Click;
            btnConfirmar.Controls.Add(new HtmlGenericControl("i") { Attributes = { ["class"] = "fas fa-save" } });

            LinkButton btnCancelar = new LinkButton
            {
                ClientIDMode = ClientIDMode.Static,
                ID = $"btnCancelar{cliente.CLI_ID}",
                Text = "Cancelar",
                Attributes = { ["class"] = "btn btn-danger btn-circle btnSelectRow btnCancelar d-none"},
            };
            btnCancelar.Attributes["onclick"] = "event.preventDefault();";
            btnCancelar.Controls.Add(new HtmlGenericControl("i") { Attributes = { ["class"] = "fas fa-times" } });

            divBtns.Controls.Add(btnEditar);
            divBtns.Controls.Add(btnConfirmar);
            divBtns.Controls.Add(btnDeletar);
            divBtns.Controls.Add(btnCancelar);
        }

        protected void toggleAtivos_CheckedChanged(object sender, EventArgs e)
        {
            CarregaClientes();
        }
    }
}