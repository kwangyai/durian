/* File Created: มกราคม 13, 2555 */
(function ($) {
    $.datepickerBetween = function () {

        $("#from,#to").mask("99/99/9999");
        var dateBefore = null;
        var dates = $("#from, #to").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            numberOfMonths: 3,
            buttonImageOnly: true,
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            changeMonth: true,
            changeYear: true,
            beforeShow: function () {
                if ($(this).val() != "") {
                    var arrayDate = $(this).val().split("/");
                    arrayDate[2] = parseInt(arrayDate[2]) - 543;
                    $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
                }
                setTimeout(function () {
                    $.each($(".ui-datepicker-year option"), function (j, k) {
                        var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
                        $(".ui-datepicker-year option").eq(j).text(textYear);
                    });

                    $('span.ui-datepicker-year').each(function () { //set the initial z-index's
                        var textYear = parseInt($(this).html()) + 543;
                        $(this).html(textYear);
                    });

                }, 50);

            },
            onChangeMonthYear: function () {
                setTimeout(function () {
                    $.each($(".ui-datepicker-year option"), function (j, k) {
                        var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
                        $(".ui-datepicker-year option").eq(j).text(textYear);
                    });
                    $('span.ui-datepicker-year').each(function () { //set the initial z-index's
                        var textYear = parseInt($(this).html()) + 543;
                        $(this).html(textYear);
                    });
                }, 50);
            },
            onClose: function () {
                //if ($(this).val() != "" && $(this).val() == dateBefore) {
                if ($(this).val() != "") {
                    var arrayDate = dateBefore.split("/");
                    arrayDate[2] = parseInt(arrayDate[2]) + 543;
                    $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);

                }
                //}
            },
            onSelect: function (dateText, inst) {
                var tempFrom = $('#from').val();
                var tempTo = $('#to').val();
                dateBefore = $(this).val();
                var arrayDate = dateText.split("/");
                arrayDate[2] = parseInt(arrayDate[2]) + 543;
                $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);

                var option = this.id == "from" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						dateText, instance.settings);
                dates.not(this).datepicker("option", option, date);
                if (this.id == "from") {
                    $('#to').val(tempTo);
                }
                if (this.id == "to") {
                    $('#from').val(tempFrom);
                }
            }
        });
    }
})(jQuery);