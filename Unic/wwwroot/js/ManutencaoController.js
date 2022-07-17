var app = angular.module('Unic', ['ui.bootstrap','ui.bootstrap.tpls']);

app.factory('serviceManutencao', ['$http', function ($http) {
    var service = {};


    service.ListarManutencao = function (carroId) {
        return $http.get('Manutencao/ListarManutencao'+carroId);
    }

    service.AdicionarManutencao = function (manutencao) {
        return $http.post('Manutencao/AdicionarCarro', manutencao)
    }

    service.DeletarManutencao = function(id){
        return $http.post('Manutencao/Deletar/'+id)
    }

    service.SelecionarCarroPorPlaca = function(placa){
        return $http.get('Carro/RecuperarCarroPorPlaca/'+placa);
    }
    return service;
}]);


app.controller('ManutencaoController', ['$scope', 'serviceManutencao', function ($scope, serviceManutencao) {
    
    $scope.Manutencao = {};
    $scope.Carro = {};

    function init() {
        
    }
    
    $scope.ListarManutencao = function (carroId) {
        serviceManutencao.ListarCarros(carroId).then(function (response) {
            $scope.Manutencao = response.data;
            console.log($scope.Manutencao);
        });
    }

        
    $scope.SelecionarCarroPorPlaca = function(){
        let placa = $('#Placa').val();
         serviceManutencao.SelecionarCarroPorPlaca(placa).then(function (response){
             $scope.Carro = response.data;
             $scope.Marca = $scope.Carro.marca;
             $scope.Modelo = $scope.Carro.modelo;
             $scope.Ano = $scope.Carro.anoModelo;
             $scope.KM =$scope.Carro.km;
             $scope.Descricao = $scope.Carro.descricao;
             $scope.PrecoCompra = $scope.Carro.precoCompra;
             console.log($scope.Carro);
          })
    }

    $scope.DeletarManutencao = function (id) {

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
                serviceManutencao.DeletarManutencao(id).then(function (response) {
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
        

    }
    
    $scope.orderByMe = function(x) {
        $scope.myOrderBy = x;
      }

    init();     
      
}]);