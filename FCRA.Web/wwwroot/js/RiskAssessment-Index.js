$(function () {
    $('.divJstree').each(function () {
        $(this).jstree()
            .bind("select_node.jstree", function (e, data) {
                var href = data.node.a_attr.href;
                if (href && href != '#') {
                    $('#anchorID').attr('href', href)[0].click();
                }
            });
    });
});