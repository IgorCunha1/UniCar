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
        return $http.get('https://parallelum.com.br/fipe/api/v1/carros/marcas');
    }
    service.CarregarModelos = function(id){
        return $http.get(`https://parallelum.com.br/fipe/api/v1/carros/marcas/`+id+`/modelos`);
    }
    service.CarregarAnos = function (codMarca, codModelo) {
        return $http.get(`https://parallelum.com.br/fipe/api/v1/carros/marcas/` + codMarca+`/modelos/` + codModelo + `/anos`);
    }
    service.verificarPlaca = function(placa){
        return $http.post('https://sinesp.contrateumdev.com.br/api', placa)
    }

    return service;
}]);


app.controller('CarroController', ['$scope', 'serviceCarro', '$http', function ($scope, serviceCarro, $http) {
    
    $scope.Carros = {};
    
    function init() {
        $scope.Carros = $scope.ListarCarros();
    }

      $scope.ListarCarros = function () {
        console.log($scope.Carros);
        serviceCarro.ListarCarros().then(function (response) {
            $scope.Carros = response.data;
            $scope.totalItems = $scope.Carros.length;           
       });
    }

        
    $scope.verificarPlaca = function(){
        let placa = document.getElementById('placaa').value;
        //  alert(placa);
         serviceCarro.verificarPlaca(placa).then(function (response){
            console.log(response);
         })
    }

    $scope.CarregarMarcas = function(){
        serviceCarro.CarregarMarcas().then(function(response){
            $scope.marcas = response.data;
            
        });
    };

    $scope.CarregarModelos = function(id){
        console.log(id);
         serviceCarro.CarregarModelos(id).then(function(response){
              console.log(response.data);
        });
    }

    var select = document.querySelector('#Marca');
    var carro = {};
    $scope.Anos = {}
            
      select.addEventListener('change', function(){
        carro = JSON.parse(select.value);
           serviceCarro.CarregarModelos(carro.codigo).then(function(response){
               $scope.modelos = response.data.modelos;
               /*$scope.Anos = response.data.anos;*/
               
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
        $scope.exibirEditar();
        console.log(id)        
        serviceCarro.SelecionarCarro(id).then(function (response){
            $('#movimentarCarro').modal('show');
            $scope.CarroId = response.data.id;
            // console.log(response);
            $scope.exMarca = response.data.marca;
            $scope.exModelo = response.data.modelo;
            $scope.exAnoFabricacao = response.data.anoFabricacao;
            $scope.exAnoModelo = response.data.anoModelo;
            $scope.exKM = response.data.km;
            $scope.exDescricao = response.data.descricao;
            $scope.exPrecoVenda = response.data.precoVenda;
            $scope.exPrecoCompra = response.data.precoCompra;
            $scope.exPessoaCompradora = response.data.pessoaCompradora;
            $scope.exPessoaVendedora = response.data.pessoaVendedora;
            $scope.exStatus = response.data.status;
            $scope.exPlaca = response.data.placa;
        })
    }

    $scope.AdicionarCarro = function (){
        
        $scope.Carro = {};
         var precoVenda = $scope.PrecoVenda.replace(/[^a-z0-9]/gi, '');
         var precoCompra = $scope.PrecoCompra.replace(/[^a-z0-9]/gi, '');
         var placa = $scope.Placa.replace(/[^a-z0-9]/gi, '');

         $scope.Carro.Marca = carro.nome;
         $scope.Carro.Modelo = $scope.Modelo;
         $scope.Carro.AnoModelo = $scope.AnoModelo;
         $scope.Carro.KM = $scope.KM;
         $scope.Carro.Descricao = $scope.Descricao;
         $scope.Carro.PrecoCompra = precoCompra;
         $scope.Carro.PessoaVendedora = $scope.PessoaVendedora;
         $scope.Carro.Status = "1";
         $scope.Carro.Placa = placa;
         
        $http({
            method: "POST",
            url: "https://localhost:44305/Carro/AdicionarCarro",
            data: $scope.Carro
        }).then(function successCallback(response) {

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
                console.log(response);
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
        

    }
    
    $scope.orderByMe = function(x) {
        $scope.myOrderBy = x;
      }

      init();
}]);