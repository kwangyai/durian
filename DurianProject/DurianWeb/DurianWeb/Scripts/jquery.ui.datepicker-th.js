// ================================= Modify =================================
function loadDatePicker() {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(".datepicker").attr({ "readonly": "readonly" }).datepicker({
        showOn: "both",
        buttonImage: "../../images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.']
    });
    $(".datepicker").click(function () {
        $(this).val('');
    });
};

function loadDatePickerImageClickOnly() {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(".datepicker").attr({ "readonly": "readonly" }).datepicker({
        showOn: "button",
        buttonImage: "../images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.']
    });

    $(".datepicker").click(function () {
        $(this).val('');
    });

};

function loadFromDatePicker(fromObj, toObj) {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(fromObj).attr({ "readonly": "readonly" }).datepicker({
        showOn: "both",
        buttonImage: "../images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        maxDate: toObj.val(),
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
        onSelect: function(selected) {
            $(toObj).datepicker("option", "minDate", selected)
        }
    });

    $(fromObj).click(function () {
        $(this).val('');
    });

    $(toObj).click(function () {
        $(this).val('');
    });
};

function loadToDatePicker(fromObj, toObj) {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(toObj).attr({ "readonly": "readonly" }).datepicker({
        showOn: "both",
        buttonImage: "../images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        minDate: fromObj.val(),
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
        onSelect: function(selected) {
            $(fromObj).datepicker("option", "maxDate", selected)
        }
    });

    $(fromObj).click(function () {
        $(this).val('');
    });

    $(toObj).click(function () {
        $(this).val('');
    });
};

function loadFromMonthYearDatePicker(fromObj, toObj) {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(fromObj).attr({ "readonly": "readonly" }).datepicker({
        showOn: "both",
        buttonImage: "../../Images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        showButtonPanel: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        maxDate: toObj.val(),
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
        onClose: function() {
            button = $(fromObj).datepicker('widget').find("button:contains('Done')");
            button.click(function() {
                var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                $(fromObj).datepicker('setDate', new Date(iYear, iMonth, 1));
                $(toObj).datepicker("option", "minDate", new Date(iYear, iMonth, 1))
            });
        }
    });

    $(fromObj).click(function () {
        $(this).val('');
    });

    $(toObj).click(function () {
        $(this).val('');
    });
};

function loadToMonthYearDatePicker(fromObj, toObj) {
    var d = new Date();
    var toDay = d.getDate() + '/' + (d.getMonth() + 1) + '/' + (d.getFullYear() + 543);

    $(toObj).attr({ "readonly": "readonly" }).datepicker({
        showOn: "both",
        buttonImage: "../../Images/Calendar_scheduleHS.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        isBuddhist: true,
        showButtonPanel: true,
        defaultDate: toDay,
        dateFormat: 'dd/mm/yy',
        minDate: fromObj.val(),
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
        onClose: function() {
            button = $(toObj).datepicker('widget').find("button:contains('Done')");
            button.click(function() {
                var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                $(toObj).datepicker('setDate', new Date(iYear, parseInt(iMonth) + 1, 0));
                $(fromObj).datepicker("option", "maxDate", new Date(iYear, iMonth, 1))
            });
        }
    });

    $(fromObj).click(function () {
        $(this).val('');
    });

    $(toObj).click(function () {
        $(this).val('');
    });
};
// ================================= Modify =================================