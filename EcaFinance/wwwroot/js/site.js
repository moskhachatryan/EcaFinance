(function ($) {
    "use strict";

    $(".search").on('keyup', function () {
        LoadOrders();
    })

    $(".filter").on('change', function () {
        LoadOrders();
    })

    function LoadOrders() {
        var _type = $('.type').val();
        var _startDate = $('.startDate').val();
        var _endDate = $('.endDate').val();
        var _service = $('.service').val();
        var _category = $('.category').val();

        $.ajax({
            type: 'Get',
            url: filterUrl + "?search=" + $(".search").val(),
            async: true,
            data: {
                type: _type,
                startDate: _startDate,
                endDate: _endDate,
                service: _service,
                category: _category,
            },
            success: function (data) {
                $("#order-list").html(data);
            }
        });
    }
})(jQuery);