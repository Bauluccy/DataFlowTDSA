$(document).ready(function () {
    $('#dataTable').DataTable();

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
        $('#dataTable').DataTable();

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

        //ProcuraID();

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
        $('.btnDeletar').css({ display: 'none' });
        $('.btnEditar').css({ display: 'none' });
        $('.btnConfirmar').addClass("d-none");
        $('.btnCancelar').addClass("d-none");

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
                        value: cellValue,
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

        row.find('.btnConfirmar').removeClass("d-none");
        row.find('.btnCancelar').removeClass("d-none");
    }
});


