$(document).ready(function () {
    $('#toggleAtivos').addClass("form-check-input");

    $('#dataTable').DataTable({
        columnDefs: [
            {
                targets: 2,
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        let date = new Date(data);
                        // Usar UTC para obtener el día, mes y año correctos
                        let day = String(date.getUTCDate()).padStart(2, '0');
                        let month = String(date.getUTCMonth() + 1).padStart(2, '0');
                        let year = date.getUTCFullYear();
                        return `${day}/${month}/${year}`;
                    }
                    return data;
                }
            }
        ]
    });

    $('.btnEditar').on('click', function () {
        const row = $(this).closest('tr');
        AlterarTable(row);
    });

    $('.btnConfirmar').on('click', function () {
        var row = $(this).closest('tr');

        setTimeout(function () {
            var id = row.find('#txbEditaID').val();
            var nome = row.find('#txbEditaNome').val();
            var data = row.find('#txbDateEdit').val();
            var isChecked = row.find('#txbAtivoEdit').is(':checked');

            $('#hiddenID').val(id);
            $('#hiddenNome').val(nome);
            $('#hiddenData').val(data);
            $('#hiddenAtivo').val(isChecked ? 'true' : 'false');
            
        }, 0);
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
        $('#dataTable').DataTable({
            columnDefs: [
                {
                    targets: 2,
                    render: function (data, type, row) {
                        if (type === 'display' || type === 'filter') {
                            let date = new Date(data);
                            // Usar UTC para obtener el día, mes y año correctos
                            let day = String(date.getUTCDate()).padStart(2, '0');
                            let month = String(date.getUTCMonth() + 1).padStart(2, '0');
                            let year = date.getUTCFullYear();
                            return `${day}/${month}/${year}`;
                        }
                        return data;
                    }
                }
            ]
        });

        $('#toggleAtivos').addClass("form-check-input");

        $('.btnFormInsereCliente').on('click', function () {
            $('#asideInsereCliente').slideToggle();

            $('#txbNome').val('');
            $('#datePicker').val('');
        });

        $('.btnConfirmar').on('click', function () {
            var row = $(this).closest('tr');

            setTimeout(function () {
                var id = row.find('#txbEditaID').val();
                var nome = row.find('#txbEditaNome').val();
                var data = row.find('#txbDateEdit').val();
                var isChecked = row.find('#txbAtivoEdit').is(':checked');

                $('#hiddenID').val(id);
                $('#hiddenNome').val(nome);
                $('#hiddenData').val(data);
                $('#hiddenAtivo').val(isChecked ? 'true' : 'false');

            }, 0);
        });

        $('.btnEditar').on('click', function () {
            const row = $(this).closest('tr');
            AlterarTable(row);
        });
    });

    $('.btnFormInsereCliente').on('click', function () {
        $('#asideInsereCliente').slideToggle();

        $('#txbNome').val('');
        $('#datePicker').val('');
    });

    $('#btnCancelCliente').on('click', function () {
        closeSectionCliente();
    });

    function closeSectionCliente() {
        $('#asideInsereCliente').slideToggle();

        $('#txbNome').val('');
        $('#datePicker').val('');
    }



    let checkboxStates = {};

    function AlterarTable(row) {
        row.find('.btnDeletar').css({ display: 'none' });
        row.find('.btnEditar').css({ display: 'none' });
        row.find('.btnConfirmar').removeClass("d-none");
        row.find('.btnCancelar').removeClass("d-none");

        row.find('td').each(function (index) {
            const cell = $(this);
            const cellValue = cell.text();

            let input;

            switch (index) {
                case 0:
                    input = $('<input>', {
                        type: 'text',
                        value: cellValue,
                        class: 'form-control',
                        id: 'txbEditaID',
                        disabled: true
                    });
                    break;
                case 1:
                    input = $('<input>', {
                        type: 'text',
                        value: cellValue,
                        class: 'form-control',
                        id: 'txbEditaNome'
                    });
                    break;
                case 2:
                    input = $('<input>', {
                        type: 'date',
                        value: convertToISO(cellValue),
                        class: 'form-control',
                        id: 'txbDateEdit'
                    });
                    break;
                case 3:
                    const checkbox = $('<input>', {
                        type: 'checkbox',
                        value: cellValue,
                        class: 'form-check-input',
                        id: 'txbAtivoEdit',
                        checked: cell.find('input[type="checkbox"]').is(':checked')
                    });

                    checkbox.on('change', function () {
                        checkboxStates[cellValue] = this.checked;
                    });

                    input = $('<div>', { class: 'form-check form-switch d-flex justify-content-center' }).append(checkbox);
                    break;
            }

            cell.html(input);
        });

        row.find('.btnDeletar').css({ display: 'none' });
        row.find('.btnEditar').css({ display: 'none' });
    }

    function convertToISO(dateStr) {
        let parts = dateStr.split('/');
        return `${parts[2]}-${parts[1]}-${parts[0]}`;
    }

    function formatDate(dateStr) {
        let parts = dateStr.split('-');
        return `${String(parts[2]).padStart(2, '0')}/${String(parts[1]).padStart(2, '0')}/${parts[0]}`;
    }

    $('.btnCancelar').on('click', function () {
        const row = $(this).closest('tr');

        // Restaura os textos das células ao seu estado original
        row.find('td').each(function (index) {
            const cell = $(this);
            switch (index) {
                case 0:
                    // ID
                    cell.text(cell.find('#txbEditaID').val());
                    break;
                case 1:
                    // Nome
                    cell.text(cell.find('#txbEditaNome').val());
                    break;
                case 2:
                    // Data
                    const dateValue = cell.find('#txbDateEdit').val();
                    cell.text(formatDate(dateValue));
                    break;
                case 3:
                    // Checkbox
                    const isChecked = cell.find('#txbAtivoEdit').is(':checked');
                    const checkboxHtml = `<div class="form-check form-switch d-flex justify-content-center"> <input type="checkbox" class="form-check-input"  ${isChecked ? 'checked' : ''} disabled> </div>`;
                    cell.html(checkboxHtml);
                    break;
            }
        });

        // Volta a mostrar os botões "Deletar" e "Editar"
        row.find('.btnDeletar').css({ display: 'inline' });
        row.find('.btnEditar').css({ display: 'inline' });
        row.find('.btnConfirmar').addClass("d-none");
        row.find('.btnCancelar').addClass("d-none");
    });
});


