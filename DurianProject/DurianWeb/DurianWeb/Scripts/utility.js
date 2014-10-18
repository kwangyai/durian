/* File Created: มกราคม 17, 2555 */
function DateThai(date) {
    var d = date.split("/");
    var strYear = parseInt(d[2]) + 543;
    var strMonth = parseInt(d[1]);
    var strDay = parseInt(d[0]);
    var strMonthCut = Array("", "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค.");
    var strMonthLong = Array('', 'มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม');
    var strMonthThai = strMonthLong[strMonth];    
    return strDay + " " + strMonthThai + " " + strYear;
}

function DateJsonThai(date) {

    var d = new Date();
    d.setTime(date.replace('/Date(', '').replace(')/', ''));
    return DateThai(d.format("dd/MM/yyyy"));
}