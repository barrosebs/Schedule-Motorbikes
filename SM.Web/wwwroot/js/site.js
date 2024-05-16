// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    var cnpjElements = document.querySelectorAll(".cnpj-mask");
    cnpjElements.forEach(function (element) {
        var cnpj = element.textContent;
        element.textContent = formatarCNPJ(cnpj);
    });
});

function formatarCNPJ(cnpj) {
    // Remove caracteres indesejados
    cnpj = cnpj.replace(/[^\d]/g, "");

    // Aplica a máscara
    return cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, "$1.$2.$3/$4-$5");
}
function maskCNPJ(input) {
    const CPFNumber = input.value.replace(/\D/g, '');

    let formattedCPFNumber = '';
    for (let i = 0; i < CPFNumber.length; i++) {
        if (i === 2) {
            formattedCPFNumber += '.';
        } else if (i === 5) {
            formattedCPFNumber += '.';
        } else if (i === 8) {
            formattedCPFNumber += '/';
        } else if (i === 12) {
            formattedCPFNumber += '-';
        }
        formattedCPFNumber += CPFNumber[i];
    }

    input.value = formattedCPFNumber;
}
function IsNumber(input) {
    const IsNumber = input.value.replace(/\D/g, '');
    input.value = IsNumber;
}
function capitalizeWords(str) {
    return str.replace(/\b\w/g, function (char) {
        return char.toUpperCase();
    });
}

function filterMotocycle() {
    // Obtém o valor digitado no campo de busca
    var plate = document.getElementById('searchPlate').value.toUpperCase();

    // Obtém todas as linhas da tabela de motos
    var rows = document.querySelectorAll('tbody tr');

    // Itera sobre as linhas e mostra ou oculta de acordo com a placa digitada
    rows.forEach(function (row) {
        var columnPlate = row.querySelector('td:first-child').textContent.toUpperCase();
        if (columnPlate.indexOf(plate) > -1) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

// Adiciona um listener para o evento 'input' no campo de busca
document.getElementById('searchPlate').addEventListener('input', filterMotocycle);