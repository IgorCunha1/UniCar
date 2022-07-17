
$(document).ready(function () {
    $('#cpf').mask('000.000.000-00');
    $('#Cep').mask('00000-000');
    $('.money').mask('#.##0', { reverse: true });
    
});

$('#Placa').mask('AAA 0U00', {
  translation: {
      'A': {
          pattern: /[A-Za-z]/
      },
      'U': {
          pattern: /[A-Za-z0-9]/
      },
  },
  onKeyPress: function (value, e, field, options) {
      // Convert to uppercase
      e.currentTarget.value = value.toUpperCase();

      // Get only valid characters
      let val = value.replace(/[^\w]/g, '');

      // Detect plate format
      let isNumeric = !isNaN(parseFloat(val[4])) && isFinite(val[4]);
      let mask = 'AAA 0U00';
      if(val.length > 4 && isNumeric) {
          mask = 'AAA-0000';
      }
      $(field).mask(mask, options);
  }
});

function verificarCPF(c) {

    c = c.replace(/[^a-z0-9]/gi, '');
    var Soma;
    var Resto;
    Soma = 0;
    
  if (c == "00000000000") return console.log("falso");
  if (c == "11111111111") return console.log("falso");
  if (c == "22222222222") return console.log("falso");
  if (c == "33333333333") return console.log("falso");
  if (c == "44444444444") return console.log("falso");
  if (c == "55555555555") return console.log("falso");
  if (c == "66666666666") return console.log("falso");
  if (c == "77777777777") return console.log("falso");
  if (c == "88888888888") return console.log("falso");
  if (c == "99999999999") return console.log("falso");


  for (i=1; i<=9; i++) Soma = Soma + parseInt(c.substring(i-1, i)) * (11 - i);
  Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11))  Resto = 0;
    if (Resto != parseInt(c.substring(9, 10)) ) return console.log("falso");

  Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(c.substring(i-1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11))  Resto = 0;
    if (Resto != parseInt(c.substring(10, 11) ) ) return console.log("falso");
    return console.log("verdadeiro");
}


var DATATABLE = {
    destroy: function (seletor = '.tabelaDinamicaV2') {
        if ($.fn.DataTable.isDataTable(seletor)) {
            $(seletor).DataTable().destroy();
        };
    },
    initializeAjax: function (options) {

        options.seletor = options.seletor !== undefined ? options.seletor : ".tabelaDinamicaV2";
        this.destroy(options.seletor);

        grid = $(options.seletor).dataTable({
            bSort: !1, language: {
                sEmptyTable: "Nenhum registro encontrado", sInfo: "Mostrando de _START_ até _END_ de _TOTAL_ registros", sInfoEmpty: "Mostrando 0 até 0 de 0 registros", sInfoFiltered: "(Filtrados de _MAX_ registros)", sInfoPostFix: "", sInfoThousands: ".", sLengthMenu: "_MENU_ resultados por página", sLoadingRecords: "Carregando...", sProcessing: "Processando...", sZeroRecords: "Nenhum registro encontrado", sSearch: "Pesquisar", oPaginate: {
                    sNext: "Próximo", sPrevious: "Anterior", sFirst: "Primeiro", sLast: "Último"
                },
                oAria: {
                    sSortAscending: ": Ordenar colunas de forma ascendente", sSortDescending: ": Ordenar colunas de forma descendente"
                }
            },
            responsive: false,
            processing: false,
            serverSide: true,
            paging: true,
            ordering: options.ordering !== undefined ? options.ordering : false,
            pagingType: "simple_numbers",
            pageLength: options.size !== undefined ? options.size : 10,
            lengthMenu: [[10, 20, 40, 60, 80, 100, -1], [10, 20, 40, 60, 80, 100, "Todos"]],
            ajax: {
                method: "POST",
                url: options.url,
                beforeSend: function () {
                    LOADING.bloquear();
                },
                complete: function () {
                    LOADING.desbloquear();

                    if (options.loadboostrap_select !== undefined ? options.loadboostrap_select : true) {
                        CONFIGS.switch();
                    };
                }
            },
            columns: options.columns
        });

        $(".dataTables_filter input")
            .unbind() // Desvincular ligações padrão anteriores
            .bind("input", function (e) { // Vincule nosso comportamento desejado
                // Se o comprimento tiver 3 ou mais caracteres ou o usuário pressionar ENTER, pesquise
                if (this.value.length >= 3 || e.keyCode == 13) {
                    // Chame a função de pesquisa da API
                    grid.api().search(this.value).draw();
                }
                // Certifique-se de limpar a pesquisa se eles retrocederem o suficiente
                if (this.value == "") {
                    grid.api().search("").draw();
                }
                return;
            });
    },
    initialize: function (seletor = '.tabelaDinamicaV2', size = 10) {
        this.destroy(seletor);

        grid = $(seletor).dataTable({
            bSort: !1, language: {
                sEmptyTable: "Nenhum registro encontrado", sInfo: "Mostrando de _START_ até _END_ de _TOTAL_ registros", sInfoEmpty: "Mostrando 0 até 0 de 0 registros", sInfoFiltered: "(Filtrados de _MAX_ registros)", sInfoPostFix: "", sInfoThousands: ".", sLengthMenu: "_MENU_ resultados por página", sLoadingRecords: "Carregando...", sProcessing: "Processando...", sZeroRecords: "Nenhum registro encontrado", sSearch: "Pesquisar", oPaginate: {
                    sNext: "Próximo", sPrevious: "Anterior", sFirst: "Primeiro", sLast: "Último"
                }
                ,
                oAria: {
                    sSortAscending: ": Ordenar colunas de forma ascendente", sSortDescending: ": Ordenar colunas de forma descendente"
                }
            },
            responsive: !1,
            lengthMenu: [[10, 20, 40, 60, 80, 100, -1], [10, 20, 40, 60, 80, 100, "Todos"]],
            pageLength: size
        });

        $(seletor).on('page.dt', function () {
            setTimeout(function () {
                CONFIGS.switch();
            });
        });
    }
};

