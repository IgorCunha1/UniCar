var app = angular.module('Unic', ['ui.bootstrap','ui.bootstrap.tpls']);

app.factory('serviceManutencao', ['$http', function ($http) {
    var service = {};


    service.ListarManutencao = function (carroId) {
        return $http.get('Manutencao/ListarManutencao/'+carroId);
    }

    service.AdicionarManutencao = function (manutencao) {
        return $http.post('Manutencao/AdicionarManutencao', manutencao)
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
    
    $scope.Manutencoes = {};
    $scope.Carro = {};

    function init() {
        
    }

    $scope.AdicionarManutencao = function(){
        $scope.Manutencao = {};
        $scope.Manutencao.descricao = $scope.DescricaoManutencao;
        $scope.Manutencao.origem = $scope.OrigemManutencao;
        $scope.Manutencao.valor = $scope.ValorManutencao.replace(/[^a-z0-9]/gi, '');
        $scope.Manutencao.carroId = $scope.CarroId;
        console.log($scope.Manutencao);
        serviceManutencao.AdicionarManutencao($scope.Manutencao).then(function (response){
            $scope.LimparCampo();
            $scope.ListarManutencao($scope.Manutencao.carroId);
            $scope.SelecionarCarroPorPlaca();
        })
    }
    
    $scope.ListarManutencao = function (carroId) {
        serviceManutencao.ListarManutencao(carroId).then(function (response) {
            $scope.Manutencoes = response.data;
            
            $scope.ValorTotalManutencao = $scope.Carro.valorTotalManutencao;
        });
    }

        
    $scope.SelecionarCarroPorPlaca = function(){
        let placa = $('#Placa').val().replace(/[^a-z0-9]/gi, '');
         serviceManutencao.SelecionarCarroPorPlaca(placa).then(function (response){
             $scope.Carro = response.data;
             console.log($scope.Carro);
             $scope.Marca = $scope.Carro.marca;
             $scope.Modelo = $scope.Carro.modelo;
             $scope.Ano = $scope.Carro.anoModelo;
             $scope.KM =$scope.Carro.km;
             $scope.Descricao = $scope.Carro.descricao;
             $scope.PrecoCompra = $scope.Carro.precoCompra;
             $scope.CarroId = $scope.Carro.id;
             $scope.Manutencoes = $scope.Carro.manutencoes;
             $scope.ValorTotalManutencao = $scope.Carro.valorTotalManutencao;
     })
    }

    $scope.DeletarManutencao = function (id) {
        
        Swal.fire({
            title: 'Você gostaria de deletar essa manutenção?',
            text: "Após a deleção não será possivel desfazer essa ação!",
            icon: 'warning',
            confirmButtonText: 'Sim, Deletar esta Manutenção',
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
                        title: 'Manutenção Deletada Com Sucesso',
                        showConfirmButton: false,
                        timer: 15000
                    })
                    $scope.ListarManutencao($scope.Carro.id);
                    $scope.SelecionarCarroPorPlaca();
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
       $scope.DescricaoManutencao = '';
       $scope.OrigemManutencao = '';
       $scope.ValorManutencao = '';
    }
    
    $scope.orderByMe = function(x) {
        $scope.myOrderBy = x;
      }

    init();     
      
}]);