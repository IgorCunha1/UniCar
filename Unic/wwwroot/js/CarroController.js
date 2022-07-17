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
    service.verificarPlaca = function(placa){
        return $http.post('https://sinesp.contrateumdev.com.br/api', placa)
    }

    return service;
}]);


app.controller('CarroController', ['$scope', 'serviceCarro', function ($scope, serviceCarro) {
    
    $scope.Carros = {
        custos: {}
    };
    
    $scope.viewby = 3;
    $scope.currentPage = 1;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 5; 
    
    function init() {
        $scope.Carros = $scope.ListarCarros();
    }

      $scope.ListarCarros = function () {
        console.log($scope.Carros);
        serviceCarro.ListarCarros().then(function (response) {
            $scope.Carros = response.data;
            $scope.totalItems = $scope.Carros.length;
            
            //Number of pager buttons to show
 
             $scope.setPage = function (pageNo) {
                 $scope.currentPage = pageNo;
             };
 
             $scope.pageChanged = function() {
                 console.log('Page changed to: ' + $scope.currentPage);
             };
 
             $scope.setItemsPerPage = function(num = 3) {
                $scope.itemsPerPage = num;
                $scope.currentPage = 1; //reset to first paghe
             }
            
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
            
      select.addEventListener('change', function(){
        carro = JSON.parse(select.value);
           serviceCarro.CarregarModelos(carro.codigo).then(function(response){
               $scope.modelos = response.data.modelos;
             
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

        serviceCarro.SelecionarCarro(id).then(function (response){
            $scope.CarroId = response.data.id;
            console.log(response);
            $scope.exMarca = response.data.marca;
            $scope.exModelo = response.data.modelo;
            $scope.exAnoFabricacao = response.data.anoFabricacao;
            $scope.exAnoModelo = response.data.anoModelo;
            $scope.exKM = response.data.kM;
            $scope.exDescricao = response.data.descricao;
            $scope.exPrecoVenda = response.precoVenda;
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

         $scope.Carro.marca = carro.nome;
         $scope.Carro.modelo = $scope.Modelo;
         $scope.Carro.anoFabricacao = $scope.AnoFabricacao;
         $scope.Carro.anoModelo = $scope.AnoModelo;
         $scope.Carro.km = $scope.KM;
         $scope.Carro.descricao = $scope.Descricao;
         $scope.Carro.precoVenda = precoVenda;
         $scope.Carro.precoCompra = precoCompra;
         $scope.Carro.pessoaCompradora = $scope.PessoaCompradora;
         $scope.Carro.pessoaVendedora = $scope.PessoaVendedora;
         $scope.Carro.status = $scope.Status;
         $scope.Carro.placa = $scope.Placa;
        
         

             serviceCarro.AdicionarCarro($scope.Carro).then(function (response){
                 console.log(response)
                 Swal.fire({
                     position: 'center',
                     icon: 'success',
                     title: 'Carro Salva Com Sucesso',
                     showConfirmButton: false,
                     timer: 15000
                   })
                 $scope.ListarCarros();
                //  $scope.LimparCampo();

             })
    }

    $scope.EditarCarro = function(){
        $scope.Carro = {};
        
        $scope.Carro.id = $scope.CarroId
        $scope.Carro.Marca = $scope.Marca;
        $scope.Carro.Modelo = $scope.Modelo;
        $scope.Carro.AnoFabricacao = $scope.AnoFabricacao;
        $scope.Carro.AnoModelo = $scope.AnoModelo;
        $scope.Carro.KM = $scope.KM;
        $scope.Carro.Descricao = $scope.Descricao;
        $scope.Carro.PrecoVenda = $scope.PrecoVenda;
        $scope.Carro.PrecoCompra = $scope.PrecoCompra;
        $scope.Carro.PessoaCompradora = $scope.PessoaCompradora;
        $scope.Carro.PessoaVendedora = $scope.PessoaVendedora;
        $scope.Carro.Status = $scope.Status;

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