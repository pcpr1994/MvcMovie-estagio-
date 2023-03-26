$(function () {
    var l = abp.localization.getResource('MvcMovie');
    var createModal = new abp.ModalManager(abp.appPath + 'Genres/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Genres/EditModal');


    var dataTable = $('#GenresTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(mvcMovie.genres.genre.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('MvcMovie.Genre.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },


                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('MvcMovie.Genre.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'GenreDeletionConfirmationMessage',
                                            data.record.description
                                        );
                                    },
                                    action: function (data) {
                                        mvcMovie.genres.genre
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
                
                {
                    title: l('Description'),
                    data: "description"

                },
                
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewGenreButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

});