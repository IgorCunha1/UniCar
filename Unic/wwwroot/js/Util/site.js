
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



