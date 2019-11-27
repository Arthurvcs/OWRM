function excluirTarefa(id_tarefa,user) {

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
                url: 'https://localhost:44372/ExcluirTarefa',
                data: {
                    idTarefa: id_tarefa,
                    u: user
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

function IniciarTarefa(id_tarefa,user) {
    $.ajax({
        type: "POST",
        url: 'https://localhost:44372/IniciarTarefa',
        data: {
            idTarefa: id_tarefa,
            u: user
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
function FinalizarTarefa(id_tarefa,user) {
    $.ajax({
        type: "POST",
        url: 'https://localhost:44372/PararTarefa',
        data: {
            idTarefa: id_tarefa,
            u: user
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
function AdicionarTarefa(user) {
    var titulo = $('#tituloTarefa').val();
    var descricao = $('#descricaoTarefa').val();

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
            url: 'https://localhost:44372/AddTarefa',
            data: {
                titulo: titulo,
                descricao: descricao,
                u: user
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

function EditarTarefa(id_tarefa,user) {
    var titulo;
    var descricao;
    $.ajax({
        type: "POST",
        url: 'https://localhost:44372/ListarTarefas',
        async: false,
        data: {
            idTarefa: id_tarefa,
            u: user
        },
        dataType: "JSON",
        success: function (result) {
            titulo = result.TITULO;
            descricao = result.DESCRICAO;
        },
        error: function (result) {
            Swal.fire(
                'Ops! :S',
                result,
                'error'
            );
        }
    });
    $('#tituloTarefaEdt').text(titulo);
    $('#descricaoTarefaEdt').text(descricao);
    $('.edt').attr('id', id_tarefa);

}

function SalvarTarefaEditada(user) {
    var titulo = $('#tituloTarefaEdt').val();
    var descricao = $('#descricaoTarefaEdt').val();
    var id = $('.edt').attr('id');


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
            url: 'https://localhost:44372/SalvarTarefa',
            async: false,
            data: {
                idTarefa: id,
                titulo: titulo,
                descricao: descricao,
                u: user
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