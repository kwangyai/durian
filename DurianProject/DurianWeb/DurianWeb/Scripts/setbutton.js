function pageLoad(sender, args) {

    $(function () {
        $("button.up").html("");
        $("button.down").html("");
        $("button.add").html("");
        $("button.remove").html("");
        $("button.circle-add").html("");
        $("button.circle-remove").html("");
        $("button.square-add").html("");
        $("button.square-remove").html("");
        $("button.excel-icon").html("");
        $("button.pdf-icon").html("");
        $("input:submit, input:button").button();
        $("button.up").button({
            text: false,
            icons: {
                primary: 'ui-icon-triangle-1-n'
            }
        });
        $("button.down").button({
            text: false,
            icons: {
                primary: 'ui-icon-triangle-1-s'
            }
        });
        $("span.ui-icon-triangle-1-n").css('margin-left', '-7.5px');
        $("span.ui-icon-triangle-1-s").css('margin-left', '-8.5px');
        $("button.add").button({
            text: false,
            icons: {
                primary: 'ui-icon-plusthick'
            }
        });
        $("button.remove").button({
            text: false,
            icons: {
                primary: 'ui-icon-close'
            }
        });
        $("button.circle-add").button({
            text: false,
            icons: {
                primary: 'ui-icon-circle-plus'
            }
        });
        $("button.circle-remove").button({
            text: false,
            icons: {
                primary: 'ui-icon-circle-close'
            }
        });
        $("button.square-add").button({
            text: false,
            icons: {
                primary: 'ui-icon-squaresmall-plus'
            }
        });
        $("button.square-remove").button({
            text: false,
            icons: {
                primary: 'ui-icon-squaresmall-close'
            }
        });
        $("button.excel-icon").button({
            text: false
        });
        $("button.pdf-icon").button({
            text: false
        });
        $('.calendarpicker').datepicker({
            dateFormat: 'd MM yy',
            showOn: 'both',
            buttonImage: 'css/images/calendar.gif',
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1
        });
    });
}