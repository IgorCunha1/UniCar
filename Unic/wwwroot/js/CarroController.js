var app = angular.module('Unic', ['ui.bootstrap','ui.bootstrap.tpls']);

app.factory('serviceCarro', ['$http', function ($http) {
    var service = {};

    service.ListarCarros = function () {
        return $http.get('Carro/ListarCarro');
    }

    service.AdicionarCarro = function (carro) {
        return $http.post('Carro/AdicionarCarro', carro)
    }

    service.AdicionarEndereco = function (endereco) {
        return $http.post('AdicionarEndereco', endereco)
    }

    service.EditarCarro = function (id, Carro) {
        return $http.post('Carro/EditarCarro/'+id, Carro)
    }

    service.EditarEndereco = function (id, endereco) {
        return $http.post('EditarEndereco/'+id, endereco)
    }

    service.DeletarCarro = function(id){
        return $http.post('Carro/Deletar/'+id)
    }

    service.PesquisaCep = function(cep){
        return $http.get('https://viacep.com.br/ws/'+cep+'/json/')
    }

    service.SelecionarCarro = function(id){
        return $http.get('Carro/RecuperarCarro/'+id);
    }

    service.CarregarMarcas = function(){
        return $http.get('Carro/verificarMarcas');
    }
    service.CarregarModelos = function(id){
        return $http.get(`Carro/verificarModelosMarcas/`+id);
    }
    service.CarregarAnos = function (codMarca) {
        return $http.get(`Carro/verificarAnosModelo/`+codMarca);
    }
    service.verificarPlaca = function(placa){
        return $http.post('https://sinesp.contrateumdev.com.br/api', placa);
    }

    service.valorFipe = function (idMarca,idModelo,idAnos) {
        return $http.get('https://parallelum.com.br/fipe/api/v1/carros/marcas/' + idMarca + '/modelos/' + idModelo + '/anos/' + idAnos+'');
    }

    return service;
}]);


