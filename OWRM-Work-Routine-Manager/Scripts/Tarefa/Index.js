function excluirTarefa(id_tarefa) {

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
                url: 'ExcluirTarefa',
                data: {
                    idTarefa: id_tarefa
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

function IniciarTarefa(id_tarefa) {
    $.ajax({
        type: "POST",
        url: 'IniciarTarefa',
        data: {
            idTarefa: id_tarefa
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
function FinalizarTarefa(id_tarefa) {
    $.ajax({
        type: "POST",
        url: 'PararTarefa',
        data: {
            idTarefa: id_tarefa
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
function AdicionarTarefa() {
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
            url: 'AddTarefa',
            data: {
                titulo: titulo,
                descricao: descricao
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

function EditarTarefa(id_tarefa) {
    var titulo;
    var descricao;
    $.ajax({
        type: "GET",
        url: 'ListarTarefas',
        async: false,
        data: {
            id_tarefa: id_tarefa
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

function SalvarTarefaEditada() {
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
            url: 'SalvarTarefa',
            async: false,
            data: {
                idTarefa: id,
                titulo: titulo,
                descricao: descricao
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