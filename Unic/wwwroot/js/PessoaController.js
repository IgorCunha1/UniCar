
var app = angular.module('Unic', []);

app.factory('servicePessoa', ['$http', function ($http) {
    var service = {};

    service.ListarPessoas = function () {
        return $http.get('Pessoa/ListarPessoa');
    }

    service.AdicionarPessoa = function (pessoa) {
        return $http.post('Pessoa/AdicionarPessoa', pessoa)
    }

    service.AdicionarEndereco = function (endereco) {
        return $http.post('AdicionarEndereco', endereco)
    }

    service.EditarPessoa = function (id, pessoa) {
        return $http.post('Pessoa/EditarPessoa/'+id, pessoa)
    }

    service.EditarEndereco = function (id, endereco) {
        return $http.post('EditarEndereco/'+id, endereco)
    }

    service.DeletarPessoa = function(id){
        return $http.post('Pessoa/Deletar/'+id)
    }

    service.PesquisaCep = function(cep){
        return $http.get('https://viacep.com.br/ws/'+cep+'/json/')
    }

    service.SelecionarPessoa = function(id){
        return $http.get('Pessoa/RecuperarPessoa/'+id);
    }

    service.EditarPessoa = function(id, pessoa){

    }

    return service;
}]);


app.controller('PessoaController', ['$scope', '$http', 'servicePessoa', function ($scope, $http, servicePessoa) {

    $scope.ListarPessoas = function () {
        servicePessoa.ListarPessoas().then(function (response) {
            $scope.Pessoas = response.data;
            
        });
    }
    $scope.DeletarPessoa = function(id){
        servicePessoa.DeletarPessoa(id).then(function (response){
            
        })
    }
    $scope.pesquisaCep = function (cep){
         servicePessoa.PesquisaCep(cep).then(function (response){
            $scope.end = response.data;
             $scope.logradouro = $scope.end.logradouro;
             $scope.bairro = $scope.end.bairro;
             $scope.cidade = $scope.end.localidade;
             $scope.estado = $scope.end.uf;
            console.log($scope.end);
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
    }

    $scope.SelecionarPessoa = function(id){
        $scope.exibirEditar();

        servicePessoa.SelecionarPessoa(id).then(function (response){
            $scope.enderecoId = response.data.endereco.id;
            $scope.pessoaId = response.data.id;
            console.log(response.data);
            $scope.nomeCompleto = response.data.nomeCompleto;
            $scope.cpf = response.data.cpf;
            $scope.logradouro = response.data.endereco.logradouro;
            $scope.cep = response.data.endereco.cep;
            $scope.numero = response.data.endereco.numero;
            $scope.bairro = response.data.endereco.bairro;
            $scope.cidade = response.data.endereco.cidade;
            $scope.estado = response.data.endereco.estado;
        })
    }

    $scope.AdicionarPessoa = function (){
        
        $scope.pessoa = {};
        let data = new Date($scope.nascimento);
        $scope.pessoa.nomeCompleto = $scope.nomeCompleto;
        $scope.pessoa.cpf = $scope.cpf.replace(/[^a-z0-9]/gi, '');
        // $scope.pessoa.nascimento = data.toLocaleDateString('EN',{timezone: 'UTC'});
        console.log(data.toLocaleDateString('EN',{timezone: 'UTC'}));        
        
        $scope.endereco = {};
        $scope.endereco.logradouro = $scope.logradouro;
        $scope.endereco.cep = $scope.cep.replace(/[^a-z0-9]/gi,'');
        $scope.endereco.numero = $scope.numero;
        $scope.endereco.bairro = $scope.bairro;
        $scope.endereco.cidade = $scope.cidade;
        $scope.endereco.estado = $scope.estado;
        console.log($scope.endereco);

        servicePessoa.AdicionarEndereco($scope.endereco).then(function(response) {
            
            $scope.pessoa.enderecoId = response.data.id;
            
            servicePessoa.AdicionarPessoa($scope.pessoa).then(function (response){
                
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Pessoa Salva Com Sucesso',
                    showConfirmButton: false,
                    timer: 15000
                  })

                $scope.LimparCampo();

            })
        })
    }

    $scope.EditarPessoa = function(){
        $scope.pessoa = {};
        
        $scope.pessoa.id = $scope.pessoaId
        $scope.pessoa.nomeCompleto = $scope.nomeCompleto;
        $scope.pessoa.cpf = $scope.cpf.replace(/[^a-z0-9]/gi, '');

        $scope.endereco = {};
        $scope.endereco.id = $scope.enderecoId;
        $scope.endereco.logradouro = $scope.logradouro;
        $scope.endereco.cep = $scope.cep.replace(/[^a-z0-9]/gi,'');
        $scope.endereco.numero = $scope.numero;
        $scope.endereco.bairro = $scope.bairro;
        $scope.endereco.cidade = $scope.cidade;
        $scope.endereco.estado = $scope.estado;

        servicePessoa.EditarEndereco($scope.endereco.id,  $scope.endereco).then( function(response){
                console.log(response);
            servicePessoa.EditarPessoa($scope.pessoa.id, $scope.pessoa).then(function(response){
                console.log(response);
            })
        })

    }


    $scope.LimparCampo = function(){
        $scope.nomeCompleto = '';
        $scope.cpf = '';
        $scope.nascimento = '';

        $scope.cep = '';
        $scope.logradouro = '';
        $scope.numero = '';
        $scope.bairro = '';
        $scope.cidade = '';
        $scope.estado = '';

    }


}]);