app.controller('CarroController', ['$scope', 'serviceCarro', '$http', function ($scope, serviceCarro, $http) {
    
    $scope.Carros = {};
    
    function init() {
        $scope.Carros = $scope.ListarCarros();
    }

      $scope.ListarCarros = function () {
        
        serviceCarro.ListarCarros().then(function (response) {
            $scope.Carros = response.data;
            $scope.totalItems = $scope.Carros.length;           
       });
    }

        
    $scope.verificarPlaca = function(){
        let placa = document.getElementById('placaa').value;
         serviceCarro.verificarPlaca(placa).then(function (response){
            
         })
    }

    $scope.CarregarMarcas = function () {
        serviceCarro.CarregarMarcas().then(function(response){
            $scope.marcas = response.data;
        });
    };

    $scope.CarregarModelos = function(id){
         serviceCarro.CarregarModelos(id).then(function(response){
        });
    }

    var select = document.querySelector('#Marca');
    var carro = {};
    $scope.Anos = {}
            
      select.addEventListener('change', function(){
        carro = JSON.parse(select.value);
           serviceCarro.CarregarModelos(carro).then(function(response){
               $scope.modelos = response.data;
               
           })
      });
    var selectModelo = document.querySelector('#Modelo');
    var m = {};
    selectModelo.addEventListener('change', function () {
        m = JSON.parse(selectModelo.value);
        serviceCarro.CarregarAnos(m).then(function (response) {
            $scope.AnosModelos = response.data;
        })
    });

    $scope.quantidade = 10;

    $scope.DeletarCarro = function (id) {

        Swal.fire({
            title: 'Você gostaria de deletar esse carro?',
            text: "Após a deleção não será possivel desfazer essa ação!",
            icon: 'warning',
            confirmButtonText: 'Sim, Deletar este carro',
            cancelButtonText: 'Cancelar',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33'
            
        }).then((result) => {
            if (result.isConfirmed) {
                serviceCarro.DeletarCarro(id).then(function (response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'warning',
                        title: 'Carro Deletado Com Sucesso',
                        showConfirmButton: false,
                        timer: 15000
                    })
                    $scope.ListarCarros();
                })
            }
        })

    }
  
    $scope.exibirEditar = function(){
        document.getElementById('botaoSalvar').style.display = 'none';
        document.getElementById('botaoEditar').style.display = 'block';
    }

    $scope.exibirSalvar = function(){
        $scope.LimparCampo()
        document.getElementById('botaoSalvar').style.display = 'block';
        document.getElementById('botaoEditar').style.display = 'none';
        $scope.CarregarMarcas();
    }

    $scope.SelecionarCarro = function(id){
        serviceCarro.SelecionarCarro(id).then(function (response){
            $('#movimentarCarro').modal('show');
            $scope.CarroId = response.data.id;
            $scope.exMarca = response.data.marca.nome;
            $scope.exModelo = response.data.modelo.nome;
            $scope.exAnoModelo = response.data.anoModelo.nome;
            $scope.exKM = response.data.km;
            $scope.exDescricao = response.data.descricao;
            $scope.exPrecoVenda = response.data.precoVenda;
            $scope.exPrecoCompra = response.data.precoCompra;
            $scope.exPessoaCompradora = response.data.pessoaCompradora;
            $scope.exPessoaVendedora = response.data.pessoaVendedora;
            $scope.exStatus = response.data.status;
            $scope.exPlaca = response.data.placa;
            $scope.exManutencoes = response.data.manutencoes;
            $scope.exValorTotalManutencao = response.data.valorTotalManutencao;

            $scope.calcCusto = $scope.exPrecoCompra + $scope.exValorTotalManutencao;
            
            serviceCarro.valorFipe(response.data.marca.codigo, response.data.modelo.codigo, response.data.anoModelo.codigo).then(function (response){
                $scope.fipe = response.data.Valor;
            })
        })
    }

    $(document).ready(function () {
        $("#imprimir").click(function () {
            //get the modal box content and load it into the printable div
            $(".printable").html($("#movimentarCarro").html());
            $(".printable").printThis();
        });
    });

    $scope.lucro = 0;
    var inputVenda = document.querySelector('#PrecoVenda');
    inputVenda.addEventListener('change', function () {
        $scope.lucro = 0;
        if (inputVenda.value.replace(/[^a-z0-9]/gi, '') <= 0) {
            $scope.lucro = 0;
            
        } else {
            $scope.lucro = inputVenda.value.replace(/[^a-z0-9]/gi, '') - $scope.calcCusto;
        }
        
    });
    var btnAlerta = document.getElementById('alerta');
    $scope.AdicionarCarro = function (){
        $scope.Carro = {};
        
        if ($scope.formAdc.$valid == false) {

            $('#alerta').fadeIn(2000, function () {
                setTimeout($('#alerta').fadeOut(2000), 4000);
            });

        } else {
            var placa = $scope.Placa.replace(/[^a-z0-9]/gi, '');
            var precoCompra = $scope.PrecoCompra.replace(/[^a-z0-9]/gi, '');
         btnAlerta.style.display = 'none';
         $scope.Carro.MarcaId = $scope.Marca;
         $scope.Carro.ModeloId = $scope.Modelo;
         $scope.Carro.AnoModeloId = $scope.AnoModelo;

         $scope.Carro.KM = $scope.KM;
         $scope.Carro.Descricao = $scope.Descricao;
            $scope.Carro.PrecoCompra = precoCompra ;
         $scope.Carro.PessoaVendedora = $scope.PessoaVendedora;
         $scope.Carro.Status = "1";
            $scope.Carro.Placa = placa;
         
        
        serviceCarro.AdicionarCarro($scope.Carro).then(function successCallback(response) {

                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Carro Salva Com Sucesso',
                        showConfirmButton: false,
                        timer: 15000
                      })
                     $scope.ListarCarros();
                     $scope.LimparCampo();
          }, function errorCallback(response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: response.data,
                        showConfirmButton: false,
                        timer: 15000
                      })
                     $scope.ListarCarros();
          });
       }
  }

    $scope.EditarCarro = function(){
        $scope.Carro = {};
        
        $scope.Carro.id = $scope.CarroId
        $scope.Carro.Marca = $scope.exMarca;
        $scope.Carro.Modelo = $scope.exModelo;
        $scope.Carro.Placa = $scope.exPlaca;
        $scope.Carro.KM = $scope.exKM;
        $scope.Carro.AnoFabricacao = $scope.exAnoFabricacao;
        $scope.Carro.AnoModelo = $scope.exAnoModelo;
        $scope.Carro.Descricao = $scope.exDescricao;
        $scope.Carro.PrecoVenda = $scope.exPrecoVenda;
        $scope.Carro.PessoaCompradora = $scope.exPessoaCompradora;
        $scope.Carro.Status = $scope.exStatus;

            serviceCarro.EditarCarro($scope.Carro.id, $scope.Carro).then(function(response){
                
            })
    }


    $scope.LimparCampo = function(){
        $scope.Marca = '';
        $scope.Modelo = '';
        $scope.AnoFabricacao = '';

        $scope.AnoModelo = '';
        $scope.KM = '';
        $scope.Descricao = '';
        $scope.PrecoVenda = '';
        $scope.PrecoCompra = '';
        $scope.PessoaCompradora = '';
        $scope.PessoaVendedora = '';
        $scope.Status = '';
        $scope.Placa = '';
        

    }
    
    $scope.orderByMe = function(x) {
        $scope.myOrderBy = x;
      }

      init();
}]);