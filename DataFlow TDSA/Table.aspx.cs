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
            CarregaClientes();
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
            string valor = hiddenID.Value;
            Cliente cliente = new Cliente();
            cliente.DeletarCliente(int.Parse(valor));
            CarregaClientes();
        }

        protected void EditarCliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            bool ativo = bool.Parse(hiddenAtivo.Value) == false? false : true;
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
                HtmlGenericControl tr = new HtmlGenericControl("tr");
                for (int i = 0; i < 5; i++)
                {
                    HtmlGenericControl td = new HtmlGenericControl("td");

                    switch (i)
                    {
                        case 0:
                            {
                                td.ID = $"Cliente{cliente.CLI_ID}";
                                td.InnerText = cliente.CLI_ID.ToString();
                                break;
                            }
                        case 1:
                            {
                                td.InnerText = cliente.CLI_NOME;
                                break;
                            }
                        case 2:
                            {
                                td.InnerText = cliente.CLI_DATANASCIMENTO.ToString("yyyy-MM-dd");
                                break;
                            }
                        case 3:
                            {
                                HtmlGenericControl div = new HtmlGenericControl("div");
                                div.Attributes["class"] = "form-check form-switch d-flex justify-content-center";

                                HtmlInputCheckBox check = new HtmlInputCheckBox();
                                check.Attributes["class"] = "form-check-input";
                                check.Attributes["disabled"] = "true";

                                if (cliente.CLI_ATIVO)
                                {
                                    check.Attributes["checked"] = "checked";
                                }

                                div.Controls.Add(check);
                                td.Controls.Add(div);

                                break;
                            }

                        case 4:
                            {
                                LinkButton btnEditar = new LinkButton();
                                btnEditar.ClientIDMode = ClientIDMode.Static;
                                btnEditar.Attributes["AutoPostBack"] = "true";
                                btnEditar.ID = $"btnEditar{cliente.CLI_ID}";
                                //anchorEditar.Click += EditarCliente_Click;
                                btnEditar.Attributes["onclick"] = "event.preventDefault();"; 
                                btnEditar.Attributes["type"] = "submit";
                                btnEditar.Attributes["class"] = "btn btn-warning btn-circle btnSelectRow btnEditar";

                                HtmlGenericControl iconEditar = new HtmlGenericControl("i");
                                iconEditar.Attributes["class"] = "fas fa-edit";
                                
                                
                                //Criar metodo para adicionar um botão e a class dele
                                LinkButton btnDeletar = new LinkButton();
                                btnDeletar.ClientIDMode = ClientIDMode.Static;
                                btnDeletar.Attributes["AutoPostBack"] = "true";
                                btnDeletar.ID = $"btnDeletar{cliente.CLI_ID}";
                                btnDeletar.Click += DeletarCliente_Click;
                                btnDeletar.Attributes["type"] = "submit";
                                btnDeletar.Attributes["class"] = "btn btn-danger btn-circle btnSelectRow btnDeletar";

                                HtmlGenericControl iconDeletar = new HtmlGenericControl("i");
                                iconDeletar.Attributes["class"] = "fas fa-trash";

                                LinkButton btnConfirmar = new LinkButton();
                                btnConfirmar.ClientIDMode = ClientIDMode.Static;
                                btnConfirmar.Attributes["AutoPostBack"] = "true";
                                btnConfirmar.ID = $"btnConfirmar{cliente.CLI_ID}";
                                btnConfirmar.Click += EditarCliente_Click;
                                btnConfirmar.Attributes["type"] = "submit";
                                btnConfirmar.Attributes["class"] = "btn btn-success btn-circle btnSelectRow btnConfirmar d-none";
                                
                                HtmlGenericControl iconConfirmar = new HtmlGenericControl("i");
                                iconConfirmar.Attributes["class"] = "fas fa-save";

                                LinkButton btnCancelar = new LinkButton();
                                btnCancelar.ClientIDMode = ClientIDMode.Static;
                                btnCancelar.Attributes["AutoPostBack"] = "true";
                                btnCancelar.ID = $"btnCancelar{cliente.CLI_ID}";
                                //anchorEditar.Click += EditarCliente_Click;
                                btnCancelar.Attributes["onclick"] = "event.preventDefault();";
                                btnCancelar.Attributes["type"] = "submit";
                                btnCancelar.Attributes["class"] = "btn btn-danger btn-circle btnSelectRow btnCancelar d-none";

                                HtmlGenericControl iconCancelar = new HtmlGenericControl("i");
                                iconCancelar.Attributes["class"] = "fas fa-times";

                                btnEditar.Controls.Add(iconEditar);
                                btnDeletar.Controls.Add(iconDeletar);
                                btnConfirmar.Controls.Add(iconConfirmar);
                                btnCancelar.Controls.Add(iconCancelar);

                                td.Controls.Add(btnEditar);
                                td.Controls.Add(btnConfirmar);
                                td.Controls.Add(btnDeletar);
                                td.Controls.Add(btnCancelar);

                                break;
                            }
                    }

                    tr.Controls.Add(td);
                }

                tableClientes.Controls.Add(tr);

                
            }
            return listaClientes;
        }
    }
}