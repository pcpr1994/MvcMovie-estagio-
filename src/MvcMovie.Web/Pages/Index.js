$(function () {
    var l = abp.localization.getResource('MvcMovie');
    abp.log.debug('Index.js initialized!');
    var detailModal = new abp.ModalManager(abp.appPath + 'Movies/DetailModal');



    var dataTable = $('#MoviesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(mvcMovie.movies.movie.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                
                                {
                                    text: l('Detail'),
                                    visible: abp.auth.isGranted('MvcMovie.Movie.Detail'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        detailModal.open({ id: data.record.id });
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
                    title: l('Image'),
                    data: "linkPoster1",
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

    //detailModal.onResult(function () {
    //    detailModal.open({ id: data.record.id });
    //    dataTable.ajax.reload();

    //});

    $('#FilterMovieButton').click(function (e) {
        dataTable.ajax.mvcMovie.movies.movie.getList.searching();
    });

});
