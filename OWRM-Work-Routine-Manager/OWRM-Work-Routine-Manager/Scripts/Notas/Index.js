var hexDigits = new Array
    ("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f");

function selecionarCor(cor) {
    $('.adicionar').css({ "background-color": cor });
    $('.editar').css({ "background-color": cor });

}

function rgb2hex(rgb) {
    rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
    return "#" + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]);
}

function hex(x) {
    return isNaN(x) ? "00" : hexDigits[(x - x % 16) / 16] + hexDigits[x % 16];
}

function SalvarNota() {
    var titulo = $('#tituloNota').val();
    var descricao = $('#descricaoNota').val();
    var rgb = $('.adicionar').css("background-color");
    var cor = rgb2hex(rgb);

    if (titulo === undefined || titulo === null || descricao === undefined || descricao === null || titulo === "" || titulo === "") {
        Swal.fire(
            'Opa!',
            'Preencha todos os campos para prosseguir',
            'warning'
        );
    }
    else {
        $.ajax({
            type: "POST",
            url: 'AddNota',
            data: {
                titulo: titulo,
                descricao: descricao,
                cor: cor
            },
            dataType: "JSON",
            success: function (result) {
                Swal.fire({
                    title: 'Tudo certo! :)',
                    text: result,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then((result) => {
                    if (result.value) {
                        window.location.reload();
                    }
                });

                $("#btnFechar").trigger("click");
            },
            error: function (result) {
                Swal.fire(
                    'Ops! :S',
                    result,
                    'error'
                );
            }
        });
    }
}
function ExcluirNota(id_nota) {

    Swal.fire({
        title: 'Você tem certeza?',
        text: "Você não poderá reverter essa ação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, pode deletar!',
        cancelButtonText: 'Cancelar'

    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: 'ExcluirNota',
                data: {
                    idNota: id_nota
                },
                dataType: "JSON",
                success: function (result) {
                    Swal.fire({
                        title: 'Tudo certo! :)',
                        text: result,
                        type: 'success',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        if (result.value) {
                            window.location.reload();
                        }
                    });
                },
                error: function (result) {
                    Swal.fire(
                        'Ops! :S',
                        result,
                        'error'
                    );
                }
            });
        }
    });
}
function EditarNota(id_nota) {
    var titulo, descricao, cor;

    $.ajax({
        type: "GET",
        url: 'ListarNotas',
        async: false,
        data: {
            IdNota: id_nota
        },
        dataType: "JSON",
        success: function (result) {
            titulo = result.TITULO;
            descricao = result.DESCRICAO;
            cor = result.COR;
        },
        error: function (result) {
            Swal.fire(
                'Ops! :S',
                result,
                'error'
            );
        }
    });

    $('#tituloNotaEdt').text(titulo);
    $('#descricaoNotaEdt').text(descricao);
    $('.editar').css("background-color", cor);
    $('.edt').attr('id', id_nota);
}

function SalvarNotaEditada() {
    var titulo = $('#tituloNotaEdt').val();
    var descricao = $('#descricaoNotaEdt').val();
    var id = $('.edt').attr('id');
    var rgb = $('.editar').css("background-color");
    var cor = rgb2hex(rgb);

    if (titulo === undefined || titulo === null || descricao === undefined || descricao === null || titulo === "" || titulo === "") {
        Swal.fire(
            'Opa!',
            'Preencha todos os campos para prosseguir',
            'warning'
        );
    }
    else {
        $.ajax({
            type: "POST",
            url: 'SalvarNota',
            async: false,
            data: {
                idnota: id,
                titulo: titulo,
                descricao: descricao,
                cor: cor
            },
            dataType: "JSON",
            success: function (result) {
                Swal.fire({
                    title: 'Tudo certo! :)',
                    text: result,
                    type: 'success',
                    confirmButtonText: 'Ok'
                }).then((result) => {
                    if (result.value) {
                        window.location.reload();
                    }
                });

                $("#btnFechar").trigger("click");
            },
            error: function (result) {
                Swal.fire(
                    'Ops! :S',
                    result,
                    'error'
                );
            }
        });
    }
}

