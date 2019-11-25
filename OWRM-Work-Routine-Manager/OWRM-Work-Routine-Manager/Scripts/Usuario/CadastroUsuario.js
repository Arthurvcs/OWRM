function EditarUsuario(id) {
    var nome, matricua, login, role;

    $.ajax({
        type: "GET",
        url: 'https://localhost:44372/GetUsuario',
        async: false,
        data: {
            id_usuario: id
        },
        dataType: "JSON",
        success: function (result) {
            nome = result.NOME;
            matricua = result.MATRICULA;
            login = result.LOGIN;
        },
        error: function (result) {
            Swal.fire(
                'Ops! :S',
                result,
                'error'
            );
        }
    });

    $('#nomeUsuEdt').text(nome);
    $('#loginUsuEdt').text(login);
    $('#matriculaUsuEdt').text(matricua);

    $('.edt').attr('id', id);
}

function SalvarUsuario() {
    var nome, matricua, login;

    var nome = $('#nomeUsuAdd').val();
    var matricua = $('#matriculaUsuAdd').val();
    var login = $('#loginUsuAdd').val();
    var senha = $('#sehhaUsuAdd').val();

    if (nome === undefined || nome === null || matricua === undefined || matricua === null || login === "" || login === "" || senha === "" || senha === null) {
        Swal.fire(
            'Opa!',
            'Preencha todos os campos para prosseguir',
            'warning'
        );
    }
    else {
        $.ajax({
            type: "POST",
            url: 'https://localhost:44372/AddUsuario',
            data: {
                nome: nome,
                matricula: matricua,
                login: login,
                senha: senha
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
function ExcluirUsuario(id_usuario) {

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
                url: 'https://localhost:44372/DeletarUsuario',
                data: {
                    idUsuario: id_usuario
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
function SalvarUsuarioEditado(id_usuario) {
    var nome = $('#nomeUsuEdt').val();
    var matricua = $('#matriculaUsuEdt').val();
    var login = $('#loginUsuEdt').val();
    var senha = $('#sehhaUsuEdt').val();
    var id_usuario = $('.edt').attr('id');


    if (nome === undefined || nome === null || matricua === undefined || matricua === null || login === "" || login === "") {
        Swal.fire(
            'Opa!',
            'Preencha todos os campos para prosseguir',
            'warning'
        );
    }
    else {
        $.ajax({
            type: "POST",
            url: 'https://localhost:44372/EditarUsuario',
            async: false,
            data: {
                idUsuario: id_usuario,
                nome: nome,
                matricula: matricua,
                login: login,
                senha: senha
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