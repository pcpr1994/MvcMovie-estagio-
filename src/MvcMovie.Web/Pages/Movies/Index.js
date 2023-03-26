$(function () {
    var l = abp.localization.getResource('MvcMovie');
    var createModal = new abp.ModalManager(abp.appPath + 'Movies/CreateModal');
    var createIdImdbModal = new abp.ModalManager(abp.appPath + 'Movies/CreateIMDBModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Movies/EditModal');
    var detailModal = new abp.ModalManager(abp.appPath + 'Movies/DetailModal');

    var inputAction = function (requestData, dataTableSettings) {
        /*debugger;*/
        return {
            movieAuthor: $('#MovieAuthor').val() ?? null
        };
    };

    // exemplo da net
    //var inputAction = function (requestData, dataTableSettings) {
    //    return {
    //        id: $('#Id').val(),
    //        name: $('#Name').val(),
    //    };
    //};

    //var responseCallback = function (result) {

    //    // your custom code.

    //    return {
    //        recordsTotal: result.totalCount,
    //        recordsFiltered: result.totalCount,
    //        data: result.items
    //    };
    //};

    //ajax: abp.libs.datatables.createAjax(acme.bookStore.books.book.getList, inputAction, responseCallback)


    var dataTable = $('#MoviesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(mvcMovie.movies.movie.getList, inputAction,null),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MvcMovie.Movie.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },

                                {
                                    text: l('Detail'),
                                    visible: abp.auth.isGranted('MvcMovie.Movie.Detail'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        detailModal.open({ id: data.record.id });
                                    }
                                },

                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MvcMovie.Movie.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'MovieDeletionConfirmationMessage',
                                            data.record.title
                                        );
                                    },
                                    action: function (data) {
                                        mvcMovie.movies.movie
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },

                //o que se poe no data vem do que esta declarado no MovieDto
                {
                    title: l('Title'),
                    data: "title"
                },

                // ADDED the NEW AUTHOR NAME COLUMN
                {
                    title: l('Genre'),
                    data: "genreDescription",
                },

                {
                    title: l('Author'),
                    data: "authorName"
                },
                {
                    title: l('ReleaseDate'),
                    data: "releaseDate",
                    //render: function (data) {
                    //    var dia = data.getDate()
                    //    var mes = data.getMonth()
                    //    var ano = data.getFullYear()
                    //    return dia+"-"+mes+"-"+ano  //dat.toLocaleDateString('en-us', { weekday: "long", year: "numeric", month: "short", day: "numeric" })
                    //}
                    dataFormat: "date"
                    //dataFormat: "date"
                },
                {
                    title: l('Price'),
                    data: "price",
                    render: function (data) {
                        return data+" €";
                    }
                    //text: l('€')
                    
                },
                {
                    title: l('Rating'),
                    data: "rating",
                    render: function (data) {
                        return data + " /10";
                    }
                }
                //,
                //{
                //    title: l('CreationTime'), data: "creationTime",
                //    dataFormat
                //}
            ]
        })
    );

    //inputAction.onResult(function () {
    //    dataTable.ajax.reload();
    //});

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    createIdImdbModal.onResult(function (){
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    detailModal.onResult(function () {
        dataTable.ajax.reload();
    });


    $('#FilterMovieAuthorButton').click(function (e) {
        dataTable.ajax.reload();
    });


    $('#NewMovieButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#NewMovieIMDBButton').click(function (e) {
        e.preventDefault();
        createIdImdbModal.open();
    });

       
});
