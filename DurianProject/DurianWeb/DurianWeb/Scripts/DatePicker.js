var jqueryDatePicker = {
    DatePicker: function (CalendarId, ButtonImageUrl) {

        var $Calendar = $("#" + CalendarId)
        var d = new Date();
        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
        $Calendar.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            defaultDate: toDay,
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {
                //alert(dateText);
            }

        });
    },
    DatePickerHdn: function (CalendarId, hdn, ButtonImageUrl) {

        var $Calendar = $("#" + CalendarId)
        var d = new Date();
        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
        $Calendar.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            defaultDate: toDay,
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {
                $("#" + hdn).val(dateText);
            }

        });
    },
    DatePickerMax: function (CalendarId, ButtonImageUrl, MaxDate) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            $Calendar.datepicker({
                changeMonth: true,
                maxDate: MaxDate,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {
                    //alert(dateText);
                }

            });


        }) //end document.ready
    },
    DatePickerRequired: function (CalendarId, ButtonImageUrl, objReq) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            var mnD = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 393);
            $Calendar.datepicker({
                changeMonth: true,
                changeYear: true,
                minDate: mnD,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {
                    ValidatorValidateX($("#" + objReq)[0]);

                }

            });


        }) //end document.ready
    },
    DatePickerRequired2: function (CalendarId, ButtonImageUrl, objReq1, objReq2) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            $Calendar.datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {


                    ValidatorValidateX($("#" + objReq1)[0]);
                    ValidatorValidateX($("#" + objReq2)[0]);

                }

            });


        }) //end document.ready
    },
    DatePickerMaxRequired: function (CalendarId, ButtonImageUrl, objReq) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            var mnD = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 393);
            $Calendar.datepicker({
                changeMonth: true,
                maxDate: toDay,
                minDate: mnD,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {

                    ValidatorValidateX($("#" + objReq)[0]);

                }

            });


        }) //end document.ready
    },
    DatePickerMaxRequiredAge: function (CalendarId, lblAge, ButtonImageUrl, objReq) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            var mnD = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 393);
            $Calendar.datepicker({
                changeMonth: true,
                maxDate: toDay,
                minDate: mnD,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {

                    ValidatorValidateX($("#" + objReq)[0]);
                    var $age = $("#" + lblAge)
                    $age.val(getAge(dateText));
                },
                onClose: function (dateText, inst) {
                    var $age = $("#" + lblAge)
                    $age.val(getAge(dateText));
                }

            });


        }) //end document.ready
    },
    DatePickerFrom: function (CalendarFrom, CalendarTo, hdnFrom, ButtonImageUrl, MxDate, objReq) {
        jQuery(document).ready(function ($) {

            var $CalendarFrom = $("#" + CalendarFrom)
            var $CalendarTo = $("#" + CalendarTo)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

            $CalendarFrom.datepicker({
                changeMonth: true,
                maxDate: MxDate,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {

                    $("#" + hdnFrom).val(dateText);
                    var objRq = $("#" + objReq)[0];
                    ValidatorValidateX(objRq);

                    var dTo = $CalendarTo.val();

                    if (objRq.isvalid) {

                        $CalendarTo.datepicker("option", "minDate", dateText);
                        $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "margin-left": '5px' });

                    } else {
                        $CalendarTo.val('');
                    }

                }

            });


        }) //end document.ready
    },
    DatePickerTo: function (CalendarFrom, CalendarTo, hdnTo, ButtonImageUrl, objReq) {
        jQuery(document).ready(function ($) {
            var $CalendarFrom = $("#" + CalendarFrom)
            var $CalendarTo = $("#" + CalendarTo)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

            $CalendarTo.datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {

                    $("#" + hdnTo).val(dateText);
                    var objRq = $("#" + objReq)[0];
                    ValidatorValidateX(objRq);


                    if (dateText >= toDay) {

                        //$CalendarFrom.datepicker("option", "maxDate", dateText);
                        //$('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "margin-left": '5px' });
                    }


                }

            });


        }) //end document.ready
    },
    datepickerBetween: function (CalendarFrom, CalendarTo, hdnFrom, hdnTo, ButtonImageUrl) {
        var d = new Date();
        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

        var dates = $("#" + CalendarFrom + ", #" + CalendarTo).datepicker({
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            defaultDate: toDay,
            numberOfMonths: 3,
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {

                var option = this.id == CalendarFrom ? "minDate" : "maxDate";
                dates.not(this).datepicker("option", option, dateText);
                $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "margin-left": '5px' });

                if (this.id == CalendarFrom) {
                    $("#" + hdnFrom).val(dateText);
                } else {
                    $("#" + hdnTo).val(dateText);
                }

            }
        });
    },
    datepickerMaxMin: function (CalendarFrom, CalendarTo, hdnFrom, hdnTo, Max, Min, ButtonImageUrl) {
        var d = new Date();
        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
        var dMax = $("#" + Max).val();
        var dMin = $("#" + Min).val();
        
        var dates = $("#" + CalendarFrom + ", #" + CalendarTo).datepicker({
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            numberOfMonths: 3,
            showOn: 'button',
            maxDate: dMax,
            minDate: dMin,
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {

                if (this.id == CalendarFrom) {
                    $("#" + hdnFrom).val(dateText);
                } else {
                    $("#" + hdnTo).val(dateText);
                }

            }
        });
    },
    datepickerClsBetween: function (ButtonImageUrl) {
        var d = new Date();
        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

        var dates = $(".From, .To").datepicker({
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            defaultDate: toDay,
            numberOfMonths: 3,
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {
                var className = $(this).attr('class');
                //alert(className);
                var option = className == "From hasDatepicker" ? "minDate" : "maxDate";
                dates.not(this).datepicker("option", option, dateText);
                $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "margin-left": '5px' });
            }
        });
    },
    datepickerClsMNBetween: function (Max, Min, ButtonImageUrl) {
        var d = new Date();
        var dMax = $("#" + Max).val();
        var dMin = $("#" + Min).val();

        var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

        var dates = $(".From, .To").datepicker({
            dateFormat: 'dd/mm/yy',
            isBuddhist: true,
            maxDate: dMax,
            minDate: dMin,
            numberOfMonths: 3,
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: ButtonImageUrl,
            buttonText: "Choose",
            dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            onSelect: function (dateText, inst) {
                //var className = $(this).attr('class');
                //alert(className);
                //var option = className == "From hasDatepicker" ? "minDate" : "maxDate";
                //dates.not(this).datepicker("option", option, dateText);
                $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "margin-left": '5px' });
            }
        });
    },
    DatePickerMaxRequired2: function (CalendarId, ButtonImageUrl, MaxDate, objReq1, objReq2) {
        jQuery(document).ready(function ($) {
            var $Calendar = $("#" + CalendarId)
            var d = new Date();
            var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);
            $Calendar.datepicker({
                changeMonth: true,
                maxDate: MaxDate,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                isBuddhist: true,
                defaultDate: toDay,
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: ButtonImageUrl,
                buttonText: "Choose",
                dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
                dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
                monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
                monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
                onSelect: function (dateText, inst) {

                    ValidatorValidateX($("#" + objReq1)[0]);
                    ValidatorValidateX($("#" + objReq2)[0]);
                }

            });


        }) //end document.ready
    }
}
function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}
function getAge(dateString) {
    try {
        var d = dateString.split("/");
        var today = new Date();
        var birthDate = new Date(d[2], d[1], d[0]);

        var age = (today.getFullYear() + 543) - birthDate.getFullYear();
        var m = (today.getMonth() + 1) - birthDate.getMonth();
        var mon = 0;

        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            if (m != 0) {
                mon = 12 + m;
            } else {
                mon = 11;
            }

            if (age != 0)
                age = age - 1;
        } else {

            age = age;
            mon = m;
            //if (m != 0)
            //    mon = m + 1;
        }
        //alert(age + ' ' + mon);
        if (!isNaN(age) && !isNaN(mon)) {
            return age + ' ปี ' + mon + ' เดือน';
        } else {
            return "";
        }

    }
    catch (err) {
        return "";
    }

}